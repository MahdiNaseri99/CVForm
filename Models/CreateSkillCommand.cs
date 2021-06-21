using System;
using System.ComponentModel.DataAnnotations;
using CVForm.DataBase;

namespace CVForm.Models
{
    public class CreateSkillCommand
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string SkillName { get; set; }

        public Skill ToSkill()
        {
            return new Skill
            {
                Id = Id,
                SkillName = SkillName
            };
        }
    }
}