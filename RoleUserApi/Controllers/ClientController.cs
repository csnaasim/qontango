using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleUserApi.Model;
using RoleUserApi.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace RoleUserApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet("GetAllClients")]
        public IActionResult GetAllClients(string Cname)
        {
            try
            {
                var res = Client.GetClients(Cname);
                ResponseModelNew response = new ResponseModelNew();
                if (res != null)
                {
                    response.StatusCode = "200";
                    response.Data = res;
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = "404";
                    response.Data = "Bad Request";
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, e.Message);
            }
        }





        [HttpGet("GetAllAccountQ")]
        public IActionResult GetAllAccountQ()
        {
            try
            {
                var res = ACCQ.GetAccountQ();
                ResponseModelNew response = new ResponseModelNew();
                if (res != null)
                {
                    response.StatusCode = "200";
                    response.Data = res;
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = "404";
                    response.Data = "Bad Request";
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpGet("GetAllTimeZone")]
        public IActionResult GetAllTimeZone()
        {
            try
            {
                var res = Model.TimeZone.GetTimeZone();
                ResponseModelNew response = new ResponseModelNew();
                if (res != null)
                {
                    response.StatusCode = "200";
                    response.Data = res;
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = "404";
                    response.Data = "Bad Request";
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllRoutes")]
        public IActionResult GetAllRoutes()
        {
            try
            {
                var res = Route.GetRuotes();
                ResponseModelNew response = new ResponseModelNew();
                if (res != null)
                {
                    response.StatusCode = "200";
                    response.Data = res;
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = "404";
                    response.Data = "Bad Request";
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("GetCollectors")]
        public IActionResult GetCollectors()
        {
            try
            {
                var res = COLLECTOR.GetCollectors();
                ResponseModelNew response = new ResponseModelNew();
                if (res != null)
                {
                    response.StatusCode = "200";
                    response.Data = res;
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = "404";
                    response.Data = "Bad Request";
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, e.Message);
            }
        }



    }
}
