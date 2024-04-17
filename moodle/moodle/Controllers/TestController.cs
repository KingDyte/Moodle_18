using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace moodle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TestController(IWebHostEnvironment hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }

        [HttpGet]

        public async Task<IActionResult> GetJsonFile()
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "test.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var json = System.IO.File.ReadAllText(filePath);

            return Content(json, "application/json");
        }
    }
}
