using System;
using System.Linq;
using System.Threading.Tasks;

namespace RewatchIt.Data
{
  public class WeatherForecastService
  {
    #region Fields

    private static readonly string[] Summaries =
    {
      "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    #endregion

    #region Methods

    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
      Random rng = new Random();
      return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = startDate.AddDays(index),
        TemperatureC = rng.Next(-20, 55),
        Summary = Summaries[rng.Next(Summaries.Length)]
      }).ToArray());
    }

    #endregion
  }
}