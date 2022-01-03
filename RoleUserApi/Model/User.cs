using System;
using System.Data;
using System.Collections.Generic;
using RoleUserApi.Model.Repositories.Interface;
using RoleUserApi.Model.Repositories.Implementation;

namespace RoleUserApi.Model
{
    public class User 
    {
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public int OrgID { get; set; }
        public Organization Org { get; set; }
        public string Password { get; set; }
        public Int64 EmpID { get; set; }
        public Employee emp { get; set; }
        public string EmpName { get; set; }
        public int DesigID { get; set; }
        public int RoleID { get; set; }
        public string RoleIDs { get; set; }
        public string RoleNames { get; set; }
        public string Logo { get; set; }
        public int UserType { get; set; }
        public string AdnlFeatures { get; set; }
        public byte LoginAttempts { get; set; }
        public DateTime LastAttemptAt { get; set; }
        public int PwdExpiryDays { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public List<Role> Roles { get; set; }
        public EntryLog EntryLog { get; set; }
        public UserLoginHistory LoginHistory { get; set; }
        public UserPwdHistory PwdHistory { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public static User Parse(DataRow row, string ColPrefix = "usr")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}UserID",
                $"{ColPrefix}UserName",
                $"{ColPrefix}OrgID",
                $"Org",
                $"{ColPrefix}Password",
                $"EmpID",
                $"EmpName",
                $"emp",
                $"RoleIDs",
                $"RoleNames",
                $"{ColPrefix}UserType",
                $"{ColPrefix}AdnlFeatures",
                $"{ColPrefix}LoginAttempts",
                $"{ColPrefix}LastAttemptAt",
                $"{ColPrefix}PwdExpiryDays",
                $"{ColPrefix}IsActive",
                $"{ColPrefix}IsDeleted",
                $"{ColPrefix}CreatedBy",
                $"{ColPrefix}CreateDate",
                $"{ColPrefix}UpdatedBy",
                $"{ColPrefix}UpdateDate",
        };
            if (!row.HasColumns(requiredColumns))
                return null;
                User usr          = new User();
                usr.UserID        = row.GetValue<Int64>($"{ColPrefix}UserID");
                usr.UserName      = row.GetValue<string>($"{ColPrefix}UserName");
                usr.OrgID         = row.GetValue<int>($"{ColPrefix}OrgID");
                usr.Org           = Organization.GetOrganization(usr.OrgID);
                usr.Password      = row.GetValue<string>($"{ColPrefix}Password");
                usr.EmpID         = row.GetValue<Int64>($"EmpID");
                usr.EmpName       = row.GetValue<string>($"EmpName");
                usr.emp           = Employee.GetEmployee(usr.EmpID);
                usr.RoleIDs       = row.GetValue<string>($"RoleIDs");
                usr.RoleNames     = row.GetValue<string>($"roleNames");
                //usr.Roles         = Role.GetRole(usr.RoleID);
                usr.UserType      = row.GetValue<int>($"{ColPrefix}UserType");
                usr.AdnlFeatures  = row.GetValue<string>($"{ColPrefix}AdnlFeatures");
                usr.LoginAttempts = row.GetValue<byte>($"{ColPrefix}LoginAttempts");
                usr.LastAttemptAt = row.GetValue<DateTime>($"{ColPrefix}LastAttemptAt");
                usr.PwdExpiryDays = row.GetValue<int>($"{ColPrefix}PwdExpiryDays");
                usr.IsActive      = row.GetValue<bool>($"{ColPrefix}IsActive");
                usr.IsDeleted     = row.GetValue<bool>($"{ColPrefix}IsDeleted");
                usr.CreatedBy     = row.GetValue<Int64>($"{ColPrefix}CreatedBy");
                usr.CreateDate    = row.GetValue<DateTime>($"{ColPrefix}CreateDate");
                usr.UpdatedBy     = row.GetValue<Int64>($"{ColPrefix}UpdatedBy");
                usr.UpdateDate    = row.GetValue<DateTime>($"{ColPrefix}UpdateDate");
                return usr;
        }

        public List<User> GetAllUsers(int OrgID, int UserType)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               OrgID,
               UserType
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllUsersByOrgID", obj);
            List<User> Users = new List<User>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Users.Add(Parse(item));
            }
            return Users;
        }
     
        public User GetUser(Int64 UserID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               UserID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetUserByID", obj);
            User User = new User();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    User = Parse(ds.Tables[0].Rows[0]);
                }
            }
            return User;
        }

        public string Insert()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
				this.UserName,
				this.OrgID,
                this.RoleIDs,
				this.Password,
				this.EmpID,
				this.UserType,
				this.AdnlFeatures,
				this.LoginAttempts,
				this.LastAttemptAt,
				this.PwdExpiryDays,
				this.IsActive,
				this.IsDeleted,
				this.CreatedBy
            };
            string res = networkRepo.Post("sp_FruInsertUser", obj);
            return res;
        }

        public string Update()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
				this.UserID,
				this.UserName,
				this.OrgID,
                this.RoleIDs,
				this.Password,
				this.EmpID,
				this.UserType,
				this.AdnlFeatures,
				this.LoginAttempts,
				this.LastAttemptAt,
				this.PwdExpiryDays,
				this.IsActive,
				this.IsDeleted,
				this.UpdatedBy
            };
            string res = networkRepo.Post("sp_FruUpdateUser", obj);
            return res;
        }

        public string Delete(int UserID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {
                0,
                UserID
            };
            string res = networkRepo.Post("sp_FruDeleteUser", obj);
            return res;
        }

        public bool UpdateUserPassword(int UserID, string Password)
        {
            //User this = (User)obj;
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] objUser = {
               0,
             UserID,
             Password
            };
            var ds = networkRepo.Execute("sp_FruUpdateUserPassword", objUser);

            if (ds.Tables[0].Rows.Count > 0)
            {
                int Count = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                if (Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public Employee CheckIfUserExists(string Email)
        {
            try
            {
                // we can set "FT" for ensuring first time change password and set "Y" for direct active.

                INetworkRepo networkRepo = new NetworkRepo();
                Object[] obj = {
                   0,
                   Email
                };
                var ds = networkRepo.Execute("sp_FruCheckIfUserExists", obj);
                Employee model = new Employee();
                if (ds == null)
                {
                    return null;
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.UserID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    model.EmpID = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
                    model.FirstName = ds.Tables[0].Rows[0][2].ToString();
                    model.LastName = ds.Tables[0].Rows[0][3].ToString();
                    model.PersonalEmail = ds.Tables[0].Rows[0][4].ToString();

                    if (model.UserID > 0)
                    {
                        return model;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}      
