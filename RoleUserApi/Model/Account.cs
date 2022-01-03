using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class Account
    {
        public int ACCNO { get; set; }
        public int SaleID { get; set; }
        public int Debtor_ID { get; set; }
        public DateTime DatePlaced { get; set; }
        public string Agency_ID { get; set; }
        public double TotalDues { get; set; }
        public double DebtAmount { get; set; }
        public double AmntCollected { get; set; }
        public double LastPayAmt { get; set; }
        public DateTime LastPayDate { get; set; }
        public DateTime SaleDate { get; set; }
        public string CONAME { get; set; }
        public string CustRef { get; set; }

        public int DateDifference { get; set; }

        #region Functions
        public static Account Parse(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}ACCNO",
                $"{ColPrefix}SaleID",
                $"{ColPrefix}Debtor_ID",
                $"{ColPrefix}DatePlaced",
                $"{ColPrefix}Agency_ID",
                $"{ColPrefix}Debtor_ID",


                $"{ColPrefix}TotalDues",
                $"{ColPrefix}DebtAmount",
                $"{ColPrefix}AmntCollected",
                $"{ColPrefix}LastPayAmt",
                $"{ColPrefix}LastPayDate",
                $"{ColPrefix}SaleDate",

        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Account acc = new Account();
            acc.Debtor_ID = row.GetValue<int>($"{ColPrefix}Debtor_ID");
            acc.ACCNO = row.GetValue<int>($"{ColPrefix}ACCNO");
            acc.SaleID = row.GetValue<int>($"{ColPrefix}SaleID");
            acc.SaleID = row.GetValue<int>($"{ColPrefix}SaleID");
            acc.DatePlaced = row.GetValue<DateTime>($"{ColPrefix}DatePlaced");
            acc.Agency_ID = row.GetValue<string>($"{ColPrefix}Agency_ID");
            acc.TotalDues = row.GetValue<double>($"{ColPrefix}TotalDues");
            acc.DebtAmount = row.GetValue<double>($"{ColPrefix}DebtAmount");
            acc.AmntCollected = row.GetValue<double>($"{ColPrefix}AmntCollected");
            acc.LastPayAmt = row.GetValue<double>($"{ColPrefix}LastPayAmt");
            acc.LastPayDate = row.GetValue<DateTime>($"{ColPrefix}LastPayDate");
            acc.SaleDate = row.GetValue<DateTime>($"{ColPrefix}SaleDate");
            acc.DateDifference = (acc.DatePlaced.Date - acc.SaleDate.Date ).Days;
            acc.CONAME = row.GetValue<string>($"{ColPrefix}CONAME");
            acc.CustRef = row.GetValue<string>($"{ColPrefix}CustRef");
            return acc;
        }

        public static dynamic getaccountbyID(int accid)
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               accid
            };
                DataSet ds = networkRepo.PostDataTable("sp_Fourgetaccountbyid", obj);
                Account Accounts = new Account();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            Accounts=Parse(item);
                        }

                        var result = Accounts;
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
