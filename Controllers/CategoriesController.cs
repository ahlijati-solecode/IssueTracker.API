using IssueTracker.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private static List<Category> _categories = new List<Category>
        {
            new Category { Id = 1, CategoryName = "Electronics" },
            new Category { Id = 2, CategoryName = "Books" }
        };

        // GET: /api/category
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(_categories);
        }

        // GET: /api/category/{id}
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound($"Category with ID = {id} not found.");
            }

            return Ok(category);
        }


        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category newCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Generate new ID
            newCategory.Id = _categories.Count + 1;
            _categories.Add(newCategory);

            return CreatedAtAction(nameof(GetCategoryById),
                                 new { id = newCategory.Id },
                 $"New category '{newCategory.CategoryName}' created with ID = {newCategory.Id}");
        }

        // PUT: /api/category/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id,
                                           [FromBody] Category updatedCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound($"Category with ID = {id} not found.");
            }
            category.CategoryName = updatedCategory.CategoryName;

            return Ok($"Category with ID = {id} updated to '{updatedCategory.CategoryName}'");
        }

        // DELETE: /api/category/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound($"Category with ID = {id} not found.");
            }

            _categories.Remove(category);

            return Ok($"Category with ID = {id} deleted.");
        }




    }
}
