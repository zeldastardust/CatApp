using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            await program.GetCatBreeds();

            /*while (true)
            {
                Clear(); Console.CursorVisible = false;
                WriteLine("Välkommen till Joke of the day !!");
                WriteLine();
                WriteLine("1. Läs dagens top tio jokes!");
                WriteLine("2. ?");
                WriteLine("X. Avsluta\n");


                string inp = ReadLine().ToLower();

                switch (inp)
                {
                    case "1":
                        await program.GetJokeItems();
                        break;

                    case "x":
                        Environment.Exit(0);
                        break;
                }
            }*/

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
            WriteLine("what cat would you like to read about?");
            var index =  ReadLine();

            for (var i=0; i< cat.Data.Count; i++)
            {
                //if(i == index)
               // {
                    WriteLine(cat.Data[i].country);
              //  }
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
