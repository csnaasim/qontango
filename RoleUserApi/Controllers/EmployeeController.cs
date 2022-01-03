using System;
using RoleUserApi.Model;
using RoleUserApi.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using RoleUserApi.Helpers;
using System.Linq;

namespace RoleUserApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase 
    {
        [HttpGet("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {

            try
            {
                Organization Organization = new Organization();
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                List<Claim> Claims = claimsIdentity.Claims.ToList();
                Organization = Claims.GetOrganization();
                List<Employee> res = Employee.GetAllEmployees(Organization.OrgID);
                //if (res == null)
                //{
                //    return BadRequest();
                //}
                //else
                //{
                //    return Ok(res);
                //}
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
        [HttpGet("GetAllEmployeesAndDesignationAndRol")]
        public IActionResult GetAllEmployeesAndDesignationAndRol()
        {
            try
            {
                Organization Organization = new Organization();
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                List<Claim> Claims = claimsIdentity.Claims.ToList();
                Organization = Claims.GetOrganization();
                List<Employee> res = Employee.GetAllEmployeesAndDesignationAndRol(Organization.OrgID);

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
        [HttpPost("GetEmployee")]
        public IActionResult GetEmployee(int EmpID)
        {
            Employee Employee = new Employee();
            Employee res = Employee.GetEmployee(EmpID);
            if (res == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee(Employee Employee)
        {
            if (Employee == null)
                return NoContent();

            string res = Employee.Insert();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("UpdateEmployee")]
        public IActionResult UpdateEmployee(Employee Employee)
        {
            string res = Employee.Update();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("DeleteEmployee")]
        public IActionResult DeleteEmployee(int EmpID)
        {
        Employee Employee = new Employee();
        string res = Employee.Delete(EmpID);
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
