using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class Portfolio
    {
        public int PortfolioID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }




        #region functions
        public static Portfolio Parse(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}PortfolioID",
                $"{ColPrefix}Name",
                $"{ColPrefix}Active",
        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Portfolio ph = new Portfolio();
            ph.PortfolioID = row.GetValue<int>($"{ColPrefix}PortfolioID");
            ph.Name = row.GetValue<string>($"{ColPrefix}Name");
            ph.Active = row.GetValue<bool>($"{ColPrefix}Active");
           
            return ph;
        }
        public static List<Portfolio> SelectAllPortfolio()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
          
            };
            DataSet ds = networkRepo.PostDataTable("sp_FourgetAllportfolio", obj);
            List<Portfolio> Accounts = new List<Portfolio>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Accounts.Add(Portfolio.Parse(item));
            }
            return Accounts;
        }
        #endregion functions
    }
}
