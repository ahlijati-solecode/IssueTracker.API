using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTracker.API.Models
{
    public class Issue
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [MaxLength(50, ErrorMessage = "Status cannot be longer than 50 characters")]
        public string Status { get; set; } = string.Empty;
        [Required(ErrorMessage = "Priority is required")]
        [MaxLength(50, ErrorMessage = "Priority cannot be longer than 50 characters")]
        public string Priority { get; set; } = string.Empty;
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public string? AssignedToUserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
