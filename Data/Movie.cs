using System.Text.Json;
using System.Text.Json.Serialization;

namespace RewatchIt.Data
{
  public class Movie
  {
    #region Properties

    [JsonPropertyName("Const")]
    public string Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Url { get; set; }

    #endregion

    #region Methods

    public override string ToString()
    {
      return JsonSerializer.Serialize(this);
    }

    #endregion
  }
}