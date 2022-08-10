using Application.Services.NewsServices.NewsMainService;
using Application.Services.NewsServices.ShowNewsForCategory;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }


        public async Task<IActionResult> Index(RequestShowNewsDto request)
        {
            
            var model = await newsService.ShowNews.ExecuteAsync(request);
            return View(model);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var model = await newsService.ShowDetails.ExecuteAsync(Id);
            return View(model);
        }
    }
}
