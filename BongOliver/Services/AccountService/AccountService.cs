using BongOliver.Constants;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.User;
using BongOliver.Repositories.UserRepository;
using BongOliver.Services.TokenService;
using System.Security.Cryptography;
using System.Text;

namespace BongOliver.Services.AccountService
{
    public class AccountService : IAccountService
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public AccountService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public ResponseDTO ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var idUser = Int16.Parse(_tokenService.GetTokenData("id"));

            var user = _userRepository.GetUserById(idUser);
            if (user == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "User không tồn tại!" };
            if (user.IsDelete) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Tài khoản của bạn đã bị vô hiệu!" };

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDTO.OldPassword));

            for (int i = 0; i < user.PasswordHash.Length; i++)
            {
                if (user.PasswordHash[i] != passwordBytes[i]) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Mật khẩu không chính xác!" };
            }

            passwordBytes = Encoding.UTF8.GetBytes(changePasswordDTO.NewPassword);

            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(passwordBytes);
            user.Update = DateTime.Now;

            _userRepository.UpdateUser(user);
            if (_userRepository.IsSaveChanges())
                return new ResponseDTO() { Message = "Đổi mật khẩu thành công!" };
            else
                return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Đổi mật khẩu thất bại!" };
        }

        public ResponseDTO GetMyProfile()
        {
            var idUser = Int16.Parse(_tokenService.GetTokenData("id"));

            var user = _userRepository.GetUserById(idUser);
            if (user == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "User không tồn tại!" };
            if (user.IsDelete) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Tài khoản của bạn đã bị vô hiệu!" };

            return new ResponseDTO() { Data = new UserDTO(user) };
        }

        public ResponseDTO UpdateProfile(UpdateUserDTO updateUserDTO)
        {
            var id = Int16.Parse(_tokenService.GetTokenData("id"));
            var user = _userRepository.GetUserById(id);
            if (user == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "User không tồn tại" };

            if (!user.Email.Equals(updateUserDTO.Email))
            {
                user.IsVerify = false;
                user.Email = updateUserDTO.Email;
            }

            user.FirstName = updateUserDTO.FirstName;
            user.LastName = updateUserDTO.LastName;
            user.Phone = updateUserDTO.Phone;
            user.Avatar = updateUserDTO.Avatar;
            user.Gender = updateUserDTO.Gender;
            user.DateOfBirth = updateUserDTO.DateOfBirth;

            user.Update = DateTime.Now;

            _userRepository.UpdateUser(user);
            if (_userRepository.IsSaveChanges())
                return new ResponseDTO() { Message = "Cập nhật thành công" };
            else
                return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Cập nhật thất bại" };
        }
    }
}
