using authentification_controller_test.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace authentification_controller_test.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
            modelBuilder.Entity<User>()
                .HasMany(a => a.Comments)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);
            modelBuilder.Entity<User>().HasData(
                               new User
                               {
                                   UserId = 1,
                                   Login = "Admin",
                                   Password = PasswordHandler.HashPassword("12345679asdfaszxc")
                               });
            modelBuilder.Entity<Role>().HasData(new Role { RoleId = 10, Name = "Admin", permissions = "adminpermissions" });
            modelBuilder.Entity<UserRole>().HasData(new UserRole { RoleId = 10, UserId = 1 });
        }


    }
}
