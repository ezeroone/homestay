using System.Data.Entity.ModelConfiguration;

namespace eZeroOne.Domain.Configurations
{
    public class RoleEntityConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleEntityConfiguration()
        {
            this.HasKey(role => role.RoleId);
            this.Property(r => r.Name).IsRequired();
        }
    }
}