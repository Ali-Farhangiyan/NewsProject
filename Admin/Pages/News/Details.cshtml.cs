using Application.Services.NewsServices.DetailNews;
using Application.Services.NewsServices.ManagementNews;
using Application.Services.NewsServices.NewsMainService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.News
{
    public class DetailsModel : PageModel
    {
        private readonly INewsService newsService;

        public DetailsModel(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public DetailNewsDto Detail { get; set; }

        [BindProperty]
        public ManageNewsDto Manage { get; set; }
        public async Task OnGet(int Id)
        {
            Detail = await newsService.DetailNews.ExecuteAsync(Id);
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await newsService.ManageNews.ExecuteAsync(Manage);

            if(result == true)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
