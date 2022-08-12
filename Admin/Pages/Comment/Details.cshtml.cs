
using Application.Services.CommentServices.ChangeStatusComment;
using Application.Services.CommentServices.CommentMainService;
using Application.Services.CommentServices.DetailCommentForAdmin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Comment
{
    public class DetailsModel : PageModel
    {
        private readonly ICommentService commentService;

        public DetailsModel(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public DetailCommentDto Comment { get; set; }

        [BindProperty]
        public RequestChangeStatusDto Request { get; set; }
        public async Task OnGet(int Id)
        {
            Comment = await commentService.DetailComment.ExecuteAsync(Id);
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await commentService.ChangeStatus.ExecuteAsync(Request);
            if (result == true) return RedirectToPage("/Index");
            return RedirectToPage();
        }
    }
}
