using Application.Interfaces;
using Application.Pagination;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CategoryServices.GetCategory
{
    public interface IGetCategoryService
    {
        Task<PaginatedList<GetCategoryDto>> ExcuteAsync(RequestCategoryDto request);
    }

    public class GetCategoryService : IGetCategoryService
    {
        private readonly IDatabaseContext db;

        public GetCategoryService(IDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<PaginatedList<GetCategoryDto>> ExcuteAsync(RequestCategoryDto request)
        {
            var query = db.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.SubCategories)
                .Where(c => c.ParentCategoryId == request.ParentCategoryId)
                .AsQueryable();

            query = ApplyRequest(request, query);

            var categories = await query.Select(c => new GetCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                NumberOfSubs = c.SubCategories.Count,
                Slug = c.Slug
            }).ToListAsync();

            return PaginatedList<GetCategoryDto>.Create(categories, request.PageSize, request.PageIndex);
        }

        private static IQueryable<Category> ApplyRequest(RequestCategoryDto request, IQueryable<Category> query)
        {
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(c => c.Name.Contains(request.SearchKey));
            }

            if (request.SortCategory == SortCategory.Newest)
            {
                query = query.OrderBy(c => c.Id);
            }

            if (request.SortCategory == SortCategory.Oldest)
            {
                query = query.OrderByDescending(c => c.Id);
            }

            if (request.SortCategory == SortCategory.MostSubCategories)
            {
                query = query.OrderBy(c => c.SubCategories.Count);
            }

            if (request.SortCategory == SortCategory.LeastSubCategories)
            {
                query = query.OrderByDescending(c => c.SubCategories.Count);
            }

            return query;
        }
    }

    public class GetCategoryDto 
    {
        public int Id { get; set; }

        public string Slug { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int NumberOfSubs { get; set; }


    }

    public class RequestCategoryDto
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;

        public int? ParentCategoryId { get; set; }

        public string? SearchKey { get; set; }

        public SortCategory SortCategory { get; set; } = SortCategory.Newest;
    }

    public enum SortCategory
    {
        Newest = 1,
        Oldest = 2,
        MostSubCategories = 3,
        LeastSubCategories = 4,
    }
}
