using BongOliver.Services.CategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategorys(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var res = _categoryService.GetCategories(page, pageSize, key, sortBy);
            return StatusCode(res.Code, res);
        }
        [HttpPost]
        public IActionResult CreateCategory(string name)
        {
            var res = _categoryService.CreateCategory(name);
            return StatusCode(res.Code, res);
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var res = _categoryService.GetCategoryById(id);
            return StatusCode(res.Code, res);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, string name)
        {
            var res = _categoryService.UpdateCategory(id, name);
            return StatusCode(res.Code, res);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var res = _categoryService.DeleteCategory(id);
            return StatusCode(res.Code, res);
        }
    }
}
