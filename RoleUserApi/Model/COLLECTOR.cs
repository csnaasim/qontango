using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class COLLECTOR
    {
        public int EMP_ID { get; set; }
        public string name { get; set; }


        #region  functions

        public static COLLECTOR Parse(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}EMP_ID",
                $"{ColPrefix}name",



        };
            if (!row.HasColumns(requiredColumns))
                return null;
            COLLECTOR colect = new COLLECTOR();
            colect.EMP_ID = row.GetValue<int>($"{ColPrefix}EMP_ID");
            colect.name = row.GetValue<string>($"{ColPrefix}name");



            return colect;
        }

        public static dynamic GetCollectors()
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,

            };
                DataSet ds = networkRepo.PostDataTable("sp_FourCollector", obj);
                List<COLLECTOR> collectoers = new List<COLLECTOR>();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            collectoers.Add(Parse(item));
                        }

                        var result = collectoers;
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
