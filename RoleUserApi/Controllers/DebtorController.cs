using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoleUserApi.Model;
using RoleUserApi.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace RoleUserApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DebtorController : ControllerBase
    {

        private IHostingEnvironment Environment;

        public DebtorController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        [HttpGet("GetAlldebtors")]
        public IActionResult GetAllClients( )
        {
            try
            {
                var res = Debtor.Getdebtors();
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


        [HttpPost("GetAlldebtorsBasedOnAccountInfo")]
        public IActionResult GetAlldebtorsBasedOnAccountInfo([FromQuery] int number, [FromQuery] int cid, [FromBody]int [] balance)
 
        {

            var res1 = Response;

            if (cid == 0)
            {
                cid = -1;
            }
            try
            {
                var res = Debtor.GetdebtorsBasedOnAccounts22(number,cid, balance);
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

        [HttpGet("Gettimezonelatlang")]
        public IActionResult Index(double lat,double lang)
        {
            lat = 42.09253000;
            lang = -88.85121000;
            var res = Debtor.gettimezone(lat,lang);
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
        [HttpGet("setallcityregions")]
        public IActionResult setallcityregions()
        {
        
            var res = Debtor.GetAllCities();
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
    

    [HttpPost("GetAllNotesBasedOnDebtorID")]
        public IActionResult GetAllNotesBasedOnDebtorID( int debid, int rowindex, List<string> filterlst)

        {

            Debtor deb = new Debtor();
            string contentPath = this.Environment.ContentRootPath;
            try
            {
                var res = deb.GetNotesBasedOnDebtorID(debid, rowindex, contentPath, filterlst);
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


        [HttpGet("Getportfolios")]
        public IActionResult Getportfolios()
        {
            try
            {
                var res = Portfolio.SelectAllPortfolio();
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


        [HttpGet("UpdateDebtorName")]
        public IActionResult UpdateDebtorName(int debid  ,string  columnvalue)
        {
            string columnname = "Name";
            if (columnname == "Name")
            { columnvalue = "'" + columnvalue + "'"; }
            try
            {
                var res = Debtor.UpdateDebtorName(debid,columnname,columnvalue);
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

        [HttpGet("UpdateDebtorAddress")]
        public IActionResult UpdateDebtorAddress(int debid, string columnvalue)
        {
            string columnname = "WebAddress";
            if (columnname == "WebAddress")
            { columnvalue = "'" + columnvalue + "'"; }
            try
            {
                var res = Debtor.UpdateDebtorName(debid, columnname, columnvalue);
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




        [HttpGet("UpdateDebtorPhone")]
        public IActionResult UpdateDebtorPhone(int debid, string columnvalue)
        {
            string fileid = "'D'";
            string columnname = "Phone_ID";
            if (columnname == "Phone_ID")
            { columnvalue = "'" + columnvalue + "'"; }
            int phonetypeid = 1;
            try
            {
                var res = Debtor.UpdateDebtorPhone(debid, columnname, columnvalue, fileid, phonetypeid,1);
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

        [HttpGet("InsertDebtorPhone")]
        public IActionResult InsertDebtorPhone(int debid, string columnvalue)
        {
            string fileid = "'D'";
            string columnname = "Phone_ID";
            if (columnname == "Phone_ID")
            { columnvalue = "'" + columnvalue + "'"; }
            int phonetypeid = 1;
            try
            {
                var res = Debtor.UpdateDebtorPhone(debid, columnname, columnvalue, fileid, phonetypeid, 0);
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


        [HttpGet("UpdateDebtorfax")]
        public IActionResult UpdateDebtorfax(int debid, string columnvalue)
        {
            string fileid = "'D'";
            string columnname = "Phone_ID";
            if (columnname == "Phone_ID")
            { columnvalue = "'" + columnvalue + "'"; }
            int phonetypeid = 3;
            try
            {
                var res = Debtor.UpdateDebtorPhone(debid, columnname, columnvalue, fileid, phonetypeid, 1);
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

        [HttpGet("InsertDebtorfax")]
        public IActionResult InsertDebtorfax(int debid, string columnvalue)
        {
            string fileid = "'D'";
            string columnname = "Phone_ID";
            if (columnname == "Phone_ID")
            { columnvalue = "'" + columnvalue + "'"; }
            int phonetypeid = 3;
            try
            {
                var res = Debtor.UpdateDebtorPhone(debid, columnname, columnvalue, fileid, phonetypeid, 0);
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
