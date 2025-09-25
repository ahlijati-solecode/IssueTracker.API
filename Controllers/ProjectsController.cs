using IssueTracker.API.Interface;
using IssueTracker.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IssueTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /*
        private static List<Project> _projects = new List<Project>
        {
            new Project{Id =1, Name="ERM", CreatedDate=DateTime.Now, CreatedByUserId="Lis"},
            new Project{Id =2, Name="MAPS", CreatedDate=DateTime.Now, CreatedByUserId="Dzaki"}

        }; */

        // GET: api/<ProjectsController>
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var _projects = await _projectService.GetProjectsAsync();
            return Ok(_projects); //200 OK + category list
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound($"Category with ID ={id} not found");
            }
            return Ok($"Details of project with ID = {id}"); //200 OK + category
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project newProject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = await _projectService.CreateAsync(newProject);

            return CreatedAtAction(nameof(GetProjectById), new { id = newProject.Id },
                 $"New project '{newProject.Name}' created with ID ={newProject.Id}");

        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] Project updatedProject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var project = await _projectService.UpdateAsync(id, updatedProject);
            if (project == null)
            {
                return NotFound($"Project with ID ={id} not found");
            }
            project.Name = updatedProject.Name;
            project.Description = updatedProject.Description;

            return Ok($"Project with ID = {id} updated to '{updatedProject.Name}'");

        }



        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectService.DeleteAsync(id);
            if (project == false)
            {
                return NotFound($"Project with ID ={id} not found");
            }
            return Ok($"Project with ID {id} deleted");
        }
    }
}
