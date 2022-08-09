using Admin.Utilities;
using Application.Pagination;
using Application.Services.NewsServices.GetNews;
using Application.Services.NewsServices.NewsMainService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Admin.Pages.News
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly INewsService newsService;

        public IndexModel(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public PaginatedList<GetNewsDto> NewsList { get; set; }

        [BindProperty]
        public RequestGetNewsDto Request { get; set; }
        public async Task OnGet(RequestGetNewsDto request)
        {
            var userId = ClaimUtility.GetUserId(User);
            request.UserId = userId;
            NewsList = await newsService.GetNews.ExecuteAsync(request);
        }
    }
}
