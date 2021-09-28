using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;
using WebShop.API.Repository;
using Xunit;

namespace WebShop.Tests.RepositoryTests
{
    public class OrderRepositoryTests
    {
        private DbContextOptions<WebShopContext> _options;
        private readonly WebShopContext _context;
        private readonly OrderRepository _sut;


        public OrderRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopContext>()
                .UseInMemoryDatabase(databaseName: "WebShopTest")
                .Options;

            _context = new WebShopContext(_options);

            _sut = new OrderRepository(_context);
        }
        [Fact]
        public async Task GetAllOrders_ShouldReturnListOfOrders_WhenOrdersExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.Order.Add(new Order
            {
                Id = 1,
                ShippingAddress = new Address { Id = 1, Customer = new Customer { Id = 1 }, CustomerId = 1 },
                BillingAddress = new Address { Id = 2, CustomerId = 1 },
                ShipmentAddressId = 1,
                BillingAddressId = 2,
                CreateDate = DateTime.Now,
                OrderItems = new()
            });
            _context.Order.Add(new Order
            {
                Id = 2,
                ShippingAddress = new Address { Id = 3, Customer = new Customer { Id = 2 }, CustomerId = 2 },
                BillingAddress = new Address { Id = 4, CustomerId = 2 },
                ShipmentAddressId = 3,
                BillingAddressId = 4,
                CreateDate = DateTime.Now,
                OrderItems = new()
            });
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            var result = await _sut.GetAllOrders();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<Order>>(result);
            Assert.Equal(2, result.Count);
            #endregion
        }
        [Fact]
        public async Task GetAllOrders_ShouldReturnEmptyListOfOrders_WhenNoOrdersExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            #endregion
            #region Act
            var result = await _sut.GetAllOrders();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<Order>>(result);
            Assert.Empty(result);
            #endregion
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnTheOrder_IfOrderExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int orderId = 1;
            _context.Order.Add(new Order
            {
                Id = orderId,
                ShippingAddress = new Address { Id = 1, Customer = new Customer { Id = 1 }, CustomerId = 1 },
                BillingAddress = new Address { Id = 2, CustomerId = 1 },
                ShipmentAddressId = 1,
                BillingAddressId = 2,
                CreateDate = DateTime.Now,
                OrderItems = new List<OrderItem>
                { new OrderItem
                    { OrderId = orderId, Id = 1 },
                 new OrderItem
                    {OrderId = orderId, Id = 2 }}
            });
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            var result = await _sut.GetOrderById(orderId);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(orderId, result.Id);
            Assert.Equal(2, result.OrderItems.Count);
            Assert.NotEmpty(result.OrderItems);
            #endregion
        }
        [Fact]
        public async Task GetOrderById_ShouldReturnNull_IfOrderDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int orderId = 1;
            #endregion
            #region Act
            var result = await _sut.GetOrderById(orderId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
        [Fact]
        public async Task CreateOrder_ShouldAddIdToOrder_WhenSavingToDatebase()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            Order order = new()
            {
                ShippingAddress = new Address { Id = 1, Customer = new Customer { Id = 1 }, CustomerId = 1 },
                BillingAddress = new Address { Id = 2, CustomerId = 1 },
                ShipmentAddressId = 1,
                BillingAddressId = 2,
                CreateDate = DateTime.Now,
                OrderItems = new()
            };
            #endregion
            #region Act
            var result = await _sut.CreateOrder(order);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(expectedId, result.Id);
            #endregion
        }
        [Fact]
        public async Task CreateOrder_ShouldFailToAddNewOrder_WhenAddingOrderWithExistingId()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            Order order = new()
            {
                Id = 1,
                ShippingAddress = new Address { Id = 1, Customer = new Customer { Id = 1 }, CustomerId = 1 },
                BillingAddress = new Address { Id = 2, CustomerId = 1 },
                ShipmentAddressId = 1,
                BillingAddressId = 2,
                CreateDate = DateTime.Now,
                OrderItems = new()
            };

            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            Func<Task> action = async () => await _sut.CreateOrder(order);
            #endregion
            #region Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("already been added", ex.Message);
            #endregion
        }
        [Fact]
        public async Task UpdateOrder_ShouldChangeValuesOnOrder_WhenOrderExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int orderId = 1;
            Order order = new()
            {
                Id = orderId,
                ShippingAddress = new Address { Id = 1},
                BillingAddress = new Address { Id = 2},
                ShipmentAddressId = 1,
                BillingAddressId = 2,
                OrderItems = new()
            };
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            Order updateOrder = new()
            {
                Id = orderId,
                ShipmentAddressId = 2,
                BillingAddressId = 2,
                OrderItems = new List<OrderItem>
                { new OrderItem
                    { OrderId = orderId, Id = 1 },
                 new OrderItem
                    {OrderId = orderId, Id = 2 }}
            };
            #endregion
            #region Act
            var result = await _sut.UpdateOrder(orderId, updateOrder);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(orderId, result.Id);
            Assert.Equal(updateOrder.ShipmentAddressId, result.ShipmentAddressId);
            Assert.Equal(updateOrder.BillingAddressId, result.BillingAddressId);
            Assert.True(result.UpdatedDate > DateTime.MinValue);
            Assert.Empty(result.OrderItems);

            #endregion
        }
        [Fact]
        public async Task UpdateOrder_ShouldReturnNull_WhenOrderDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int orderId = 1;
            Order updateOrder = new()
            {
                Id = orderId,
                ShippingAddress = new Address { Id = 1, Customer = new Customer { Id = 1 }, CustomerId = 1 },
                BillingAddress = new Address { Id = 2, CustomerId = 1 },
                ShipmentAddressId = 1,
                BillingAddressId = 2,
                CreateDate = DateTime.Now,
                OrderItems = new()
            };
            #endregion
            #region Act
            var result = await _sut.UpdateOrder(orderId, updateOrder);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion

        }

    }
}
