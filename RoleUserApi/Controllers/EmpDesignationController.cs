using System;
using RoleUserApi.Model;
using RoleUserApi.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using RoleUserApi.Helpers;

namespace RoleUserApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpDesignationController : ControllerBase 
    {
        [HttpGet("GetAllEmpDesignations")]
        public IActionResult GetAllEmpDesignations()
        {
            try
            {
                Organization Organization = new Organization();
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                List<Claim> Claims = claimsIdentity.Claims.ToList();
                Organization = Claims.GetOrganization();
                List<EmpDesignation> res = EmpDesignation.SelectAllEmpDesignations(Organization.OrgID);
                ResponseModelNew response = new ResponseModelNew();
                if (res != null)
                {
                    response.StatusCode = "200";
                    response.Data = res;
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = "400";
                    response.Data = "Bad Request";
                    return Ok(response);
                }
            }
            catch (Exception e)
            {

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpPost("GetAllEmpDesignations")]
        public IActionResult GetAllEmpDesignations(int OrgID)
        {
            EmpDesignation EmpDesignation = new EmpDesignation();
                string res = EmpDesignation.Select(OrgID);
                if (string.IsNullOrWhiteSpace(res))
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(res);
                }
        }

        [HttpPost("AddEmpDesignation")]
        public IActionResult AddEmpDesignation(EmpDesignation EmpDesignation)
        {
            if (EmpDesignation == null)
                return NoContent();

            string res = EmpDesignation.Insert();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("UpdateEmpDesignation")]
        public IActionResult UpdateEmpDesignation(EmpDesignation EmpDesignation)
        {
            string res = EmpDesignation.Update();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("DeleteEmpDesignation")]
        public IActionResult DeleteEmpDesignation(int EmpDesigID)
        {
        EmpDesignation EmpDesignation = new EmpDesignation();
        string res = EmpDesignation.Delete(EmpDesigID);
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }
	}
}
