namespace Domain.Entites
{
    public class Category
    {
        public int Id { get; private set; }

        public string Slug { get; private set; } = null!;

        public string Name { get; private set; } = null!;

        public Category? ParentCategory { get; private set; }
        public int? ParentCategoryId { get; private set; }

        public ICollection<Category> SubCategories { get; private set; } = null!;

        public ICollection<News> News { get; private set; } = null!;

        public Category()
        {
            // ef
        }

        public Category(string name, string slug, int? parentCategoryId)
        {
            Name = name;
            Slug = slug;
            ParentCategoryId = parentCategoryId;
        }

        public void AddSubCategories(Category category)
        {
            SubCategories.Add(category);
        }


    }
}
