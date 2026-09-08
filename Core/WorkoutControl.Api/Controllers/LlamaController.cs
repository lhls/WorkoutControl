using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WorkoutControl.Api.Services;

namespace WorkoutControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LlamaController : ControllerBase
    {
        private readonly LlamaService _llamaService;

        public LlamaController(IConfiguration config)
        {
            _llamaService = new LlamaService(config);
        }

        [HttpPost("send-prompt")]
        public async Task<IActionResult> SendPrompt([FromBody] PromptRequest request)
        {
            var result = await _llamaService.SendPromptAsync(request.Prompt);
            return Ok(new { response = result });
        }

        public class PromptRequest
        {
            public string Prompt { get; set; }
        }
    }
}
