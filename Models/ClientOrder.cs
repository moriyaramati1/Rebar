using static Rebar.Models.ServerOrder;

namespace Rebar.Models
{
    public class ClientOrder
    {
        public string? customerName { get; set; }
        public List<OrderItem>? orderItems { set; get; }

    }
}
