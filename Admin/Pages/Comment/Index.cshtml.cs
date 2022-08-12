using Application.Pagination;
using Application.Services.CommentServices.CommentMainService;
using Application.Services.CommentServices.ShowCommentForAdmin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Comment
{
    public class IndexModel : PageModel
    {
        private readonly ICommentService commentService;

        public IndexModel(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public PaginatedList<ShowCommentDto> Comments { get; set; }
        public async Task OnGet(RequestShowCommentDto request)
        {
            Comments = await commentService.ShowComment.ExecuteAsync(request);
        }
    }
}
