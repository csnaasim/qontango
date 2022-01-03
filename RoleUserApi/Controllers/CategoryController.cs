using System;
using RoleUserApi.Model;
using Microsoft.AspNetCore.Mvc;
using RoleUserApi.Model.Response;
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
    public class CategoryController : ControllerBase 
    {
        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            try
            {
                List<Category> res = Category.SelectAllCategories();
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
        
        [HttpPost("GetAllCategories")]
        public IActionResult GetAllCategories(int OrgID)
        {
            Category Category = new Category();
                string res = Category.Select(OrgID);
                if (string.IsNullOrWhiteSpace(res))
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(res);
                }
        }      
        [HttpPost("GetAllCategoriesRoleBased")]
        public IActionResult GetAllCategoriesRoleBased()
        {
            Organization Organization = new Organization();
            Role Role = new Role();
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            List<Claim> Claims = claimsIdentity.Claims.ToList();
            Organization = Claims.GetOrganization();
            Role = Claims.GetRole();
            Category Category = new Category();
            
            List<Category> Categores = Category.getAllCategoriesMasterRole( Role.RoleID,Organization.OrgID);
                if (Categores.Count <1 )
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(Categores);
                }
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(Category Category)
        {
            if (Category == null)
                return NoContent();

            string res = Category.Insert();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("UpdateCategory")]
        public IActionResult UpdateCategory(Category Category)
        {
            string res = Category.Update();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("DeleteCategory")]
        public IActionResult DeleteCategory(int CategoryID)
        {
        Category Category = new Category();
        string res = Category.Delete(CategoryID);
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
