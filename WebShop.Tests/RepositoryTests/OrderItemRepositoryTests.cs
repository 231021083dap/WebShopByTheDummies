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
    public class OrderItemRepositoryTests
    {
        private DbContextOptions<WebShopContext> _options;
        private readonly WebShopContext _context;
        private readonly OrderItemRepository _sut;


        public OrderItemRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopContext>()
                .UseInMemoryDatabase(databaseName: "WebShopTest")
                .Options;

            _context = new WebShopContext(_options);

            _sut = new OrderItemRepository(_context);
        }
       
        [Fact]
        public async Task CreateOrderItem_ShouldAddIdToOrderItem_WhenSavingToDatebase()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            //int expectedId = 1;
            List<OrderItem> orderItems = new();

            OrderItem item1 = new () { Product = new() { Id = 1 }, ProductId = 1, Id = 1, Amount = 13, CurrentPrice = 1337, OrderId = 1 };
            OrderItem item2 = new () { Product = new() { Id = 2 }, ProductId = 2, Id = 2, Amount = 14, CurrentPrice = 2337, OrderId = 1 };
            OrderItem item3 = new () { Product = new() { Id = 3 }, ProductId = 3, Id = 3, Amount = 15, CurrentPrice = 3337, OrderId = 1 };

            orderItems.Add(item1);
            orderItems.Add(item2);
            orderItems.Add(item3);
            #endregion
            #region Act
            var result = await _sut.CreateOrderItem(orderItems);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<OrderItem>>(result);
            Assert.NotEmpty(result);
            
            #endregion
        }
        [Fact]
        public async Task CreateOrderItem_ShouldFailToAddNewOrderItem_WhenAddingOrderItemWithExistingId()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            List<OrderItem> orderItems = new();
            OrderItem orderItem = new()
            {
                Id = 1,
                Amount = 2,
                CurrentPrice = 1337,
                Product = new() { Id = 1, Name = "Tennisbold"},
                ProductId = 1
            };
            orderItems.Add(orderItem);

            _context.OrderItem.AddRange(orderItems);
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            Func<Task> action = async () => await _sut.CreateOrderItem(orderItems);
            #endregion
            #region Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("already been added", ex.Message);
            #endregion
        }
        //[Fact]
        //public async Task UpdateOrderItem_ShouldChangeValuesOnOrderItem_WhenOrderItemExists()
        //{
        //    #region Arrange
        //    await _context.Database.EnsureDeletedAsync();
        //    int orderItemId = 1;
        //    OrderItem orderItem = new()
        //    {
        //        Id = orderItemId,
        //        Name = "testOrderItem",
        //        Picture = "testOrderItemPic"
        //    };
        //    _context.OrderItem.Add(orderItem);
        //    await _context.SaveChangesAsync();

        //    OrderItem updateOrderItem = new()
        //    {
        //        Id = orderItemId,
        //        Name = "Test2OrderItem",
        //        Picture = "Test2OrderItemPicture"
        //    };
        //    #endregion
        //    #region Act
        //    var result = await _sut.UpdateOrderItem(orderItemId, updateOrderItem);
        //    #endregion
        //    #region Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<OrderItem>(result);
        //    Assert.Equal(orderItemId, result.Id);
        //    Assert.Equal(updateOrderItem.Name, result.Name);
        //    Assert.Equal(updateOrderItem.Picture, result.Picture);

        //    #endregion
        //}
        //[Fact]
        //public async Task UpdateOrderItem_ShouldReturnNull_WhenOrderItemDoesNotExists()
        //{
        //    #region Arrange
        //    await _context.Database.EnsureDeletedAsync();
        //    int orderItemId = 1;
        //    OrderItem updateOrderItem = new()
        //    {
        //        Id = orderItemId,
        //        Name = "TestOrderItem",
        //        Picture = "TestOrderItemPicture"
        //    };
        //    #endregion
        //    #region Act
        //    var result = await _sut.UpdateOrderItem(orderItemId, updateOrderItem);
        //    #endregion
        //    #region Assert
        //    Assert.Null(result);
        //    #endregion

        //}
        //[Fact]
        //public async Task DeleteOrderItem_ShouldReturnDeletedOrderItem_WhenOrderItemIsDeleted()
        //{
        //    #region Arrange
        //    await _context.Database.EnsureDeletedAsync();
        //    int orderItemId = 1;
        //    OrderItem orderItem = new()
        //    {
        //        Id = orderItemId,
        //        Name = "OrderItemTest",
        //        Picture = "TestPathPicture"

        //    };
        //    _context.OrderItem.Add(orderItem);
        //    await _context.SaveChangesAsync();

        //    #endregion
        //    #region Act
        //    var result = await _sut.DeleteOrderItem(orderItemId);
        //    var deletedAddress = await _sut.GetAllCategories();
        //    #endregion
        //    #region Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<OrderItem>(result);
        //    Assert.Equal(orderItemId, result.Id);

        //    Assert.Empty(deletedAddress);
        //    #endregion


        //}

        //[Fact]
        //public async Task DeleteOrderItem_ShouldReturnNull_WhenOrderItemDoesNotExist()
        //{
        //    #region Arrange
        //    await _context.Database.EnsureDeletedAsync();
        //    int orderItemId = 1;
        //    #endregion
        //    #region Act
        //    var result = await _sut.DeleteOrderItem(orderItemId);
        //    #endregion
        //    #region Assert
        //    Assert.Null(result);
        //    #endregion
        //}
    }
}
