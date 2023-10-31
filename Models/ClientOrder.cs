namespace Rebar.Models
{
    public class ClientOrder
    {
        public Guid uId { set; get; }
        public record struct OrderItem(Guid shakeId, string size, double price);
        public List<OrderItem> orderItems { set; get; }
        public double totalPrice { set; get; }
        public string customerName { set; get; }
        public DateTime date { set; get; }
        public double? discount { set; get; }


        public ClientOrder(string name)
        {
            this.uId = Guid.NewGuid();
            this.orderItems = new List<OrderItem>();
            this.customerName = name;
            this.date = DateTime.Now;
            this.totalPrice = 0;
        }

        public void AddToOrder(Guid shakeId, string size, double price)
        {
            this.orderItems.Add(new OrderItem(shakeId, size, price));

            if (discount != null)
            {
                double precentAfterDiscount = (double)(100 - discount) / 100;
                this.totalPrice += price * precentAfterDiscount;
            }
            else
            {
                this.totalPrice += price;

            }
        }

        public DateTime CalculateFinishTime()
        {
            // Assume that the preparation time of a smoothie is 2 minutes 

            var onePreparationTime = 2;
            var minutes = onePreparationTime * orderItems.Count;

            return date.AddMinutes(minutes);
        }
    }
}
}
