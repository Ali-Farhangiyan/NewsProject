using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NewsServices.GetInfoUsers
{
    public interface IGetInfoUsersService
    {
        Task<GetUserInfoDto> ExecuteAsync(string userId);
    }

    public class GetInfoUsersService : IGetInfoUsersService
    {
        private readonly IIdentityDatabaseContext identityDb;

        public GetInfoUsersService(IIdentityDatabaseContext identityDb)
        {
            this.identityDb = identityDb;
        }

        public async Task<GetUserInfoDto> ExecuteAsync(string userId)
        {
            var user = await identityDb.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null) return null!;
            
            var model = new GetUserInfoDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email
            };

            return model;
        }
    }

    public class GetUserInfoDto
    {
        public string UserId { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
