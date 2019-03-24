using backend.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace backend.Services
{
    public class TvShowsService
    {
        private readonly IMongoCollection<TvShow> _tvshows;
        static HttpClient client = new HttpClient();

        public TvShowsService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("tvshowsmongodb"));
            var database = client.GetDatabase("tvshowsmongodb");
            _tvshows = database.GetCollection<TvShow>("TvShows");
        }

        public List<TvShow> Get()
        {
            return _tvshows.Find(show => true).ToList();
        }

        public TvShow Create(TvShow show)
        {
            _tvshows.InsertOne(show);
            return show;
        }

        public bool ReadConfigFile()
        {
            var results = ConfigFile.ReadFile();
            testApiCall(results);
            return true;
        }

     void ShowProduct(TvShow show)
        {
            Console.WriteLine($"Name: {show.name}\t");
            Create(show);
            // AddProductToDatabase(show);
        }
     async Task RunAsync(string[] showNames)
        {
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                foreach (string showName in showNames)
                {
                    var url = ApiLinks.getTvShowUrlQueryUrl + showName + ApiLinks.embedEpisodes;
                    var show = await GetTvShowAsync(url);
                    ShowProduct(show);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("error!" + err);
            }
        }
     async Task<TvShow> GetTvShowAsync(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var showResponse = await response.Content.ReadAsAsync<JToken>();

                TvShow show = new TvShow
                {
                    id = (string)showResponse["id"],
                    url = (string)showResponse["url"],
                    name = (string)showResponse["name"],
                    type = (string)showResponse["type"],
                    language = (string)showResponse["language"],
                    genres = showResponse["genres"].Select(item => (string)item).ToList(),
                    status = (string)showResponse["status"],
                    runtime = (int)showResponse["runtime"],
                    officialSite = (string)showResponse["officialSite"],
                    days = showResponse["schedule"]["days"].Select(item => (string)item).ToList(),
                    rating = showResponse["rating"]["average"].Type == JTokenType.Null ? 0 : (float)showResponse["rating"]["average"],
                    weight = (int)showResponse["weight"],
                    network = showResponse["network"].Type == JTokenType.Null ? "" : (string)showResponse["network"]["name"],
                    imageUrl = (string)showResponse["image"]["medium"],
                    summary = (string)showResponse["summary"],
                };

                show.Episodes = showResponse["_embedded"]["episodes"].Select(episode => new Episode
                {
                    episodeId = (string)episode["id"],
                    url = (string)episode["url"],
                    name = (string)episode["name"],
                    runtime = (int)episode["runtime"],
                    imageUrl = episode["image"].Type == JTokenType.Null ? "" : (string)episode["image"]["medium"],
                    summary = (string)episode["summary"],
                }).ToList();

                return show;
            }
            return null;
        }
        public void testApiCall(string[] items)
        {
            RunAsync(items).GetAwaiter().GetResult();
        }

        public TvShow Get(string id)
        {
            return _tvshows.Find<TvShow>(show => show.id == id).FirstOrDefault();
        }
    }
}