using System;
using Newtonsoft.Json;

namespace RewatchIt.Data
{
    public class TmdbMovie
    {
        #region Properties

        public string Id { get; set; }
        public string Title { get; set; }
        [JsonProperty("release_date")]
        public DateTime? ReleaseDate { get; set; }
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }
        [JsonProperty("vote_average")]
        public double VoteAvg { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{nameof(Title)}: {Title}";
        }

        #endregion
    }
}