using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class Route
    {
        #region  Properties
        public int Q_ID { get; set; }
        public string QNAME { get; set; }
        public string Descp { get; set; }
        public int SortOrder { get; set; }
        #endregion  Properties

        #region  functions

        public static Route Parse(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}Q_ID",
                $"{ColPrefix}QNAME",

           

        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Route rout = new Route();
            rout.Q_ID = row.GetValue<int>($"{ColPrefix}Q_ID");
            rout.QNAME = row.GetValue<string>($"{ColPrefix}QNAME");

       

            return rout;
        }

        public static dynamic GetRuotes()
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               
            };
                DataSet ds = networkRepo.PostDataTable("sp_FourAllRoute", obj);
                List<Route> ruotes = new List<Route>();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            ruotes.Add(Parse(item));
                        }

                        var result = ruotes;
                        return result;
                    }
                    else
                    {
                        //throw new Exception("No results found.");
                        return null;
                    }

                }
                else
                {
                    throw new Exception("No results found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion  functions
    }
}
