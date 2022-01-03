using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class PhoneType
    {
        public int PHONETYPE_ID { get; set; }
        public string PHONETYPE { get; set; }
        public string CommLabelDescription { get; set; }
        public string RetailLabelDescription { get; set; }

        #region functions

        public static PhoneType Parse(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}PHONETYPE_ID",
                $"{ColPrefix}PHONETYPE",
                $"{ColPrefix}CommLabelDescription",
                $"{ColPrefix}RetailLabelDescription",

            };
            if (!row.HasColumns(requiredColumns))
                return null;
            PhoneType ph = new PhoneType();
            ph.PHONETYPE_ID = row.GetValue<int>($"{ColPrefix}PHONETYPE_ID");
            ph.PHONETYPE = row.GetValue<string>($"{ColPrefix}PHONETYPE");
            ph.CommLabelDescription = row.GetValue<string>($"{ColPrefix}CommLabelDescription");
            ph.RetailLabelDescription = row.GetValue<string>($"{ColPrefix}RetailLabelDescription");

            return ph;
        }


        #endregion functions

    }
}
