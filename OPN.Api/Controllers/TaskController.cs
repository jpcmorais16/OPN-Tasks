using Microsoft.AspNetCore.Mvc;
using OPN.Services.Interfaces;
using OPN.Services.Requests;

namespace OPN.Api.Controllers
{
    [Route("/api/Tasks")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        
        [HttpGet("CreateRandomTask")]
        public async Task<IActionResult> CreateRandomTask([FromQuery] string idn)
        {
            var result = await _taskService.CreateRandomProductHandlingTask(idn);

            return Ok(result);
        }

        [HttpPost("CompleteTask")]
        public IActionResult CompleteTask([FromQuery] string idn)
        {
            _taskService.CompleteTask(idn);
            return Ok();
        }

        [HttpGet("GetUserCompletedTasks")]
        public IActionResult GetUserCompletedTasks([FromQuery] string idn)
        {
            var result = _taskService.GetUserCompletedTasks(idn);
            return Ok(result);
        }

        [HttpGet("NumberOfCompletedTasksByUser")]
        public IActionResult GetUserNumberOfCompletedTasks([FromQuery] string idn)
        {
            var result = _taskService.GetUserNumberOfCompletedTasks(idn);
            return Ok(result);
        }

        [HttpGet("GetAllCompletedTasks")]
        public IActionResult GetAllCompletedTasks()
        {
            var result = _taskService.GetAllCompletedTasks();

            return Ok(result);
        }

        [HttpGet("GetUserCurrentTask")]
        public IActionResult GetUserCurrentTask(string idn)
        {
            var result = _taskService.GetUserCurrentTask(idn);

            return Ok(result);
        }

        [HttpPut("CancelTask")]
        public IActionResult CancelTask([FromQuery] string idn)
        {
            _taskService.CancelTask(idn);
            return Ok();
        }

        [HttpGet("GetRanking")]
        public IActionResult GetRanking()
        {
            return Ok(_taskService.GetRanking());
        }
    }
}
