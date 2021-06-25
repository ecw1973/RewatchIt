using System.Collections.Generic;
using Newtonsoft.Json;

namespace RewatchIt.Data
{
    public class TmdbSearchResultsContainer<T>
    {
        #region Properties

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("results")]
        public List<T> Results { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        #endregion
    }
}