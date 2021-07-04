using System;
using System.Linq;
using System.Threading.Tasks;
using CVForm.Models;
using CVForm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CVForm.Pages.User
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public UpdateUserCommand Input { get; set; }
        private readonly UserService _service;

        public string OldPassword { get; set; }
        public EditModel(UserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int id)
        {

            Input = await _service.GetUserForUpdate(id);
            if (Input is null)
            {
                // If id is not for a valid Recipe, generate a 404 error page
                // TODO: Add status code pages middleware to show friendly 404 page
                return NotFound();
            }

            OldPassword = Input.getPass();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Password)
        {
            try
            {
                if (!Input.PassCompare(Input.OldPassword))
                {
                    ModelState.AddModelError(String.Empty, "Old Password Is InCorrect");
                }

                if (Input.PassCompare(Input.NewPassword))
                    ModelState.AddModelError(String.Empty, "New Password Is Same As Old Password");
                
                if (!IsValidPassword(Input.NewPassword))
                    ModelState.AddModelError(String.Empty, "Password Is Not Accepted");
                
                if (ModelState.IsValid)
                {
                    await _service.UpdateUser(Input);
                    return RedirectToPage("/User/View", new { id = Input.Id });
                }
            }
            catch (Exception)
            {
                // TODO: Log error
                // Add a model-level error by using an empty string key
                ModelState.AddModelError(
                    string.Empty,
                    "An error occured saving the User"
                );
            }

            //If we got to here, something went wrong
            return Page();
        }
        
        private bool IsValidPassword(string passwd)
        {
            // Min 8 char and max 16 char
            if (passwd.Length < 8 || passwd.Length > 16)
                return false;

            // One upper case
            if (!passwd.Any(char.IsUpper))
                return false;

            // Atleast one lower case
            if (!passwd.Any(char.IsLower))
                return false;

            // No white space
            if (passwd.Contains(" "))
                return false;

            // Check for one special character
            string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialChArray = specialCh.ToCharArray();
            
            foreach (char ch in specialChArray)
                if (passwd.Contains(ch))
                    return true;
            
            return false;
        }
    }
}