using CI_Entity.Models;

namespace CI.Models
{
    public class AdminThemeViewModel
    {
        public List<MissionTheme> missionThemes { get; set; }
        public string themeName { get; set; }
        public long themeId { get; set; }
    }

}
