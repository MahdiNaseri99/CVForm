using System;
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
        
        public async Task<List<UserSummaryViewModel>> GetUsers()
        {
            return await _context.Users
                .Where(r => !r.IsDeleted)
                .Select(x => new UserSummaryViewModel
                {
                    Id = x.UserId,

                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    
                    Photo = x.Image,
                    Phone = x.Phone
                })
                .ToListAsync();
        }

        public async Task<int> CreateUser (CreateUserCommand cmd)
        {
            var user = cmd.ToUser();
            _context.Attach (user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }
        public async Task DeleteUser (int UserId)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user is null) { throw new Exception("Unable to find User"); }

            user.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        
        public async Task<UserDetailViewModel> GetUserDetail(int UserId)
        {
            return await _context.Users
                .Where(x => x.UserId == UserId)
                .Where(x => !x.IsDeleted)
                .Select(x => new UserDetailViewModel
                {
                    Id = x.UserId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Phone = x.Phone,
                    Photo = x.Image,
                    Sex = x.Sex,
                    BirthDate = x.BirthDate,
                    Skills = x.Skills
                        .Select(skill => new UserDetailViewModel.Skill
                        {
                            SkillName = skill.SkillName,
                            Id = skill.Id
                        }),
                    Nationalities = x.Nationalities
                        .Select(nationality => new UserDetailViewModel.Nationality
                        {
                            NationalityName = nationality.NationalityName,
                            Id = nationality.Id
                        })
                })
                .SingleOrDefaultAsync();
        }
        public async Task<UpdateUserCommand> GetUserForUpdate(int UserId)
        {
            return await _context.Users
                .Where(x => x.UserId == UserId)
                .Where(x => !x.IsDeleted)
                .Select(x => new UpdateUserCommand
                {
                    Id = x.UserId,
                    Email = x.Email,
                    MyoldPassword = x.Password,
                    Phone = x.Phone,
                })
                .SingleOrDefaultAsync();
        }
        
        public async Task UpdateUser(UpdateUserCommand cmd)
        {
            var recipe = await _context.Users.FindAsync(cmd.Id);
            if (recipe == null) { throw new Exception("Unable to find the User"); }
            if (recipe.IsDeleted) { throw new Exception("Unable to update a deleted User"); }

            cmd.UpdateUser(recipe);
            await _context.SaveChangesAsync();
        }
    }
}