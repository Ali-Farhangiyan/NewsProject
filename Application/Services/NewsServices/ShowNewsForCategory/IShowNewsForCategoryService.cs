using Application.Interfaces;
using Application.Pagination;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NewsServices.ShowNewsForCategory
{
    public interface IShowNewsForCategoryService
    {
        Task<PaginatedList<ShowNewsForCategoryDto>> ExecuteAsync(RequestShowNewsDto request);
    }

    public class ShowNewsForCategoryService : IShowNewsForCategoryService
    {
        private readonly IDatabaseContext db;

        public ShowNewsForCategoryService(IDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<PaginatedList<ShowNewsForCategoryDto>> ExecuteAsync(RequestShowNewsDto request)
        {
            var query = db.News
                .Include(n => n.Category)
                .ThenInclude(c => c.ParentCategory)
                .Include(n => n.Tags)
                .AsQueryable();

            query = ApplyReqestShowNews(request, query);

            var news = await query
                .Where(n => n.NewsStatus == NewsStatus.Published)
                .Select(n => new ShowNewsForCategoryDto
                {
                    Id = n.Id,
                    Slug = n.Slug,
                    ImageTitle = n.ImageTitle,
                    MetaDescription = n.MetaDescription,
                    Title = n.Title,
                    CategoryName = n.Category.Name
                }).ToListAsync();

            return PaginatedList<ShowNewsForCategoryDto>.Create(news, request.PageSize, request.PageIndex);
        }

        private static IQueryable<News> ApplyReqestShowNews(RequestShowNewsDto request, IQueryable<News> query)
        {
            if(request.CategoryId is not null)
            {
                query = query.Where(n => n.CategoryId == request.CategoryId);
            }

            if (request.Slug is not null)
            {
                query = query.Where(n => n.Category.Slug == request.Slug);
            }

            if (request.SortShowNews == SortShowNews.Newest)
            {
                query = query.OrderBy(n => n.Id);
            }

            if (!string.IsNullOrWhiteSpace(request.TagName))
            {
                query = query
                .Where(n => n.Tags.Any(t => t.Name.Contains(request.TagName)));
            }

            if (request.SortShowNews == SortShowNews.Oldest)
            {
                query = query.OrderByDescending(n => n.Id);
            }

            if (request.SortShowNews == SortShowNews.HotNews)
            {
                query = query.OrderBy(n => n.HotNews);
            }

            if (request.SortShowNews == SortShowNews.MostVisited)
            {
                query = query.OrderBy(n => n.NumberOfVisited);
            }

            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(n => n.Title.Contains(request.SearchKey) && n.MetaDescription.Contains(request.SearchKey));
            }

            return query;
        }
    }

    public class ShowNewsForCategoryDto
    {
        public int Id { get; set; }

        public string Slug { get; set; } = null!;

        public string ImageTitle { get; set; } = null!;

        public string MetaDescription { get; set; } = null!;

        public string Title { get; set; } = null!;

        

        public string CategoryName { get; set; } = null!;
    }
    public class RequestShowNewsDto
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
        public string SearchKey { get; set; } = null!;
        public int? CategoryId { get; set; }
        public string? Slug { get; set; }
        public string TagName { get; set; } = null!;
        public SortShowNews SortShowNews { get; set; } = SortShowNews.Newest;

    }

    public enum SortShowNews
    {
        Newest = 1,
        Oldest = 2,
        HotNews = 3,
        MostVisited = 4,
    }
}
