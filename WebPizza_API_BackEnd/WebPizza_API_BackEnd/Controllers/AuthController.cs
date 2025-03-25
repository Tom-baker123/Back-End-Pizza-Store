using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        // Inject IUserService vào Controller
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        // API đăng ký tài khoản
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestVModel model)
        {
            var result = await _userService.RegisterUser(model);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }
        // API đăng nhập tài khoản
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestVModel model)
        {
            var result = await _userService.LoginUser(model);
            if (!result.Success)
                return Unauthorized(result.Message);
            return Ok(result);
        }

        // API kích hoạt tài khoản
        [HttpGet("activate")]
        public async Task<IActionResult> ActivateAccount([FromQuery] string token)
        {
            var result = await _userService.ActivateAccount(token);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("resend-activation")]
        public async Task<IActionResult> ResendActivationEmail([FromQuery] string email)
        {
            var result = await _userService.ResendActivationEmail(email);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("users")]
        //[Authorize(Roles = "Admin")]  // Chỉ Admin mới có quyền lấy danh sách user
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
    }

}
