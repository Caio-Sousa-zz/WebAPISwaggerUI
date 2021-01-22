using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIRedDoc.Models;

namespace WebAPIRedDoc.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;

        private static readonly string[] Summaries = new[]
        {
            "Ford", "Honda", "Ferrari", "Chevy", "Renault"
        };

        public CarController(ILogger<CarController> logger)
        {
            _logger = logger;
        }

        
        [HttpGet]
        public IEnumerable<Cars> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Cars
            {
                DateCreated = DateTime.Now.AddDays(index),
                Model = Summaries[rng.Next(Summaries.Length)],
                Year = rng.Next(1990, 2021)
            })
            .ToArray();
        }
    }
}
