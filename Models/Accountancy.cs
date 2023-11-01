namespace Rebar.Models
{
    public class Accountancy
    {
        public List<ServerOrder> orders { set; get; }
        public double totalDailyIncome { set; get; }

        private const int _manager_password = 1234;

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
        public bool CheckAuthentication(int password)
        {
            if (password != _manager_password)
                throw new Exception("Access denied, incorrect password");

            return true;
        }

    }
}
