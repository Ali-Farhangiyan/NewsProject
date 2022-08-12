using Application.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CommentServices.ChangeStatusComment
{
    public interface IChangeStatusCommentService
    {
        Task<bool> ExecuteAsync(RequestChangeStatusDto request);
    }

    public class ChangeStatusCommentService : IChangeStatusCommentService
    {
        private readonly IDatabaseContext db;

        public ChangeStatusCommentService(IDatabaseContext db)
        {
            this.db = db;
        }
        public async Task<bool> ExecuteAsync(RequestChangeStatusDto request)
        {
            var comment = await db.Comments.FirstOrDefaultAsync(c => c.Id == request.Id);

            comment.ChangeStatusComment(request.StatusComment);
            var result = await db.SaveChangesAsync();
            if (result > 0) return true;
            return false;
        }
    }

    public class RequestChangeStatusDto
    {
        public int Id { get; set; }
        

        public StatusComment StatusComment { get; set; }
    }
}
