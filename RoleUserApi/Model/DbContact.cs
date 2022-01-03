using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class DbContact
    {
        public string Agency_ID { get; set; }
        public int Contact_ID { get; set; }
        public int Debtor_ID { get; set; }
        public string Title { get; set; }
        public bool IsPrimary { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string LASTNAME { get; set; }
        public string  DIRECT_PH1 { get; set; }
        public string DIRECT_PH2 { get; set; }
        public string DIRECT_PH2_Phone_Mask { get; set; }



        #region functions

        public static DbContact Parse(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}Agency_ID",
                $"{ColPrefix}Contact_ID",
                $"{ColPrefix}Debtor_ID",
                $"{ColPrefix}Title",
                $"{ColPrefix}IsPrimary",
                $"{ColPrefix}FIRSTNAME",
                $"{ColPrefix}MIDDLENAME",
                $"{ColPrefix}LASTNAME",
                $"{ColPrefix}DIRECT_PH1",
                $"{ColPrefix}DIRECT_PH2",
                $"{ColPrefix}PhoneMask",



        };
            if (!row.HasColumns(requiredColumns))
                return null;
            DbContact dbc = new DbContact();
            dbc.Agency_ID = row.GetValue<string>($"{ColPrefix}Agency_ID");
            dbc.Contact_ID = row.GetValue<int>($"{ColPrefix}Contact_ID");
            dbc.Debtor_ID = row.GetValue<int>($"{ColPrefix}Debtor_ID");
            dbc.Title = row.GetValue<string>($"{ColPrefix}Title");
            dbc.IsPrimary = row.GetValue<bool>($"{ColPrefix}IsPrimary");
            dbc.FIRSTNAME = row.GetValue<string>($"{ColPrefix}FIRSTNAME");
            dbc.MIDDLENAME = row.GetValue<string>($"{ColPrefix}MIDDLENAME");
            dbc.LASTNAME = row.GetValue<string>($"{ColPrefix}LASTNAME");
            dbc.DIRECT_PH1 = row.GetValue<string>($"{ColPrefix}DIRECT_PH1");
            dbc.DIRECT_PH2 = row.GetValue<string>($"{ColPrefix}DIRECT_PH2");
            dbc.DIRECT_PH2_Phone_Mask = row.GetValue<string>($"{ColPrefix}PhoneMask");
            if (dbc.DIRECT_PH2_Phone_Mask != null)
            {
                var a = dbc.DIRECT_PH2_Phone_Mask;
                string b = a.Replace('%', '#');
                var c = "{0:" + b + "}";
                dbc.DIRECT_PH1 = String.Format(c, Convert.ToInt64(dbc.DIRECT_PH1));
            }
            else
            {
                 var a = "(%%%) %%%-%%%%";
                string b = a.Replace('%', '#');
                var c = "{0:" + b + "}";
                dbc.DIRECT_PH1 = String.Format(c, Convert.ToInt64(dbc.DIRECT_PH1));
            }




            return dbc;
        }
        #endregion functions

    }
}
