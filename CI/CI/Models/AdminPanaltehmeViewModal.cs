using CI_Entity.Models;


using System.ComponentModel.DataAnnotations;

namespace CI.Models
{
    public class AdminPanaltehmeViewModal
    {
        public List<MissionTheme> MissionThemes { get; set; }
        [Required(ErrorMessage = "Name is required")]
         public string NewTheme { get; set; }
        public long ThemeID { get; set; }
    }
}
 