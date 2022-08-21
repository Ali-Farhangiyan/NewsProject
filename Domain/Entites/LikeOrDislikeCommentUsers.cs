using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class LikeOrDislikeCommentUsers
    {
        public int Id { get; private set; }

        public string Email { get; private set; } = null!;

        public Comment Comment { get; private set; } = null!;

        public int CommentId { get; private set; }

        public LikeOrDislikeCommentUsers()
        {
            // ef
        }

        public LikeOrDislikeCommentUsers(string email, int commentId)
        {
            Email = email;
            CommentId = commentId;
        }


    }
}
