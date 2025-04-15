using GoogleCalendarApi.Interfaces;
using GoogleCalendarApi.Models;
using GoogleCalendarApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace GoogleCalendarApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController(IGoogleCalendarService googleCalendarService) : ControllerBase
    {
        private readonly IGoogleCalendarService _googleCalendarService = googleCalendarService;

        [HttpPost("criar-evento")]
        public IActionResult CriarEvento([FromBody] InformacoesProcesso jsonData)
        {
            try
            {
                string result = _googleCalendarService.CriarEvento(jsonData);
                return Ok( result );
            }
            catch (Exception ex) {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

