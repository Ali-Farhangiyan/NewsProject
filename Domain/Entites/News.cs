using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class News
    {
        public int Id { get; private set; }
        public string Slug { get; private set; } = null!;
        public string UserId { get; private set; } = null!;
        public string Title { get; private set; } = null!;
        public string MetaDescription { get; private set; } = null!;
        public int NumberOfVisited { get; private set; }
        public string ImageTitle { get; private set; }
        public NewsStatus NewsStatus { get; private set; } = NewsStatus.Waiting;
        public int HotNews { get; private set; }
        public Category Category { get; private set; } = null!;
        public int CategoryId { get; private set; }
        public ICollection<NewsBody> NewsBodies { get; private set; } = new List<NewsBody>();
        public ICollection<Tags> Tags { get; private set; } = new List<Tags>();
        public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
        public ICollection<Image> Images { get; private set; } = new List<Image>();
        public void IncreaseVisited()
        {
            NumberOfVisited += 1;
        }

        public void IncreaseHotNews()
        {
            HotNews += 1;
        }

        public void SetImageTitle(string Src)
        {
            ImageTitle = Src;
        }
        public void AddNewsBody(NewsBody newsBody)
        {
            NewsBodies.Add(newsBody);
        }

        public void AddTag(Tags tag)
        {
            Tags.Add(tag);
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public void AddImages(Image image)
        {
            Images.Add(image);
        }

        public void ChangeNewsStatus(NewsStatus newsStatus)
        {
            NewsStatus = newsStatus;
        }

        public News()
        {
            // ef
        }

        public News(string slug, string userId, string metaDescription, string title,int categoryId)
        {
            Slug = slug;
            UserId = userId;
            MetaDescription = metaDescription;
            Title = title;
            CategoryId = categoryId;
        }


    }


    public enum NewsStatus
    {
        Waiting = 1,
        Published = 2,
        Rejected =3
    }
}
