using Application.Interfaces;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NewsServices.AddNews
{
    public interface IAddNewsService
    {
        Task<bool> ExecuteAsync(AddNewsDto news);
    }

    public class AddNewsService : IAddNewsService
    {
        private readonly IDatabaseContext db;

        public AddNewsService(IDatabaseContext db)
        {
            this.db = db;
        }


        public async Task<bool> ExecuteAsync(AddNewsDto news)
        {
            var newNews = new News(news.Slug, news.UserId, news.MetaDescription, news.Title, news.ImageTitle, news.CategoryId);

            foreach (var item in news.NewsBodies)
            {
                var newbody = new NewsBody(item.NewsId, item.TitleParagraph, item.ImageParagraph, item.BodyParagraph);
                newNews.AddNewsBody(newbody);
            }

            foreach (var item in news.Tags)
            {
                var tag = new Tags(item.Name);
                newNews.AddTag(tag);
            }

            await db.News.AddAsync(newNews);
            var result = await db.SaveChangesAsync();
            if (result > 0) return true;
            return false;
        }
    }

    public class AddNewsDto
    {
        public string Slug { get; private set; } = null!;
        public string UserId { get; private set; } = null!;
        public string Title { get; private set; } = null!;
        public string MetaDescription { get; private set; } = null!;
        public string ImageTitle { get; private set; } = null!;
        public int CategoryId { get; private set; }

        public ICollection<NewsBodyDto> NewsBodies { get; set; } = null!;

        public ICollection<TagsDto> Tags { get; set; } = null!;

    }

    public class NewsBodyDto
    {
        public int NewsId { get; private set; }
        public string? TitleParagraph { get; private set; }
        public string? ImageParagraph { get; private set; }
        public string? BodyParagraph { get; private set; }
    }

    public class TagsDto
    {
        public string Name { get; private set; } = null!;
    }
}
