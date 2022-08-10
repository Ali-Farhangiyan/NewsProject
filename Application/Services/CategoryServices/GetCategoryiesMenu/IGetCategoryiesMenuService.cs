using Application.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CategoryServices.GetCategoryiesMenu
{
    public interface IGetCategoryiesMenuService
    {
        Task<List<Category>> ExecuteAsync();
    }

    public class GetCategoryiesMenuService : IGetCategoryiesMenuService
    {
        private readonly IDatabaseContext db;

        public GetCategoryiesMenuService(IDatabaseContext db)
        {
            this.db = db;
        }
        public async Task<List<Category>> ExecuteAsync()
        {
            var categories = await db.Categories
                .Include(c => c.SubCategories)
                .Include(c => c.ParentCategory)
                .ToListAsync();
                //.Select(c => new GetCategoryMenuDto
                //{
                //    Id = c.Id,
                //    Name = c.Name,
                //    Slug = c.Slug,
                //    SubCategories = c.SubCategories.Select(s => new GetCategoryMenuDto
                //    {
                //        Id = s.Id,
                //        Name = s.
                //    })
                //}).ToListAsync();

            return categories;
        }
    }

    public class GetCategoryMenuDto
    {
        public int Id { get; set; }

        public string Slug { get; set; } = null!;

        public string Name { get; set; } = null!;

        public List<GetCategoryMenuDto> SubCategories { get; set; } = null!;

        public GetCategoryMenuDto ParentCategory { get; set; } = null!;
    }
}
