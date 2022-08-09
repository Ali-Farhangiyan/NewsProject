namespace Domain.Entites
{
    public class NewsBody
    {
        public int Id { get; private set; }

        public News News { get; private set; } = null!;

        public int NewsId { get; private set; }

        public string? TitleParagraph { get; private set; }
        
        public string? BodyParagraph { get; private set; }

        public NewsBody()
        {
            // ef
        }

        public NewsBody(int newsId, string titleParagraph, string bodyParagraph)
        {
            NewsId = newsId;
            TitleParagraph = titleParagraph;
            BodyParagraph = bodyParagraph;
        }
    }
}
