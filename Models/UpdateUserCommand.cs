using System;
using System.IO;
using CVForm.DataBase;
using Microsoft.AspNetCore.Http;

namespace CVForm.Models
{
    public class UpdateUserCommand : EditUserBase
    {
        public int Id { get; set; }
        public string MyoldPassword { private get; set; }

        public String getPass()
        {
            return MyoldPassword;
        }

        public bool PassCompare(String password)
        {
            if (MyoldPassword == password) return true;
            return false;
        }

        public void UpdateUser(User user)
        {
            user.Email = Email;
            user.Password = NewPassword;
            user.Phone = Phone;
        }
    }
}