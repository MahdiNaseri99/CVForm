using System.Collections.Generic;
using System.Linq;
using CVForm.DataBase;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CVForm.Services
{
    public class NationalityService
    {
        readonly AppDbContext _context;
        readonly ILogger _logger;
        
        public NationalityService(AppDbContext context, ILoggerFactory factory)
        {
            _context = context;
            _logger = factory.CreateLogger<UserService>();
        }
        
        public IList<SelectListItem> GetNationality()
        {
            return _context.Nationalities
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text  = x.NationalityName
                })
                .ToList<SelectListItem>();
        }

        public string GetNationalityName(int id)
        {
            return _context.Nationalities
                .Where(x => x.Id == id)
                .Select(x => x.NationalityName)
                .SingleOrDefault();
        }
    }
}