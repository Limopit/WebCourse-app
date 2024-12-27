using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Domain;
using CourseAppUserService_Persistance.EntityTypeConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseAppUserService_Persistance
{
    public class UserServiceDbContext : IdentityDbContext<User>, IUserServiceDbContext
    {
        public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options) {}

        public DbSet<UserCreatedCourses> UserCreatedCourses { get; set; }
        public DbSet<UserTakenCourses> UserTakenCourses { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserCreatedCoursesConfiguration());
            modelBuilder.ApplyConfiguration(new UserTakenCoursesConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}