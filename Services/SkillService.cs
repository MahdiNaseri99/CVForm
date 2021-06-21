using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVForm.DataBase;
using CVForm.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CVForm.Services
{
    public class SkillService
    {
        readonly AppDbContext _context;
        readonly ILogger _logger;
        
        public SkillService(AppDbContext context, ILoggerFactory factory)
        {
            _context = context;
            _logger = factory.CreateLogger<UserService>();
        }
        
        public IList<SelectListItem> GetSkills()
        {
            return _context.Skills
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.SkillName
                })
                .ToList<SelectListItem>();
        }
    }
}