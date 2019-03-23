using System;
using System.IO;

namespace backend.Models
{
    public class ConfigFile {
        public static string[] ReadFile(){
            var result = System.IO.File.ReadAllLines("./tvmaze_shows.txt");
            return result;
            // FileStream fileStream = new FileStream("./tvmaze_shows.txt", FileMode.Open);
            // using (StreamReader reader = new StreamReader(fileStream)){
            //     string data = reader.ReadLine();
            //     Console.WriteLine(data);
            // }
        }
    }
}