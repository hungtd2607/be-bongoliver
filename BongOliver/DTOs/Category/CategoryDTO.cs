namespace BongOliver.DTOs.Category
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }
        public CategoryDTO(Models.Category category)
        {
            Id = category.Id;
            Name = category.Name;
            IsDelete = category.IsDelete;
        }
    }
}
