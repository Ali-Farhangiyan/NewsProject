namespace Domain.Entites
{
    public class Image
    {
        public int Id { get;private set; }

        public string Src { get;private set; } = null!;

        public News News { get;private set; } = null!;

        public int NewsId { get;private set; }

        public Image()
        {
            //ef
        }

        public Image(int newsId, string src)
        {
            NewsId = newsId;
            Src = src;
        }
    }
}
