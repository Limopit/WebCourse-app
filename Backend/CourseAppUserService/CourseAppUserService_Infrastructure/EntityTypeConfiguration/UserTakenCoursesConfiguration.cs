using CourseAppUserService_Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseAppUserService_Persistance.EntityTypeConfiguration;

public class UserTakenCoursesConfiguration: IEntityTypeConfiguration<UserTakenCourses>
{
    public void Configure(EntityTypeBuilder<UserTakenCourses> builder)
    {
        builder.ToTable("UserTakenCourses");

        builder.HasKey(utc => utc.RecordId); // Пример использования RecordId

        builder.Property(utc => utc.Status)
            .IsRequired()
            .HasMaxLength(50); // Ограничение на статус

        builder.Property(utc => utc.DateStart)
            .IsRequired();

        builder.Property(utc => utc.DateFinished)
            .IsRequired(false); // Может быть null, если курс не завершен

        builder.Property(utc => utc.IsFavourite)
            .IsRequired();

        // Композитный ключ
        builder.HasIndex(utc => new { utc.UserId, utc.CourseId })
            .IsUnique();

        // Связь с User
        builder.HasOne(utc => utc.User)
            .WithMany(u => u.TakenCourses)
            .HasForeignKey(utc => utc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}