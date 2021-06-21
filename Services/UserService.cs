using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVForm.DataBase;
using CVForm.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CVForm.Services
{
    public class UserService
    {
        readonly AppDbContext _context;
        readonly ILogger _logger;
        
        public UserService(AppDbContext context, ILoggerFactory factory)
        {
            _context = context;
            _logger = factory.CreateLogger<UserService>();
        }

        public async Task<List<UserSummaryViewModel>> GetUsers() => await _context.Users
                .Where(r => !r.IsDeleted)
                .Select(x => new UserSummaryViewModel
                {
                    Id = x.UserId,

                    FirstName = x.FirstName,
                    LastName = x.LastName,

                    Phone = x.Phone
                })
                .ToListAsync();

        public async Task<int> CreateUser (CreateUserCommand cmd)
        {
            var user = cmd.ToUser();
            _context.Attach (user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }
    }
}