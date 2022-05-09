using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using core.Entities.OrderAggregate;
using core.Interfaces;
using core.Specification;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
       
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
           
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
             return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();

        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddres)
        {
            //get basket from the repo
            var basket = await _basketRepo.GetBasketAsync(basketId);
            //get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }
            // get delivary method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            // calc Subtotal
            var subtotal = items.Sum(item => item.price * item.Quantity);
            //create order
            var order = new Order(items, buyerEmail, shippingAddres, deliveryMethod, subtotal);
             _unitOfWork.Repository<Order>().Add(order);

            // TODO: save to db
            var result = await _unitOfWork.Complete();
            if (result<=0) return null;
            //delete basket
            await _basketRepo.DeleteBasketAsync(basketId);
            // return order
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int Id, string buyerEmail )
        {
             var spec = new OrdersWithItemsAndOrderingSpecification(Id, buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}