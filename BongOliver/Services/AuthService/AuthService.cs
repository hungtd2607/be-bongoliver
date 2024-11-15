using BongOliver.Constants;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.User;
using BongOliver.Models;
using BongOliver.Repositories.UserRepository;
using BongOliver.Services.MailService;
using BongOliver.Services.TokenService;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BongOliver.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMailService _mailService;
        public AuthService(IUserRepository userRepository, ITokenService tokenService, IMailService mailService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mailService = mailService;
        }

        public ResponseDTO ForgotPassword(string email)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "User không tồn tại!" };
            if (user.IsDelete) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Tài khoản của bạn đã bị vô hiệu!" };
            if (!user.IsVerify) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Tài khoản của bạn chưa được xác thực!" };
            if (user.RoleId == AppConst.ROLE_ADMIN) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Có gì đó không ổn!" };

            string code = Guid.NewGuid().ToString("N").Substring(0, 10);
            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(code);
            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(passwordBytes);
            user.Update = DateTime.Now;

            _userRepository.UpdateUser(user);
            if (_userRepository.IsSaveChanges())
            {
                string body = $"Mật khẩu mới của bạn là : {code}";

                while (true)
                {
                    bool res = _mailService.SendEmail(email, "Bổng Oliver hair salon cấp lại mật khẩu", body);

                    if (res) break;
                }

                return new ResponseDTO() { Message = "Yêu cầu cấp lại mật khẩu thành công!" };
            }
            else
                return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Yêu cầu cấp lại mật khẩu thất bại!" };
        }

        public ResponseDTO Login(LoginUserDTO loginUserDTO)
        {
            var currentUser = _userRepository.GetUserByUsername(loginUserDTO.Username);
            if (currentUser == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Username không tồn tại!" };
            if (currentUser.IsDelete) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Tài khoản của bạn đã bị vô hiệu!" };

            using var hmac = new HMACSHA512(currentUser.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUserDTO.Password));

            for (int i = 0; i < currentUser.PasswordHash.Length; i++)
            {
                if (currentUser.PasswordHash[i] != passwordBytes[i]) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Mật khẩu không chính xác!" };
            }

            return new ResponseDTO() { Code = AppConst.SUCCESS_CODE, Message = "Đăng nhâp thành công!", Data = _tokenService.CreateToken(currentUser) };
        }

        public ResponseDTO Register(RegisterUserDTO registerUserDTO)
        {
            var currentUser = _userRepository.GetUserByUsername(registerUserDTO.Username);
            if (currentUser != null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Username đã tồn tại!" };

            currentUser = _userRepository.GetUserByEmail(registerUserDTO.Email);
            if (currentUser != null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Email đã tồn tại!" };

            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(registerUserDTO.Password);

            var newUser = new User()
            {
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                Email = registerUserDTO.Email,
                Phone = registerUserDTO.Phone,
                DateOfBirth = registerUserDTO.DateOfBirth,
                Username = registerUserDTO.Username,
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(passwordBytes),
                RoleId = AppConst.ROLE_USER
            };

            _userRepository.CreateUser(newUser);
            if (_userRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Đăng ký thành công!" };
            else return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Đăng ký thất bại!" };
        }
    }
}
