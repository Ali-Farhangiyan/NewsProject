using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NewsServices.GetCategories
{
    public interface IGetCategoriesService
    {
        Task<List<GetCategoryDto>> ExecuteAsync();
    }

    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IDatabaseContext db;

        public GetCategoriesService(IDatabaseContext db)
        {
            this.db = db;
        }
        public async Task<List<GetCategoryDto>> ExecuteAsync()
        {
            var categories = await db.Categories
                .Where(c => c.SubCategories.Count == 0)
                .Include(c => c.ParentCategory)
                .Include(c => c.SubCategories)
                .Select(c => new GetCategoryDto
            {
                Id = c.Id,
                Name = c.ParentCategory.Name + " -> " + c.Name
            }).ToListAsync();

            return categories;
        }
    }

    public class GetCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
