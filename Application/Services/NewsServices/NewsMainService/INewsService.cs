using Application.Interfaces;
using Application.Services.NewsServices.AddNews;
using Application.Services.NewsServices.DetailNews;
using Application.Services.NewsServices.GetCategories;
using Application.Services.NewsServices.GetNews;
using Application.Services.NewsServices.ManagementNews;
using Application.Services.NewsServices.ShowDetailsNews;
using Application.Services.NewsServices.ShowNewsForCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NewsServices.NewsMainService
{
    public interface INewsService
    {
        IAddNewsService Add { get; }

        IGetCategoriesService GetCategories { get; }

        IGetNewsService GetNews { get; }

        IDetailNewsService DetailNews { get; }

        IManagementNewsService ManageNews { get; }

        IShowNewsForCategoryService ShowNews { get; }

        IShowDetailsNewsService ShowDetails { get; }
    }

    public class NewsService : INewsService
    {
        private readonly IDatabaseContext db;
        private readonly IIdentityDatabaseContext identityDb;

        public NewsService(IDatabaseContext db, IIdentityDatabaseContext identityDb)
        {
            this.db = db;
            this.identityDb = identityDb;
        }





        private IAddNewsService add;
        public IAddNewsService Add =>
            add ?? new AddNewsService(db);

        private IGetCategoriesService getCategories;
        public IGetCategoriesService GetCategories =>
            getCategories ?? new GetCategoriesService(db);




        private IGetNewsService getNews;
        public IGetNewsService GetNews =>
            getNews ?? new GetNewsService(db,identityDb);


        private IDetailNewsService detailNews;
        public IDetailNewsService DetailNews =>
            detailNews ?? new DetailNewsService(db);


        private IManagementNewsService manageNews;
        public IManagementNewsService ManageNews =>
            manageNews ?? new ManagementNewsService(db);



        private IShowNewsForCategoryService showNews;
        public IShowNewsForCategoryService ShowNews =>
            showNews ?? new ShowNewsForCategoryService(db);



        private IShowDetailsNewsService showDetails;
        public IShowDetailsNewsService ShowDetails =>
            showDetails ?? new ShowDetailsNewsService(db);
    }
}
