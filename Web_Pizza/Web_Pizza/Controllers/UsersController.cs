using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Pizza.Repositories;
using Web_Pizza.Services.IServices;
using Web_Pizza.VMODEL;

namespace Web_Pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserRepository _userRepository;
        public UsersController (IUserService userService, UserRepository userRepository)
        {
            _userRepository = userRepository;
            _userService = userService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateUserAsync([FromBody] VMUser vMUser)
        {
            //Kiểm tra xem có đúng ràng buộc không
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (vMUser.Password != vMUser.RetypePassword)
            {
                return BadRequest(ModelState);
            }
            Console.WriteLine($"Name: {vMUser.Name}, Email: {vMUser.Email}, Phone: {vMUser.Phone},Password:{vMUser.Password},RetypePassword:{vMUser.RetypePassword},Address:{vMUser.Address}");

            try
            {
                await _userService.CreateUserAsync(vMUser);
                //var registrationResult = new RegistrationResult(true, "Đăng kí thành công");
                //return Ok(registrationResult);
                return Ok(new { message = "Đăng kí thành công" });
            }
            catch (Exception ex)
            {
                //var registrationResult = new RegistrationResult(false, $"Có lỗi xảy ra: {ex.Message}");
                //return BadRequest(registrationResult);
                return BadRequest(new {message = $"Có lỗi xảy ra: {ex.Message}"});
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token xác nhận không hợp lệ.");
            }

            try
            {
                var user = await _userService.ConfirmEmailAsync(token);
                if (user == null)
                {
                    return BadRequest("Token không hợp lệ hoặc đã hết hạn.");
                }

                return Ok(new { message = "Xác nhận tài khoản thành công. Bạn có thể đăng nhập." });
            }
            catch (Exception ex)
            {             
                    return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] VMLogin vMLogin)
        {
            try
            {
                var loginResponse = await _userService.LoginAsync(vMLogin);
                return Ok(loginResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet("resend-activation")]
        public async Task<IActionResult> ResendActivationEmailAsync(string email)
        {
            try
            {
                await _userService.ResendActivationEmailAsync(email);
                return Ok(new { message = "Email kích hoạt đã được gửi lại. Vui lòng kiểm tra email của bạn." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
