using Microsoft.AspNetCore.Mvc;
using OPN.Services.Interfaces;
using OPN.Services.Requests;

namespace OPN.Api.Controllers
{
    [ApiController]
    [Route("/api/Login")]
    public class LoginController: Controller
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromQuery] LoginRequest request)
        {
            var user = await _loginService.Login(request);
            return Ok(user);
        }
    }
}
