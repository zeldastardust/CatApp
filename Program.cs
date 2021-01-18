using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Console;


namespace CatApp
{
    class Program
    {
        HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Program program = new Program();
           

            while (true)
            {
                Clear(); Console.CursorVisible = false;
                WriteLine("Välkommen till Katt o skratt!!");
                WriteLine();
                WriteLine("Välj ett alternativ och tryck enter!");
                WriteLine();
                WriteLine("1. Läs fakta om katter utifrån ras ");
                WriteLine("2. Läs dagens tio jokes ");
                WriteLine("X. Avsluta\n");


                string inp = ReadLine().ToLower();

                switch (inp)
                {
                    case "1":
                        WriteLine("Skriv namnet på kattrasen du vill hämta fakta om och tryck enter !");
                        await program.GetCatBreeds();
                        ReadLine();
                        break;

                    case "2":
                        WriteLine("Tryck enter för att läsa punchlinen, enter för nästa joke !");
                        await program.GetJokeItems();
                        ReadLine();
                        break;

                    case "x":
                        Environment.Exit(0);
                        break;
                }
            }

        }
        private async Task GetJokeItems()
        {
            string response = await client.GetStringAsync
                ("https://official-joke-api.appspot.com/random_ten");

            List<Joke> joke = JsonConvert.DeserializeObject<List<Joke>>(response);


            foreach (var item in joke)
            {
                Console.WriteLine(item.setup);
                ReadKey();
                Console.WriteLine(item.punchline);
                ReadKey();
                WriteLine();
            }
        }
        private async Task GetCatBreeds()
        {
            string response = await client.GetStringAsync
                ("https://catfact.ninja/breeds?");

            Breeds cat = JsonConvert.DeserializeObject<Breeds>(response);

            foreach (var data in cat.Data)
            {
                WriteLine(data.breed);
                WriteLine();
            }

                WriteLine("Vilken kattras vill du veta mer om ?");
                WriteLine("Skriv rasens namn och tryck enter!");
                WriteLine("obs du måste använda stor bokstav där det är stor bokstav i listan!!");
                WriteLine();

            string index = ReadLine();

            if (String.IsNullOrEmpty(index))//error handeling empty input
            {
                WriteLine("Fältet får inte vara tomt!");
                return;
            }
            else
            {

                foreach (var item in cat.Data)
                {
                    if (index == item.breed)
                    {
                        WriteLine("Ras:" + item.breed);
                        WriteLine("Ursprungsland:" + item.country);
                        WriteLine("Härkomst: " + item.origin);
                        WriteLine("Päls: " + item.coat);
                        WriteLine("Mönster: " + item.pattern);

                    }
                }
            }
            
        }
        /*private async Task GetJokeId()
        {
            string response = await client.GetStringAsync
                ("https://official-joke-api.appspot.com/random_ten");

            List<Cats> cat = JsonConvert.DeserializeObject<List<Joke>>(response);
            foreach (var item in joke)
            {
                Console.WriteLine(item.id);

            }

        }*/

    }


    class Joke
    {
        public int id { get; set; }
        public string type { get; set; }
        public string setup { get; set; }
        public string punchline { get; set; }
    }


    class Breeds
    {
        public List<Data> Data { get; set; }
    }
    class Data
    {
        public string breed { get; set; }
        public string country { get; set; }
        public string origin { get; set; }
        public string coat { get; set; }
        public string pattern { get; set; }
    }

}
