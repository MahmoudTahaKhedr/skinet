using core.Entities.OrderAggregate;

namespace core.Specification
{
    public class OrderByPaymentIntentWithItemsSpecification : BaseSpecification<Order>
    {
                public OrderByPaymentIntentWithItemsSpecification(string paymentIntentId) 
            : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }

    }
}