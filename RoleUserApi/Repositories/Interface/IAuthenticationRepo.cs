using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoleUserApi.Repositories.Interface
{
    public interface IAuthenticationRepo
    {
        string GenerateToken(string TokenID, string UserId, string EmpID, string DesigID, string RoleID, string OrgID,int Days);
        public ClaimsPrincipal GetPrincipalFromValidToken(string token);
    }
}
