using BongOliver.DTOs.Role;
using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; } = DateTime.Now;
        public ICollection<User> Users { get; set; }


        public Role() { }
        public Role(RoleDTO roleDTO)
        {
            this.Id = roleDTO.Id;
            this.Name = roleDTO.Name;
        }
    }
}
