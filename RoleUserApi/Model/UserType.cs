using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class UserType
    {
        public int UserType_ID { get; set; }
        public string UserType_Name { get; set; }
        public Boolean Internal_User { get; set; }


        public static UserType Parse(DataRow row, string ColPrefix = "ust")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}UserType_ID",
                $"{ColPrefix}UserType_Name",
               
                $"{ColPrefix}Internal_User",
         
        };
            if (!row.HasColumns(requiredColumns))
                return null;
            UserType ut = new UserType();

            ut.UserType_ID = row.GetValue<int>($"{ColPrefix}UserType_ID");
            ut.UserType_Name = row.GetValue<string>($"{ColPrefix}UserType_Name");
            ut.Internal_User = row.GetValue<Boolean>($"{ColPrefix}Internal_User");
            return ut;
        }
        public List<UserType> GetUserTypes()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,

            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllUserType", obj);
            List<UserType> Users = new List<UserType>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Users.Add(Parse(item));
            }
            return Users;
        }

    }







}
