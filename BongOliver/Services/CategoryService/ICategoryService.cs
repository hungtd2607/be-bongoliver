using BongOliver.DTOs.Response;

namespace BongOliver.Services.CategoryService
{
    public interface ICategoryService
    {
        ResponseDTO GetCategories(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        ResponseDTO GetCategoryById(int id);
        ResponseDTO UpdateCategory(int id, string nameCategory);
        ResponseDTO DeleteCategory(int id);
        ResponseDTO CreateCategory(string nameCategory);
    }
}
