using Microsoft.AspNetCore.Mvc;
using OPN.Domain;
using OPN.Services.Interfaces;
using OPN.Services.Requests;

namespace OPN.Api.Controllers
{
    [Route("/api/Tasks")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUnitOfWork _unitOfWork;
        public TaskController(ITaskService taskService, IUnitOfWork unitOfWork)
        {
            _taskService = taskService;
            _unitOfWork = unitOfWork;
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
        public async Task<IActionResult> GetUserCompletedTasks([FromQuery] string idn)
        {
            var result = await _unitOfWork.ProductHandlingTasksRepository.GetUserCompletedTasks(idn);
            
            return Ok(result);
        }

        [HttpGet("GetAllCompletedTasks")]
        public async Task<IActionResult> GetAllCompletedTasks()
        {
            var result = await _unitOfWork.ProductHandlingTasksRepository.GetAllCompletedTasks();

            return Ok(result);
        }

        [HttpGet("GetUserCurrentTask")]
        public async Task<IActionResult> GetUserCurrentTask(string idn)
        {
            var result =  await _unitOfWork.ProductHandlingTasksRepository.GetCurrentTask(idn);

            return Ok(result);
        }

        [HttpPut("CancelTask")]
        public IActionResult CancelTask([FromQuery] string idn)
        {
            _taskService.CancelTask(idn);
            return Ok();
        }

        [HttpGet("GetRanking")]
        public async Task<IActionResult> GetRanking()
        {
            var result = await _unitOfWork.UserRepository.GetRanking();
            
            return Ok(result);
        }
    }
}
