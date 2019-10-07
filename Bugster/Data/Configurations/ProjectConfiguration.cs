using Bugster.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bugster.Data.Configurations
{
    public class ProjectConfiguration :
        IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(project => project.Id);

            builder.Property(project => project.Name).IsRequired().HasMaxLength(60);
            builder.Property(project => project.Description).HasMaxLength(200);

            builder.HasMany(project => project.Bugs)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(project => project.Manager)
                .WithOne()
                .IsRequired()
                .HasForeignKey<Project>(project => project.ManagerId);

            builder.HasMany(project => project.TeamMembers)
                .WithOne();
        }
    }
}
