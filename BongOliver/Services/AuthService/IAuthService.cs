using BongOliver.DTOs.Response;
using BongOliver.DTOs.User;

namespace BongOliver.Services.AuthService
{
    public interface IAuthService
    {
        ResponseDTO Register(RegisterUserDTO registerUserDTO);
        ResponseDTO Login(LoginUserDTO loginUserDTO);
        ResponseDTO ForgotPassword(string email);
        //ResponseDTO VerifyEmail(string email, string token);
    }
}
