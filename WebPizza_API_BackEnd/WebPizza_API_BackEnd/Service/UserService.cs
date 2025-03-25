using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public UserService(AppDbContext context, IConfiguration config, IEmailService emailService)
        {
            _context = context;
            _config = config;
            _emailService = emailService;
        }

        public async Task<LoginResponseVModel> RegisterUser(RegisterRequestVModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return new LoginResponseVModel { Success = false, Message = "Email đã tồn tại" };

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Phone = model.Phone,
                Address = model.Address,
                Role = "Customer",
                IsActive = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            try
            {
                await SendActivationEmail(user);
            }
            catch (Exception ex)
            {
                return new LoginResponseVModel
                {
                    Success = true,
                    Message = $"Đăng ký thành công, nhưng gửi email xác nhận thất bại: {ex.Message}. Vui lòng liên hệ hỗ trợ."
                };
            }

            return new LoginResponseVModel
            {
                Success = true,
                Message = "Đăng ký thành công. Vui lòng kiểm tra email để kích hoạt tài khoản."
            };
        }

        public async Task<LoginResponseVModel> LoginUser(LoginRequestVModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                return new LoginResponseVModel { Success = false, Message = "Email hoặc mật khẩu không đúng" };

            if (!user.IsActive)
                return new LoginResponseVModel { Success = false, Message = "Tài khoản chưa được kích hoạt. Vui lòng kiểm tra email hoặc yêu cầu gửi lại link kích hoạt." };

            var token = GenerateJwtToken(user);
            return new LoginResponseVModel { Success = true, Token = token, Role = user.Role };
        }

        public async Task<LoginResponseVModel> ActivateAccount(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // Loại bỏ độ lệch thời gian mặc định (5 phút)
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                var userIdClaim = principal.FindFirst("UserId")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                    return new LoginResponseVModel { Success = false, Message = "Token không hợp lệ" };

                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    return new LoginResponseVModel { Success = false, Message = "Người dùng không tồn tại" };

                if (user.IsActive)
                    return new LoginResponseVModel { Success = false, Message = "Tài khoản đã được kích hoạt" };

                user.IsActive = true;
                await _context.SaveChangesAsync();

                return new LoginResponseVModel { Success = true, Message = "Tài khoản đã được kích hoạt thành công" };
            }
            catch (SecurityTokenExpiredException)
            {
                return new LoginResponseVModel
                {
                    Success = false,
                    Message = "Link kích hoạt đã hết hạn. Vui lòng nhấp vào 'Gửi lại' trong email để nhận link mới."
                };
            }
            catch (Exception ex)
            {
                return new LoginResponseVModel { Success = false, Message = $"Lỗi khi kích hoạt: {ex.Message}" };
            }
        }

        public async Task<LoginResponseVModel> ResendActivationEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return new LoginResponseVModel { Success = false, Message = "Email không tồn tại" };

            if (user.IsActive)
                return new LoginResponseVModel { Success = false, Message = "Tài khoản đã được kích hoạt" };

            try
            {
                await SendActivationEmail(user);
            }
            catch (Exception ex)
            {
                return new LoginResponseVModel
                {
                    Success = false,
                    Message = $"Gửi email xác nhận thất bại: {ex.Message}. Vui lòng liên hệ hỗ trợ."
                };
            }

            return new LoginResponseVModel
            {
                Success = true,
                Message = "Email xác nhận đã được gửi lại. Vui lòng kiểm tra hộp thư."
            };
        }

        public async Task<IEnumerable<UserResponseVModel>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(u => new UserResponseVModel
            {
                UserID = u.UserID,
                FullName = u.FullName,
                Email = u.Email,
                Phone = u.Phone,
                Address = u.Address,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            }).ToList();
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateActivationToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("UserId", user.UserID.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task SendActivationEmail(User user)
        {
            var activationToken = GenerateActivationToken(user);
            var confirmationLink = $"{_config["AppSettings:BaseUrl"]}/api/auth/activate?token={activationToken}";
            var resendLink = $"{_config["AppSettings:BaseUrl"]}/api/auth/resend-activation?email={user.Email}";
            var emailBody = GenerateEmailBody(user.FullName, confirmationLink, resendLink);

            await _emailService.SendEmailAsync(user.Email, "Xác nhận đăng ký tài khoản", emailBody);
        }

        private string GenerateEmailBody(string userName, string confirmationLink, string resendLink)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Template", "index.html");
            var templateContent = File.ReadAllText(templatePath);
            return templateContent
                .Replace("{{userName}}", userName)
                .Replace("{{confirmationLink}}", confirmationLink)
                .Replace("{{resendActivationEmail}}", resendLink);
        }
    }
}