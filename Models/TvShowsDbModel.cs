using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class TvShowsContext : DbContext
    {
        public DbSet<TvShow> Shows { get; set; }
        public DbSet<Episode> Episodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=tvshows.db");
        }
    }
    public class TvShowParent
    {
        public int score;
        public TvShow tvShow;
    }
    public class TvShow
    {
        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string language { get; set; }
        public ICollection<string> genres { get; set; }
        public string status { get; set; }
        public int runtime { get; set; }
        public string premiered { get; set; }
        public string officialSite { get; set; }
        public ICollection<string> days { get; set; }
        public float rating { get; set; }
        public int weight { get; set; }
        public string network { get; set; }
        // public Object webChannel { get; set; }
        // public Object externals { get; set; }
        public string imageUrl { get; set; }
        public string summary { get; set; }
        // public Object updated { get; set; }
        // public Object _links { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
    public class Image
    {
        public string medium { get; set; }
        public string original { get; set; }
    }
    public class Rating
    {
        public float average;
    }
    public class Episode
    {
        public int id { get; set; }
        public int parentId { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public float rating { get; set; }
    }
}