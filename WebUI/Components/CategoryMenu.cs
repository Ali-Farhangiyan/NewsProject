using Application.Services.CategoryServices.CategoryMainService;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public CategoryMenu(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }


        public  IViewComponentResult Invoke()
        {
            var model =  categoryService.GetCategoryiesMenu.ExecuteAsync().Result;
            return View("CategoryMenuView", model);
        }
    }
}
