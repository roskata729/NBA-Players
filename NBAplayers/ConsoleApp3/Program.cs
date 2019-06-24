using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;


namespace ConsoleApp3
{

    class Program
    {
        public static List<Player> JsonReader(string myPath) {
            

            using (StreamReader r = new StreamReader(myPath))
            {
                string json = r.ReadToEnd();
                List<Player> items = JsonConvert.DeserializeObject<List<Player>>(json);
                
                return items;

                
            }
        }

        public class Player
        {
            public string Name { get; set; }
            public int PlayingSince { get; set; }
            public string Position { get; set; }
            public int Rating { get; set; }

            public Player()
            {

            }
            
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Insert Json file path: ");
            List<Player> allPlayers = Program.JsonReader(Console.ReadLine());
            Console.WriteLine("Years to qualify: ");
            int yearsToQualify = int.Parse(Console.ReadLine());
            Console.WriteLine("Minimum rating: ");
            int minRating = int.Parse(Console.ReadLine());
            Console.WriteLine("Insert CSV file path:");
            string pathToCSV = Console.ReadLine();
            

            List<Player> qualifiedPlayers = new List<Player>();

            foreach (var player in allPlayers) {
                if (player.Rating >= minRating) {
                    if (DateTime.Now.Year - player.PlayingSince <= yearsToQualify) {
                        qualifiedPlayers.Add(player);
                    }
                }
            }

            using (StreamWriter writeToCSV = new StreamWriter(pathToCSV)) {
                writeToCSV.WriteLine("Name, Rating");
                foreach (var player in qualifiedPlayers.OrderByDescending(x=>x.Rating)) {
                    writeToCSV.WriteLine($"{player.Name}, {player.Rating}");
                }
            }

            Console.WriteLine("Sorting completed");
            
        }
    }
}
