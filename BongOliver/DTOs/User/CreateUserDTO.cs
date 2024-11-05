using System.ComponentModel.DataAnnotations;

namespace BongOliver.DTOs.User
{
    public class CreateUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsVerify { get; set; }
        public bool IsDelete { get; set; }
        public int RoleId { get; set; }
    }
}
