using Microsoft.AspNetCore.Mvc;

namespace moodle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult tryNumeroUno() 
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "test.json");

            if (!System.IO.File.Exists(filePath)) return NotFound();

            var json = System.IO.File.ReadAllText(filePath, "application/json");
            return Content(json);
        }
    }
}
