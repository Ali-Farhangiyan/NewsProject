using Admin.Utilities;
using Application.Services.NewsServices.AddNews;
using Application.Services.NewsServices.NewsMainService;
using Infrastructure.InfrastructureServices.ImageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.Pages.News
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IImageService imageService;
        private readonly INewsService newsService;

        public CreateModel(IImageService imageService,INewsService newsService)
        {
            this.imageService = imageService;
            this.newsService = newsService;
        }

        public SelectList Categories { get; set; }

        

        [BindProperty]
        public AddNewsDto Data { get; set; }
        public async void OnGet()
        {
            Categories = new SelectList(await newsService.GetCategories.ExecuteAsync(), "Id", "Name");
        }

        public async Task<IActionResult> OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var files = Request.Form.Files;
            var imageAdd = await imageService.ExecuteAsync(files);
            var images = new List<ImageDto>();
            foreach (var item in imageAdd.Address)
            {
                images.Add(new ImageDto { Src = imageService.CompserImage(item).Result});
            }
            var userId = ClaimUtility.GetUserId(User);
            Data.UserId = userId;
            Data.Images = images;

            var result = await newsService.Add.ExecuteAsync(Data);
            if(result == true)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
