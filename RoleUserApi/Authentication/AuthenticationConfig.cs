using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoleUserApi.Authentication
{
    public static class AuthenticationConfig
    {
        internal static TokenValidationParameters tokenValidationParams;

        public static void ConfigureJwtAuthentication(this IServiceCollection services)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EF7DBFFC794A0A784551774CD79A0A040E964E6A47C098A584E73D1CA4CFA985"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            tokenValidationParams = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://localhost:44354/",
                ValidateLifetime = true,
                ValidAudience = "https://localhost:44354/",
                ValidateAudience = true,
                RequireSignedTokens = true,
                IssuerSigningKey = credentials.Key,
                ClockSkew = TimeSpan.FromMinutes(10)
            };
            services.AddAuthentication(options => {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = tokenValidationParams;
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var accessToken = context.SecurityToken as JwtSecurityToken;

                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        var Claim = claimsIdentity.Claims.ToList();
                        string UserId = Claim[0].Value.ToString();
                        string TokenID = Claim[1].Value.ToString();

                        //if (UserId != null)
                        //{
                            // return unauthorized //if user no longer exists
                            INetworkRepo networkRepo = new NetworkRepo();
                            object[] obj = 
                            {
                                0,
                                UserId
                            };
                            DataSet ds = networkRepo.PostDataTable("sp_FruGetTokenByUserID", obj);
                            int tokenExists = 0;
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                            {
                                tokenExists = Convert.ToInt32(ds.Tables[0].Rows[0]["IsExist"]);
                            }
                            if (tokenExists == 0)
                            {
                                context.Fail("Unauthorized");
                            }
                        //}

                        return Task.CompletedTask;
                    }
                   
                };
#if PROD || UAT
                options.IncludeErrorDetails = False;
#elif DEBUG
                options.RequireHttpsMetadata = false;
#endif
            });
        }
    }
}
