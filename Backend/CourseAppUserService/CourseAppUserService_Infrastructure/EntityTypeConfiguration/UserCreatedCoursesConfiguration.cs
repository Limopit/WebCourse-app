using CourseAppUserService_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseAppUserService_Persistance.EntityTypeConfiguration;

public class UserCreatedCoursesConfiguration: IEntityTypeConfiguration<UserCreatedCourses>
{
    public void Configure(EntityTypeBuilder<UserCreatedCourses> builder)
    {
        builder.ToTable("UserCreatedCourses");

        builder.HasKey(course => course.RecordId);
        builder.Property(course => course.RecordId)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        builder.Property(course => course.CourseId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(course => course.ApprovementStatus)
            .IsRequired()
            .HasMaxLength(15); 
        
        builder.HasIndex(course => new { course.UserId, course.CourseId })
            .IsUnique();

        builder.HasOne(course => course.User)
            .WithMany(user => user.CreatedCourses)
            .HasForeignKey(course => course.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}