using Microsoft.AspNetCore.Mvc;
using OPN.Services.Interfaces;

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
        public IActionResult Login([FromBody] string IDN)
        {
            var user = _loginService.Login(IDN);
            return Ok(user);
        }
    }
}
