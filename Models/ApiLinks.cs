namespace backend.Models
{
    public class ApiLinks
    {
        public static string getTvShowUrlQueryUrl = "http://api.tvmaze.com/singlesearch/shows?q=";
        public static string embedEpisodes = "&embed=episodes";
        public static string getEpisodesUrl = "http://api.tvmaze.com/shows/";
        public static string getEpisodesSuffix = "/episodes";
        public static string getEpisodesByDateUrl = "http://api.tvmaze.com/shows/";
        public static string getEpisodesByDateSuffix = "/episodesbydate?date=";
    }
}