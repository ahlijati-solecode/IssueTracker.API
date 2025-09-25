using System.Threading.Tasks;
using IssueTracker.API.Interfaces;
using IssueTracker.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IssueTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _CommentService;

        public CommentsController(ICommentService commentService)
        {
            _CommentService = commentService;
        }

        /*
         private static List<Comment> _comments = new List<Comment>
         {
             new Comment { Id=1, IssueId =1, UserId = "8248", Content = "Komentar 1", CreatedDate= DateTime.Now}
         };
        */

        // GET api/<AllComments>
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var _comments = await _CommentService.GetAllAsync();
            return Ok(_comments);
        }

        // GET api/<Comments>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var Comments = await _CommentService.GetCommentByIdAsync(id);
            if (Comments == null)
            {
                return NotFound($"Comment with ID = {id} not found.");
            }

            return Ok(Comments);
        }

        // POST api/<Comments>
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment newComment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Generate new ID
            var comment = await _CommentService.CreateAsync(newComment);

            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.Id },
                $"New Comments '{newComment.Issue}' created with ID = {newComment.Id}");
        }

        // PUT api/<Comments>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment UpdateComment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _CommentService.UpdateAsync(id, UpdateComment);

            if (updated == null) return NotFound($"Comments with ID = {id} not found");

            return Ok(updated);
        }

        // DELETE api/<Comments>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var success = await _CommentService.DeleteAsync(id);
            if (!success) return NotFound($"Comments with ID = {id} not found");

            return Ok($"Comments with ID = {id} deleted");
        }
    }
}