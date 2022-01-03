using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class City
    {
        public string city { get; set; }
        public string growth { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public Int64 populations { get; set; }
        public int ranks { get; set; }
        public string states { get; set; }
        public string TimeZoneRegion { get; set; }



        #region functions 
        public static City Parse(DataRow row, string ColPrefix = "")
        {



            string[] requiredColumns = new[]
                {
                $"{ColPrefix}city",
                $"{ColPrefix}growth",
                $"{ColPrefix}latitude",
                $"{ColPrefix}longitude",

                $"{ColPrefix}populations",
                $"{ColPrefix}ranks",
                $"{ColPrefix}states",
                $"{ColPrefix}TimeZoneRegion",

              
         

        };
            if (!row.HasColumns(requiredColumns))
                return null;
            City cty = new City();
            cty.city = row.GetValue<string>($"{ColPrefix}city");
            cty.growth = row.GetValue<string>($"{ColPrefix}growth");
            cty.latitude = row.GetValue<double>($"{ColPrefix}latitude");
            cty.longitude = row.GetValue<double>($"{ColPrefix}longitude");
            cty.populations = row.GetValue<Int64>($"{ColPrefix}populations");
            cty.ranks = row.GetValue<int>($"{ColPrefix}ranks");
            cty.states = row.GetValue<string>($"{ColPrefix}states");
            cty.TimeZoneRegion = Debtor.gettimezone(cty.latitude,cty.longitude);
            GetAllCities(cty.city, cty.TimeZoneRegion);




            return cty;
        }


        public static dynamic GetAllCities(string city ,string timezoneregion_name)
        {
            int statid = 0;
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               timezoneregion_name,
               city,
               
            };
                DataSet ds = networkRepo.PostDataTable("sp_Foursetcityregion", obj);


                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {

                             statid = item.GetValue<int>("STATUS");

                        }

                        var result = statid;
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
        #endregion functions 

    }
}
