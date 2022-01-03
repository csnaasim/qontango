using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class Promise
    {
        public string Agency_Id { get; set; }
        public int Promise_Id { get; set; }
        public int Debtor_ID { get; set; }
        public int ACCNo { get; set; }
        public DateTime Promise_Date { get; set; }
        public DateTime Expected_Date { get; set; }
        public double Amount { get; set; }
        public int Confidence_Level  { get; set; }
        public string Remarks { get; set; }
        public string Name { get; set; }
        public int BrokenPromise { get; set; }



        #region functions
        public static Promise Parse(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}Agency_Id",
                $"{ColPrefix}Promise_Id",
                $"{ColPrefix}Debtor_ID",
                $"{ColPrefix}ACCNo",
                $"{ColPrefix}Promise_Date",
                $"{ColPrefix}Expected_Date",
                $"{ColPrefix}Amount",
                $"{ColPrefix}Confidence_Level",
                $"{ColPrefix}Remarks",
                $"{ColPrefix}Name",
               



        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Promise ph = new Promise();
            ph.Agency_Id = row.GetValue<string>($"{ColPrefix}Agency_Id");
            ph.Promise_Id = row.GetValue<int>($"{ColPrefix}Promise_Id");
            ph.Debtor_ID = row.GetValue<int>($"{ColPrefix}Debtor_ID");
            ph.ACCNo = row.GetValue<int>($"{ColPrefix}ACCNo");
            ph.Promise_Date = row.GetValue<DateTime>($"{ColPrefix}Promise_Date");
            ph.Expected_Date = row.GetValue<DateTime>($"{ColPrefix}Expected_Date");
            ph.Amount = row.GetValue<double>($"{ColPrefix}Amount");
            ph.Confidence_Level = row.GetValue<int>($"{ColPrefix}Confidence_Level");
            ph.Remarks = row.GetValue<string>($"{ColPrefix}Remarks");
            ph.Name = row.GetValue<string>($"{ColPrefix}Name");
            ph.BrokenPromise = row.GetValue<int>($"{ColPrefix}BrokenPromise");
           




            return ph;
        }
        #endregion functions
    }
}
