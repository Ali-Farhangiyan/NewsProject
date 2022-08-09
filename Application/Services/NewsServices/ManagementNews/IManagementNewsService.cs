using Application.Interfaces;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NewsServices.ManagementNews
{
    public interface IManagementNewsService
    {
        Task<bool> ExecuteAsync(ManageNewsDto manage);
    }

    public class ManagementNewsService : IManagementNewsService
    {
        private readonly IDatabaseContext db;

        public ManagementNewsService(IDatabaseContext db)
        {
            this.db = db;
        }


        public async Task<bool> ExecuteAsync(ManageNewsDto manage)
        {
            var news = await db.News
                
                .FindAsync(manage.Id);

            news.SetImageTitle(manage.Image);
            if(manage.ManageStatus == ManageStatus.Published)
            {
                news.ChangeNewsStatus(NewsStatus.Published);
            }
            if (manage.ManageStatus == ManageStatus.Waiting)
            {
                news.ChangeNewsStatus(NewsStatus.Waiting);
            }
            if (manage.ManageStatus == ManageStatus.Rejected)
            {
                news.ChangeNewsStatus(NewsStatus.Rejected);
            }
            var result = await db.SaveChangesAsync();
            if(result > 0) return true;
            return false;

        }
    }

    public class ManageNewsDto
    {
        public int Id { get; set; }

        public string Image { get; set; } = null!;

        public ManageStatus ManageStatus { get; set; }

    }

    public enum ManageStatus
    {
        Waiting = 1,
        Published = 2,
        Rejected = 3
    }
}
