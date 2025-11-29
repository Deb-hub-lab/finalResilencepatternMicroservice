using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _svc;
        public AttendanceController(IAttendanceService svc) => _svc = svc;

        [HttpGet("{year:int}/{month:int}")]
        public async Task<IActionResult> GetByMonth(int year, int month)
        {
            if (month < 1 || month > 12) return BadRequest("month must be 1..12");

            var list = await _svc.GetAttendanceByMonthAsync(year, month);
            return Ok(list);
        }
    }
}
