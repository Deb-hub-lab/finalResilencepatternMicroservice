using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using StudentAPI.Services;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        private readonly IAttendanceService _attendanceService;
        public StudentController(IGradeService gradeService, IAttendanceService attendanceService)
        {
            _gradeService = gradeService;
            _attendanceService = attendanceService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetStudent(int id)
        {
            var student = await GetStudentById(id);
            return Ok(student);
        }

        private async Task<Student> GetStudentById(int id)
        {
            //In real apps, the student details should be retrieved from the DB
            //Hard coded for this demo
           
            Student student = new Student { Id = id, FirstName = "Paul", LastName = "Davis" };

            int grade = 0;
            //Fetch the student's grade from the Grade API
            try
            {
                grade = await _gradeService.GetStudentGrade(id);
            }
            catch
            {
                //Code to log the exception
            }
            
            student.Grade = grade;

            return student;
        }

        [HttpGet("summary/{studentId}")]
        public async Task<IActionResult> GetSummary(Guid studentId)
        {
            //int grade = await _gradeService.GetGradeAsync(studentId);
            int attendance = await _attendanceService.GetMonthlyAttendanceAsync(Guid.Parse("11111111-1111-1111-1111-111111111107")); // "IntToHashedGuid(studentId));

            return Ok(new
            {
                StudentId = studentId,
                Grade = 2,
                MonthlyAttendance = attendance
            });
        }

        public static Guid IntToHashedGuid(int value)
        {
            using var md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value.ToString()));
            return new Guid(hash);
        }
    }
}
