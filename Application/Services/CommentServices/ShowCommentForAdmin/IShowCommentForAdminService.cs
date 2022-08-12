using Application.Interfaces;
using Application.Pagination;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CommentServices.ShowCommentForAdmin
{
    public interface IShowCommentForAdminService
    {
        Task<PaginatedList<ShowCommentDto>> ExecuteAsync(RequestShowCommentDto request);
    }

    public class ShowCommentForAdminService : IShowCommentForAdminService
    {
        private readonly IDatabaseContext db;

        public ShowCommentForAdminService(IDatabaseContext db)
        {
            this.db = db;
        }


        public async Task<PaginatedList<ShowCommentDto>> ExecuteAsync(RequestShowCommentDto request)
        {
            var query = db.Comments
                .Include(c => c.News)
                .AsQueryable();

            query = ApplyRequestMethod(request, query);

            var comments = await query
                .Select(c => new ShowCommentDto
                {
                    Id = c.Id,
                    Email = c.Email,
                    NewsContent = c.News.Slug,
                    Status = c.StatusComment.ToString()

                }).ToListAsync();

            return PaginatedList<ShowCommentDto>.Create(comments, request.PageSize, request.PageIndex);


        }

        private static IQueryable<Comment> ApplyRequestMethod(RequestShowCommentDto request, IQueryable<Comment> query)
        {
            if (request.CommentRequest == CommentRequest.Accepted)
            {
                query = query.Where(c => c.StatusComment == StatusComment.Accepted);
            }
            if (request.CommentRequest == CommentRequest.Waiting)
            {
                query = query.Where(c => c.StatusComment == StatusComment.Waiting);
            }
            if (request.CommentRequest == CommentRequest.Rejected)
            {
                query = query.Where(c => c.StatusComment == StatusComment.Rejected);
            }
            if (request.CommentRequest == CommentRequest.Newest)
            {
                query = query.OrderBy(c => c.Id);
            }
            if (request.CommentRequest == CommentRequest.Oldest)
            {
                query = query.OrderByDescending(c => c.Id);
            }

            return query;
        }
    }

    public class RequestShowCommentDto
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
        public CommentRequest CommentRequest { get; set; } = CommentRequest.Newest;

    }

    public class ShowCommentDto
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string NewsContent { get; set; } = null!;

        

    }

    public enum CommentRequest
    {
        Waiting = 1,
        Accepted = 2,
        Rejected = 3,
        Newest = 4,
        Oldest = 5
    }
}
