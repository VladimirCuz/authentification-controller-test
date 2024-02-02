using authentification_controller_test.Handlers;
using authentification_controller_test.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace authentification_controller_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        public CommentsController(IGenericRepository<Comment> commentRepository, IGenericRepository<User> userRepository)
        {
            _commentRepository = commentRepository;
        }


        [HttpGet("ByCommentId")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _commentRepository.GetById(id);
            if(comment == null)
            {
                return NotFound("Такого комментария нет");
            }
            return Ok(comment);
        }
        [HttpGet("ByUserId")]
        public async Task<IActionResult> GetByUserId(int userid)
        {
            List<Comment> comments = await _commentRepository.GetAllEntities();
            if (comments == null)
            {
                return BadRequest();
            }
            comments.FirstOrDefault(u => u.AuthorId == userid);
            return Ok(comments);
        }
        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment(string comment, User user)
        {
            if (Validator.Validate(comment))
            {
                Comment Comment = new Comment();
                Comment.comment = comment;
                Comment.AuthorId = user.UserId;
                await _commentRepository.CreateAsync(Comment);
                return Ok(Comment);
            }
            return BadRequest("Комментарий не может быть пустым.");
        }
        [HttpPost("UpdateComment")]

        public async Task<IActionResult> UpdateComment(string newcomment, int commentid)
        {
            if(Validator.Validate(newcomment))
            {
            var comment = await _commentRepository.GetById(commentid);
                await _commentRepository.UpdateFieldsAsync(comment, comment =>
                {
                    comment.comment = newcomment;
                });
            return Ok(comment);

            }
            return BadRequest("Комментарий не найден. Попробуйте снова.");
        }
    }
}
