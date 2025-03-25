namespace WebPizza_API_BackEnd.VModel
{
    public class LoginResponseVModel
    {
        public bool Success { get; set; }  // Trạng thái đăng nhập (true/false)
        public string Message { get; set; } // Thông báo lỗi hoặc thành công
        public string Token { get; set; }  // JWT Token
        public string Role { get; set; }  // Quyền (Admin, Customer)
        public string FullName { get; set; }  // Tên đầy đủ của User
    }
}
