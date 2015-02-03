using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace eZeroOne.Domain.Configurations
{
    public class PasswordResetTokenEntityConfiguration : EntityTypeConfiguration<PasswordResetToken>
    {
        public PasswordResetTokenEntityConfiguration()
        {
            this.HasKey(p => p.TokenId);
            this.Property(p => p.TokenId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

          
        }
    }
}