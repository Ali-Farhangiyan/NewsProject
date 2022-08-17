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
        Task<ShowDetailNewsDto> ExecuteAsync( string slug);
    }

    public class ShowDetailsNewsService : IShowDetailsNewsService
    {
        private readonly IDatabaseContext db;

        public ShowDetailsNewsService(IDatabaseContext db)
        {
            this.db = db;
        }


        public async Task<ShowDetailNewsDto> ExecuteAsync(string slug)
        {
            var news = await db.News
                .Include(n => n.Category)
                .ThenInclude(c => c.ParentCategory)
                .Include(n => n.Images)
                .Include(n => n.NewsBodies)
                .Include(n => n.Comments)
                .ThenInclude(c => c.ParentComment)
                .ThenInclude(c => c.ParentComment)
                .Include(n => n.Tags)
                .Where(n => n.Slug == slug)
                .Select(n => new ShowDetailNewsDto
                {
                    Id = n.Id,
                    ImageTitle = n.ImageTitle,
                    Title = n.Title,
                    MetaDescription = n.MetaDescription,
                    Images = n.Images.Where(i => i.NewsId == n.Id).Select(n => n.Src).ToList(),
                    Slug = n.Slug,
                    NewsBodies = n.NewsBodies.Where(nb => nb.NewsId == n.Id).Select(nb => new NewsBodyDto
                    {
                        TitleParagraph = nb.TitleParagraph,
                        BodyParagraph = nb.BodyParagraph
                    }).ToList(),
                    Tags = n.Tags.Select(t => t.Name).ToList(),
                    Comments = n.Comments.Where(co => co.StatusComment == Domain.Entites.StatusComment.Accepted)
                    .Select(c => new DetailCommentDto
                    {
                        Id = c.Id,
                        FullName = c.FullName,
                        Body = c.Body,
                        DateOfRegisterTime = c.DateOfRegisteryComment,
                        NumberOfDislike = c.NumberOfDisLikes,
                        NumberOfLike = c.NumberOfLikes,
                        Replies = c.Replies.Select( r => new DetailCommentDto
                        {
                            Id = r.Id,
                            FullName = r.FullName,
                            Body = r.Body,
                            DateOfRegisterTime = r.DateOfRegisteryComment,
                            NumberOfDislike = r.NumberOfDisLikes,
                            NumberOfLike = r.NumberOfLikes,
                        }).ToList()
                    }).ToList()
                    
                }).FirstOrDefaultAsync();

            return news ?? null!;

        }
    }

    public class ShowDetailNewsDto
    {
        public int Id { get; set; }

        public string? UserFullName { get; set; }

        public string ImageTitle { get; set; } = null!;

        public string? Slug { get; set; }

        public string MetaDescription { get; set; } = null!;

        public string Title { get; set; } = null!;

        public List<string> Images { get; set; } = null!;

        public List<NewsBodyDto> NewsBodies { get; set; } = null!;

        public List<string> Tags { get; set; } = null!;
        public List<DetailCommentDto> Comments { get; set; } = null!;

    }

    public class DetailCommentDto
    {
        public int Id { get; set; }

        public ICollection<DetailCommentDto> Replies { get; set; }
        public string FullName { get; set; } = null!;

        public DateTime DateOfRegisterTime { get; set; }

        public int NumberOfLike { get; set; }
        public int NumberOfDislike { get; set; }

        public string Body { get; set; } = null!;
    }

   
}
