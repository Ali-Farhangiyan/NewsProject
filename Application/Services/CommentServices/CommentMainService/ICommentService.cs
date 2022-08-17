using Application.Interfaces;
using Application.Services.CommentServices.AddComment;
using Application.Services.CommentServices.ChangeStatusComment;
using Application.Services.CommentServices.DetailCommentForAdmin;
using Application.Services.CommentServices.LikeOrDislikeComment;
using Application.Services.CommentServices.ShowCommentForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CommentServices.CommentMainService
{
    public interface ICommentService
    {
        IAddCommentService AddComment { get; }

        IShowCommentForAdminService ShowComment {get;}

        IDetailCommentForAdminService DetailComment { get; }

        IChangeStatusCommentService ChangeStatus { get; }

        ILikeOrDislikeCommentService LikeOrDislike { get; }
    }

    public class CommentService : ICommentService
    {
        private readonly IDatabaseContext db;
        private readonly IIdentityDatabaseContext identityDb;



        public CommentService(IDatabaseContext db, IIdentityDatabaseContext identityDb)
        {
            this.db = db;
            this.identityDb = identityDb;
        }




        private IAddCommentService addComment;
        public IAddCommentService AddComment =>
            addComment ?? new AddCommentServie(db, identityDb);


        private IShowCommentForAdminService showComment;
        public IShowCommentForAdminService ShowComment =>
            showComment ?? new ShowCommentForAdminService(db);


        private IDetailCommentForAdminService detailComment;
        public IDetailCommentForAdminService DetailComment =>
            detailComment ?? new DetailCommentForAdminService(db);


        private IChangeStatusCommentService changeStatus;
        public IChangeStatusCommentService ChangeStatus =>

            changeStatus ?? new ChangeStatusCommentService(db);



        private ILikeOrDislikeCommentService likeOrDislike;
        public ILikeOrDislikeCommentService LikeOrDislike =>
            likeOrDislike ?? new LikeOrDislikeCommentService(db);
    }
}
