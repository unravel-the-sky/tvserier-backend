using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new TvShowsContext())
            {
                // db.Shows.Add(new TvShow { id = 14657, title = "Maniac" });
                // db.Shows.Add(new TvShow { id = 123, title = "Wee" });
                // var count = db.SaveChanges();
                // Console.WriteLine("{0} records saved to database, whoa!", count);
                Console.WriteLine();
                Console.WriteLine("All shows in database: ");
                foreach (var show in db.Shows)
                {
                    Console.WriteLine(" - {0}", show.title);
                }
            }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
