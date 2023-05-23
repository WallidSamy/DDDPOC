using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDDPOC.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ProductController : ControllerBase
    {
        private static readonly string[] Products = new[]
        {
            "Mobile", "Laptop", "TV"
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Products);
        }
    }
}
