namespace WebPizza_API_BackEnd.VModel
{
    public class RegisterRequestVModel
    {
        public string FullName { get; set; }  // Tên đầy đủ
        public string Password { get; set; }  // Mật khẩu
        public string Email { get; set; }  // Email đăng nhập
        public string Phone { get; set; }  // Số điện thoại
        public string Address { get; set; }  // Địa chỉ
        //public string Role { get; set; } = "Customer";  // Mặc định là Customer
    }
}
