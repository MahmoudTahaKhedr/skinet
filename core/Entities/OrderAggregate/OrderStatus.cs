using System.Runtime.Serialization;

namespace core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
   
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Payment Received")]
        PaymentReceived,

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed
    }
}