using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MongoDB.Driver;
using Rebar.Models;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace Rebar.DataAccess
{
    public class MongoDataBase
    {

        private const string connectionString = "mongodb://127.0.0.1:27017/";
        private const string databaseName = "Rebar";
        private const string ordersCollection = "Orders";
        private const string reportsCollection = "Reports";
        private const string shakeCollection = "Menu";

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase(databaseName);
                return db.GetCollection<T>(collection);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<OrderDBModel>> GetOrderFromDB(ServerOrder order)
        {
            try
            {
                var orderCollection = ConnectToMongo<OrderDBModel>(ordersCollection);
                var result = await orderCollection.FindAsync(o => o.orderId == order.uId);
                return result.ToList();
            }
            catch
            {
                throw;
            }
        }
        public Task CreateOrder(OrderDBModel order)
        {
            try
            {
                var orders = ConnectToMongo<OrderDBModel>(ordersCollection);
                return orders.InsertOneAsync(order);
            }
            catch
            {
                throw;
            }
        }
        public Task CreateReport(ReportModel report)
        {
            try
            {
                var reports = ConnectToMongo<ReportModel>(reportsCollection);
                return reports.InsertOneAsync(report);
            }
            catch
            {
                throw;
            }
        }
        public Task CreateShake(Shake shake)
        {
            var menu = ConnectToMongo<Shake>(shakeCollection);
            try
            {
                var result = menu.InsertOneAsync(shake);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Shake>?> GetShakeCollectionAsync()
        {
            try
            {
                var menu = ConnectToMongo<Shake>(shakeCollection);
                var collection = await menu.Find(_ => true).ToListAsync();
                return collection;
            }
            catch (MongoCommandException e) when (e.ErrorMessage.EndsWith("not found."))
            {
                return null;
            }

        }
        public List<OrderDBModel>? GetTodayOrders()
        {
            try
            {
                var orders = ConnectToMongo<OrderDBModel>(ordersCollection);
                Console.WriteLine(DateTime.Today.Date);
                var allOrders =orders.Find(_ => true).ToList();
                return allOrders.Where(o => o.finishTime.Date == DateTime.Today.Date).ToList();
            }
            catch (MongoCommandException e) when (e.ErrorMessage.EndsWith("not found."))
            {
                return null;
            }

        }

        public List<Shake>? GetShakeCollection()
        {
            try
            {
                var menu = ConnectToMongo<Shake>(shakeCollection);
                var collection = menu.Find(_id => true).ToList();
                return collection;
            }
            catch (MongoCommandException e) when (e.ErrorMessage.EndsWith("not found."))
            {
                return null;
            }
        }
    }
}
