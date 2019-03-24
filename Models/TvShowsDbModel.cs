using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class TvShowsContext : DbContext
    {
        // public DbSet<TvShow> Shows { get; set; }
        // public DbSet<Episode> Episodes { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlite("Data Source=tvshows_new.db");
        // }
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<TvShow>()
        //                 .Property(p => p.genres)
        //                 .HasConversion(
        //                     v => string.Join(',', v),
        //                     v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
        //                 );

        //     modelBuilder.Entity<TvShow>()
        //     .Property(p => p.days)
        //     .HasConversion(
        //         v => string.Join(',', v),
        //         v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
        //     );
        // }
    }
    // public class TvShow
    // {
    //     public int id { get; set; }
    //     public string url { get; set; }
    //     public string name { get; set; }
    //     public string type { get; set; }
    //     public string language { get; set; }
    //     public ICollection<string> genres { get; set; }
    //     public string status { get; set; }
    //     public int runtime { get; set; }
    //     public string premiered { get; set; }
    //     public string officialSite { get; set; }
    //     public ICollection<string> days { get; set; }
    //     public float rating { get; set; }
    //     public int weight { get; set; }
    //     public string network { get; set; }
    //     public string imageUrl { get; set; }
    //     public string summary { get; set; }
    //     // public ICollection<Episode> Episodes { get; set; }
    // }
    // public class Episode
    // {
    //     public int id { get; set; }
    //     public string url { get; set; }
    //     public string name { get; set; }
    //     public int season { get; set; }
    //     public int number { get; set; }
    //     public DateTime airtime { get; set; }
    //     public int runtime { get; set; }
    //     public string imageUrl { get; set; }
    //     public string summary { get; set; }
    // }
}