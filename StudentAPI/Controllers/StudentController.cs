using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using StudentAPI.Services;
using System.Diagnostics;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public StudentController(IGradeService gradeService)
        {
            _gradeService = gradeService;
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
    }
}
