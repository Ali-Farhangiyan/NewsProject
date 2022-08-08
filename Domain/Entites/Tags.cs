namespace Domain.Entites
{
    public class Tags
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = null!;

        public ICollection<News> News { get; private set; } = null!;

        public Tags()
        {
            // ef
        }

        public Tags(string name) 
        {
            Name = name;
            
        }
    }
}
