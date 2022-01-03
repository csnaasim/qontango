using RoleUserApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoleUserApi.Helpers
{
    public static class ClaimsExtensions
    {
        public static User GetUser(this List<Claim> Claims)
        {
            int UserID = Convert.ToInt32(Claims[0].Value.ToString());
            return new User { UserID = UserID };
        }

        public static Employee GetEmployee(this List<Claim> Claims)
        {
            int EmpID = Convert.ToInt32(Claims[1].Value.ToString());
            return new Employee { EmpID = EmpID };
        }

        public static EmpDesignation GetDesignation(this List<Claim> Claims)
        {
            int DesigID = Convert.ToInt32(Claims[2].Value.ToString());
            return new EmpDesignation { DesigID = DesigID };
        }

        public static Role GetRole(this List<Claim> Claims)
        {
            int RoleID = Convert.ToInt32(Claims[3].Value.ToString());
            return new Role { RoleID = RoleID };
        }

        public static Organization GetOrganization(this List<Claim> Claims)
        {
            int OrgID = Convert.ToInt32(Claims[4].Value.ToString());
            return new Organization { OrgID = OrgID };
        }
        //public static Site GetSite(this List<Claim> Claims)
        //{
        //    var site = Claims[6].Value.ToString();

        //    int Site = (string.IsNullOrEmpty(site) || site.Equals("0")) ? -1 : Convert.ToInt32(site);
        //    return new Site { SiteID = Site };
        //}
    }
}
