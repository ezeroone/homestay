using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eZeroOne.Common
{
    public class RoleNames
    {
        public const string Admin = "Admin";
        public const string Client = "Client";
        public const string Visitor = "Visitor";
        public const string All = "All";
    }
    public enum UserRoles
    {
        Admin = 1,
        Client = 2,
        Visitor = 3,
        All = 4
    }
    public enum PromotionTypes
    {
        Daily=1,
        Weekly=2,
        Monthly=3
    }
}
