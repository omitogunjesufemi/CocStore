using Api.DTOs;
using Api.Models;
using Api.Repositories;
using Api.Services.Interfaces;
using Api.Services.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService (IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Order?> CreateOrder(CUOrderDTO orderDTO)
        {
            var order = new Order
            {
                OrderAmount = 0,
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };

            foreach (var itemDTO in orderDTO.OrderItems)
            {
                var product = await _productRepository.GetProductWithCategoryAsync(itemDTO.ProductId);
                if (product == null)
                    return null;

                var orderItem = new OrderItem
                {
                    ProductId = itemDTO.ProductId,
                    Quantity = itemDTO.Quantity,
                    UnitPrice = product.Price,
                };
                order.OrderItems.Add(orderItem);
            }
            order.OrderAmount = order.OrderItems.Sum(or => or.Quantity * or.UnitPrice);
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveAsync();
            return order;
        }

        public async Task<Order?> GetOrderById(Guid id)
        {
            var order = await _orderRepository.GetOrderWithOrderItems(id);
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersWithItems()
        {
            return await _orderRepository.GetOrdersWithOrderItems();
        }

        public async Task<StatusRecord> UpdateOrder(Guid orderId, CUOrderDTO orderDTO)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            decimal unitPrice = 0;
            if (order == null)
                return new StatusRecord(StatusEnum.OrderNotFound, "Order not found.");

            order.OrderDate = DateTime.UtcNow;
            
            foreach (var item in orderDTO.OrderItems)
            {
                var product = await _productRepository.GetProductWithNoTracking(item.ProductId);
                if (product == null)
                    return new StatusRecord(StatusEnum.InvalidProduct, "Product not found.");
                unitPrice = product.Price;
            }
            order.OrderItems.Clear();
            order.OrderItems = orderDTO.OrderItems.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = unitPrice
            }).ToList();

            order.OrderAmount = order.OrderItems.Sum(i => i.Quantity * i.UnitPrice);
            _orderRepository.Update(order);
            await _orderRepository.SaveAsync();
            return new StatusRecord(StatusEnum.Updated, "Order has been updated.");
        }

        public async Task<StatusRecord> DeleteOrder(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return new StatusRecord(StatusEnum.OrderNotFound, "Order not found.");

            _orderRepository.Delete(order);
            return new StatusRecord(StatusEnum.Deleted, "Order has been deleted.");
        }

        public async Task<StatusRecord> AddOrderItem(Guid orderId, CUOrderItemDTO orderItemDTO)
        {
            var order = await _orderRepository.GetOrderWithOrderItemsWT(orderId);
            if (order == null)
                return new StatusRecord(StatusEnum.OrderNotFound, "Order not found.");

            var product = await _productRepository.GetProductWithNoTracking(orderItemDTO.ProductId);
            if (product == null)
                return new StatusRecord(StatusEnum.InvalidProduct, "Product not found.");
            var productInItem = false;

            order.OrderDate = DateTime.UtcNow;

            foreach (var item in order.OrderItems)
            {
                if (item.ProductId == orderItemDTO.ProductId)
                {
                    productInItem = true;
                    item.Quantity = orderItemDTO.Quantity;
                    item.UnitPrice = product.Price;
                }
            }
            if (!productInItem)
            {
                var newOrderItem = new OrderItem
                {
                    ProductId = orderItemDTO.ProductId,
                    Quantity = orderItemDTO.Quantity,
                    UnitPrice = product.Price
                };
                order.OrderItems.Add(newOrderItem);
            }
            order.OrderAmount = order.OrderItems.Sum(i => i.Quantity * i.UnitPrice);
            _orderRepository.Update(order);
            await _orderRepository.SaveAsync();
            return new StatusRecord(StatusEnum.Updated, "Order Item has been added.");
        }

        //public async Task<StatusRecord> DeleteOrderItem(Guid orderId)
        //{
        //    var order = await _orderRepository.GetByIdAsync(orderId);
        //    if (order == null)
        //        return new StatusRecord(StatusEnum.OrderNotFound, "Order not found.");

        //    _orderRepository.Delete(order);
        //    return new StatusRecord(StatusEnum.Deleted, "Order has been deleted.");
        //}
    }
}
