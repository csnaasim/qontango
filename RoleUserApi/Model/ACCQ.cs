using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class ACCQ
    {
        public int Q_ID { get; set; }
        public string QNAME { get; set; }
        public string Descp { get; set; }
        public int SortOrder { get; set; }



        public static ACCQ Parse(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}AccountQID",
                $"{ColPrefix}name",


        };
            if (!row.HasColumns(requiredColumns))
                return null;
            ACCQ accq = new ACCQ();
            accq.Q_ID = row.GetValue<int>($"{ColPrefix}AccountQID");
            accq.QNAME = row.GetValue<string>($"{ColPrefix}name");


            return accq;
        }


        public static dynamic GetAccountQ()
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = { 0, };
                DataSet ds = networkRepo.PostDataTable("sp_FourAccountQSearch", obj);
                List<ACCQ> accq = new List<ACCQ>();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            accq.Add(Parse(item));
                        }

                        var result = accq;
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
    }
}
