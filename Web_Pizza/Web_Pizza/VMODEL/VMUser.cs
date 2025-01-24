using System.ComponentModel.DataAnnotations;

namespace Web_Pizza.VMODEL
{
    public class VMUser
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Tên đăng nhập có ít nhất 6 ký tự.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có đủ nhất 10 ký tự.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc.")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string RetypePassword { get; set; }
        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Địa chỉ phải có ít nhất 6 ký tự.")]
        public string Address { get; set; }
    }
}
