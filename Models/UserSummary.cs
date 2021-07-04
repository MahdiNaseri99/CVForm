using System;
using System.Collections.Generic;
using System.Net.Mime;
using CVForm.DataBase;

namespace CVForm.Models
{
    public class UserSummaryViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }
        public string Phone { get; set; }

        public static UserSummaryViewModel FromUser(User user)
        {
            return new UserSummaryViewModel
            {
                Id = user.UserId,

                FirstName = user.FirstName,
                LastName = user.LastName,
                
                Photo = user.Image,
                Phone = user.Phone
            };
        }
    }

    public class UserDetailViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public bool?  Sex { get; set; }
        public IEnumerable <Skill> Skills { get; set; }

        public class Skill
        {
            public int Id { get; set; }
            public string SkillName { get; set; }
        }
        
        public IEnumerable <Nationality> Nationalities { get; set; }

        public class Nationality
        {
            public int Id { get; set; }
            public string NationalityName { get; set; }
        }
    }

    public class UserEditViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

    }
}