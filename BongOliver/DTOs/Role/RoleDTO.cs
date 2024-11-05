namespace BongOliver.DTOs.Role
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public RoleDTO() { }
        public RoleDTO(BongOliver.Models.Role role) 
        {
            this.Id = role.Id;
            this.Name = role.Name;
        }
    }
}
