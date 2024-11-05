using BongOliver.DTOs.Response;
using BongOliver.DTOs.User;

namespace BongOliver.Services.AccountService
{
    public interface IAccountService
    {
        ResponseDTO ChangePassword(ChangePasswordDTO changePasswordDTO);
        ResponseDTO GetMyProfile();
        ResponseDTO UpdateProfile(UpdateUserDTO updateUserDTO);
    }
}
