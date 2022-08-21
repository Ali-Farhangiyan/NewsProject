using Application.Interfaces;
using Application.Services.HomeServices.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.HomeServices.HomeMainService
{
    public interface IHomeService
    {
        Task<List<HomeNewsDto>> LastNews();
        Task<List<HomeNewsDto>> HotNews();
        Task<List<HomeNewsDto>> MostVisited();
        Task<List<HomeNewsDto>> RandomNews();


    }

    public class HomeService : IHomeService
    {
        private readonly IDatabaseContext db;
        public const int NUMBER_OF_RANDOM_NUMBER = 5;

        public HomeService(IDatabaseContext db)
        {
            this.db = db;
        }
        public async Task<List<HomeNewsDto>> HotNews()
        {
            var news = await db.News
                
                .OrderByDescending(n => n.HotNews)
                .Select(n => new HomeNewsDto
                {
                    Id = n.Id,
                    Slug = n.Slug,
                    Image = n.ImageTitle,
                    MetaDescription = n.MetaDescription,
                    Title = n.Title
                })
                .Take(5)
                .ToListAsync();

            return news;
        }

        public async Task<List<HomeNewsDto>> LastNews()
        {
            var news = await db.News
                .OrderByDescending(n => n.Id)
                .Select(n => new HomeNewsDto
                {
                    Id = n.Id,
                    Slug = n.Slug,
                    Image = n.ImageTitle,
                    MetaDescription = n.MetaDescription,
                    Title = n.Title
                })
                .Take(5)
                .ToListAsync();

            return news;
        }

        public async Task<List<HomeNewsDto>> MostVisited()
        {
            var news = await db.News
                
                .OrderByDescending(n => n.NumberOfVisited)
                .Select(n => new HomeNewsDto
                {
                    Id = n.Id,
                    Slug = n.Slug,
                    Image = n.ImageTitle,
                    MetaDescription = n.MetaDescription,
                    Title = n.Title
                })
                .Take(5)
                .ToListAsync();

            return news;
        }

        public async Task<List<HomeNewsDto>> RandomNews()
        {
            var ids = await db.News.Select(n => n.Id).ToArrayAsync();
            var news = new List<HomeNewsDto>();

            var random = new Random();

            for (int i = 0; i < NUMBER_OF_RANDOM_NUMBER; i++)
            {
                var randId = random.Next(0, ids.Length - 1);
                var newsFindWithRandId = await db.News.FindAsync(ids[randId]);
                if (newsFindWithRandId is null) return null!;
                news.Add(new HomeNewsDto
                {
                    Id = newsFindWithRandId.Id,
                    Slug = newsFindWithRandId.Slug,
                    Image = newsFindWithRandId.ImageTitle,
                    MetaDescription = newsFindWithRandId.MetaDescription,
                    Title = newsFindWithRandId.Title
                });
            }

            return news;



        }
    }
}
