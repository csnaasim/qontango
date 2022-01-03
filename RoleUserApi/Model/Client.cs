using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class Client
    {
        #region  Properties
        public string ClientID { get; set; }
        public string AgencyID { get; set; }
        public string Name { get; set; }
        public string Clientname { get; set; }
        public int SaleID { get; set; }
        public int Q_ID { get; set; } 
        public string FileID { get; set; } 
        public string STAT_ID { get; set; }


        #endregion Properties

        #region Functions
        public static Client Parse(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}ClientID",
                $"{ColPrefix}cname",
                $"{ColPrefix}clientname",
              
                $"{ColPrefix}SaleID",
                $"{ColPrefix}FileID",
                $"{ColPrefix}Q_ID",
                $"{ColPrefix}STAT_ID",
              
        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Client client = new Client();
            client.AgencyID = row.GetValue<string>($"{ColPrefix}ClientID");
            client.ClientID =  row.GetValue<string>($"{ColPrefix}ClientID");
            client.Clientname =  row.GetValue<string>($"{ColPrefix}clientname");
          
            client.Name = row.GetValue<string>($"{ColPrefix}cname");
            client.SaleID = row.GetValue<int>($"{ColPrefix}SaleID");
            client.FileID = row.GetValue<string>($"{ColPrefix}FileID");
            client.Q_ID = row.GetValue<int>($"{ColPrefix}Q_ID");
            client.STAT_ID = row.GetValue<string>($"{ColPrefix}STAT_ID");
        
            return client;
        }

        public static dynamic GetClients(string cname)
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               cname
            };
                DataSet ds = networkRepo.PostDataTable("sp_FourClientSearch", obj);
                List<Client> Clients = new List<Client>();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            Clients.Add(Parse(item));
                        }

                        var result = Clients ;
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



      






        #endregion Functions




    }
}
