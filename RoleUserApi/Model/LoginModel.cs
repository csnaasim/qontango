using RoleUserApi.Model.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string JWTToken { get; set; }
        public string GoogleToken { get; set; }
        public UserLoginHistory LoginHistory { get; set; }
        public UserPwdHistory PwdHistory { get; set; }
    }

    public class AuthenticateModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string JWTToken { get; set; }
        //public string GoogleToken { get; set; }

        public User AuthenticateUser()
        {
            var network = new NetworkRepo();

            object[] obj = { 0, this.Email,this.Password };
            var user = new User();
            try
            {
                var data = network.PostDataTable("sp_FruAuthenticateUser", obj);
                if (data != null && data.Tables.Count > 0)
                {
                    if (data.Tables[0] != null && data.Tables[0].Rows.Count > 0)
                        user.UserID = Convert.ToInt32(data.Tables[0].Rows[0]["usrUserID"]);
                        user.EmpID = Convert.ToInt32(data.Tables[0].Rows[0]["usrEmpID"]);
                        //user.DesigID = Convert.ToInt32(data.Tables[0].Rows[0]["DesigID"]);
                        user.RoleIDs = data.Tables[0].Rows[0]["RoleIDs"].ToString();
                        user.OrgID = Convert.ToInt32(data.Tables[0].Rows[0]["usrOrgID"]);
                        user.UserType = Convert.ToInt32(data.Tables[0].Rows[0]["usrUserType"]);
                        user.IsActive = Convert.ToBoolean(data.Tables[0].Rows[0]["usrIsActive"]);
                        user.IsDeleted = Convert.ToBoolean(data.Tables[0].Rows[0]["usrIsDeleted"]);
                        user.EmpName = data.Tables[0].Rows[0]["EmpName"].ToString();
                        user.Logo = data.Tables[0].Rows[0]["Logo"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return user;
        }

        public User AuthenticateUserMbl()
        {
            var network = new NetworkRepo();

            object[] obj = { 0, this.Email, this.Password };
            var user = new User();
            try
            {
                var data = network.PostDataTable("sp_FruAuthenticateUserMbl", obj);
                if (data != null && data.Tables.Count > 0)
                {
                    if (data.Tables[0] != null && data.Tables[0].Rows.Count > 0)
                        user.UserID = Convert.ToInt32(data.Tables[0].Rows[0]["UserID"]);
                    user.EmpID = Convert.ToInt32(data.Tables[0].Rows[0]["EmpID"]);
                    //user.DesigID = Convert.ToInt32(data.Tables[0].Rows[0]["DesigID"]);
                    user.RoleID = Convert.ToInt32(data.Tables[0].Rows[0]["RoleID"]);
                    user.OrgID = Convert.ToInt32(data.Tables[0].Rows[0]["OrgID"]);
                    user.IsActive = Convert.ToBoolean(data.Tables[0].Rows[0]["IsActive"]);
                    user.IsDeleted = Convert.ToBoolean(data.Tables[0].Rows[0]["IsDelete"]);
                    user.EmpName = data.Tables[0].Rows[0]["EmpName"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return user;
        }
    }
    public class UserAuthenticationModel
    {
        public AuthenticateModel Authentication { get; set; }
        public User User { get; set; }
    }
}
