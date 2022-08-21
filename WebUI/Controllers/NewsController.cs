using Admin.Utilities;
using Application.Services.CommentServices.AddComment;
using Application.Services.CommentServices.CommentMainService;
using Application.Services.NewsServices.NewsMainService;
using Application.Services.NewsServices.ShowNewsForCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService newsService;
        private readonly ICommentService commentService;

        public NewsController(INewsService newsService, ICommentService commentService)
        {
            this.newsService = newsService;
            this.commentService = commentService;
        }


        public async Task<IActionResult> Index(RequestShowNewsDto request)
        {
            
            var model = await newsService.ShowNews.ExecuteAsync(request);
            
            return View(model);
        }


        public async Task<IActionResult> Details(string slug)
        {
            var model = await newsService.ShowDetails.ExecuteAsync(slug);
            if (User.Identity.IsAuthenticated)
            {
                var userFind = await newsService.GetInfoUser.ExecuteAsync(ClaimUtility.GetUserId(User));

                model.UserFullName = userFind.FullName;

            }


            return View(model);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(AddCommentDto comment)
        {
            var user = ClaimUtility.GetUserId(User);
            comment.UserId = user;
            var result = await commentService.AddComment.ExecuteAsync(comment);

            if(result == true)
            {
                ViewData["SuccessComment"] = "Your comment has been successfully registered";

                return RedirectToAction(nameof(Details), new { slug = comment.Slug });
            }

            ViewData["FaildComment"] = "There was a problem registering your comment";
            return RedirectToAction(nameof(Details), new { slug = comment.Slug });




        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> LikeAction(int Id,string Slug)
        {
            var userFind = await newsService.GetInfoUser.ExecuteAsync(ClaimUtility.GetUserId(User));
            var result = await commentService.LikeOrDislike.LikeExecute(Id,userFind.Email);
            return new JsonResult(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DislikeAction(int Id, string Slug)
        {
            var userFind = await newsService.GetInfoUser.ExecuteAsync(ClaimUtility.GetUserId(User));
            var result = await commentService.LikeOrDislike.DislikeExecute(Id,userFind.Email);
            return new JsonResult(result);
        }
    }
}
