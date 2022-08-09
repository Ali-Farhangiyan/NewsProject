using Application.Interfaces;
using Application.Pagination;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NewsServices.GetNews
{
    public interface IGetNewsService
    {
        Task<PaginatedList<GetNewsDto>> ExecuteAsync(RequestGetNewsDto request);
    }

    public class GetNewsService : IGetNewsService
    {
        private readonly IDatabaseContext db;
        private readonly IIdentityDatabaseContext identityDb;

        public GetNewsService(IDatabaseContext db, IIdentityDatabaseContext identityDb)
        {
            this.db = db;
            this.identityDb = identityDb;
        }


        public async Task<PaginatedList<GetNewsDto>> ExecuteAsync(RequestGetNewsDto request)
        {
            var query = db.News
                .Include(n => n.Category)
                .AsQueryable();

            var author = await identityDb.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            

            query = ApplyRequestNews(request, query);

            var news = await query.Select(n => new GetNewsDto
            {
                Id = n.Id,
                Title = n.Title,
                MetaDescription = n.MetaDescription,
                CategoryName = n.Category.Name,
                Author = author.Email,
                Status = n.NewsStatus.ToString()
            }).ToListAsync();

            return PaginatedList<GetNewsDto>.Create(news, request.PageSize, request.PageIndex);
        }

        private static IQueryable<News> ApplyRequestNews(RequestGetNewsDto request, IQueryable<News> query)
        {
            if (request.SortNews == SortNews.All)
            {
                query = query.OrderBy(n => n.Id);
            }

            if (request.SortNews == SortNews.Newest)
            {
                query = query.OrderBy(n => n.Id);
            }

            if (request.SortNews == SortNews.Oldest)
            {
                query = query.OrderByDescending(n => n.Id);
            }

            if (request.SortNews == SortNews.Published)
            {
                query = query.Where(n => n.NewsStatus == Domain.Entites.NewsStatus.Published)
                    .OrderBy(n => n.Id);
            }

            if (request.SortNews == SortNews.Rejected)
            {
                query = query.Where(n => n.NewsStatus == Domain.Entites.NewsStatus.Rejected)
                    .OrderBy(n => n.Id);
            }

            if (request.SortNews == SortNews.Waiting)
            {
                query = query.Where(n => n.NewsStatus == Domain.Entites.NewsStatus.Waiting)
                    .OrderBy(n => n.Id);
            }

            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(n => n.Title.Contains(request.SearchKey)
                    || n.Category.Name.Contains(request.SearchKey));
            }

            return query;
        }
    }

    public class GetNewsDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string MetaDescription { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string Status { get; set; } = null!;
    }

    public class RequestGetNewsDto
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;

        public string UserId { get; set; } = null!;

        public string? SearchKey { get; set; }

        public SortNews SortNews { get; set; } = SortNews.All;
    }

    public enum SortNews
    {
        Newest = 1,
        Oldest = 2,
        Published = 3,
        Rejected = 4,
        Waiting = 5,
        All = 6
    }
}
