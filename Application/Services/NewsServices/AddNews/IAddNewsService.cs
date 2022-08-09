using Application.Interfaces;
using Domain.Entites;
using Microsoft.AspNetCore.Http;
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

        public AddNewsService(IDatabaseContext db )
        {
            this.db = db;
        }


        public async Task<bool> ExecuteAsync(AddNewsDto news)
        {
            var newNews = new News(news.Slug, news.UserId,
                news.MetaDescription, news.Title, news.CategoryId);

            foreach (var item in news.NewsBodies)
            {
                var newbody = new NewsBody(newNews.Id, item.TitleParagraph, item.BodyParagraph);
                newNews.AddNewsBody(newbody);
            }

            foreach (var item in news.Tags)
            {
                var tag = new Tags(item.Name);
                newNews.AddTag(tag);
            }

            foreach (var item in news.Images)
            {
                var image = new Image(newNews.Id, item.Src);
                newNews.AddImages(image);
            }

            await db.News.AddAsync(newNews);
            var result = await db.SaveChangesAsync();
            if (result > 0) return true;
            return false;
        }
    }

    public class AddNewsDto
    {
        public string Slug { get;  set; } = null!;
        public string UserId { get;  set; } = null!;
        public string Title { get;  set; } = null!;
        public string MetaDescription { get; set; } = null!;
        public int CategoryId { get;  set; }

        public ICollection<NewsBodyDto> NewsBodies { get; set; } = null!;

        public ICollection<TagsDto> Tags { get; set; } = null!;
        public ICollection<ImageDto> Images { get; set; } = null!;

    }

    public class NewsBodyDto
    {
        public string? TitleParagraph { get;  set; }
        public string? BodyParagraph { get;  set; }
    }

    public class ImageDto
    {
        public string Src { get; set; }
    }

    public class TagsDto
    {
        public string Name { get;  set; } = null!;
    }
}
