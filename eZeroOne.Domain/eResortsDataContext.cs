using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;


namespace eZeroOne.Domain
{
    public class eResortsEntities : DbContext
    {
        public DbSet<AccommodationStandard> AccommodationStandards { get; set; }
        public DbSet<AccommodationType> AccommodationTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingCart> BookingCarts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Dining> Dinings { get; set; }
        public DbSet<EmailAttachment> EmailAttachments { get; set; }
        public DbSet<EmailNotification> EmailNotifications { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<InvoiceNumber> InvoiceNumbers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<MainCity> MainCities { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Notification> Notifications { get; set; }
       // public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PointOfInterest> PointOfInterests { get; set; }
        public DbSet<PurposeOfVisiting> PurposeOfVisitings { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TravelBy> TravelBies { get; set; }
       // public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<UserMenuPermission> UserMenuPermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersCompany> UsersCompanies { get; set; }
        public DbSet<UsersInRole> UsersInRoles { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Translator> Translators { get; set; }
        public DbSet<FeatureCategory> FeatureCategories { get; set; }
        public DbSet<BannerImage> BannerImages { get; set; }

        public DbSet<DistrictImage> DistrictImages { get; set; }

        public DbSet<YoutubeUrl> YoutubeUrls { get; set; }

        public DbSet<VisitorReview> VisitorReviews { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }

        public eResortsEntities()
            : base("eResortDB")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
        }
        static eResortsEntities()
        {
            //Calling the Method to ReCreate the database
            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<eResortsDataContext>());

            // Database.SetInitializer(new CustomDatabaseInitializer());

            //this line will disbale the code first database initialization feature
            Database.SetInitializer<eResortsEntities>(null);

            //Database.SetInitializer(new CreateDatabaseIfNotExists<eResortsDataContext>());
        }
    }
   
}
