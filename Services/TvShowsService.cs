using backend.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace backend.Services
{
    public class TvShowsService
    {
        private readonly IMongoCollection<TvShow> _tvshows;
        private readonly IMongoCollection<Episode> _episodes;
        static HttpClient client = new HttpClient();

        public TvShowsService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("tvshowsmongodb"));
            var database = client.GetDatabase("tvshowsmongodb");
            _tvshows = database.GetCollection<TvShow>("TvShows");
            _episodes = database.GetCollection<Episode>("Episodes");
        }

        public List<TvShow> Get()
        {
            return _tvshows.Find(show => true).ToList();
        }

        public List<TvShowShort> GetAllShows()
        {
            var ratingList = _tvshows
                    .Find(e => true)
                    .SortByDescending(e => e.rating)
                    .ToList();

            var filterBuilder = Builders<Episode>.Filter;
            var startDate = new DateTime(2019, 03, 25);

            List<TvShowShort> rankedTvShowsList = new List<TvShowShort>();
            foreach (var item in ratingList)
            {
                TvShowShort tvshow = new TvShowShort
                {
                    showName = item.name,
                    imageUrl = item.imageUrl,
                    rating = item.rating,
                    network = item.network,
                    summary = item.summary,
                    genres = item.genres,
                    numEpisodes = item.Episodes.Count,
                };

                var filter = filterBuilder.Eq(x => x.showName, item.name) &
                                filterBuilder.Lte(x => x.airdate, startDate);

                var numOfReleasedEpisodes = _episodes.Find(filter).ToList().Count;

                tvshow.numReleasedEpisodes = numOfReleasedEpisodes;

                rankedTvShowsList.Add(tvshow);
            }
            return rankedTvShowsList;
        }

        public List<Episode> GetEpisodes()
        {
            return _episodes.Find(show => true).ToList();
        }

        public List<String> GetUserGenres()
        {
            var showsList = _tvshows
                                .Find(e => e.genres.Count > 0)
                                .ToList();

            List<String> genresList = new List<String>();
            foreach (var item in showsList)
            {
                foreach (var genre in item.genres)
                {
                    if (!genresList.Contains(genre))
                        genresList.Add(genre);
                }
            }
            var uniqueGenres = genresList.Distinct().ToArray();
            return genresList;
        }

        public List<TvShowShort> GetTopTen()
        {
            var ratingList = _tvshows
                                .Find(e => e.rating > 0)
                                .SortByDescending(e => e.rating)
                                .Limit(10)
                                .ToList();

            List<TvShowShort> rankedTvShowsList = new List<TvShowShort>();
            foreach (var item in ratingList)
            {
                TvShowShort tvshow = new TvShowShort
                {
                    showName = item.name,
                    imageUrl = item.imageUrl,
                    rating = item.rating,
                    network = item.network,
                    summary = item.summary,
                    genres = item.genres,
                    numEpisodes = item.Episodes.Count,
                };
                rankedTvShowsList.Add(tvshow);
            }
            return rankedTvShowsList;
        }
        public List<TvShowShort> GetTopTenAndSave()
        {
            var ratingList = _tvshows
                                .Find(e => e.rating > 0)
                                .SortByDescending(e => e.rating)
                                .Limit(10)
                                .ToList();

            var logPath = System.IO.Path.GetTempFileName();


            using (var writer = File.CreateText(logPath))
            {
                List<TvShowShort> rankedTvShowsList = new List<TvShowShort>();
                foreach (var item in ratingList)
                {
                    TvShowShort tvshow = new TvShowShort
                    {
                        showName = item.name,
                        imageUrl = item.imageUrl,
                        rating = item.rating,
                        network = item.network,
                        summary = item.summary,
                        genres = item.genres,
                        numEpisodes = item.Episodes.Count,
                    };

                    writer.WriteLine(item.name + ";" + item.network + ";" + item.Episodes.Count + "\n"); //or .Write(), if you wish
                }
                // rankedTvShowsList.Add(tvshow);
            }
            return null;
        }
        public List<EpisodeShort> GetNextWeek()
        {
            var filterBuilder = Builders<Episode>.Filter;
            var startDate = new DateTime(2019, 03, 25);
            var endDate = startDate.AddDays(7);
            var filter = filterBuilder.Gte(x => x.airdate, startDate) &
                        filterBuilder.Lte(x => x.airdate, endDate);

            var filterResult = _episodes.Find(filter).ToList();

            List<EpisodeShort> filteredEpisodesList = new List<EpisodeShort>();
            foreach (var item in filterResult)
            {
                EpisodeShort episode = new EpisodeShort
                {
                    showName = item.showName,
                    season = item.season,
                    number = item.number,
                    airdate = item.airdate
                };
                filteredEpisodesList.Add(episode);
            }
            return filteredEpisodesList;
        }
        public List<TvShowNetwork> GetByNetwork()
        {
            var aggreate = _tvshows.Aggregate()
                                    .Match(x => x.network != "")
                                    .Group(
                                        key => key.network,
                                        value => new
                                        {
                                            Key = value.Key,
                                            avgRating = value.Average(x => x.rating),
                                            topShow = value.Max(x => x.name),
                                            numShows = value.Count(x => x.name != "")
                                        })
                                    .SortByDescending(x => x.avgRating)
                                    .Project(p => new TvShowNetwork
                                    {
                                        averageRating = p.avgRating,
                                        network = p.Key,
                                        showName = p.topShow,
                                        numShows = p.numShows
                                    })
                                    .ToList();

            List<TvShowNetwork> filteredShowsList = aggreate;

            return filteredShowsList;
        }
        public TvShow Create(TvShow show)
        {
            _tvshows.InsertOne(show);
            _episodes.InsertMany(show.Episodes);
            return show;
        }

        public bool ReadConfigFile()
        {
            var results = ConfigFile.ReadFile();
            // callMazeApi(results);
            return true;
        }

        public bool ReadFromConfigFile(List<string> lines)
        {
            var results = lines;
            callMazeApi(results);
            return true;
        }

        public List<string> ReadFileNew(IFormFile file)
        {
            var result = string.Empty;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                result = reader.ReadToEnd();
            }

            return string.IsNullOrEmpty(result)
                ? null
                : result.Split('\n').ToList();
        }

        void ShowProduct(TvShow show)
        {
            Console.WriteLine($"Name: {show.name}\t");
            Create(show);
        }
        void AddEpisodesToDb(ICollection<Episode> episodes)
        {
            foreach (var episode in episodes)
            {
                _episodes.InsertOne(episode);
            }
        }
        async Task RunAsync(List<string> showNames)
        {
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                foreach (string showName in showNames)
                {
                    var urlShows = ApiLinks.getTvShowUrlQueryUrl + showName + ApiLinks.embedEpisodes;
                    var show = await GetTvShowAsync(urlShows);
                    ShowProduct(show);
                    Thread.Sleep(500);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("error!" + err);
            }
        }
        async Task FetchDataFromApi(string showName)
        {
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var urlShows = ApiLinks.getTvShowUrlQueryUrl + showName + ApiLinks.embedEpisodes;
                var show = await GetTvShowAsync(urlShows);
                ShowProduct(show);

                // var urlEpisodes = ApiLinks.getEpisodesUrl + showName + ApiLinks.getEpisodesSuffix;
                // var episodes = await GetEpisodesAsync(urlEpisodes, showName);
                // AddEpisodesToDb(episodes);
            }
            catch (Exception err)
            {
                Console.WriteLine("error!" + err);
            }
        }
        async Task<ICollection<Episode>> GetEpisodesAsync(string url, string showName)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var showResponse = await response.Content.ReadAsAsync<JToken>();

                ICollection<Episode> episodes = showResponse["_embedded"]["episodes"].Select(episode => new Episode
                {
                    showName = showName,
                    id = (string)episode["id"],
                    url = (string)episode["url"],
                    season = (int)episode["season"],
                    number = (int)episode["number"],
                    name = (string)episode["name"],
                    runtime = (int)episode["runtime"],
                    imageUrl = episode["image"].Type == JTokenType.Null ? "" : (string)episode["image"]["medium"],
                    summary = (string)episode["summary"],
                }).ToList();

                return episodes;
            }
            return null;
        }
        async Task<TvShow> GetTvShowAsync(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var showResponse = await response.Content.ReadAsAsync<JToken>();

                var showName = (string)showResponse["name"];

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
                    showName = showName,
                    id = (string)episode["id"],
                    url = (string)episode["url"],
                    season = (int)episode["season"],
                    number = (int)episode["number"],
                    airdate = (string)episode["airdate"] == "" ? DateTime.UtcNow : (DateTime)episode["airdate"],
                    name = (string)episode["name"],
                    runtime = (int)episode["runtime"],
                    imageUrl = episode["image"].Type == JTokenType.Null ? "" : (string)episode["image"]["medium"],
                    summary = (string)episode["summary"],
                }).ToList();

                return show;
            }
            return null;
        }
        public bool callMazeApi(List<string> items)
        {
            RunAsync(items).GetAwaiter().GetResult();
            return true;
        }

        public TvShow Get(string id)
        {
            return _tvshows.Find<TvShow>(show => show.id == id).FirstOrDefault();
        }
    }
}