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
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _loginService.Login(request);
            return Ok(user);
        }
    }
}
