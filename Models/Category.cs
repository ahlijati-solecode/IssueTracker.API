using System.ComponentModel.DataAnnotations;

namespace IssueTracker.API.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(100, ErrorMessage = "Category Name cannot be longer than 100 characters")]
        public string CategoryName { get; set; } = string.Empty;
    }

}
