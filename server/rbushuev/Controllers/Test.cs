using Microsoft.AspNetCore.Mvc;

namespace rbushuev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok($"ok");
        }
    }
}