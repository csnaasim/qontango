using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class TimeZone
    {
    //    public string TimeZoneID { get; set; }
        public int GMT_Diff { get; set; }
        public double dstOffset { get; set; }
        public double rawOffset { get; set; }
        public string status { get; set; }
        public string timeZoneId { get; set; }
        public string timeZoneName { get; set; }




        public static TimeZone Parse(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}TimeZoneID",
                $"{ColPrefix}GMT_Diff",


        };
            if (!row.HasColumns(requiredColumns))
                return null;
            TimeZone tim = new TimeZone();
            tim.timeZoneId = row.GetValue<string>($"{ColPrefix}TimeZoneID");
            tim.GMT_Diff = row.GetValue<int>($"{ColPrefix}GMT_Diff");


            return tim;
        }



        public static dynamic GetTimeZone()
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = { 0, };
                DataSet ds = networkRepo.PostDataTable("sp_FourTimeZone", obj);
                List<TimeZone> times = new List<TimeZone>();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            times.Add(Parse(item));
                        }

                        var result = times;
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
