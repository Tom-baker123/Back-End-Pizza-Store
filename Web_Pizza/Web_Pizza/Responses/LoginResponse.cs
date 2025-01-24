namespace Web_Pizza.Responses
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
