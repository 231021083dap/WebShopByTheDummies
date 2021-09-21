using Microsoft.EntityFrameworkCore;
using WebShop.API.Database.Entities;


namespace WebShop.API.Database
{
    public class WebShopContext : DbContext
    {
        public WebShopContext() { }
        public WebShopContext(DbContextOptions<WebShopContext> options) : base(options) { }
        public DbSet<Address> Address { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ZipCity> ZipCity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User

            //ADMIN
            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Email = "admin@admin.dk",
                Password = "admin",
                Role = Helpers.Role.Admin
            },

            //USER - (CUSTOMER)
            new User
            {
                Id = 10,
                Email = "user@user.dk",
                Password = "test",
                Role = Helpers.Role.User
            });

            #endregion

            #region Address

            modelBuilder.Entity<Address>().HasData(
            new Address
            {
                CustomerId = 10,
                Id = 10,
                StreetName = "Nyborggade",
                Number = 34,
                Floor = "2. TV",
                Zipcode = 2100,
                County = "Danmark"
            });

            #endregion

            #region Category

            modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Test category",
                Picture = ""
            });

            #endregion

            #region Customer

            modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                UserId = 10,
                Id = 10,
                FirstName = "Christian",
                MiddleName = "Møller",
                LastName = "Jørgensen"
            });

            #endregion

            #region Image

            modelBuilder.Entity<Image>().HasData(
            new Image
            {
                Id = 1,
                productId = 1,
                Path = "Here/Perfect"
            },
            new Image
            {
                Id = 2,
                productId = 1,
                Path = "Someware/Nice"
            });

            #endregion

            #region Order

            //modelBuilder.Entity<Order>().HasData(
            //new Order
            //{
            //    Id = 1,
            //    OrdreDate = "",
            //    AddressId = 10
            //});

            #endregion

            #region OrderItem

            //modelBuilder.Entity<OrderItem>().HasData(
            //new OrderItem
            //{
            //    OrderId = 1,
            //    Id = 1,
            //    ProductId = 1,
            //    Amount = 2,
            //    CurrentPrice = 
            //});

            #endregion

            #region Product

            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "TestProduct",
                CategoryId = 1,
                Price = 100,
                Description = "MAKE TESTS GREAT AGIAN",
                
            });

            #endregion

            #region ZipCity

            modelBuilder.Entity<ZipCity>().HasData(
            new ZipCity
            {
                Zipcode = 2100,
                City = "Østerbro"
            });

            #endregion
        }
    }
}
