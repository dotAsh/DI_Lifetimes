using DependencyInjection.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IScopedGuidService _scoped1;
        private readonly IScopedGuidService _scoped2;

        private readonly ITransientGuidService _transient1;
        private readonly ITransientGuidService _transient2;

        private readonly ISingletonGuidService _singletone1;
        private readonly ISingletonGuidService _singletone2;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger ,
            IScopedGuidService scoped1, IScopedGuidService scoped2,
            ITransientGuidService transient1, ITransientGuidService transient2,
            ISingletonGuidService singletone1, ISingletonGuidService singletone2
            )
        {
            _logger = logger;

            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _transient1 = transient1;
            _transient2 = transient2;
            _singletone1 = singletone1;
            _singletone2 = singletone2;


        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            Console.WriteLine("Scoped_1: "+_scoped1.GetGuid());
            Console.WriteLine("Scoped_2: "+_scoped2.GetGuid());

            Console.WriteLine("Transient_1: "+_transient1.GetGuid());
            Console.WriteLine("Transient_2: " + _transient2.GetGuid());


            Console.WriteLine("singletone1: " + _singletone1.GetGuid());
            Console.WriteLine("singletone2: " + _singletone2.GetGuid());

            Console.WriteLine("*********************************");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
