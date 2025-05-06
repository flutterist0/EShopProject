using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
	public class AppDbContext:DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=sql.bsite.net\MSSQL2016;Database=elmir001_EShopDb;User ID=elmir001_EShopDb; Password=5867640e; Trusted_Connection=false;TrustServerCertificate=true");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //optionsBuilder.EnableSensitiveDataLogging();
        }
        //Server=DESKTOP-K0V6ESA\SQLEXPRESS;Database=EShopDb;Trusted_Connection=true;TrustServerCertificate=true
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
