using Microsoft.AspNetCore.Mvc;
using OPN.Services.Interfaces;
using OPN.Services.Requests;

namespace OPN.Api.Controllers
{
    [Route("/api/Task")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        
        [HttpGet("CreateRandomTask")]
        public IActionResult CreateRandomTask([FromQuery] string IDN)
        {
            var result = _taskService.CreateRandomProductHandlingTask(new TaskRequest { LoggedUserIDN = IDN});

            return Ok(result);
        }

        [HttpPost("CompleteTask")]
        public IActionResult CompleteTask([FromQuery] string IDN)
        {
            _taskService.CompleteTask(IDN);
            return Ok();
        }
    }
}
