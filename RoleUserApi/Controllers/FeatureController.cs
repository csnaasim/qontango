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
    public class FeatureController : ControllerBase 
    {
        [HttpGet("GetAllFeatures")]
        public IActionResult GetAllFeatures()
        {
            try
            {
                List<Feature> res = Feature.GetAllFeatures();
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

        [HttpGet("GetAllFeaturesJson")]
        public IActionResult GetAllFeaturesJson()
        {
            try
            {
                var res = Feature.GetAllFeaturesJson();
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

        [HttpPost("AddFeature")]
        public IActionResult AddFeature(Feature Feature)
        {
            if (Feature == null)
                return NoContent();

            string res = Feature.Insert();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("UpdateFeature")]
        public IActionResult UpdateFeature(Feature Feature)
        {
            string res = Feature.Update();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("DeleteFeature")]
        public IActionResult DeleteFeature(int FeatureID)
        {
        Feature Feature = new Feature();
        string res = Feature.Delete(FeatureID);
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpGet("GetAllMenuCategories")]
        public IActionResult GetAllMenuCategories()
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

        [HttpPost("AssignCrudToFeature")]
        public IActionResult AssignCrudToFeature(int FeatureID)
        {
            Feature Feature = new Feature();
            string res = Feature.AssignCrudToFeature(FeatureID);
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("AssignFunctionToFeature")]
        public IActionResult AssignFunctionToFeature(int FeatureID, int FunctionID)
        {
            Feature Feature = new Feature();
            string res = Feature.AssignFunctionToFeature(FeatureID, FunctionID);
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

        #region Functions
        [HttpGet("GetAllFunctions")]
        public IActionResult GetAllFunctions(int OrgID)
        {
            try
            {
                List<Function> res = Function.GetAllFunctions();
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

        [HttpPost("GetFunction")]
        public IActionResult GetFunction(Int16 FunctionID)
        {
            Function Function = new Function();
            Function res = Function.GetFunction(FunctionID);
            if (res == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("AddFunction")]
        public IActionResult AddFunction(Function Function)
        {
            if (Function == null)
                return NoContent();

            string res = Function.Insert();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("UpdateFunction")]
        public IActionResult UpdateFunction(Function Function)
        {
            string res = Function.Update();
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("DeleteFunction")]
        public IActionResult DeleteFunction(Int16 FunctionID)
        {
            Function Function = new Function();
            string res = Function.Delete(FunctionID);
            if (string.IsNullOrWhiteSpace(res))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }
        #endregion
    }
}
