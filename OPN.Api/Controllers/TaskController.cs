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

        [HttpGet("GetUserCompletedTasks")]
        public IActionResult GetUserCompletedTasks([FromQuery] string IDN)
        {
            var result = _taskService.GetUserCompletedTasks(IDN);
            return Ok(result);
        }

        [HttpGet("NumberOfCompletedTasksByUser")]
        public IActionResult GetUserNumberOfCompletedTasks([FromQuery] string IDN)
        {
            var result = _taskService.GetUserNumberOfCompletedTasks(IDN);
            return Ok(result);
        }

        [HttpGet("GetAllCompletedTasks")]
        public IActionResult GetAllCompletedTasks()
        {
            var result = _taskService.GetAllCompletedTasks();

            return Ok(result);
        }

        [HttpPut("CancelTask")]
        public IActionResult CancelTask([FromQuery]string IDN)
        {
            _taskService.CancelTask(IDN);
            return Ok();
        }
    }
}
