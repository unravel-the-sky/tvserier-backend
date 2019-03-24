using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace backend.Models
{
    class TvShowApiCaller
    {
        // static HttpClient client = new HttpClient();
        // static void ShowProduct(TvShow show)
        // {
        //     Console.WriteLine($"Name: {show.name}\t");
        //     // AddProductToDatabase(show);
        // }
        // // static void AddProductToDatabase(TvShow tvshow)
        // // {
        // //     using (var db = new TvShowsContext())
        // //     {
        // //         db.Shows.Add(tvshow);
        // //         var count = db.SaveChanges();
        // //         Console.WriteLine("All shows in database: ");
        // //         foreach (var show in db.Shows)
        // //         {
        // //             Console.WriteLine(" - {0}", show.name);
        // //         }
        // //         Console.WriteLine("{0} records saved to database, whoa!", count);
        // //     }
        // // }
        // static async Task RunAsync(string[] showNames)
        // {
        //     client.BaseAddress = new Uri("http://localhost:5000/");
        //     client.DefaultRequestHeaders.Accept.Clear();
        //     client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //     try
        //     {
        //         foreach (string showName in showNames)
        //         {
        //             var url = ApiLinks.getTvShowUrlQueryUrl + showName + ApiLinks.embedEpisodes;
        //             var show = await GetTvShowAsync(url);
        //             ShowProduct(show);
        //         }
        //     }
        //     catch (Exception err)
        //     {
        //         Console.WriteLine("error!" + err);
        //     }
        // }
        // static async Task<TvShow> GetTvShowAsync(string url)
        // {
        //     HttpResponseMessage response = await client.GetAsync(url);
        //     if (response.IsSuccessStatusCode)
        //     {
        //         var showResponse = await response.Content.ReadAsAsync<JToken>();

        //         TvShow show = new TvShow
        //         {
        //             id = (int)showResponse["id"],
        //             url = (string)showResponse["url"],
        //             name = (string)showResponse["name"],
        //             type = (string)showResponse["type"],
        //             language = (string)showResponse["language"],
        //             genres = showResponse["genres"].Select(item => (string)item).ToList(),
        //             status = (string)showResponse["status"],
        //             runtime = (int)showResponse["runtime"],
        //             officialSite = (string)showResponse["officialSite"],
        //             days = showResponse["schedule"]["days"].Select(item => (string)item).ToList(),
        //             rating = showResponse["rating"]["average"].Type == JTokenType.Null ? 0 : (float)showResponse["rating"]["average"],
        //             weight = (int)showResponse["weight"],
        //             network = showResponse["network"].Type == JTokenType.Null ? "" : (string)showResponse["network"]["name"],
        //             imageUrl = (string)showResponse["image"]["medium"],
        //             summary = (string)showResponse["summary"],
        //         };

        //         // show.Episodes = showResponse["_embedded"]["episodes"].Select(episode => new Episode
        //         // {
        //         //     id = (int)episode["id"],
        //         //     url = (string)episode["url"],
        //         //     name = (string)episode["name"],
        //         //     runtime = (int)episode["runtime"],
        //         //     imageUrl = episode["image"].Type == JTokenType.Null ? "" : (string)episode["image"]["medium"],
        //         //     summary = (string)episode["summary"],
        //         // }).ToList();

        //         return show;
        //     }
        //     return null;
        // }
        // public static void testApiCall(string[] items)
        // {
        //     RunAsync(items).GetAwaiter().GetResult();
        // }
    }
}