using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using RewatchIt.Data;

namespace RewatchIt.Services
{
  public class JsonFileMovieService
  {
    #region Constructors

    public JsonFileMovieService(IWebHostEnvironment webHostEnvironment)
    {
      WebHostEnvironment = webHostEnvironment;
    }

    #endregion

    #region Properties

    public IWebHostEnvironment WebHostEnvironment { get; }

    private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "ImdbWatchList.json");

    #endregion

    #region Methods

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