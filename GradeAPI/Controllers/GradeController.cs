using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace GradeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private static int counter;

        [HttpGet]
        [Route("{id}")]
        //public ActionResult<int> Get(int id)
        //{
        //    //this should come from the db.
        //    //For this example we will be returning a random number between 1 and 10
        //    return GetStudentGrade();
        //    // return StatusCode(StatusCodes.Status500InternalServerError);

        //}
        //public ActionResult<int> Get(int id)
        //{
        //    //transient error simulation
        //    try
        //    {
        //        counter++;
        //        //int remainder = counter % 3;
        //        //if (remainder > 0)

        //        if (counter <= 5) // first 5 call will throw exception
        //        {
        //            throw new Exception();
        //        }
        //        return GetStudentGrade();
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //public ActionResult<int> Get(int id)
        //{
        //    counter++;

        //    // Simulate failure for first 5 attempts
        //    if (counter <= 5)
        //    {
        //        Console.WriteLine($"❌ API FAIL Simulated, Count: {counter}");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Simulated Server Error");
        //    }

        //    Console.WriteLine($"✔ API SUCCESS on attempt #{counter}");
        //    return GetStudentGrade();
        //}
        //public ActionResult<int> Get(int id)
        //{
        //    counter++;

        //    if (counter <= 5)
        //    {
        //        throw new Exception("Simulated failure for retry + CB + fallback");
        //    }

        //    return GetStudentGrade();
        //}
        public ActionResult<int> Get(int id)
        {
            counter++;

            if (counter <= 5)  // FAIL FIRST 10 CALLS
            {
                Console.WriteLine(DateTime.Now.ToString());
                Console.WriteLine($"API FAIL #{counter}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Console.WriteLine("API SUCCESS returning grade = 75");
            return GetStudentGrade();
        }

        private int GetStudentGrade()
        {
            Random rnd = new Random();
            int grade = rnd.Next(1, 11);  // creates a number between 1 and 10
            return grade;
        }

    }
}
