using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoleUserApi.Model;
using RoleUserApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RoleUserApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        [HttpGet("CustomerDashboardJobStatistics")]
        public IActionResult GetBookingsByJobID(string criteria, int customerID)
        {
            try
            {
                Dashboard dashboard = new Dashboard();
                var res = dashboard.CustomerDashboardJobStatistics(criteria, customerID);

                ResponseModel response = new ResponseModel();
                response.Data = res;
                response.StatusCode = 200;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("DashboardJobs")]
        public IActionResult sp_DashboardJobs(int customerID)
        {
            try
            {
                Dashboard dashboard = new Dashboard();
                var res = dashboard.DashboardJobs(customerID);

                ResponseModel response = new ResponseModel();
                response.Data = res;
                response.StatusCode = 200;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("MainDashboardData")]
        public IActionResult MainDashboardData(int jobID)
        {
            try
            {
                Dashboard dashboard = new Dashboard();
                var res = dashboard.MainDashboardData(jobID);

                ResponseModel response = new ResponseModel();
                response.Data = res;
                response.StatusCode = 200;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}