using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class Phone
    {
        public string Agency_ID { get; set; }
        public string Phone_ID { get; set; }
        public int PhoneType_ID { get; set; }
        public string FileID { get; set; }
        public int ProfileID { get; set; }
        public int Debtor_ID { get; set; }
        public bool IsGood { get; set; }
        public string PhoneType { get; set; }

        public PhoneType PhoneType1 { get; set; }
     
        #region functions

        public static Phone Parse(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}Agency_ID",
                $"{ColPrefix}Phone_ID",
                $"{ColPrefix}PhoneType_ID",
                $"{ColPrefix}FileID",
                $"{ColPrefix}ProfileID",
                $"{ColPrefix}IsGood",
                $"{ColPrefix}PhoneType",


               
        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Phone ph = new Phone();
            ph.Agency_ID = row.GetValue<string>($"{ColPrefix}Agency_ID");
            ph.Phone_ID = row.GetValue<string>($"{ColPrefix}Phone_ID");
            ph.PhoneType_ID = row.GetValue<int>($"{ColPrefix}PhoneType_ID");
            ph.FileID = row.GetValue<string>($"{ColPrefix}FileID");
            ph.ProfileID = row.GetValue<int>($"{ColPrefix}ProfileID");
            ph.IsGood = row.GetValue<bool>($"{ColPrefix}IsGood");
            
            ph.PhoneType1 = Phone.getPhonetypebyPhonetypeID(ph.PhoneType_ID);
        



            return ph;
        } public static Phone ParseforPhoneType(DataRow row, string ColPrefix = "")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}Agency_ID",
                $"{ColPrefix}Phone_ID",
                $"{ColPrefix}PhoneType_ID",
                $"{ColPrefix}FileID",
                $"{ColPrefix}ProfileID",
                $"{ColPrefix}IsGood",
                $"{ColPrefix}PhoneType",


               
        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Phone ph = new Phone();
            ph.Agency_ID = row.GetValue<string>($"{ColPrefix}Agency_ID");
            ph.Phone_ID = row.GetValue<string>($"{ColPrefix}Phone_ID");
            ph.PhoneType_ID = row.GetValue<int>($"{ColPrefix}PhoneType_ID");
            ph.FileID = row.GetValue<string>($"{ColPrefix}FileID");
            ph.ProfileID = row.GetValue<int>($"{ColPrefix}ProfileID");
            ph.IsGood = row.GetValue<bool>($"{ColPrefix}IsGood");
            ph.PhoneType = row.GetValue<string>($"{ColPrefix}PhoneType");
        




            return ph;
        }

        public static dynamic getPhonetypebyPhonetypeID(int phtid)
        {
            try
            {
                INetworkRepo networkRepo = new NetworkRepo();
                object[] obj = {
               0,
               phtid
            };
                DataSet ds = networkRepo.PostDataTable("sp_FourgetPhonetypeByPhoneTypeID", obj);
                PhoneType PhoneTypes = new PhoneType();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            PhoneTypes = Model.PhoneType.Parse(item);
                        }

                        var result = PhoneTypes;
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
