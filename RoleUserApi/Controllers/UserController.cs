using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoleUserApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RoleUserApi.Common;
using RoleUserApi.Model.Response;
using RoleUserApi.Repositories.Implementation;
using RoleUserApi.Repositories.Interface;
using ChipsEmailProvider.Factories;
using Microsoft.AspNetCore.Hosting;


namespace RoleUserApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Obsolete]
        private IHostingEnvironment _env;

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers(int OrgID, int UserType)
        {
        User user = new User();
            List<User> res = user.GetAllUsers(OrgID, UserType);
            if (res == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpGet("GetUserTypes")]
        public IActionResult GetUserTypes()
        {
            UserType user = new UserType();
            List<UserType> res = user.GetUserTypes();
            if (res != null)
            {
                ResponseModelNew obj = new ResponseModelNew();
                obj.Data = res;
                obj.StatusCode = "200";
                return Ok(obj);
              
            }
            else
            {
                return BadRequest();

            }
        }
        [HttpGet("GetUser")]
        public IActionResult GetUser(Int64 UserID)
        {
            User user = new User();
            User res = user.GetUser(UserID);
            if (res == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {
            if (user == null)
                return NoContent();

            string res = user.Insert();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            string res = user.Update();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            User user = new User();
            string res = user.Delete(id);
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [AllowAnonymous]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            try
            {
                IAuthenticationRepo SecurityFactory = new AuthenticationRepo();
                //if (!Common.PasswordPolicy.ValidatePassword(model.Password)) { return BadRequest("Password Poliy"); }
                var principal = SecurityFactory.GetPrincipalFromValidToken(model.Token);

                var claimsIdentity = principal.Identity as ClaimsIdentity;
                var Claim = claimsIdentity.Claims.ToList();
                string UserId = Claim[0].Value.ToString();
                string TokenId = Claim[5].Value.ToString();
                Token TokenModel = new Token();

                if (TokenModel.TokenExists(TokenId, 2))
                {
                    //var ICypher = CypherFactory<ICryptographer>.GetService(typeof(Cryptographer));
                    //model.Password = ICypher.EncryptString(model.Password);
                    User UserModel = new User();
                    if (UserModel.UpdateUserPassword(Convert.ToInt32(UserId), model.Password))
                    {
                        ResponseModel obj = new ResponseModel();
                        obj.Data = "Password Updated Successfully";
                        obj.StatusCode = 200;
                        return Ok(obj);
                        //return Ok("");
                    }
                    else
                    {
                        //ResponseModel obj = new ResponseModel();
                        //obj.Data = "Password Updated Successfully";
                        //obj.StatusCode = 200;
                        //return Ok(obj);
                        return BadRequest("Unable TO Process Your Request");
                    }

                }
                else
                {
                    return BadRequest("Invalid Token");
                }
            }
            catch (Exception e)
            {
                //string controllerName = this.ControllerContext.RouteData.Values["action"].ToString();
                //ExceptionLogModel.Insert(controllerName, e.Message, 0);
                return Ok(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword model)
        {
            try
            {
                //throw new System.ArgumentException("Parameter cannot be null", "original");
                User UserModel = new User();
                var User = UserModel.CheckIfUserExists(model.UserName);
                if (User != null)
                {
                    //var service_type = ConfigModel.Select();
                    var EmailService = EmailFact.GetMailServer("smtp343");
                    var TemplateProvider = EmailFact.GetTemplateProvider();
                    DateTime ExpiresOn = DateTime.UtcNow.ToUniversalTime().AddDays(1);
                    IAuthenticationRepo SecurityFactory = new AuthenticationRepo();
                    var TokenId = Guid.NewGuid().ToString();
                    //SecurityFactory.GenerateToken(TokenId, User.UserID.ToString(), "", "", "", "", 1);
                    string tokenString = SecurityFactory.GenerateToken(TokenId, User.UserID.ToString(), "", "", "", "", 1);
                    Token tokens = new Token();
                    tokens.TokenID = TokenId;
                    tokens.UserID = User.UserID;
                    tokens.sToken = "";
                    tokens.Insert_TokenLog();

                    if (true)
                    {
                        var Template = TemplateProvider.GetForgotPasswordTemplate(_env, User.FirstName + " " + User.LastName, Constants.ForgotPaswordEmailURL + "login?token=" + tokenString, model.OS, model.Browser);
                        var Test = EmailService.SendEmailAsync("CHIPS", User.PersonalEmail, "Reset Password", Template);
                        ResponseModel obj = new ResponseModel();
                        obj.Data = "Mail Sent";
                        obj.StatusCode = 200;
                        return Ok(obj);
                    }
                    else
                    {
                        return BadRequest("UnAble To Process Your Request");
                    }
                    //return StatusCode(808, "CustomError");
                }
                else
                {
                    ResponseModel obj = new ResponseModel();
                    obj.Data = "Mail Sent";
                    obj.StatusCode = 200;
                    return Ok(obj);
                    //return StatusCode(808, "CustomError");
                }
            }
            catch (Exception e)
            {
                //string controllerName = this.ControllerContext.RouteData.Values["action"].ToString();
                //ExceptionLogModel.Insert(controllerName, e.Message, 0);
                return Ok(e.Message);
            }
        }
    }
}