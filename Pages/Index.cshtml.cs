using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVForm.Models;
using CVForm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CVForm.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserService _service;
        public IEnumerable <UserSummaryViewModel> Users { get; private set; }
        
        public IndexModel(ILogger<IndexModel> logger, UserService service)
        {
            _logger = logger;
            _service = service;
        }
        
        public async Task OnGet()
        {
            Users = await _service.GetUsers();

        }
    }
}