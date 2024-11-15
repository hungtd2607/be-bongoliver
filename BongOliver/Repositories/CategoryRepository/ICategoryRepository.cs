using BongOliver.Models;

namespace BongOliver.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories(ref int total, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        Category GetCategoryById(int id);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
        void CreateCategory(Category createCategory);
        int GetTotal();
        bool IsSaveChanges();
    }
}
