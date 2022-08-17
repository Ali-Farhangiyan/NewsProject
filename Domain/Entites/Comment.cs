namespace Domain.Entites
{
    public class Comment
    {
        public int Id { get; private set; }

        public string Body { get; private set; } = null!;

        public string Email { get; private set; } = null!;

        public string FullName { get; private set; } = null!;

        public Comment? ParentComment { get; private set; }
        public int? ParentCommentId { get; private set; }

        public ICollection<Comment>? Replies { get; private set; }

        public DateTime DateOfRegisteryComment { get; private set; } = DateTime.Now;

        public StatusComment StatusComment { get; private set; } = StatusComment.Waiting;

        public int NumberOfLikes { get; private set; }
        public int NumberOfDisLikes { get; private set; }

        public News News { get; private set; } = null!;

        public int NewsId { get; private set; }

        public void ChangeStatusComment(StatusComment statusComment)
        {
            StatusComment = statusComment;
        }

        public void IncreaseLikes()
        {
            NumberOfLikes += 1;
        }

        public void IncreaseDisLikes()
        {
            NumberOfDisLikes += 1;
        }

        public Comment()
        {
            // ef
        }

        public Comment(int? parentCommentId,string body, string email, int newsId, string fullName)
        {
            ParentCommentId = parentCommentId;
            FullName = fullName;
            Body = body;
            Email = email;
            NewsId = newsId;
        }

    }
}
