using IssueTracker.API.Interfaces;
using IssueTracker.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IssueTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        //private static List<Issue> _issues = new List<Issue>
        //{
        //    new Issue { id = 1, Title = "Menu Bugs", Description = "menu bugs description", Status = "Active", Priority = "Major", ProjectId = 1, AssignedToUserId = "Dzaki", CreatedDate = DateTime.Now },
        //    new Issue { id = 2, Title = "Upload Bugs", Description = "upload bugs description", Status = "Active", Priority = "Minor", ProjectId = 1, AssignedToUserId = "Lisdiana", CreatedDate = DateTime.Now },
        //};

        [HttpGet]
        public async Task<IActionResult> GetAllIssues()
        {
            var _issues = await _issueService.GetIssuesAsync();
            return Ok(_issues);
        }

        // GET api/<IssueController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssueById(int id)
        {
            var issue = await _issueService.GetIssueByIdAsync(id);
            if (issue == null)
            {
                return NotFound($"Issue with ID = {id} not found.");
            }

            return Ok(issue);
        }

        // POST api/<IssueController>
        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] Issue newIssue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newsissue = await _issueService.CreateAsync(newIssue);

            return CreatedAtAction(nameof(GetIssueById),
                new { id = newIssue.id }, newIssue);
        }

        // PUT api/<IssueController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(int id, [FromBody] Issue updateIssue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Update data
            var updated = await _issueService.UpdateAsync(id, updateIssue);
            if (updated == null)
            {
                return NotFound($"Issue with ID = {id} not found.");
            }

            return Ok(updated);

        }

        // DELETE api/<IssueController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssueAsync(int id)
        {
            // Delete data
            var success = await _issueService.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Issue with ID = {id} not found.");
            }

            return Ok($"Issue with ID = {id} has been deleted");
        }
    }
}