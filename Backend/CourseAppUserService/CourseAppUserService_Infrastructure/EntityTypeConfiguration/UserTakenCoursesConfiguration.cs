using CourseAppUserService_Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseAppUserService_Persistance.EntityTypeConfiguration;

public class UserTakenCoursesConfiguration: IEntityTypeConfiguration<UserTakenCourses>
{
    public void Configure(EntityTypeBuilder<UserTakenCourses> builder)
    {
        builder.ToTable("UserTakenCourses");

        builder.HasKey(utc => utc.RecordId);
        builder.Property(utc => utc.RecordId)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        builder.Property(utc => utc.Status)
            .IsRequired()
            .HasMaxLength(50); 

        builder.Property(utc => utc.DateStart)
            .IsRequired();

        builder.Property(utc => utc.DateFinished)
            .IsRequired(false);

        builder.Property(utc => utc.IsFavourite)
            .IsRequired();
        
        builder.HasIndex(utc => new { utc.UserId, utc.CourseId })
            .IsUnique();
        
        builder.HasOne(utc => utc.User)
            .WithMany(u => u.TakenCourses)
            .HasForeignKey(utc => utc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}