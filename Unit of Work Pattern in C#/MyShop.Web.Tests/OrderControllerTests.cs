using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyShop.Domain.Models;
using MyShop.Infrastructure.Repositories;
using MyShop.Web.Controllers;
using MyShop.Web.Models;
using System;

namespace MyShop.Web.Tests {
    [TestClass]
    public class OrderControllerTests {
        [TestMethod]
        public void CanCreateOrderWithCorrectModel() {
            var orderRepository = new Mock<IRepository<Order>>();
            var productRepository = new Mock<IRepository<Product>>();

            var orderController = new OrderController(
                orderRepository.Object,
                productRepository.Object
            );

            var createOrderModel = new CreateOrderModel { 
                Customer = new CustomerModel { 
                    Name = "David Thomas",
                    ShippingAddress = "10301 Wilson Blvd, Blythewood",
                    City = "Blythewood",
                    PostalCode = "29016",
                    Country = "USA"
                }, 
                LineItems = new[] { 
                    new LineItemModel {ProductId=Guid.NewGuid(), Quantity = 2},
                    new LineItemModel {ProductId=Guid.NewGuid(), Quantity = 12},
                }
            };

            orderController.Create(createOrderModel);
            orderRepository.Verify(repository => repository.Add(It.IsAny<Order>()),
                Times.AtMostOnce());
        }
    }
}