using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Repository;

namespace WebShop.API.Services
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetAllOrders();
        Task<OrderResponse> GetOrderById(int orderId);
        Task<OrderResponse> CreateOrder(NewOrder newOrder);
        Task<OrderItemResponse> UpdateOrderItem(int orderItemId, UpdateOrderItem updateOrderItem);
        Task<OrderResponse> UpdateOrder(int orderId, UpdateOrder updateOrder);
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IAddressRepository _addressRepository;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IAddressRepository addressRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _addressRepository = addressRepository;
        }

        public async Task<OrderResponse> CreateOrder(NewOrder newOrder)
        {
            Order order = new Order
            {
                CreateDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ShipmentAddressId = newOrder.ShipmentAddressId,
                BillingAddressId = newOrder.BillingAddressId,

            };
            order = await _orderRepository.CreateOrder(order);
            Address Shipmentaddress = await _addressRepository.GetAddressById(order.ShipmentAddressId);
            Address Billingaddress;
            if (order.ShipmentAddressId != order.BillingAddressId)
            {
                Billingaddress = await _addressRepository.GetAddressById(order.BillingAddressId);
            }
            else
            {
                Billingaddress = Shipmentaddress;
            }
            return order == null ? null : new OrderResponse
            {
                Id = order.Id,
                OrderDate = order.CreateDate,
                ShipmentAddress = new AddressResponse
                {
                    Id = Shipmentaddress.Id,
                    CustomerId = Shipmentaddress.Customer.Id,
                    Customer = new CustomerResponse
                    {
                        FirstName = Shipmentaddress.Customer.FirstName,
                        MiddleName = Shipmentaddress.Customer.MiddleName,
                        LastName = Shipmentaddress.Customer.LastName
                    },
                    StreetName = Shipmentaddress.StreetName,
                    Number = Shipmentaddress.Number,
                    Zipcode = Shipmentaddress.ZipCity.Id,
                    City = Shipmentaddress.ZipCity.City,
                    Country = Shipmentaddress.Country,

                },
                Email = order.ShippingAddress.Customer.User.Email,
                BillingAddress = new AddressResponse
                {
                    Id = Billingaddress.Id,
                    StreetName = Billingaddress.StreetName,
                    Number = Billingaddress.Number,
                    Zipcode = Billingaddress.ZipCity.Id,
                    City = Billingaddress.ZipCity.City,
                    Country = Billingaddress.Country
                    
                },
                OrderItems = order.OrderItems.Select(i => new OrderItemOrderResponse
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Amount = i.Amount,
                    CurrentPrice = i.CurrentPrice,
                }).ToList()
            };
        }

        public async Task<List<OrderResponse>> GetAllOrders()
        {
            List<Order> orders = await _orderRepository.GetAllOrders();
            return orders == null ? null : orders.Select(o => new OrderResponse
            {
                Id = o.Id,
                OrderDate = o.CreateDate,
                CustomerName = $"{o.ShippingAddress.Customer.FirstName} {o.ShippingAddress.Customer.MiddleName} {o.ShippingAddress.Customer.LastName}",
                Email = o.ShippingAddress.Customer.User.Email
            }).ToList();
        }

        public async Task<OrderResponse> GetOrderById(int orderId)
        {
            Order order = await _orderRepository.GetOrderById(orderId);

            return order == null ? null : new OrderResponse
            {
                Id = order.Id,
                OrderDate = order.CreateDate,
                CustomerId = order.ShippingAddress.Customer.Id,
                CustomerName = $"{order.ShippingAddress.Customer.FirstName} {order.ShippingAddress.Customer.MiddleName} {order.ShippingAddress.Customer.LastName}",
                Email = order.ShippingAddress.Customer.User.Email,
                OrderItems = order.OrderItems.Select(oi => new OrderItemOrderResponse
                {
                    ProductId = oi.Product.Id,
                    ProductName = oi.Product.Name,
                    CurrentPrice = oi.CurrentPrice,
                    Amount = oi.Amount,

                }).ToList(),
                ShipmentAddress = new AddressResponse
                {
                    StreetName = order.ShippingAddress.StreetName,
                    Number = order.ShippingAddress.Number,
                    Floor = order.ShippingAddress.Floor,
                    Zipcode = order.ShippingAddress.ZipCity.Id,
                    City = order.ShippingAddress.ZipCity.City
                },
                BillingAddress = new AddressResponse
                {
                    StreetName = order.BillingAddress.StreetName,
                    Number = order.BillingAddress.Number,
                    Floor = order.BillingAddress.Floor,
                    Zipcode = order.BillingAddress.ZipCity.Id,
                    City = order.BillingAddress.ZipCity.City
                }
            };
        }

        public async Task<OrderResponse> UpdateOrder(int orderId, UpdateOrder updateOrder)
        {
            Order order = new()
            {
                UpdatedDate = DateTime.Now,
                ShipmentAddressId = updateOrder.ShipmentAddressId,
                BillingAddressId = updateOrder.BillingAddressId
                
            };
            order = await _orderRepository.UpdateOrder(orderId, order);
            return order == null ? null : new OrderResponse
            {
                Id = order.Id,
                BillingAddress = new AddressResponse
                {
                    Id = order.BillingAddressId
                },
                ShipmentAddress = new AddressResponse
                {
                    Id = order.ShipmentAddressId
                }
            };
        }
        public async Task<OrderItemResponse> UpdateOrderItem(int orderItemId, UpdateOrderItem updateOrderItem)
        {
            OrderItem orderItem = new()
            {
                Amount = updateOrderItem.Amount,
                CurrentPrice = updateOrderItem.CurrentPrice
            };
            orderItem = await _orderItemRepository.UpdateOrderItem(orderItemId, orderItem);
            return orderItem == null ? null : new OrderItemResponse
            {
                Id = orderItem.Id,
                Amount = orderItem.Amount,
                CurrentPrice = orderItem.CurrentPrice
            };
        }

    }
}
