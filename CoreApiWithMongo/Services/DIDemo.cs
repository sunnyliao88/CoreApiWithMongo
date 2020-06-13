using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithMongo.Services
{
    public interface IDIDemo
    {
        int Id { get; set; }
        string Name { get; set; }
    }


    public class DIDemoSQL : IDIDemo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DIDemoSQL()
        {
            Id = 100;
            Name = "sql";
        }
    }

    public class DIDemoOracle : IDIDemo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DIDemoOracle()
        {
            Id = 101;
            Name = "Oracle";
        }
    }
}
