using Microsoft.AspNetCore.Mvc;
using IssueTracker.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IssueTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentsController : ControllerBase
    {
        private static List<Comment> _comments = new List<Comment>
        {
            new Comment { Id=1, IssueId =1, UserId = "8248", Content = "Komentar 1", CreatedDate= DateTime.Now}
        };

        // GET api/<AllComments>
        [HttpGet]
        public IActionResult GetAllComments()
        {
            return Ok(_comments);
        }

        // GET api/<Comments>/5
        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            var Comment = _comments.FirstOrDefault(x => x.Id == id);
            if (Comment == null)
            {
                return NotFound($"Comment with ID = {id} not found.");
            }

            return Ok(Comment);
        }

        // POST api/<Comments>
        [HttpPost]
        public IActionResult CreateComment([FromBody] Comment newComment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Generate new ID
            newComment.Id = _comments.Count + 1;
            _comments.Add(newComment);

            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.Id },
                $"New Comments '{newComment.Issue}' created with ID = {newComment.Id}");
        }

        // PUT api/<Comments>/5
        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, [FromBody] Comment UpdateComment)
        {
            if (string.IsNullOrWhiteSpace(UpdateComment.Content))
            {
                return BadRequest("Category Name Cannot be Empty");
            }

            var newComment = _comments.FirstOrDefault(c => c.Id == id);
            if (newComment == null)
            {
                return NotFound($"Comment with id = {id} not found.");
            }
            newComment.Content = UpdateComment.Content;
            return Ok($"Comments with {id} updated");
        }

        // DELETE api/<Comments>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var Comment = _comments.FirstOrDefault(c => c.Id == id);
            if (Comment == null)
            {
                return NotFound($"Comments with ID = {id} Not Found. ");
            }

            _comments.Remove(Comment);

            return Ok($"Comments with {id} deleted");
        }
    }
}