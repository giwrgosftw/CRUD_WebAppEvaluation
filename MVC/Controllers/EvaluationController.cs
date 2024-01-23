using CRUD_WebAppEvaluation.DbApplicationContext;
using CRUD_WebAppEvaluation.MVC.Models;
using CRUD_WebAppEvaluation.MVC.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CRUD_WebAppEvaluation.MVC.Controllers
{
    [Route("api/evaluation")]
    public class EvaluationController : Controller
    {
        private readonly EvaluationService evaluationService;
        private Response response;

        public EvaluationController(EvaluationService evaluationService)
        {
            this.evaluationService = evaluationService;
            response = new Response();
        }

        [HttpPost("getEvaluationByIdPost")]
        public IActionResult GetEvaluationByIdPost([FromBody] Evaluation evaluation)
        {
            try
            {
                // Give a JSON Evaluation Object including the id only
                var result = evaluationService.FindEvaluationById(evaluation.Id);
                return Ok(result);
            }
            catch
            {
                // Return an appropriate error response
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpGet("getEvaluationByIdGet/{id}")]
        public IActionResult GetEvaluationByIdGet(int id)
        {
            try
            {
                var result = evaluationService.FindEvaluationById(id);
                return Ok(result);
            }
            catch
            {
                // Return an appropriate error response
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpGet("getAllEvaluation")]
        public IActionResult GetAllEvaluation()
        {
            try
            {
                var result = evaluationService.FindAllEvaluations();
                return Ok(result);
            }
            catch
            {
                // Return an appropriate error response
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpPost("addNewEvaluation")]
        public ActionResult<Response> AddNewEvaluation([FromBody] Evaluation evaluation)
        {
            try
            {
                // Call the service method to add the new Evaluation
                evaluationService.AddEvaluation(evaluation);
                // Return Successful Response
                response.message = "Evaluation Successfully Added!";
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response.message = ex.Message;  // more than one scenario
                return Ok(response);
            }
            catch (DuplicateNameException)
            {
                response.message = "Evaluation already exists. Please use a different id.";
                return BadRequest(response);
            }
            catch (Exception)
            {
                // Return an appropriate error response
                response.message = "Something went wrong, please contact IT Support";
                return BadRequest(response);
            }
        }


        [HttpPost("updateEvaluation")]
        public ActionResult<Response> UpdateEvaluation([FromBody] Evaluation evaluation)
        {
            try
            {
                // Update
                evaluationService.UpdateEvaluation(evaluation);
                // Return Successful Response
                response.message = "Evaluation Successfully Updated!";
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                response.message = $"Evaluation with given Id NOT found (Id: {evaluation.Id})";
                return NotFound(response);
            }
            catch (ArgumentException ex)
            {
                response.message = ex.Message;  // more than one scenario
                return BadRequest(response);
            }
            catch (Exception)
            {
                // Return an InteralServer error response
                response.message = "Something went wrong, please contact IT Support";
                return BadRequest(response);
            }
        }

        [HttpPost("deleteEvaluation")]
        public ActionResult<Response> DeleteEvaluation([FromQuery] int id)
        {
            try
            {
                evaluationService.DeleteEvaluation(id);
                response.message = "Evaluation Successfully Deleted!";
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                response.message = $"Evaluation with given Id NOT found (Id: {id})";
                return NotFound(response);
            }
            catch (Exception)
            {
                // Return an InteralServer error response
                response.message = "Something went wrong, please contact IT Support";
                return BadRequest(response);
            }
        }

    }
}
