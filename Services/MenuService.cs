using Rebar.Models;
using Rebar.DataAccess;
using MongoDB.Driver;

namespace Rebar.Services
{
    public class MenuService
    {
        public List<Shake>? menuShakes;
        public Accountancy account;
        public MongoDataBase db;

        public MenuService()
        {
            
            this.db = new MongoDataBase();
            try
            {
                this.menuShakes = db.GetShakeCollection();
            }
            catch
            {
                throw;
            }
            
        }

        public void CreateMenu()
        {
            Shake shake1 = new Shake("Oreo", "milkshake made with vanilla ice cream, milk, and Oreo cookies!");
            Shake shake2 = new Shake("Chocolate", "milkshake");
            Shake shake3 = new Shake("Strawberry", "milkshake ");
            Shake shake4 = new Shake("Vanilla", "milkshake");
            Shake shake5 = new Shake("Mango", "milkshake");
  
            try
            {

                CreateNewShake(shake1);
                CreateNewShake(shake2);
                CreateNewShake(shake3);
                CreateNewShake(shake4);
                CreateNewShake(shake5);
                
                this.menuShakes.Add(shake1);
                this.menuShakes.Add(shake2);
                this.menuShakes.Add(shake3);
                this.menuShakes.Add(shake4);
                this.menuShakes.Add(shake5);
            }
            catch
            {
                throw;
            }

        }

        public Task<List<Shake>?> GetMenu()
        {
            return db.GetShakeCollectionAsync();
        }

        public bool CheckValidName(string? name)
        {
            return !string.IsNullOrEmpty(name);
        }

        public bool CheckValidParams(Shake shake)
        {
            var shakeSize = shake.sizesPrice;

            if (CheckValidName(shake.name) == false)
            {
                throw new Exception("Shake's name field cannot be empty");
            }
            if (CheckValidName(shake.description) == false)
            {
                throw new Exception("Shake's description field cannot be empty");
            }
            if (shakeSize.small <= 0 || shakeSize.medium <= 0 || shakeSize.large <= 0)
            {
                throw new Exception("Shake's price must be a valid number");

            }
           
            return true;
        }

        public void CheckValidAmount(List<OrderItem>? orderedShakes)
        {
            if (orderedShakes.Count > 10)
            {
                throw new Exception("There are more than 10 shakes in this order.");
            }
        }

        public void CheckIfEmptyOrder(List<OrderItem>? orderedShakes)
        {
            if (orderedShakes == null || !orderedShakes.Any())
            {
                throw new Exception("Order content is empty");
            }
        }

        public void CheckIfExistInMenu(List<OrderItem> orderedShakes)
        {

            foreach (var shake in orderedShakes)
            {
                var found = menuShakes.Find(item => item.uId == shake.id);
                if (found == null)
                {
                    throw new Exception("Shake does not exist in menu");

                }
            }
        }

        public void CheckValidOrder(ClientOrder order)
        {
            if (CheckValidName(order.customerName) == false)
            {
                throw new Exception("Name field cannot be empty");
            }
            try
            {
                CheckIfEmptyOrder(order.orderItems);
                CheckValidAmount(order.orderItems);
                CheckIfExistInMenu(order.orderItems);
            }
            catch
            {
                throw;
            }

        }

        public void CheckUniqueName(Shake shake)
        {
            var found = menuShakes.Find(item => item.name == shake.name);
            Console.WriteLine(found);
            if (found != null)
            {
                throw new Exception("Another shake with similar name already exist");
            }
        }

        public async Task AddShakeToDB(Shake newShake)
        {
            await this.db.CreateShake(newShake); 

        }

        public bool CreateNewShake(Shake newShake)
        {
            try
            {
                var res = CheckValidParams(newShake);
                CheckUniqueName(newShake);
                AddShakeToDB(newShake);
                this.menuShakes.Add(newShake);
            }
            catch
            {
                throw;
            }
            return true;
            

        }
       public bool MultipleNewShake(List<Shake> shakes)
        {
            try
            {
                foreach(Shake shake in shakes)
                {
                    CreateNewShake(shake);
                }
            }
            catch
            {
                throw;
            }
            return true;
        }


        public void AddOrderToDB(ServerOrder order)
        {

            DateTime finish = order.CalculateFinishTime();
            if (order.orderItems != null)
            {
                List<Guid>? shakesIds = order.orderItems.Select(element => element.id).ToList();

                _ = db.CreateOrder(new OrderDBModel()
                {
                    startTime = order.date,
                    finishTime = finish,
                    shakesId = shakesIds,
                    totalPrice = order.totalPrice,
                    orderId = order.uId,
                    customerName = order.customerName
                });
            }
        }

        public bool TakeOrder(ClientOrder newOrder)
        {
            try
            {
                CheckValidOrder(newOrder);
                ServerOrder order = new ServerOrder(newOrder.customerName, newOrder.orderItems);
                order.CalculatePayment();
                Console.WriteLine(order.totalPrice);
                AddOrderToDB(order);
                CheckIfAdded(order);
            }
            catch
            {
                throw;
            }

            return true;
        }

        public async void CheckIfAdded(ServerOrder order)
        {
            var result = await db.GetOrderFromDB(order);
            if (result == null)
            {
                throw new Exception("Failed to save the order");
            }
        }
        
        public string EndOfDay(int password)
        {
            this.account = new Accountancy();
            account.CheckAuthentication(password);
            
            var ordersList = db.GetTodayOrders();
            
            int amount = ordersList == null ? 0 : ordersList.Count();

            account.totalDailyIncome = ordersList == null ? 0 : ordersList.Sum(o => o.totalPrice); ;

            string message = $"Total number of orders today {amount} \n Total number of orders today {account.totalDailyIncome} \n";

            db.CreateReport(new ReportModel()
            {
                date = DateTime.Today.Date,
                ordersAmount = amount,
                totalDailyIncome = account.totalDailyIncome
            });

            account.Reset();
            return message;
        }

    }
}
