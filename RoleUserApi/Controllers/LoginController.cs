using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoleUserApi.Model;
using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using RoleUserApi.Repositories.Implementation;
using RoleUserApi.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace RoleUserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        INetworkRepo networkRepo;
        private static IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            networkRepo = new NetworkRepo();
            _configuration = configuration;
        }

        // GET: api/Login
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //new VaultConfig(_configuration).getValue();
            return new string[] { "value1", "value2" };
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Login([FromBody]AuthenticateModel login)
        {
            try
            {
                    var user = login.AuthenticateUser();
                    int OrgID = user.OrgID;
                    if (user != null && user.UserID > 0)
                    {
                        if (!user.IsActive || user.IsDeleted)
                        {
                            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status423Locked, user);
                        }
                        user.GetAllUsers(OrgID, -1);

                        string tokenID = Guid.NewGuid().ToString();
                        IAuthenticationRepo authentication = new AuthenticationRepo();
                        //string token = authentication.GenerateToken("1", tokenID);
                        string token = authentication.GenerateToken(tokenID, user.UserID.ToString(), user.EmpID.ToString(), user.DesigID.ToString(), user.RoleIDs.ToString(), user.OrgID.ToString(),30);
                        Token tokens = new Token();
                        tokens.TokenID = tokenID;
                        tokens.UserID = user.UserID;
                        tokens.sToken = token;
                        string res = tokens.Insert();
                        if (!string.IsNullOrWhiteSpace(res))
                        {
                            var auth = new UserAuthenticationModel();
                            auth.Authentication = new AuthenticateModel();
                            auth.Authentication.JWTToken = token;
                            //auth.Authentication.GoogleToken = login.GoogleToken;
                            auth.User = user;

                            return Ok(auth);
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
            }
            catch (Exception e)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("AuthenticateMbl")]
        public async Task<IActionResult> AuthenticateMbl([FromBody]AuthenticateModel login)
        {
            try
            {
                var user = login.AuthenticateUserMbl();
                if (user != null && user.UserID > 0)
                {
                    if (!user.IsActive || user.IsDeleted)
                    {
                        return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status423Locked, user);
                    }
                    user.GetAllUsers(1, -1);

                    string tokenID = Guid.NewGuid().ToString();
                    IAuthenticationRepo authentication = new AuthenticationRepo();
                    //string token = authentication.GenerateToken("1", tokenID);
                    string token = authentication.GenerateToken(tokenID, user.UserID.ToString(), user.EmpID.ToString(), user.DesigID.ToString(), user.RoleID.ToString(), user.OrgID.ToString(), 30);
                    Token tokens = new Token();
                    tokens.TokenID = tokenID;
                    tokens.UserID = user.UserID;
                    tokens.sToken = token;
                    string res = tokens.Insert();
                    if (!string.IsNullOrWhiteSpace(res))
                    {
                        var auth = new UserAuthenticationModel();
                        auth.Authentication = new AuthenticateModel();
                        auth.Authentication.JWTToken = token;
                        //auth.Authentication.GoogleToken = login.GoogleToken;
                        auth.User = user;

                        return Ok(auth);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
                
            }
            catch (Exception e)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [Authorize]
        [HttpGet("GetUserInfo")]
        public IActionResult GetUserInfo(int Id)
        {
            //object[] obj = {
            //    0,
            //    Id
            //};
            try
            {
                User user = new User();
                user.UserID = Id;
                user.GetUser(Id);
                //string data = networkRepo.Post("sp_GetUserInfoById", obj);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}