using BongOliver.Constants;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.Role;
using BongOliver.DTOs.User;
using BongOliver.Models;
using BongOliver.Repositories.RoleRepository;
using BongOliver.Repositories.UserRepository;
using System.Security.Cryptography;
using System.Text;

namespace BongOliver.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public ResponseDTO CreateUser(CreateUserDTO createUserDTO)
        {
            var user = _userRepository.GetUserByUsername(createUserDTO.Username);
            if (user != null) return new ResponseDTO { Code = Constant.FAILED_CODE, Message = "User đã tồn tại!" };

            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(createUserDTO.Password);

            var newUser = new User()
            {
                FirstName = createUserDTO.FirstName,
                LastName = createUserDTO.LastName,
                Email = createUserDTO.Email,
                Phone = createUserDTO.Phone,
                Avatar = createUserDTO.Avatar,
                Gender = createUserDTO.Gender,
                DateOfBirth = createUserDTO.DateOfBirth,
                Username = createUserDTO.Username,
                IsDelete = createUserDTO.IsDelete,
                IsVerify = createUserDTO.IsVerify,
                RoleId = createUserDTO.RoleId,
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(passwordBytes),
            };

            _userRepository.CreateUser(newUser);
            if (_userRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Tạo thành công!" };
            else return new ResponseDTO() { Code = Constant.FAILED_CODE, Message = "Tạo thất bại!" };
        }

        public ResponseDTO DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return new ResponseDTO { Code = Constant.FAILED_CODE, Message = "User không tồn tại!" };
            if (user.IsDelete) return new ResponseDTO { Code = Constant.FAILED_CODE, Message = "User đã bị vô hiệu hóa!" };

            user.IsDelete = false;

            _userRepository.UpdateUser(user);
            if (_userRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Vô hiệu hóa thành công!" };
            else return new ResponseDTO() { Code = Constant.FAILED_CODE, Message = "Vô hiệu hóa thất bại!" };
        }

        public ResponseDTO GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user == null) return new ResponseDTO { Code = Constant.FAILED_CODE, Message = "User không tồn tại!" };

            return new ResponseDTO { Data = new UserDTO(user) };
        }

        public ResponseDTO GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var total = _userRepository.GetTotal();

            if (total == 0) return new ResponseDTO() { Code = Constant.FAILED_CODE, Message = "Không có User nào" };

            var users = _userRepository.GetUsers(ref total, page, pageSize, key, sortBy);

            return new ResponseDTO()
            {
                Data = users.Select(u => new UserDTO(u)).ToList(),
                Total = total
            };
        }

        public ResponseDTO UpdateUser(int id, UpdateUserDTO updateUserDTO)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return new ResponseDTO() { Code = Constant.FAILED_CODE, Message = "User không tồn tại" };

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
                return new ResponseDTO() { Code = Constant.FAILED_CODE, Message = "Cập nhật thất bại" };
        }
    }
}
