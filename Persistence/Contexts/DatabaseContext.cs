using Application.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> op):base(op)
        {

        }

        public DbSet<News> News { get; set; } = null!;
        public DbSet<NewsBody> NewsBodies { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Tags> Tags { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
    }
}
