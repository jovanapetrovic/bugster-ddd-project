using Bugster.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Bugster.Data.Configurations
{
    public class UserConfiguration :
        IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Username).IsRequired().HasMaxLength(30);
            builder.Property<string>("Password").IsRequired().HasMaxLength(30);
            builder.Property(user => user.Email).IsRequired().HasMaxLength(100);
            builder.Property(user => user.FullName).IsRequired().HasMaxLength(100);
            builder.Property(user => user.Role).IsRequired()
                .HasConversion(userRoleValue => userRoleValue.ToString(), userRoleValue => Enum.Parse<UserRole>(userRoleValue));
            builder.Property(user => user.Status).IsRequired()
                .HasConversion(userStatusValue => userStatusValue.ToString(), userStatusValue => Enum.Parse<UserStatus>(userStatusValue));
        }
    }
}
