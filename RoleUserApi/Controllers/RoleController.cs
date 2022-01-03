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
    public class RoleController : ControllerBase 
    {
        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            try
            {
                //my new code 
                //getting organization value from  claim
                Organization Organization = new Organization();
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                List<Claim> Claims = claimsIdentity.Claims.ToList();
                Organization = Claims.GetOrganization();
              

                List<Role> res = Role.GetAllRoles(Organization.OrgID);
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

        [HttpGet("GetAllRolesWithFeatures")]
        public IActionResult GetAllRolesWithFeatures(int OrgID)
        {
            try
            {
                var res = Role.GetAllRolesWithFeatures(OrgID);
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

        [HttpPost("GetRole")]
        public IActionResult GetRole(int RolID)
        {
            Role Role = new Role();
                Role res = Role.GetRole(RolID);
                if (res == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(res);
                }
        }

        [HttpPost("AddRole")]
        public IActionResult AddRole(Role Role)
        {
            if (Role == null)
                return NoContent();

            string res = Role.Insert();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("UpdateRole")]
        public IActionResult UpdateRole(Role Role)
        {
            string res = Role.Update();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("DeleteRole")]
        public IActionResult DeleteRole(int RoleID)
        {
        Role Role = new Role();
        string res = Role.Delete(RoleID);
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("AssignFeatureToRole")]
        public IActionResult AssignFeatureToRole(int FeatureID, int FunctionID, int RoleID, int OrgID)
        {
            Feature feature = new Feature();
            string res = feature.AssignFeatureToRole(FeatureID, FunctionID, RoleID, OrgID);
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpGet("GetFeaturesAssignedToRole")]
      //  public IActionResult GetFeaturesAssignedToRole(int RoleID, int OrgID)
        public IActionResult GetFeaturesAssignedToRole()
        {
            try
            {
                //my new code 
                //getting organization value from  claim
                Organization Organization = new Organization();
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                List<Claim> Claims = claimsIdentity.Claims.ToList();
                Organization = Claims.GetOrganization();
                //getting role value from claim
                Role role = new Role();
            
               
                role = Claims.GetRole();


                //old code 
                Feature feature = new Feature();
                List<Feature> res = feature.GetFeaturesAssignedToRole(role.RoleID, Organization.OrgID);
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

        [HttpGet("GetFeaturesNotAssignedToRole")]
        public IActionResult GetFeaturesNotAssignedToRole(int RoleID, int OrgID)
        {
            try
            {
                Feature feature = new Feature();
                List<Feature> res = feature.GetFeaturesNotAssignedToRole(RoleID, OrgID);
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
    }
}
