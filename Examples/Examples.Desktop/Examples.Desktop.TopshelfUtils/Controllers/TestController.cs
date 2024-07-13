using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Examples.Desktop.TopshelfUtils.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        public TestController(IConfiguration configuration)
        {
        }

        [HttpGet("ping")]
        public bool Ping() => true;
    }
}