using MongoDB.Driver;
using Rebar.Models;

namespace Rebar.DataAccess
{
    public class MongoDataBase
    {
        private const string connectionString = "mongodb://127.0.0.1:27017";
        private const string databaseName = "Rebar";
        private const string ordersCollection = "Orders";
        private const string reportsCollection = "Reports";
        private const string shakeCollection = "Menu";


        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            return db.GetCollection<T>(collection);

        }

        public async Task<List<OrderModel>> GetOrderFromDB(ClientOrder order)
        {
            var orderCollection = ConnectToMongo<OrderModel>(ordersCollection);
            var result = await orderCollection.FindAsync(o => o.orderId == order.uId);
            return result.ToList();
        }

        public Task CreateOrder(OrderModel order)
        {
            var orders = ConnectToMongo<OrderModel>(ordersCollection);
            return orders.InsertOneAsync(order);

        }

        public Task CreateReport(ReportModel report)
        {
            var reports = ConnectToMongo<ReportModel>(reportsCollection);
            return reports.InsertOneAsync(report);

        }
        public Task CreateShake(Shake shake)
        {
            var menu = ConnectToMongo<Shake>(shakeCollection);
            return menu.InsertOneAsync(shake);

        }

        public async Task<List<Shake>> GetShakeCollection()
        {
            var menu = ConnectToMongo<Shake>(shakeCollection);
            var collection = await menu.Find(_ => true).ToListAsync();
            return collection;
        }


    }
}
