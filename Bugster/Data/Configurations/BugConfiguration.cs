using Bugster.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Bugster.Data.Configurations
{
    public class BugConfiguration :
        IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> builder)
        {
            builder.ToTable("Bugs");

            builder.HasKey(bug => bug.Id);

            builder.Property(bug => bug.Title).IsRequired().HasMaxLength(60);
            builder.Property(bug => bug.Description).HasMaxLength(500);
            builder.Property(bug => bug.Status).IsRequired()
                .HasConversion(bugStatusValue => bugStatusValue.ToString(), bugStatusValue => Enum.Parse<BugStatus>(bugStatusValue));
            builder.Property(bug => bug.Priority).IsRequired()
                .HasConversion(bugPriorityValue => bugPriorityValue.ToString(), bugPriorityValue => Enum.Parse<BugPriority>(bugPriorityValue));
            builder.Property(bug => bug.DueDate).IsRequired();
            builder.Property(bug => bug.DateCreated).IsRequired();

            builder.HasOne(bug => bug.Assignee)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
