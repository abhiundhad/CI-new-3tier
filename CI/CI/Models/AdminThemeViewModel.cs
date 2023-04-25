using CI_Entity.Models;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace CI.Models
{
    public class AdminThemeViewModel
    {
        public List<MissionTheme> missionThemes { get; set; }
        [Required(ErrorMessage = "Theme name is a Required field.")]

        public string themeName { get; set; }
        public long themeId { get; set; }
    }

}
