using FoxyBurrow.Core.Entity;
using FoxyBurrow.Database.DbContext.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Database.DbContext
{
    public class EFDbContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;
        public EFDbContext(DbContextOptions<EFDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options) { }
        new public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserMap(modelBuilder.Entity<User>());
            new PostMap(modelBuilder.Entity<Post>());
            new ChatMap(modelBuilder.Entity<Chat>());
        }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("FoxyBurrow.Database"));
        }*/
    }
}
