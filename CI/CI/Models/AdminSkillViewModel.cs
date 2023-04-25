using CI_Entity.Models;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace CI.Models
{
    public class AdminSkillViewModel
    {
        public List<Skill> skill { get; set; }
        [Required(ErrorMessage = "Skill name is a Required field.")]
        public string skillName { get; set; }
        public long skillId { get; set; }
    }

}
