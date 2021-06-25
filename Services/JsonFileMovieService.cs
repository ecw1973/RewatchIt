using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using RewatchIt.Data;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RewatchIt.Services
{
    public class JsonFileMovieService
    {
        #region Fields

        private readonly HttpClient client;

        #endregion

        #region Constructors

        public JsonFileMovieService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;

            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region Properties

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "ImdbWatchList.json");

        #endregion

        #region Methods

        // Where to watch:
        //https://api.themoviedb.org/3/movie/23168/watch/providers?api_key=f61a6707532daef48213f2fb4e861c15

        // Use: 
        // https://api.themoviedb.org/3/find/tt0090605?api_key=f61a6707532daef48213f2fb4e861c15&external_source=imdb_id
        // Or:
        // https://api.themoviedb.org/3/movie/tt0090605?api_key=f61a6707532daef48213f2fb4e861c15&language=en-US

        // to Get: 
        //{"movie_results":[{"adult":false,"backdrop_path":"/jMBpJFRtrtIXymer93XLavPwI3P.jpg","genre_ids":[28,53,878],"original_language":"en","original_title":"Aliens","poster_path":"/r1x5JGpyqZU8PYhbs4UcrO1Xb6x.jpg","title":"Aliens","video":false,"vote_average":7.9,"vote_count":6960,"overview":"When Ripley's lifepod is found by a salvage crew over 50 years later, she finds that terra-formers are on the very planet they found the alien species. When the company sends a family of colonists out to investigate her story—all contact is lost with the planet and colonists. They enlist Ripley and the colonial marines to return and search for answers.","release_date":"1986-07-18","id":679,"popularity":22.584}],"person_results":[],"tv_results":[],"tv_episode_results":[],"tv_season_results":[]}

        // To Make:
        // https://www.themoviedb.org/t/p/original/r1x5JGpyqZU8PYhbs4UcrO1Xb6x.jpg

        //https://www.themoviedb.org/movie/218

        public IEnumerable<Movie> GetMovies()
        {
            using (StreamReader jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Movie[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        //public async Task<int> FindMovieCount()
        //{
        //    SearchContainer<TmdbMovie> searchContainer = new SearchContainer<TmdbMovie>();
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage response = await client.GetAsync("discover/movie?api_key=f61a6707532daef48213f2fb4e861c15&page=1&primary_release_year=1984&with_watch_providers=true");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var queryResponse = response.Content.ReadAsStringAsync().Result;
        //            searchContainer = JsonConvert.DeserializeObject<SearchContainer<TmdbMovie>>(queryResponse);
        //        }

        //        return searchContainer.TotalPages;
        //    }
        //}

        public async Task<int> FindMovieCount()
        {
            SearchContainer<TmdbMovie> searchContainer = new SearchContainer<TmdbMovie>();
            HttpResponseMessage response =
                await client.GetAsync("discover/movie?api_key=f61a6707532daef48213f2fb4e861c15&page=1&primary_release_year=1984&with_watch_providers=true");
            if (response.IsSuccessStatusCode)
            {
                string queryResponse = response.Content.ReadAsStringAsync().Result;
                searchContainer = JsonConvert.DeserializeObject<SearchContainer<TmdbMovie>>(queryResponse);
            }

            return searchContainer.TotalPages;
        }

        public async Task<List<TmdbMovie>> GetMovies(int page)
        {
            SearchContainer<TmdbMovie> searchContainer = new SearchContainer<TmdbMovie>();
            HttpResponseMessage response = await client.GetAsync(
                $"discover/movie?api_key=f61a6707532daef48213f2fb4e861c15&page={page}&primary_release_year=1984&with_watch_providers=true");
            if (response.IsSuccessStatusCode)
            {
                string queryResponse = response.Content.ReadAsStringAsync().Result;
                searchContainer = JsonConvert.DeserializeObject<SearchContainer<TmdbMovie>>(queryResponse);
            }

            return searchContainer.Results;
        }

        #endregion

        //public void AddRating(string productId, int rating)
        //{
        //    var products = GetProducts();

        //    if(products.First(x => x.Id == productId).Ratings == null)
        //    {
        //        products.First(x => x.Id == productId).Ratings = new int[] { rating };
        //    }
        //    else
        //    {
        //        var ratings = products.First(x => x.Id == productId).Ratings.ToList();
        //        ratings.Add(rating);
        //        products.First(x => x.Id == productId).Ratings = ratings.ToArray();
        //    }

        //    using(var outputStream = File.OpenWrite(JsonFileName))
        //    {
        //        JsonSerializer.Serialize<IEnumerable<Product>>(
        //            new Utf8JsonWriter(outputStream, new JsonWriterOptions
        //            {
        //                SkipValidation = true,
        //                Indented = true
        //            }), 
        //            products
        //        );
        //    }
        //}
    }
}