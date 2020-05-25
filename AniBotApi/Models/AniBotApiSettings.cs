using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AniBotApi.Models
{
    public class AniBotApiSettings : IAniBotApiSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IAniBotApiSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }


}
