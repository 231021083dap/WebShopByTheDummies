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
            //modelBuilder.Entity<Address>().HasData(
            //new Address
            //{
            //});

            //modelBuilder.Entity<Category>().HasData(
            //new Category
            //{
            //});

            //modelBuilder.Entity<Customer>().HasData(
            //new Customer
            //{
            //});

            //modelBuilder.Entity<Image>().HasData(
            //new Image
            //{
            //});

            //modelBuilder.Entity<Order>().HasData(
            //new Order
            //{
            //});

            //modelBuilder.Entity<OrderItem>().HasData(
            //new OrderItem
            //{
            //});

            //modelBuilder.Entity<Product>().HasData(
            //new Product
            //{
            //});

            //modelBuilder.Entity<User>().HasData(
            //new User
            //{
            //});

            //modelBuilder.Entity<ZipCity>().HasData(
            //new ZipCity
            //{
            //});
        }
    }
}
