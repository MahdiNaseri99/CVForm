using System.Collections.Generic;
using System.IO;
using System.Linq;
using CVForm.DataBase;
using Microsoft.AspNetCore.Http;

namespace CVForm.Models
{
    public class CreateUserCommand : EditUserBase
    {
        public IList<CreateSkillCommand> Skills { get; set; } = new List<CreateSkillCommand>();
        public IList<CreateNationalityCommand> Nationalities { get; set; } = new List<CreateNationalityCommand>();
        private byte [] UploadedFile(IFormFile image)
        {
            byte [] toReturn = null;
            using (var memoryStream = new MemoryStream())
            {
                image.CopyToAsync(memoryStream);

                toReturn = memoryStream.ToArray();

            }

            return toReturn;
        }

        public User ToUser()
        {
            return new User
            {
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = Birthdate,
                Email = Email,
                Password = Password,
                Sex = Gender,
                Phone = Phone,
                Image = UploadedFile(ProfileImage),
                Skills = Skills?.Select(x=>x.ToSkill()).ToList(),
                Nationalities = Nationalities?.Select(x=>x.ToNationality()).ToList()
            };
        }
    }
}