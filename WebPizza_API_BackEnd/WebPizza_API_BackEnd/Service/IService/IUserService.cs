using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface IUserService
    {
        Task<LoginResponseVModel> LoginUser(LoginRequestVModel model);
        Task<LoginResponseVModel> RegisterUser(RegisterRequestVModel model);
        Task<IEnumerable<UserResponseVModel>> GetAllUsers();
        Task<LoginResponseVModel> ActivateAccount(string token);
        Task<LoginResponseVModel> ResendActivationEmail(string email);
    }
}
