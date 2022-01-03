using RoleUserApi.Repositories.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoleUserApi.Repositories.Implementation
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        public string GenerateToken(string TokenID, string UserId, string EmpID, string DesigID, string RoleID, string OrgID,int Days)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EF7DBFFC794A0A784551774CD79A0A040E964E6A47C098A584E73D1CA4CFA985"));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claim = new[]
                {
                    new Claim("UserID", UserId),
                    new Claim("EmpID", EmpID),
                    new Claim("DesigID", DesigID),
                    new Claim("RoleID", RoleID),
                    new Claim("OrgID", OrgID),
                    new Claim("TokenID", TokenID)
                };
                var token = new JwtSecurityToken("https://localhost:44354/", "https://localhost:44354/", claims: claim, expires: DateTime.Now.AddDays(Days), signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ClaimsPrincipal GetPrincipalFromValidToken(string token)
        {

            token = token.Trim();

            var tokenValidationParameters = new TokenValidationParameters
            {

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EF7DBFFC794A0A784551774CD79A0A040E964E6A47C098A584E73D1CA4CFA985")),
            ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");
                return principal;
            }
            catch (Exception e)
            {

            }
            return null;


        }
    }
}
