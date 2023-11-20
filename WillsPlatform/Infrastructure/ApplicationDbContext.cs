using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WillsPlatform.Application;
using WillsPlatform.Domains.Entities;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Token> Tokens { get; set; }

        public ApplicationDbContext(DbContextOptions options)
           : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}