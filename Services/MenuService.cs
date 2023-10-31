using Rebar.Models;
using Rebar.DataAccess;
using MongoDB.Driver;

namespace Rebar.Services
{
    public class MenuService
    {
        public List<Shake> menuShakes;
        public Accountancy account = new Accountancy();
        public MongoDataBase db = new MongoDataBase();

        public MenuService()
        {
            this.menuShakes = new List<Shake>();
        }

        public void CreateMenu()
        {
            Shake shake1 = new Shake("Oreo", "milkshake made with vanilla ice cream, milk, and Oreo cookies!");
            Shake shake2 = new Shake("Chocolate", "milkshake");
            Shake shake3 = new Shake("Strawberry", "milkshake");
            Shake shake4 = new Shake("Vanilla", "milkshake");
            Shake shake5 = new Shake("Mango", "milkshake");

            this.menuShakes.Add(shake1);
            this.menuShakes.Add(shake2);
            this.menuShakes.Add(shake3);
            this.menuShakes.Add(shake4);
            this.menuShakes.Add(shake5);
            System.Console.WriteLine("PRINT THE MENU");

        }

        public bool CheckValidName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        public void CheckValidParams(string shakeName, string description, double smallPrice = 20.0, double mediumPrice = 30.0, double largePrice = 40.0)
        {
            if (CheckValidName(shakeName) == false)
            {
                throw new Exception("Shake's name field cannot be empty");
            }
            if (CheckValidName(description) == false)
            {
                throw new Exception("Shake's description field cannot be empty");
            }
            if (smallPrice <= 0 || mediumPrice <= 0 || largePrice <= 0)
            {
                throw new Exception("Shake's price must be a valid number");

            }

        }
        public void CheckValidAmount(List<Guid> orderedShakesId)
        {
            if (orderedShakesId.Count > 10)
            {
                throw new Exception("There are more than 10 shakes in this order.");
            }
        }

        public void CheckValidShakes(List<Guid> orderedShakesId)
        {

            foreach (Guid shakeId in orderedShakesId)
            {
                var found = menuShakes.Find(menuShake => menuShake.uId == shakeId);
                if (found == null)
                {
                    Console.WriteLine(found);
                    throw new Exception("Shake does not exist in menu");

                }
            }
        }
        public void CheckValidOrder(string customerName, List<Guid> orderedShakesId)
        {
            if (CheckValidName(customerName) == false)
            {
                throw new Exception("Name field cannot be empty");
            }
            try
            {
                CheckValidAmount(orderedShakesId);
                CheckValidShakes(orderedShakesId);
            }
            catch
            {
                throw;
            }

        }
        public void CreateNewShake(string shakeName, string description, double smallPrice, double mediumPrice, double largePrice)
        {
            try
            {
                CheckValidParams(shakeName, description, smallPrice, mediumPrice, largePrice);
                Shake shake = new Shake(shakeName, description, smallPrice, mediumPrice, largePrice);
                this.menuShakes.Add(shake);
            }
            catch
            {
                throw;
            }

        }



        public bool CheckAuthentication()
        {
            Console.WriteLine("enter Your ps");
            return true;
        }


        public void UpdateAccountancy(ClientOrder order)
        {
            account.orders.Add(order);
            account.totalDailyIncome += order.orderItems.Sum(elemnet => elemnet.price);
        }
        public void AddOrderToDB(ClientOrder order)
        {

            DateTime finish = order.CalculateFinishTime();
            List<Guid>? shakesIds = order.orderItems.Select(element => element.shakeId).ToList();


            db.CreateOrder(new OrderModel()
            {
                startTime = order.date,
                finishTime = finish,
                shakesId = shakesIds,
                totalPrice = order.totalPrice,
                orderId = order.uId,
                customerName = order.customerName
            });


        }
        public bool TakeOrder(string customerName, List<Guid> orderedShakesId)
        {
            try
            {
                CheckValidOrder(customerName, orderedShakesId);
                ClientOrder order = new ClientOrder(customerName);
                //for each shake we need to add AddToOrder(Guid shakeName, string size, double price)

                Console.WriteLine(order.totalPrice);
                AddOrderToDB(order);
                CheckIfAdded(order);
                UpdateAccountancy(order);

            }
            catch
            {
                throw;
            }

            return true;

        }
        public async void CheckIfAdded(ClientOrder order)
        {
            var result = await db.GetOrderFromDB(order);
            if (result == null)
            {
                throw new Exception("Failed to save an order");
            }
        }
        
        public void EndOfDay()
        {
            CheckAuthentication();
            int amount = account.orders.Count;
            double ordersProfit = account.totalDailyIncome;

            db.CreateReport(new ReportModel()
            {
                date = DateTime.Today.Date,
                ordersAmount = amount,
                totalDailyIncome = ordersProfit
            });

            account.Reset();

        }

    }
}
