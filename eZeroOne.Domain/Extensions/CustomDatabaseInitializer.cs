using eZeroOne.Common;
using System.Data.Entity;

namespace eZeroOne.Domain.Extensions
{
    public class CustomDatabaseInitializer : DropCreateDatabaseIfModelChanges<eResortsEntities>
    {
        protected override void Seed(eResortsEntities context)
        {
            //Adding the list of the existing roles to the database
            context.Roles.Add(new Role { Name = RoleNames.Admin });
            context.Roles.Add(new Role { Name = RoleNames.Visitor });
            context.Roles.Add(new Role { Name = RoleNames.Client });
            context.Roles.Add(new Role { Name = RoleNames.All });

            context.SaveChanges();

        }

    }
}