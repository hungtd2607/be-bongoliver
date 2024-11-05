using BongOliver.DTOs.User;
using BongOliver.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize]
        [HttpPost("change_password")]
        public ActionResult ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var res = _accountService.ChangePassword(changePasswordDTO);
            return StatusCode(res.Code, res);
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetMyProfile()
        {
            var res = _accountService.GetMyProfile();
            return StatusCode(res.Code, res);
        }
        [Authorize]
        [HttpPost]
        public IActionResult UpdateProfile(UpdateUserDTO updateUserDTO)
        {
            var res = _accountService.UpdateProfile(updateUserDTO);
            return StatusCode(res.Code, res);
        }
    }
}
