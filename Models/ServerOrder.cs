namespace Rebar.Models
{
    public class ServerOrder
    {
        public Guid uId { set; get; }
        //public record struct OrderItem(Guid shakeId, string size, double price);
        public List<OrderItem>? orderItems { set; get; }
        public double totalPrice { set; get; }
        public string customerName { set; get; }
        public DateTime date { set; get; }
        public double? discount { set; get; }


        public ServerOrder(string name, List<OrderItem>? items)
        {
            this.uId = Guid.NewGuid();
            this.orderItems = items;
            this.customerName = name;
            this.date = DateTime.Now;
            this.totalPrice = 0;
        }

        public void CalculatePayment()
        {
            foreach(OrderItem item in this.orderItems)
            {
                if (item.discount != null)
                {
                    double precentAfterDiscount = (double)(100 - item.discount) / 100;
                    this.totalPrice += item.price * precentAfterDiscount;
                }
                else
                {
                    this.totalPrice += item.price;

                }
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

