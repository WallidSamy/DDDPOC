using DDDPOC.Application;
using KeyClockAuthorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDDPOC.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Authorize()]
        public async Task<bool> Add(AddCustomerCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("getRandomWeatherForecast")]
        [Authorize(Policy = "RequireToBeInKeycloakGroupAsReader")]
        //[Authorize()]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }
}
