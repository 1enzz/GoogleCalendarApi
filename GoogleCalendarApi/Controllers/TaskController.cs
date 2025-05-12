using GoogleCalendarApi.Models;
using Microsoft.AspNetCore.Mvc;
using GoogleCalendarApi.Interfaces;
using GoogleCalendarApi.Services;


namespace GoogleCalendarApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController(IGoogleTaskService taskService) : ControllerBase
    {
        private readonly IGoogleTaskService _googleTaskService = taskService;


        [HttpPost("criar-task")]
        public IActionResult CriarEvento([FromBody] EventRequestTask request)
        {
            if (string.IsNullOrEmpty(request.AccessToken))
                return BadRequest("Token nao informado");
            try
            {
                string result = _googleTaskService.CriarTask(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


    }
}
