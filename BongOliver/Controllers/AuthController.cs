using BongOliver.DTOs.User;
using BongOliver.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace BongOliver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult Register(RegisterUserDTO registerUserDTO)
        {
            var res = _authService.Register(registerUserDTO);
            return StatusCode(res.Code, res);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login(LoginUserDTO loginUserDTO)
        {
            var res = _authService.Login(loginUserDTO);
            return StatusCode(res.Code, res);
        }

        [AllowAnonymous]
        [HttpPost("forgot_password")]
        public ActionResult ForgotPassword(string email)
        {
            var res = _authService.ForgotPassword(email);
            return StatusCode(res.Code, res);
        }
    }
}
