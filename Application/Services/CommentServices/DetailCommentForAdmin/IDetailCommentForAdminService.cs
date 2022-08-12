using Application.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CommentServices.DetailCommentForAdmin
{
    public interface IDetailCommentForAdminService
    {
        public Task<DetailCommentDto> ExecuteAsync(int Id);
    }

    public class DetailCommentForAdminService : IDetailCommentForAdminService
    {
        private readonly IDatabaseContext db;

        public DetailCommentForAdminService(IDatabaseContext db)
        {
            this.db = db;
        }
        public async Task<DetailCommentDto> ExecuteAsync(int Id)
        {
            var comment = await db.Comments
                .FirstOrDefaultAsync(c => c.Id == Id);

            if (comment is null) return null!;
                
                var model =  new DetailCommentDto
                {
                     Id = comment.Id,
                     Body = comment.Body,
                     Email = comment.Email,
                     StatusComment = comment.StatusComment
                 };

            return model;
        }
    }

    public class DetailCommentDto
    {
        public int Id { get; set; }
        public string Body { get; set; } = null!;

        public string Email { get; set; } = null!;
        public StatusComment StatusComment { get; set; }
    }
}
