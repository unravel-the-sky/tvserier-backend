using Microsoft.EntityFrameworkCore;
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

    public class TvShow
    {
        public int id { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public float rating { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
    public class Episode
    {
        public int id { get; set; }
        public int parentId { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public float rating { get; set; }
    }
}