using Moq;
using System.Collections.Generic;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Repository;
using WebShop.API.Services;
using Xunit;

namespace WebShop.Tests
{
    public class OrderServiceTests
    {
        private readonly OrderService _sut;
        private readonly Mock<IOrderRepository> _orderRepository = new Mock<IOrderRepository>();
        private readonly Mock<IOrderItemRepository> _orderItemRepository = new Mock<IOrderItemRepository>();
        private readonly Mock<IAddressRepository> _addressRepository = new Mock<IAddressRepository>();

        public OrderServiceTests()
        {
            _sut = new OrderService(_orderRepository.Object, _orderItemRepository.Object, _addressRepository.Object);
        }

        #region GetAll
        [Fact]
        public async void GetAll_ShouldReturnListOfOrderResponses_WhenOrdersExist()
        {
            //#region Arrange
            //List<Order> Orders = new List<Order>();
            //Orders.Add(new Order
            //{
            //    Id = 1,
            //    CreateDate = DateTime.Now(),

            //});

            //Orders.Add(new Order
            //{

            //});

            //_orderRepository
            //    .Setup(a => a.GetOrderById())
            //    .ReturnsAsync(Orders);
            //#endregion

            //#region Act
            //var result = await _sut.GetAllOrders();
            //#endregion

            //#region Assert
            //Assert.NotNull(result);
            //Assert.Equal(2, result.Count);
            //Assert.IsType<List<OrderResponse>>(result);
            //#endregion
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfOrderResponses_WhenNoOrdersExists()
        {
            #region Arrange
            List<Order> Orders = new List<Order>();

            _orderRepository
                .Setup(a => a.GetAllOrders())
                .ReturnsAsync(Orders);
            #endregion

            #region Act
            var result = await _sut.GetAllOrders();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<OrderResponse>>(result);
            #endregion
        }
        #endregion

        #region GetById
        [Fact]
        public async void GetById_ShouldReturnAnOrderResponse_WhenOrderExists()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            {
                #region Arrange
                int orderId = 1;

                _orderRepository
                    .Setup(a => a.GetOrderById(It.IsAny<int>()))
                    .ReturnsAsync(() => null);
                #endregion

                #region Act
                var result = await _sut.GetOrderById(orderId);
                #endregion

                #region Assert
                Assert.Null(result);
                #endregion
            }
        }
        #endregion

        #region Create
        [Fact]
        public async void Create_ShouldReturnOrderResponse_WhenCreateIsSuccess()
        {
            //#region Arrange
            //int orderId = 1;

            //NewOrder newOrder = new NewOrder
            //{
            //    ShipmentAddressId = 1,
            //    BillingAddressId = 1
            //};

            //Order order = new Order
            //{
            //    Id = orderId,
            //    CreateDate = DateTime.Now,
            //    //UpdatedDate = 
            //    //OrderItems = 
            //    //ShippingAddress = 
            //    ShipmentAddressId = 1,
            //    BillingAddressId = 1
            //};

            //_orderRepository
            //    .Setup(a => a.CreateOrder(It.IsAny<Order>()))
            //    .ReturnsAsync(order);
            //#endregion

            //#region Act
            //var result = await _sut.CreateOrder(newOrder);
            //#endregion

            //#region Assert
            //Assert.NotNull(result);
            //Assert.IsType<OrderResponse>(result);
            //Assert.Equal(orderId, result.Id);
            //Assert.Equal(newOrder.ShipmentAddressId, result.ShipmentAddress);
            //Assert.Equal(newOrder.Number, result.Number);
            //Assert.Equal(newOrder.Floor, result.Floor);
            //Assert.Equal(newOrder.Zipcode, result.Zipcode);
            //Assert.Equal(newOrder.Country, result.Country);
            //Assert.Equal(newAddress.CustomerId, result.Customer.Id); // TODO* Fejler på Customer.Id
            //#endregion
        }
        #endregion

        #region Update
        [Fact]
        public async void Update_ShouldReturnUpdateOrderItemResponse_WhenUpdateIsSuccess()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenOrderItemDoesNotExist()
        {
            #region Arrange

            UpdateOrderItem updateOrderItem = new UpdateOrderItem
            {
                Amount = 1,
                CurrentPrice = 123,
                ProductId = 1
            };

            int OrderItemId = 1;

            _orderItemRepository
                .Setup(a => a.UpdateOrderItem(It.IsAny<int>(), It.IsAny<OrderItem>()))
                .ReturnsAsync(() => null);
            #endregion

            #region Act
            var result = await _sut.UpdateOrderItem(OrderItemId, updateOrderItem);
            #endregion

            #region Assert
            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async void Update_ShouldReturnUpdateOrderResponse_WhenUpdateIsSuccess()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            #region Arrange
            OrderItem order = new OrderItem
            {
                ProductId = 1,
                CurrentPrice = 123,
                Amount = 1
            };

            UpdateOrder updateOrder = new UpdateOrder
            {
                BillingAddressId = 1,
                ShipmentAddressId = 1
            };

            int OrderId = 1;

            _orderRepository
                .Setup(a => a.UpdateOrder(It.IsAny<int>(), It.IsAny<Order>()))
                .ReturnsAsync(() => null);
            #endregion

            #region Act
            var result = await _sut.UpdateOrder(OrderId, updateOrder);
            #endregion

            #region Assert
            Assert.Null(result);
            #endregion
        }
        #endregion
    }
}
