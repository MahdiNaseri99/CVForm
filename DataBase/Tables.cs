using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVForm.DataBase
{
    public class User
    {
        [Column("Id")]
        [Required]
        public int    UserId { get; set; }
        
        [Column(TypeName = "varchar(45)")]
        [Required]
        public string FirstName { get; set; }
        
        [Column(TypeName = "varchar(45)")]
        [Required]
        public string LastName { get; set; }
        
        [Column(TypeName = "Date")]
        [Required]
        public DateTime BirthDate { get; set; }
        
        
        public bool?  Sex { get; set; }
        
        [Column(TypeName = "varchar(90)")]
        [Required]
        public string Email { get; set; }
        
        [Column(TypeName = "varchar(45)")]
        [Required]
        public string Password { get; set; }
        
        [Column(TypeName = "varchar(45)")]
        [Required]
        public string Phone { get; set; }
        
        [Column(TypeName = "longblob")]
        [Required]
        public byte[] Image { get; set; }
        
        public bool   IsDeleted { get; set; }
        
        public ICollection <Nationality> Nationalities { get; set; }
        public ICollection <Skill> Skills { get; set; }
    }

    public class Nationality
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string NationalityName { get; set; }
        
        public ICollection <User> Users { get; set; }
    }

    public class Skill
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string SkillName { get; set; }
        
        public ICollection <User> Users { get; set; }
    }
}