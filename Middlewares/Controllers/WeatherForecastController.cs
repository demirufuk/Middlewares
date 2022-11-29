using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Middlewares.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "Ok";
        }

        [HttpGet("Student")]
        public Student GetStudent()
        {
            return new Student()
            {
                Id = 1,
                FullName = "Ufuk"
            };
        }

        [HttpPost("AddStudent")]
        public string InsertStudent([FromBody]Student model)
        {
            return "Added Student";
        }

    }
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
