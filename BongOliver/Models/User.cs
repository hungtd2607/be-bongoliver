using BongOliver.Constants;
using BongOliver.DTOs.User;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace BongOliver.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(256)]
        public string FirstName { get; set; }
        [MaxLength(256)]
        public string LastName { get; set; }
        [MaxLength(256)]
        public string Email { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        [MaxLength(256)]
        public string Avatar { get; set; } = AppConst.DEFAULT_AVATAR;
        public bool Gender { get; set; } = AppConst.MALE;
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Required]
        [StringLength(32)]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsVerify { get; set; } = AppConst.NOT_VERIFY;
        public bool IsDelete { get; set; } = AppConst.NOT_DELETE;
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; } = DateTime.Now;
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public User() { }
    }
}
