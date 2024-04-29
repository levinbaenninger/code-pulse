using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repository.Implementation
{
    public class CategoryRespository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRespository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Categories.ToListAsync();
        }
        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await _db.Categories.FindAsync(category.Id);

            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.UrlHandle = category.UrlHandle;

                await _db.SaveChangesAsync();
                return category;
            }

            return null;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await _db.Categories.FindAsync(id);

            if (existingCategory != null)
            {
                _db.Categories.Remove(existingCategory);
                await _db.SaveChangesAsync();
                return existingCategory;
            }

            return null;
        }
    }
}
