using BongOliver.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BongOliver.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateCategory(Category createCategory)
        {
            _context.Categories.Add(createCategory);
        }

        public void DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategories(ref int total, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(au => au.Name.ToLower().Contains(key.ToLower()));
            }

            query = query.OrderBy(u => u.Id);

            total = query.Count();

            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public int GetTotal()
        {
            return _context.Categories.Count();
        }

        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
        }
    }
}
