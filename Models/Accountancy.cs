namespace Rebar.Models
{
    public class Accountancy
    {
        public List<ServerOrder> orders { set; get; }
        public double totalDailyIncome { set; get; }

        public Accountancy()
        {
            this.orders = new List<ServerOrder>();
            this.totalDailyIncome = 0;
        }

        public void Reset()
        {
            this.orders.Clear();
            this.totalDailyIncome = 0;
        }
    }
}
