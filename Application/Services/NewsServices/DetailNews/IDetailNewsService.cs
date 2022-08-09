using Application.Interfaces;
using Application.Services.NewsServices.AddNews;
using Application.Services.NewsServices.ManagementNews;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NewsServices.DetailNews
{
    public interface IDetailNewsService
    {
        Task<DetailNewsDto> ExecuteAsync(int Id);
    }

    public class DetailNewsService : IDetailNewsService
    {
        private readonly IDatabaseContext db;

        public DetailNewsService(IDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<DetailNewsDto> ExecuteAsync(int Id)
        {
            var news = db.News
                .Include(n => n.Images)
                .Include(n => n.NewsBodies)
                .SingleOrDefault(n => n.Id == Id);



            var images = new List<string>();
            foreach (var item in news.Images)
            {
                images.Add(item.Src);
            }

            var newsBodies = new List<NewsBodyDto>();
            foreach (var item in news.NewsBodies)
            {
                newsBodies.Add(new NewsBodyDto
                {
                    BodyParagraph = item.BodyParagraph,
                    TitleParagraph = item.TitleParagraph
                });
            }
            var newsDetail = new DetailNewsDto
            {
                Id = news.Id,
                Images = images,
                NewsBody = newsBodies,
                NewsStatus = news.NewsStatus
            };

            return newsDetail;
        }
    }

    public class DetailNewsDto
    {
        public int Id { get; set; }

        public List<string> Images { get; set; } = null!;

        public List<NewsBodyDto> NewsBody { get; set; } = null!;

        public NewsStatus NewsStatus { get; set; }
    }
}
