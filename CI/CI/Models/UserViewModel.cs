using CI_Entity.Models;
using System.ComponentModel.DataAnnotations;

namespace CI.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Email name is a Required field.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "password name is a Required field.")]
        public string Password { get; set; }
        public int? MissionId { get; set; }
        public int? StoryId { get; set; }
    }
}
