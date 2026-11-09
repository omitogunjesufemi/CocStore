using Api.DTOs;
using Api.Models;
using Api.Repositories;
using Api.Services.Interfaces;
using Api.Services.Results;

namespace Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order?> CreateOrder(CUOrderDTO orderDTO)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var order = new Order
                {
                    OrderAmount = 0,
                    OrderDate = DateTime.UtcNow,
                    OrderItems = new List<OrderItem>()
                };

                foreach (var itemDTO in orderDTO.OrderItems)
                {
                    var product = await _unitOfWork.Products.GetProductWithCategoryAsync(itemDTO.ProductId);
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
                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.CommitAsync();
                return order;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return null;
            }
            
        }

        public async Task<Order?> GetOrderById(Guid id)
        {
            var order = await _unitOfWork.Orders.GetOrderWithOrderItems(id);
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _unitOfWork.Orders.GetAllAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersWithItems()
        {
            return await _unitOfWork.Orders.GetOrdersWithOrderItems();
        }

        public async Task<StatusRecord> UpdateOrder(Guid orderId, CUOrderDTO orderDTO)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
                decimal unitPrice = 0;
                if (order == null)
                    return new StatusRecord(StatusEnum.OrderNotFound, "Order not found.");

                order.OrderDate = DateTime.UtcNow;

                foreach (var item in orderDTO.OrderItems)
                {
                    var product = await _unitOfWork.Products.GetProductWithNoTracking(item.ProductId);
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
                _unitOfWork.Orders.Update(order);
                await _unitOfWork.CommitAsync();
                return new StatusRecord(StatusEnum.Updated, "Order has been updated.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return new StatusRecord(StatusEnum.FailedDBTransaction, "Order could not be proceed, try again.");
            }
        }

        public async Task<StatusRecord> DeleteOrder(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
                return new StatusRecord(StatusEnum.OrderNotFound, "Order not found.");

            _unitOfWork.Orders.Delete(order);
            return new StatusRecord(StatusEnum.Deleted, "Order has been deleted.");
        }

        public async Task<StatusRecord> AddOrderItem(Guid orderId, CUOrderItemDTO orderItemDTO)
        {
            var order = await _unitOfWork.Orders.GetOrderWithOrderItemsWT(orderId);
            if (order == null)
                return new StatusRecord(StatusEnum.OrderNotFound, "Order not found.");

            var product = await _unitOfWork.Products.GetProductWithNoTracking(orderItemDTO.ProductId);
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
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.Orders.SaveAsync();
            return new StatusRecord(StatusEnum.Updated, "Order Item has been added.");
        }

        //public async Task<StatusRecord> DeleteOrderItem(Guid orderId)
        //{
        //    var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
        //    if (order == null)
        //        return new StatusRecord(StatusEnum.OrderNotFound, "Order not found.");

        //    _unitOfWork.Orders.Delete(order);
        //    return new StatusRecord(StatusEnum.Deleted, "Order has been deleted.");
        //}
    }
}
