using System.Collections.Generic;
using System.Threading.Tasks;
using core.Entities.OrderAggregate;

namespace core.Interfaces
{
    public interface IOrderService
    {
         Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId,
         Address shippingAddres);
        
         Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
         
         Task<Order> GetOrderByIdAsync(int Id,string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();

    }
}