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
            //modelBuilder.Entity<Address>().HasData(
            //new Address
            //{
            //});
            #endregion

            #region Category
            //modelBuilder.Entity<Category>().HasData(
            //new Category
            //{
            //});
            #endregion

            #region Customer
            //modelBuilder.Entity<Customer>().HasData(
            //new Customer
            //{
            //});
            #endregion

            #region Image
            //modelBuilder.Entity<Image>().HasData(
            //new Image
            //{
            //});
            #endregion

            #region Order
            //modelBuilder.Entity<Order>().HasData(
            //new Order
            //{
            //});
            #endregion

            #region OrderItem
            //modelBuilder.Entity<OrderItem>().HasData(
            //new OrderItem
            //{
            //});
            #endregion

            #region Product
            //modelBuilder.Entity<Product>().HasData(
            //new Product
            //{
            //});
            #endregion

            #region MyRegion
            //modelBuilder.Entity<ZipCity>().HasData(
            //new ZipCity
            //{
            //});
            #endregion
        }
    }
}
