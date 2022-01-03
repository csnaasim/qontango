using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleUserApi.Model;
using RoleUserApi.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        //[HttpGet("InsertPhone")]
        //public IActionResult InsertPhone(int debid, string columnvalue)
        //{
        //    string fileid = "'D'";
        //    string columnname = "Phone_ID";
        //    if (columnname == "Phone_ID")
        //    { columnvalue = "'" + columnvalue + "'"; }
        //    try
        //    {
        //        var res = Debtor.UpdateDebtorPhone(debid, columnname, columnvalue, fileid);
        //        ResponseModelNew response = new ResponseModelNew();
        //        if (res != null)
        //        {
        //            response.StatusCode = "200";
        //            response.Data = res;
        //            return Ok(response);
        //        }
        //        else
        //        {
        //            response.StatusCode = "404";
        //            response.Data = "Bad Request";
        //            return Ok(response);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, e.Message);
        //    }
        //}















    }
}
