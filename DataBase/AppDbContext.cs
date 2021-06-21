using Microsoft.EntityFrameworkCore;

namespace CVForm.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }
        
       
        public DbSet <User> Users { get; set; }
        public DbSet <Skill> Skills { get; set; }
        public DbSet <Nationality> Nationalities { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, SkillName = "Java" },
                new Skill { Id = 2, SkillName = "HTML" },
                new Skill { Id = 3, SkillName = "PHP" },
                new Skill { Id = 4, SkillName = "C" },
                new Skill { Id = 5, SkillName = "C++" },
                new Skill { Id = 6, SkillName = "C#" },
                new Skill { Id = 7, SkillName = "Dot Net Core" },
                new Skill { Id = 8, SkillName = "Python" }
            );

            modelBuilder.Entity<Nationality>().HasData(
                new Nationality {Id = 1, NationalityName = "Lebanese"},
                new Nationality {Id = 2, NationalityName = "Russian"},
                new Nationality {Id = 3, NationalityName = "French"},
                new Nationality {Id = 4, NationalityName = "American"}
                );
        }
    }
}