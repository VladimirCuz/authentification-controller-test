using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace authentification_controller_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        [HttpGet("ByCommentId")]
        public async Task<IActionResult> Get(int id)
        {
            using (_dataContext)
            {
                if (await _dataContext.Comments.AnyAsync(u => u.id == id))
                {
                    var comment = _dataContext.Comments.Find(id);
                    return Ok(comment);
                }
                return BadRequest("Комментарий не найден. Попробуйте снова.");
            }
        }
        [HttpGet("ByUserId")]
        public async Task<IActionResult> GetByUserId(int userid)
        {
            using (_dataContext) 
            {
                if (await _dataContext.Comments.AnyAsync(u => u.AuthorId == userid))
                {
                    var comments = await _dataContext.Comments
                        .Where(a => a.AuthorId == userid)
                        .ToListAsync();

                    return Ok(comments);
                }
                return BadRequest("Комментарии не найден. Попробуйте снова.");
            }
        }
        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment(string comment, User user)
        {
            using (_dataContext)
            {
                Comment Comment = new Comment();
                Comment.comment = comment;
                Comment.AuthorId = user.UserId;
                _dataContext.Add(Comment);
                await _dataContext.SaveChangesAsync();
                return Ok(Comment);
            }
        }
        [HttpPost("UpdateComment")]

        public async Task<IActionResult> UpdateComment(string newcomment, int commentid)
        {
            if (await _dataContext.Comments.AnyAsync(u => u.id == commentid))
            {
                var comment = _dataContext.Comments.Find(commentid);
                comment.comment = newcomment;
                return Ok(comment);
            }
            return BadRequest("Комментарий не найден. Попробуйте снова.");
        }
    }
}
