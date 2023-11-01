
namespace Rebar.Models
{
    public class ClientOrder
    {
        public string? customerName { get; set; }
        public List<OrderItem>? orderItems { set; get; }

        public ClientOrder() {
            orderItems = new List<OrderItem>();
        }
        public ClientOrder(string name) {
            customerName = name;
            orderItems = new List<OrderItem>();
        }
        public ClientOrder(string name, List<OrderItem>? items)
        {
            customerName = name;
            orderItems = orderItems;
        }

    }
}
