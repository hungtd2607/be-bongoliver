using BongOliver.DTOs.Response;
using BongOliver.DTOs.User;

namespace BongOliver.Services.AuthService
{
    public interface IAuthService
    {
        ResponseDTO Register(RegisterUserDTO registerUserDTO);
        ResponseDTO Login(LoginUserDTO loginUserDTO);
        ResponseDTO ForgotPassword(string email);
        ResponseDTO ResetPassword(string email, string otp);
        bool VerifyOTP(string email, string otp);
        //ResponseDTO VerifyEmail(string email, string token);
    }
}
