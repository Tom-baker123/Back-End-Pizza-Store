using Web_Pizza.Entities;
using Web_Pizza.Responses;
using Web_Pizza.VMODEL;

namespace Web_Pizza.Services.IServices
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(VMUser vMUser);
        Task<User> ConfirmEmailAsync(string token);
        Task<LoginResponse> LoginAsync(VMLogin vMLogin);
        Task ResendActivationEmailAsync(string email);

    }
}
