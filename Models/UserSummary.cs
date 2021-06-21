using CVForm.DataBase;

namespace CVForm.Models
{
    public class UserSummaryViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Phone { get; set; }

        public static UserSummaryViewModel FromUser(User user)
        {
            return new UserSummaryViewModel
            {
                Id = user.UserId,

                FirstName = user.FirstName,
                LastName = user.LastName,

                Phone = user.Phone
            };
        }
    }
}