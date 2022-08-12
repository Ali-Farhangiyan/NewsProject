using Application.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CommentServices.AddComment
{
    public interface IAddCommentService
    {
        Task<bool> ExecuteAsync(AddCommentDto comment);
    }

    public class AddCommentServie : IAddCommentService
    {
        private readonly IDatabaseContext db;
        private readonly IIdentityDatabaseContext identityDb;

        public AddCommentServie(IDatabaseContext db, IIdentityDatabaseContext identityDb)
        {
            this.db = db;
            this.identityDb = identityDb;
        }

        public async Task<bool> ExecuteAsync(AddCommentDto comment)
        {
            var user = await identityDb.Users.FirstOrDefaultAsync(u => u.Id == comment.UserId);

            var news = await db.News.FindAsync(comment.NewsId);

            if (user is null || news is null) return false;

            var newComment = new Comment(comment.Body, user.Email, comment.NewsId, user.FullName);

            await db.Comments.AddAsync(newComment);
            var result = await db.SaveChangesAsync();
            if (result > 0) return true;
            return false;


        }


    }

    public class AddCommentDto
    {
        public int NewsId { get; set; }

        public string UserId { get; set; } = null!;

        public string Body { get; set; } = null!;
    }
}
