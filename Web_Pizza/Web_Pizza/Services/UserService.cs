using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_Pizza.Entities;
using Web_Pizza.Repositories;
using Web_Pizza.Responses;
using Web_Pizza.Services.IServices;
using Web_Pizza.VMODEL;

namespace Web_Pizza.Services
{
    public class UserService:IUserService
    {
        private readonly IEmailService _emailService;
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IEmailService emailService, UserRepository userRepository, IConfiguration configuration)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> ConfirmEmailAsync(string token)
        {
            var user = await _userRepository.GetUserByConfirmationTokenAsync(token);

            if (user == null)
            {
                throw new Exception("Token không hợp lệ hoặc không tồn tại.");
            }

            if (user.TokenExpiration.HasValue && user.TokenExpiration < DateTime.UtcNow)
            {
                throw new Exception("Token đã hết hạn.");
            }

            user.IsActive = true;
            user.ConfirmationToken = string.Empty;
            user.TokenExpiration = null;
            await _userRepository.UpdateUserAsync(user);

            return user;
        }

        public async Task<User> CreateUserAsync(VMUser vMUser)
        {
            if (vMUser == null)
            {
                throw new ArgumentNullException(nameof(vMUser));
            }

            var existingPhone = await _userRepository.GetUserByPhoneAsync(vMUser.Phone);
            var existingMail = await _userRepository.GetByEmailAsync(vMUser.Email);

            if (existingPhone != null)
            {
                throw new Exception("Số điện thoại đã được đăng ký.");
            }

            if (existingMail != null)
            {
                throw new Exception("Email đã được đăng ký.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(vMUser.Password);
            var user = new User
            {
                Name = vMUser.Name,
                Email = vMUser.Email,
                Phone = vMUser.Phone,
                Password = hashedPassword,
                Address = vMUser.Address,
                RoleId = 1,
                IsActive = false,
                ConfirmationToken = GenerateConfirmationToken(vMUser.Email),
                TokenExpiration = DateTime.UtcNow.AddMinutes(3),
            };

            await _userRepository.AddUserAsync(user);

            string confirmationLink = $"http://localhost:5015/api/users/confirm-email?token={user.ConfirmationToken}";
            string resendActivationEmail = $"http://localhost:5015/api/users/resend-activation?email={user.Email}";


            var subject = "Xác nhận đăng ký tài khoản";
            var emailBody = GenerateEmailBody(user.Name, confirmationLink,resendActivationEmail);

            await _emailService.SendMailAsync(user.Email, subject, emailBody);

            return user;
        }

        public async Task<LoginResponse> LoginAsync(VMLogin vMLogin)
        {
            var user = await _userRepository.GetByEmailAsync(vMLogin.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Email chưa được đăng kí");
            }
            if (!BCrypt.Net.BCrypt.Verify(vMLogin.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Mật khẩu không chính xác");
            }
            if (user.IsActive == false)
            {
                throw new UnauthorizedAccessException("Tài khoản chưa được kích hoạt. Vui lòng kiểm tra Email để kích hoạt tài khoản.");
            }
            //GenerateJwtToken(user);
            return new LoginResponse
            {
                Token = GenerateJwtToken(user),
                Message = "Đăng nhập thành công"
            };
        }
        private string GenerateConfirmationToken(string email)
        {
            return Guid.NewGuid().ToString();
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task ResendActivationEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Email không tồn tại trong hệ thống.");
            }
            if (user.IsActive)
            {
                throw new Exception("Tài khoản đã được kích hoạt.");
            }
            // Tạo token mới gửi lại người dùng
            user.ConfirmationToken = GenerateConfirmationToken(email);
            user.TokenExpiration = DateTime.UtcNow.AddMinutes(3);
            await _userRepository.UpdateUserAsync(user);
            // Gửi email xác nhận
            string confirmationLink = $"http://localhost:5015/api/users/confirm-email?token={user.ConfirmationToken}";

            string resendActivationEmail = $"http://localhost:5015/api/users/resend-activation?email={user.Email}";

            var subject = "Xác nhận đăng ký tài khoản";
            string htmlTemplateGmailPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Templates", "index.html");
            string htmlTemplateGmail = File.ReadAllText(htmlTemplateGmailPath);
            Console.WriteLine(htmlTemplateGmail);
            string Gmailbody = GenerateEmailBody(user.Name,confirmationLink,resendActivationEmail);

            await _emailService.SendMailAsync(user.Email, subject, Gmailbody);
        }
        private string GenerateEmailBody(string userName, string confirmationLink,string resendActivationEmail)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Templates", "index.html");
            var templateContent = File.ReadAllText(templatePath);

            return templateContent
                .Replace("{{userName}}", userName)
                .Replace("{{confirmationLink}}", confirmationLink)
                .Replace("{{resendActivationEmail}}", resendActivationEmail);

        }
    }
}
