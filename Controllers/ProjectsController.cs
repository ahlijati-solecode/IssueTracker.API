using IssueTracker.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IssueTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private static List<Project> _projects = new List<Project>
        {
            new Project{Id =1, Name="ERM", CreatedDate=DateTime.Now, CreatedByUserId="Lis"},
            new Project{Id =2, Name="MAPS", CreatedDate=DateTime.Now, CreatedByUserId="Dzaki"}

        };

        // GET: api/<ProjectsController>
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            return Ok(_projects); //200 OK + category list
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public IActionResult GetProjectById(int id)
        {
            var project = _projects.FirstOrDefault(c => c.Id == id);
            if (project == null)
            {
                return NotFound($"Category with ID ={id} not found");
            }
            return Ok($"Details of project with ID = {id}"); //200 OK + category
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public IActionResult CreateProject([FromBody] Project newProject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Generate new ID
            newProject.Id = _projects.Count + 1;
            _projects.Add(newProject);

            return CreatedAtAction(nameof(GetProjectById), new { id = newProject.Id },
                 $"New project '{newProject.Name}' created with ID ={newProject.Id}");

        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] Project updatedProject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var project = _projects.FirstOrDefault(c => c.Id == id);
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
        public IActionResult DeleteProject(int id)
        {
            var project = _projects.FirstOrDefault(c => c.Id == id);
            if (project == null)
            {
                return NotFound($"Project with ID ={id} not found");
            }
            _projects.Remove(project);
            return Ok($"Project with ID {id} deleted");
        }
    }
}
