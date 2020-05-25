using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AniBotApi.Models
{
    public class UserItem
    {   
       public string Id { get; set; }
       public List<Userresultitem> results { get; set; }
       public List<Userresultitem> anime { get; set; }
    }
}
