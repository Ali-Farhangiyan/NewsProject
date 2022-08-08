using Application.Pagination;
using Application.Services.CategoryServices.CategoryMainService;
using Application.Services.CategoryServices.GetCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Admin.Pages.Category
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICategoryService categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public PaginatedList<GetCategoryDto> Categories { get; set; }
        public async Task OnGet(RequestCategoryDto request)
        
        {
            Categories = await categoryService.GetCategory.ExcuteAsync(request);
        }
    }
}
