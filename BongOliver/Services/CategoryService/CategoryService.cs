using BongOliver.Constants;
using BongOliver.DTOs.Category;
using BongOliver.DTOs.Response;
using BongOliver.DTOs.Role;
using BongOliver.Models;
using BongOliver.Repositories.CategoryRepository;
using BongOliver.Repositories.RoleRepository;
using System.Reflection.Metadata;

namespace BongOliver.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ResponseDTO CreateCategory(string nameCategory)
        {
            var category = new Category { Name = nameCategory };
            _categoryRepository.CreateCategory(category);

            if (_categoryRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Tạo thành công" };
            else return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Tạo thất bại" };
        }

        public ResponseDTO DeleteCategory(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Category không tồn tại" };

            category.IsDelete = true;
            category.Update = DateTime.Now;

            _categoryRepository.UpdateCategory(category);
            if (_categoryRepository.IsSaveChanges())
                return new ResponseDTO() { Message = "Vô hiệu hóa thành công" };
            else
                return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Vô hiệu hóa thất bại" };
        }

        public ResponseDTO GetCategories(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var total = _categoryRepository.GetTotal();

            if (total == 0) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Không có Category nào" };

            var categories = _categoryRepository.GetCategories(ref total, page, pageSize, key, sortBy);

            return new ResponseDTO()
            {
                Data = categories.Select(c => new CategoryDTO(c)).ToList(),
                Total = total
            };
        }

        public ResponseDTO GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            if (category == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Category không tồn tại" };
            else
                return new ResponseDTO() { Data = new CategoryDTO(category) };
        }

        public ResponseDTO UpdateCategory(int id, string nameCategory)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null) return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Category không tồn tại" };

            category.Name = nameCategory;
            category.Update = DateTime.Now;

            _categoryRepository.UpdateCategory(category);
            if (_categoryRepository.IsSaveChanges())
                return new ResponseDTO() { Message = "Cập nhật thành công" };
            else
                return new ResponseDTO() { Code = AppConst.FAILED_CODE, Message = "Cập nhật thất bại" };
        }
    }
}
