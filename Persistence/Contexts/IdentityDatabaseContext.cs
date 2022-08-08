using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class IdentityDatabaseContext : IdentityDbContext<User, IdentityRole, string>
    {
        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> op) : base(op)
        {

        }

        
    }
}
