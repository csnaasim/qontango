using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class CQ
    {
        public int Q_ID { get; set; }
        public string QNAME { get; set; }
        public string Descp { get; set; }
        public int SortOrder { get; set; }




        #region functions 

        public static CQ Parse(DataRow row, string ColPrefix = "")
        {



            string[] requiredColumns = new[]
                {
                $"{ColPrefix}Q_ID",
                $"{ColPrefix}QNAME",
                $"{ColPrefix}Descp",

                $"{ColPrefix}SortOrder",
               

        };
            if (!row.HasColumns(requiredColumns))
                return null;
            CQ cq = new CQ();
            cq.Q_ID = row.GetValue<int>($"{ColPrefix}Q_ID");
            cq.QNAME = row.GetValue<string>($"{ColPrefix}QNAME");
            cq.Descp = row.GetValue<string>($"{ColPrefix}Descp");
            cq.SortOrder = row.GetValue<int>($"{ColPrefix}SortOrder");
           



            return cq;
        }

        #endregion functions 
    }
}
