using System.Collections.Generic;
using System.Threading.Tasks;
using CVForm.Models;
using CVForm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CVForm.Pages.forms
{
    public class ViewModel : PageModel
    {
        private readonly UserService _service;
        public UserDetailViewModel user { get; private set; }

        public ViewModel (UserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            user = await _service.GetUserDetail(id);
            if (user is null)
            {
                // If id is not for a valid User, generate a 404 error page
                // TODO: Add status code pages middleware to show friendly 404 page
                return NotFound();
            }
            return Page();
        }
    }
}