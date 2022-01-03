using RoleUserApi.Model.Repositories.Interface;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RoleUserApi.Model.Repositories.Implementation
{
    public class NetworkRepo : INetworkRepo
    {
        //    const string _connectionString = "server=CSPAK007;user=sa; database=CHIPS;password=csna123SA";
        //const string _connectionString = "server=chipsdbserver.database.windows.net;user=AdminCitris; database=Chips_Internal; password=CiTri$@8765$%43^21!@#&*";
        //const string _connectionString = "server=chipsdbserver.database.windows.net;user=AdminCitris; database=CHIPS_QA; password=CiTri$@8765$%43^21!@#&*";
        const string _connectionString = @"server=DESKTOP-DKKC5TK\SQLEXPRESS;Database=qt_Freezed;user=sa;password=Admin@123"; 
        //const string _connectionString = "server=chipsdbserver.database.windows.net;user=AdminCitris; database=Chips_Dev; password=CiTri$@8765$%43^21!@#&*";
        //const string _connectionString = "server=DESKTOP-L7CMVFJ;user=sa; database=Chips_Dev; password=csna123AS";
        //const string _connectionString = "server=chipsdbserver.database.windows.net;user=AdminCitris; database=Chips_Prod; password=CiTri$@8765$%43^21!@#&*";

        public string Post(string storedProcedure, object[] sqlParameter)
        {
            DataSet dataSet = SqlHelper.ExecuteDataset(_connectionString, storedProcedure,sqlParameter);
            string json = "";
            if(dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                json = JsonConvert.SerializeObject(dataSet.Tables[0]);
            return json;
        }

        public DataSet PostDataTable(string storedProcedure, object[] sqlParameter)
        {
            DataSet dataSet = SqlHelper.ExecuteDataset(_connectionString,storedProcedure, sqlParameter);
            return dataSet;
        }
        public DataSet Execute(string storedProcedure, object[] sqlParameter)
        {
            DataSet dataSet = SqlHelper.ExecuteDataset(_connectionString, storedProcedure, sqlParameter);
            string json = "";
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows.Count > 0) 
            {
                return dataSet;
            }
            return null;
       }

        public DataSet CustomTypeDataTable(string StoredProcedureName, Dictionary<string, object> parameterValues)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = StoredProcedureName; //The name of the above mentioned stored procedure.  
                foreach (var para in parameterValues)
                {
                    SqlParameter param = cmd.Parameters.AddWithValue($"@{para.Key}", para.Value); //Here"@MyUDTableType" is the User-defined Table Type as a parameter.  
                }
                conn.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(ds);
                    // Detach the SqlParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();
                    conn.Close();
                    // Return the dataset
                    return ds;
                }

            }
        }
    }
}
