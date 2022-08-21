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
        Task<int> LikeExecute(int Id, string Email);

        Task<int> DislikeExecute(int Id, string Email);
    }

    public class LikeOrDislikeCommentService : ILikeOrDislikeCommentService
    {
        private readonly IDatabaseContext db;

        public LikeOrDislikeCommentService(IDatabaseContext db)
        {
            this.db = db;
        }
        public async Task<int> LikeExecute(int Id, string Email)
        {
            var comment = await db.Comments.FindAsync(Id);
            if (comment is null) return 0;
            var isReactionToComment = db.LikeOrDislikeCommentUsers
                .Where(c => c.Email == comment.Email && c.CommentId == comment.Id)
                .Any();

            if (isReactionToComment == false)
            {
                comment.IncreaseLikes();
                var result = await db.SaveChangesAsync();
                if (result > 0) return comment.NumberOfLikes;
            }


            return comment.NumberOfLikes;
        }


        public async Task<int> DislikeExecute(int Id, string Email)
        {   
            var comment = await db.Comments.FindAsync(Id);
            if (comment is null) return 0;

            var isReactionToComment = db.LikeOrDislikeCommentUsers
                .Where(c => c.Email == comment.Email && c.CommentId == comment.Id)
                .Any();

            if(isReactionToComment == false)
            {
                comment.IncreaseDisLikes();
                var result = await db.SaveChangesAsync();
                if (result > 0) return comment.NumberOfDisLikes;
            }

            
            return comment.NumberOfDisLikes;
        }

        
    }
}
