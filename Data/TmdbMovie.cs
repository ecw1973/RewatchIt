using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RewatchIt.Data
{
    public class TmdbMovie
    {
        #region Properties

        public string Id { get; set; }
        public string Title { get; set; }
        [JsonProperty("release_date")]
        public DateTime? ReleaseDate { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        #endregion
    }
}