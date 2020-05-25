using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AniBotApi.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using AniBotApi.Models.DB;
using System.Net.Http;
using System.Net;
using AniBotApi.Models.DB;

namespace AniBotApi.Controllers
{
    [Route("api/[controller]")]
    public class AniController:Controller
    {
        Commands commands = new Commands();

        [HttpPost]
        [Route("genre")]
        public async Task<ObjectResult> FindGenre([FromBody] UserInfo item)
        {
            item.Genre = item.Genre.ToLower();
            string item1 = commands.FindByGenre(item);

            if(item1 == "BAD")
            {
                return new ObjectResult("BAD");
            }

            using var client = new HttpClient();
            var content = await client.GetStringAsync(item1); //item1 - ссылка

            Models.UserItem useritem = JsonConvert.DeserializeObject<Models.UserItem>(content);
            useritem.Id = item.Id;

            return new ObjectResult(useritem);
        }
        [HttpPost]
        [Route("Name")]
        public async Task<ObjectResult> FindName([FromBody] UserInfo item)
        {
            string item1 = commands.FindByName(item);

            using var client = new HttpClient();
            var content = await client.GetStringAsync(item1);

            Models.UserItem useritem = JsonConvert.DeserializeObject<Models.UserItem>(content);
            useritem.Id = item.Id;

            return new ObjectResult(useritem);

        }
        [HttpPost]
        [Route("GetFavorite")]
        public async Task<ObjectResult> GetFavorite([FromBody] UserInfo item)
        {
            UserItem useritem = commands.Get(item.Id);

            if (useritem == null)
            {
                return new ObjectResult("BAD");
            }
            else
            {
                DBuseritem dBuseritem = new DBuseritem();
                dBuseritem.jsresults=useritem;
                return new ObjectResult(dBuseritem);
            }
        }
        [HttpPost]
        [Route("AddFavorite")]
        public async Task<ObjectResult> AddFavorite([FromBody] Models.UserItem item)
        {

            UserItem useritem = commands.Get(item.Id);
            DBuseritem dbuseritem = new DBuseritem();

            if (useritem == null)
            {
                UserItem useritem1 = new UserItem();
                useritem1.Id = item.Id;
                useritem1.results = (item.results);
                await commands.Create(useritem1);
                return new ObjectResult("OK");
            }
            else
            {
                
                dbuseritem.jsresults = useritem;

                bool animecontaince = false;
                    
                    for (int i = 0; i < dbuseritem.jsresults.results.Count; i++)
                    {
                        if (dbuseritem.jsresults.results[i].title == item.results[0].title)
                            animecontaince = true;
                    }

                if (animecontaince == false)
                {
                    dbuseritem.jsresults.results.Add(item.results[0]);
                    commands.Update(item.Id,dbuseritem.jsresults);
                }
                else
                {
                    return new ObjectResult("BAD");
                }
            }
            return new ObjectResult(dbuseritem);
        }
        [HttpPut]
        [Route("DeleteFavorite")]
        public async Task<ObjectResult> DeleteFavorite([FromBody] UserItem item)
        {
            UserItem useritem = commands.Get(item.Id);
            DBuseritem dbuseritem = new DBuseritem();

            if (useritem == null)
            {
                return new ObjectResult("BAD");
            }
            else
            {

                dbuseritem.jsresults = useritem;
                int index = 0;
                bool animecontaince = false;

                for (int i = 0; i < dbuseritem.jsresults.results.Count; i++)
                {
                    if (dbuseritem.jsresults.results[i].title == item.results[0].title)
                    {
                        index = i;
                        animecontaince = true;
                        break;
                    }
                }

                if (animecontaince == false)
                {
                    return new ObjectResult("BAD");
                }
                else
                {
                    dbuseritem.jsresults.results.RemoveAt(index);
                    commands.Update(item.Id,dbuseritem.jsresults);
                }
            }
            return new ObjectResult(dbuseritem);
        }
    }
}
