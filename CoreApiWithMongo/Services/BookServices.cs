using CoreApiWithMongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CoreApiWithMongo.Services
{

    public interface IBookServices
    {
        List<Book> GetBooks();
    }
    public class BookServices:IBookServices
    {
        private readonly SqlDBSettings _DBSettings;
        
        public BookServices(SqlDBSettings DBSettings)
        {
            _DBSettings = DBSettings;
        }

        public List<Book> GetBooks()
        {
            List<Book> result = new List<Book>();


            MongoClient client = new MongoClient(_DBSettings.Connectionsting);
            IMongoDatabase db = client.GetDatabase("demo");
            IMongoCollection<Book> col = db.GetCollection<Book>("Books");
            result = col.Find(b => true).ToList();
            return result;
        }
    }
}
