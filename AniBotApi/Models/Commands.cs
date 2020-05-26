using AniBotApi.Models;
using AniBotApi.Models.DB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AniBotApi.Models
{
    public class Commands
    {
        public IMongoCollection<UserItem> userInfo;
        public Commands()
        {
            var client = new MongoClient("DB link");
            var database = client.GetDatabase("AniTelegramBotDB");
            userInfo = database.GetCollection<UserItem>("FavouriteAnim");
        }
        public UserItem Get(string id) =>
            userInfo.Find<UserItem>(user => (user.Id) == (id)).FirstOrDefault();

        public async Task<UserItem> Create(UserItem user)
        {
            await userInfo.InsertOneAsync(user);
            return user;
        }

        public async void Update(string id, UserItem userIn) =>
            await userInfo.ReplaceOneAsync(user => (user.Id) == (id), userIn);




        string[] genres = { "action", "adventure", "cars", "comedy", "dementia", "demons", "mystery", "drama", "ecchi", "fantasy", "game", "hentai", "historical", "horror", "kids", "magic", "martial arts", "mecha", "music", "parody", "samurai", "romance", "school", "sci fi", "shoujo", "shoujo ai", "shounen", "shounen Ai", "space", "sports", "super power", "vampire", "yaoi", "yuri", "harem", "slice of life", "supernatural", "military", "police", "psychological", "thriller", "seinen", "josei" };
        //public Commands()
        //{
        //    FindByName(new UserInfo { Name ="Naruto" });
        //}
        public string FindByGenre(UserInfo item)
        {
            item.Genre = item.Genre.Trim();

            int number = Array.IndexOf(genres, item.Genre)+1;

            if(number==0)
            {
                return ("BAD");
            }

            item.link = $"https://api.jikan.moe/v3/genre/anime/{number}";
            return item.link;
        }
        public string FindByName(UserInfo item)
        {
            item.Name = item.Name.Trim();
            item.link = $"http://api.jikan.moe/v3/search/anime?q={item.Name}";
            return item.link;
        }
    }
}
