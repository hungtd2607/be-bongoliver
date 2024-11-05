using BongOliver.DTOs.Role;
using System.ComponentModel.DataAnnotations;

namespace BongOliver.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsVerify { get; set; }
        public bool IsDelete { get; set; }
        public RoleDTO Role { get; set; }
        public UserDTO(Models.User user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Phone = user.Phone;
            this.Avatar = user.Avatar;
            this.Gender = user.Gender;
            this.DateOfBirth = user.DateOfBirth;
            this.IsVerify = user.IsVerify;
            this.IsDelete = user.IsDelete;
            this.Role = new RoleDTO(user.Role);
        }
    }
}
