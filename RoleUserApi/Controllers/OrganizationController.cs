using System;
using RoleUserApi.Model;
using RoleUserApi.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace RoleUserApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        [HttpGet("GetAllOrganizations")]
        public IActionResult GetAllOrganizations()
        {
            try
            {
                List<Organization> res = Organization.GetAllOrganizations();
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

        [HttpPost("GetOrganization")]
        public IActionResult GetOrganization(int OrgID)
        {
            Organization Organization = new Organization();
            Organization res = Organization.GetOrganization(OrgID);
            if (res == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("AddOrganization")]
        public IActionResult AddOrganization(Organization Org)
        {
            if (Org == null)
                return NoContent();

            string res = Org.Insert();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("UpdateOrganization")]
        public IActionResult UpdateOrganization(Organization Org)
        {
            string res = Org.Update();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("DeleteOrganization")]
        public IActionResult DeleteOrganization(int OrgID)
        {
            Organization Organization = new Organization();
            string res = Organization.Delete(OrgID);
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
