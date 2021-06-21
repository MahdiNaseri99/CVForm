using CVForm.DataBase;

namespace CVForm.Models
{
    public class SkillSummaryViewModel
    {
        public int Id { get; set; }
        public string SkillName { get; set; }
        
        public static SkillSummaryViewModel FromSkill(Skill skill)
        {
            return new SkillSummaryViewModel
            {
                Id = skill.Id,

                SkillName = skill.SkillName
            };
        }
    }
}