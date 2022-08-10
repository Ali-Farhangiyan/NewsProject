using Application.Interfaces;
using Application.Pagination;
using Application.Services.NewsServices.AddNews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NewsServices.ShowDetailsNews
{
    public interface IShowDetailsNewsService
    {
        Task<ShowDetailNewsDto> ExecuteAsync(int id);
    }

    public class ShowDetailsNewsService : IShowDetailsNewsService
    {
        private readonly IDatabaseContext db;

        public ShowDetailsNewsService(IDatabaseContext db)
        {
            this.db = db;
        }


        public async Task<ShowDetailNewsDto> ExecuteAsync(int id)
        {
            var news = await db.News
                .Include(n => n.Category)
                .ThenInclude(c => c.ParentCategory)
                .Include(n => n.Images)
                .Include(n => n.NewsBodies)
                .Include(n => n.Comments)
                .Include(n => n.Tags)
                .Where(n => n.Id == id)
                .Select(n => new ShowDetailNewsDto
                {
                    Id = n.Id,
                    ImageTitle = n.ImageTitle,
                    Title = n.Title,
                    MetaDescription = n.MetaDescription,
                    Images = n.Images.Where(i => i.NewsId == n.Id).Select(n => n.Src).ToList(),
                    NewsBodies = n.NewsBodies.Where(nb => nb.NewsId == n.Id).Select(nb => new NewsBodyDto
                    {
                        TitleParagraph = nb.TitleParagraph,
                        BodyParagraph = nb.BodyParagraph
                    }).ToList(),
                    Tags = n.Tags.Select(t => t.Name).ToList()
                }).FirstOrDefaultAsync();

            return news ?? null!;

        }
    }

    public class ShowDetailNewsDto
    {
        public int Id { get; set; }

        public string ImageTitle { get; set; } = null!;

        public string MetaDescription { get; set; } = null!;

        public string Title { get; set; } = null!;

        public List<string> Images { get; set; } = null!;

        public List<NewsBodyDto> NewsBodies { get; set; } = null!;

        public List<string> Tags { get; set; } = null!;

    }

   
}
