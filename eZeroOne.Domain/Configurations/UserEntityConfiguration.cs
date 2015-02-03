using System.Data.Entity.ModelConfiguration;

namespace eZeroOne.Domain.Configurations
{
    public class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            this.HasKey(u => u.UserId);

            /* configure the required fields */
            this.Property(u => u.Email).IsRequired()
                                  .HasColumnType("varchar")
                                  .HasMaxLength(250);

            this.Property(u => u.Password).IsRequired();

            this.Property(u => u.FirstName).IsRequired()
                                      .HasColumnType("varchar")
                                      .HasMaxLength(50);

            this.Property(u => u.LastName).IsRequired()
                                     .HasColumnType("varchar")
                                     .HasMaxLength(50);

  

        }
    }
}
