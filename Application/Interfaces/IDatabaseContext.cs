using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<News> News { get; set; }
        DbSet<NewsBody> NewsBodies { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Tags> Tags { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<LikeOrDislikeCommentUsers> LikeOrDislikeCommentUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}
