using Bugster.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Bugster.Data.Configurations
{
    public class TagConfiguration :
        IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");

            builder.HasKey(tag => tag.Id);
            builder.Property(tag => tag.Name).IsRequired().HasMaxLength(100);
            builder.Property(tag => tag.Bound).IsRequired()
                .HasConversion(userRoleValue => userRoleValue.ToString(), userRoleValue => Enum.Parse<UserRole>(userRoleValue));

            builder.HasData(new Tag(1, "JQuery", UserRole.FRONTEND));
            builder.HasData(new Tag(2, "React", UserRole.FRONTEND));
            builder.HasData(new Tag(3, "Angular", UserRole.FRONTEND));
            builder.HasData(new Tag(4, "Vue.js", UserRole.FRONTEND));
            builder.HasData(new Tag(5, "JavaScript", UserRole.FRONTEND));
            builder.HasData(new Tag(6, "HTML", UserRole.FRONTEND));
            builder.HasData(new Tag(7, "CSS", UserRole.FRONTEND));
            builder.HasData(new Tag(8, "GraphQL", UserRole.FRONTEND));
            builder.HasData(new Tag(9, "Bootstrap", UserRole.FRONTEND));
            builder.HasData(new Tag(10, "React Native", UserRole.FRONTEND));
            builder.HasData(new Tag(11, "Php", UserRole.BACKEND));
            builder.HasData(new Tag(12, "Java", UserRole.BACKEND));
            builder.HasData(new Tag(13, ".NET Core", UserRole.BACKEND));
            builder.HasData(new Tag(14, "Node.js", UserRole.BACKEND));
            builder.HasData(new Tag(15, "MySQL", UserRole.BACKEND));
            builder.HasData(new Tag(16, "PostgreSQL", UserRole.BACKEND));
            builder.HasData(new Tag(17, "MS SQL", UserRole.BACKEND));
            builder.HasData(new Tag(18, "Rabbit MQ", UserRole.BACKEND));
            builder.HasData(new Tag(19, "OAuth", UserRole.BACKEND));
            builder.HasData(new Tag(20, "Kafka", UserRole.BACKEND));
            builder.HasData(new Tag(21, "Docker", UserRole.BACKEND));
            builder.HasData(new Tag(22, "Mongo DB", UserRole.BACKEND));
        }
    }
}
