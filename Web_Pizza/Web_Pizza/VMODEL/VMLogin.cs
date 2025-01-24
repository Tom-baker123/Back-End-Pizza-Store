using System.ComponentModel.DataAnnotations;

namespace Web_Pizza.VMODEL
{
    public class VMLogin
    {
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Sai mật khẩu")]
        public string Password { get; set; }
    }
}
