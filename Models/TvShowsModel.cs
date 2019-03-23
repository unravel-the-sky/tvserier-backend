using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace backend.Models
{
    public class TvShowResult
    {
        public TvShow show { get; set; }
        public Episode episode { get; set; }
    }

    class TvShowApiCaller
    {
        static HttpClient client = new HttpClient();
        static void ShowProduct(TvShow show)
        {
            Console.WriteLine($"Name: {show.name}\t");
        }
        static string getTvShowUrlTest = "http://api.tvmaze.com/search/shows?q=maniac";
        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var url = getTvShowUrlTest;
                var show = await GetTvShowAsync(url);
                ShowProduct(show);
            }
            catch (Exception err)
            {
                Console.WriteLine("error!" + err);
            }
        }
        static async Task<TvShow> GetTvShowAsync(string url)
        {
            // TvShow show = null;
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var showTemp = await response.Content.ReadAsAsync<JArray>();
                var first = showTemp.First; // here take the whole objet actually and then iterate - TODO
                TvShow show = new TvShow
                {
                    id = (int)first["show"]["id"],
                    url = (string)first["show"]["url"],
                    name = (string)first["show"]["name"],
                    type = (string)first["show"]["type"],
                    language = (string)first["show"]["language"],
                    genres = first["show"]["genres"].Select(item => (string)item).ToList(),
                    status = (string)first["show"]["status"],
                    runtime = (int)first["show"]["runtime"],
                    officialSite = (string)first["show"]["officialSite"],
                    days = first["show"]["schedule"]["days"].Select(item => (string)item).ToList(),
                    rating = (float)first["show"]["rating"]["average"],
                    weight = (int)first["show"]["weight"],
                    network = (string)first["show"]["network"],
                    imageUrl = (string)first["show"]["image"]["medium"],
                    summary = (string)first["show"]["summary"],
                };

                foreach(string item in show.genres){
                    Console.WriteLine($"Genres: {item}\t");
                }
                return show;
            }
            return null;
        }
        public static void testApiCall()
        {
            RunAsync().GetAwaiter().GetResult();
        }
    }
}