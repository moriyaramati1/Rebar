namespace Rebar.Models
{
    public class Accountancy
    {
        public List<ClientOrder> orders { set; get; }
        public double totalDailyIncome { set; get; }

        public Accountancy()
        {
            this.orders = new List<ClientOrder>();
            this.totalDailyIncome = 0;
        }

        public void Reset()
        {
            this.orders.Clear();
            this.totalDailyIncome = 0;
        }
    }
}
