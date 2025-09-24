using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IssueTracker.API.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Project Name harus diisi")]
        [StringLength(100, ErrorMessage = "Panjang max 100")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Panjang max 500")]
        [Required(ErrorMessage = "Decription harus diisi")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Date Time harus diisi")]
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "Created By User ID harus diisi")]
        public string CreatedByUserId { get; set; }


    }
}
