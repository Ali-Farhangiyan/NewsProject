using Application.Services.CategoryServices.AddCategory;
using Application.Services.CategoryServices.CategoryMainService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService categoryService;

        public CreateModel(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [BindProperty]
        public AddCategoryDto ModelCategory { get; set; }
        public void OnGet(int? parentCategoryId)
        {
            ViewData["parentCategoryId"] = parentCategoryId;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await categoryService.AddCategory.ExecuteAsync(ModelCategory);
            if (result == true) return RedirectToPage("Index");
            return Page();
        }
    }
}
