using CourseAppUserService_Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseAppUserService_Persistance.EntityTypeConfiguration;

public class UserCreatedCoursesConfiguration: IEntityTypeConfiguration<UserCreatedCourses>
{
    public void Configure(EntityTypeBuilder<UserCreatedCourses> builder)
    {
        builder.ToTable("UserCreatedCourses");

        builder.HasKey(uc => uc.RecordId);

        builder.Property(uc => uc.CourseId)
            .IsRequired()
            .HasMaxLength(24);

        builder.Property(uc => uc.ApprovementStatus)
            .IsRequired()
            .HasMaxLength(15); 

        builder.Property(uc => uc.ApprovementDate)
            .IsRequired();

        builder.HasIndex(uc => new { uc.UserId, uc.CourseId })
            .IsUnique();

        builder.HasOne(uc => uc.User)
            .WithMany(u => u.CreatedCourses)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}