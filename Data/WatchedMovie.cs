using System.ComponentModel.DataAnnotations;

namespace RewatchIt.Data
{
    public class WatchedMovie
    {
        #region Properties

        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int UserRating { get; set; }
        public string Username{ get; set; }

        #endregion
    }
}