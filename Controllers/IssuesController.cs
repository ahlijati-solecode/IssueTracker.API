using IssueTracker.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IssueTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private static List<Issue> _issues = new List<Issue>
        {
            new Issue { id = 1, Title = "Menu Bugs", Description = "menu bugs description", Status = "Active", Priority = "Major", ProjectId = 1, AssignedToUserId = "Dzaki", CreatedDate = DateTime.Now },
            new Issue { id = 2, Title = "Upload Bugs", Description = "upload bugs description", Status = "Active", Priority = "Minor", ProjectId = 1, AssignedToUserId = "Lisdiana", CreatedDate = DateTime.Now },
        };

        [HttpGet]
        public IActionResult GetAllIssues()
        {
            return Ok(_issues);
        }

        // GET api/<IssueController>/5
        [HttpGet("{id}")]
        public IActionResult GetIssueById(int id)
        {
            var issue = _issues.FirstOrDefault(c => c.id == id);
            if (issue == null)
            {
                return NotFound($"Issue with ID = {id} not found.");
            }

            return Ok(issue);
        }

        // POST api/<IssueController>
        [HttpPost]
        public IActionResult CreateIssue([FromBody] Issue newIssue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _issues.Add(newIssue);

            return CreatedAtAction(nameof(GetIssueById),
                new { id = newIssue.id }, newIssue);
        }

        // PUT api/<IssueController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateIssue(int id, [FromBody] Issue updateIssue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Update data
            var issue = _issues.FirstOrDefault(c => c.id == id);
            if (issue == null)
            {
                return NotFound($"Issue with ID = {id} not found.");
            }
            
            issue.id = updateIssue.id;
            issue.Title = updateIssue.Title;
            issue.Description = updateIssue.Description;
            issue.Status = updateIssue.Status;
            issue.Priority = updateIssue.Priority;
            issue.ProjectId = updateIssue.ProjectId;
            issue.AssignedToUserId = updateIssue.AssignedToUserId;
            issue.CreatedDate = updateIssue.CreatedDate;

            return Ok(issue);

        }

        // DELETE api/<IssueController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteIssue(int id)
        {
            // Delete data
            var issue = _issues.FirstOrDefault(c => c.id == id);
            if (issue == null)
            {
                return NotFound($"Issue with ID = {id} not found.");
            }

            _issues.Remove(issue);

            return Ok($"Issue with ID = {id} has been deleted");
        }
    }
}