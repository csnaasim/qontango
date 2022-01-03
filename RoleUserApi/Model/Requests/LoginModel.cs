using ChipsApi.Model.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChipsApi.Model
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
                var data = network.PostDataTable("sp_AuthenticateUser", obj);
                if (data != null && data.Tables.Count > 0)
                {
                    if (data.Tables[0] != null && data.Tables[0].Rows.Count > 0)
                        user.UserID = Convert.ToInt32(data.Tables[0].Rows[0]["UserID"]);
                        user.EmpID = Convert.ToInt32(data.Tables[0].Rows[0]["EmpID"]);
                        //user.DesigID = Convert.ToInt32(data.Tables[0].Rows[0]["DesigID"]);
                        user.RoleID = Convert.ToInt32(data.Tables[0].Rows[0]["RoleID"]);
                        user.OrgID = Convert.ToInt32(data.Tables[0].Rows[0]["OrgID"]);
                        user.IsActive = Convert.ToBoolean(data.Tables[0].Rows[0]["IsActive"]);
                        user.IsDelete = Convert.ToBoolean(data.Tables[0].Rows[0]["IsDelete"]);
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
