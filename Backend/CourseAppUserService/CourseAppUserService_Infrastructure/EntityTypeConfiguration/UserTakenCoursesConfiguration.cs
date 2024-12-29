using CourseAppUserService_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseAppUserService_Persistance.EntityTypeConfiguration;

public class UserTakenCoursesConfiguration: IEntityTypeConfiguration<UserTakenCourses>
{
    public void Configure(EntityTypeBuilder<UserTakenCourses> builder)
    {
        builder.ToTable("UserTakenCourses");

        builder.HasKey(course => course.RecordId);
        builder.Property(course => course.RecordId)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        builder.Property(course => course.Status)
            .IsRequired()
            .HasMaxLength(50); 

        builder.Property(course => course.DateStart)
            .IsRequired();

        builder.Property(course => course.DateFinished)
            .IsRequired(false);

        builder.Property(course => course.IsFavourite)
            .IsRequired();
        
        builder.HasIndex(course => new { course.UserId, course.CourseId })
            .IsUnique();
        
        builder.HasOne(course => course.User)
            .WithMany(user => user.TakenCourses)
            .HasForeignKey(course => course.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}