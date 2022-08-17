using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CommentServices.LikeOrDislikeComment
{
    public interface ILikeOrDislikeCommentService
    {
        Task<int> LikeExecute(int Id);

        Task<int> DislikeExecute(int Id);
    }

    public class LikeOrDislikeCommentService : ILikeOrDislikeCommentService
    {
        private readonly IDatabaseContext db;

        public LikeOrDislikeCommentService(IDatabaseContext db)
        {
            this.db = db;
        }
        public async Task<int> DislikeExecute(int Id)
        {
            var comment = await db.Comments.FindAsync(Id);
            if (comment is null) return 0;
            comment.IncreaseDisLikes();

            var result = await db.SaveChangesAsync();
            if (result > 0) return comment.NumberOfDisLikes;
            return 0;
        }

        public async Task<int> LikeExecute(int Id)
        {
            var comment = await db.Comments.FindAsync(Id);
            if (comment is null) return 0;
            comment.IncreaseLikes();

            var result = await db.SaveChangesAsync();
            if (result > 0) return comment.NumberOfLikes;
            return 0;
        }
    }
}
