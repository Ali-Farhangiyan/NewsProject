using Application.Interfaces;
using Application.Pagination;
using Application.Services.CategoryServices.AddCategory;
using Application.Services.CategoryServices.GetCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CategoryServices.CategoryMainService
{
    public interface ICategoryService
    {
        IAddCategoryService AddCategory { get; }

        IGetCategoryService GetCategory { get; }
    }

    public class CategoryService : ICategoryService
    {
        private readonly IDatabaseContext db;

        public CategoryService(IDatabaseContext db)
        {
            this.db = db;
        }

        private IAddCategoryService addCategory;
        public IAddCategoryService AddCategory =>
            addCategory ?? new AddCategoryService(db);


        private IGetCategoryService getCategory;
        public IGetCategoryService GetCategory =>
            getCategory ?? new GetCategoryService(db);
    }
}
