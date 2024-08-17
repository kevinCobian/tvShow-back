namespace TvShowApi.Models
{
    public class TvShow
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Favorite { get; set; }
    }
}
