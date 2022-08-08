using Application.Interfaces;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CategoryServices.AddCategory
{
    public interface IAddCategoryService
    {
        Task<bool> ExecuteAsync(AddCategoryDto category);
    }

    public class AddCategoryService : IAddCategoryService
    {
        private readonly IDatabaseContext db;

        public AddCategoryService(IDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<bool> ExecuteAsync(AddCategoryDto category)
        {
            var newCategory = new Category(category.Name, category.Slug, category.ParentCategoryId);

            await db.Categories.AddAsync(newCategory);
            var result = await db.SaveChangesAsync();
            if (result > 0) return true;
            return false;
        }
    }

    public class AddCategoryDto
    {
        public string Slug { get; set; } = null!;

        public string Name { get; set; } = null!;
        public int? ParentCategoryId { get; set; }

        
    }
}
