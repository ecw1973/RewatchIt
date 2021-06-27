using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RewatchIt.Data;

namespace RewatchIt.Services
{
    public class TmdbService
    {
        #region Fields

        private readonly HttpClient client;
        private readonly string apiKey = "f61a6707532daef48213f2fb4e861c15";

        #endregion

        #region Constructors

        public TmdbService()
        {
            client = new HttpClient {BaseAddress = new Uri("https://api.themoviedb.org/3/")};
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

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

        public async Task<int> GetMoviePageCount(int year)
        {
            HttpResponseMessage response =
                await client.GetAsync($"discover/movie?api_key={apiKey}&page=1&primary_release_year={year}");

            if (response.IsSuccessStatusCode)
            {
                string queryResponse = response.Content.ReadAsStringAsync().Result;
                TmdbSearchResultsContainer<TmdbMovie> resultsContainer = JsonConvert.DeserializeObject<TmdbSearchResultsContainer<TmdbMovie>>(queryResponse);
                return resultsContainer.TotalPages;
            }

            return default;
        }

        public async Task<List<TmdbMovie>> GetMovies(int page, int year)
        {
            HttpResponseMessage response =
                await client.GetAsync($"discover/movie?api_key={apiKey}&page={page}&primary_release_year={year}&sort_by=vote_count.desc");

            if (response.IsSuccessStatusCode)
            {
                string queryResponse = response.Content.ReadAsStringAsync().Result;
                TmdbSearchResultsContainer<TmdbMovie> resultsContainer = JsonConvert.DeserializeObject<TmdbSearchResultsContainer<TmdbMovie>>(queryResponse);

                resultsContainer.Results = resultsContainer.Results.Where(x => x.VoteCount > 10).ToList();

                foreach (TmdbMovie movie in resultsContainer.Results)
                {
                    movie.PosterUrl = "https://www.themoviedb.org/t/p/original/" + movie.PosterPath;
                }

                return resultsContainer.Results;
            }

            return default;
        }

        public async Task<TmdbMovie> GetMovie(int id)
        {
            HttpResponseMessage response =
                await client.GetAsync($"movie/{id}?api_key={apiKey}");

            if (response.IsSuccessStatusCode)
            {
                string queryResponse = response.Content.ReadAsStringAsync().Result;
                TmdbMovie movie = JsonConvert.DeserializeObject<TmdbMovie>(queryResponse);
                movie.PosterUrl = "https://www.themoviedb.org/t/p/original/" + movie.PosterPath;
                return movie;
            }

            return default;
        }

        #endregion
    }
}