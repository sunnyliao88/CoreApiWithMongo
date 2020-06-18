using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithMongo.Services
{

    //public interface IMongoDBSettings
    //{
    //    string Connectionsting { get; }
    //}

    //public class MongoDBSettings : IMongoDBSettings
    //{
    //    private IConfiguration _configuration;

    //    public MongoDBSettings(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    public string Connectionsting
    //    {
    //        get
    //        {
    //            return _configuration.GetSection("MongoDBConnectionsting").Value;
    //        }
    //    }
    //}


    //public interface IDBSettings
    //{
    //    string Connectionsting { get; set; }
    //}

    public class MongoDBSettings
    {
        public string Connectionsting { get; set; }
    }

    public class SqlDBSettings
    {
        public string Connectionsting { get; set; }
    }
}
