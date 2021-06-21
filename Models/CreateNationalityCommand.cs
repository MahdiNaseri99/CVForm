using System.ComponentModel.DataAnnotations;
using CVForm.DataBase;

namespace CVForm.Models
{
    public class CreateNationalityCommand
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NationalityName { get; set; }

        public Nationality ToNationality()
        {
            return new Nationality
            {
                Id = Id,
                NationalityName = NationalityName
            };
        }
    }
}