using GameOria.Domains.Entities.Identity;
using GameOria.Domains.Entities.Stores;
using GameOria.Domains.Entities.Users;
using GameOria.Domains.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameOria.Infrastructure.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("ApplicationUsers");

            builder.HasKey(u => u.ID);

            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.EmailAddress)
                   .HasMaxLength(200);

            builder.Property(u => u.PasswordHash)
                   .HasMaxLength(500);

            builder.Property(u => u.MobileNumber)
                   .HasMaxLength(20);

            builder.Property(u => u.Role)
                   .HasConversion(
                        v => v.ToString(),
                        v => (Roles)Enum.Parse(typeof(Roles), v))
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasDiscriminator<string>("UserType")
                   .HasValue<ApplicationUser>("Admin")
                   .HasValue<OrganizerUser>("Organizer")
                   .HasValue<CustomerUser>("Customer");



        }
    }
}
