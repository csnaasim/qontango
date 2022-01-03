using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class Token : ICrud
    {
        public int ID { get; set; }
        public string TokenID { get; set; }
        public Int64 UserID { get; set; }
        public int TokenTypeID { get; set; }
        public string TokenTypeName { get; set; }
        public string sToken { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public object SecurityFactory { get; private set; }

        public string Delete(int id)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] objToken = {
                0,
                id
                };
            string res = networkRepo.Post("", objToken);
            return res;
        }

        public string Insert()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] objToken = {
                0,
                this.TokenID,
                this.UserID,
                1,
                "Login",
                this.sToken,
                true,
                DateTime.UtcNow
                };
            string res = networkRepo.Post("sp_FruSaveToken", objToken);
            return res;
        }

        public  string Insert_TokenLog()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] objToken = {
                0,
                this.TokenID,
                this.UserID,
                2,
                "ForgotPassword",
                this.sToken,
                true,
                DateTime.UtcNow
                };
            string res = networkRepo.Post("sp_FruSaveToken", objToken);
            return res;
        }

        public string Select()
        {
            throw new NotImplementedException();
        }

        public string Select(int id)
        {
            throw new NotImplementedException();
        }

        public bool TokenExists(string TokenID, int TokenTypeID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] objToken = {
               0,
               TokenID,
               TokenTypeID
            };
            var ds = networkRepo.Execute("sp_FruCheckIfTokenExists", objToken);
            if (ds == null) 
            {
                return false;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public string GetClaims(string token)
        {
            JwtSecurityToken t = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return "";
        }

        public string Update()
        {
            throw new NotImplementedException();
        }
    }
}
