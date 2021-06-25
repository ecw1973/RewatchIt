namespace RewatchIt.Data
{
    public class WatchedMovie
    {
        #region Properties

        public string Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{nameof(Title)}: {Title}";
        }

        #endregion
    }
}