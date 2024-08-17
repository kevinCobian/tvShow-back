namespace TvShowApi.Repositories
{
    using TvShowApi.Models;

    public class TvShowRepository : ITvShowRepository
    {
        private readonly List<TvShow> _tvShows;

        public TvShowRepository()
        {
            _tvShows = new List<TvShow>
            {
                new TvShow { Id = 1, Name = "Breaking Bad", Favorite = true },
                new TvShow { Id = 2, Name = "Stranger Things", Favorite = false }
            };
        }

        public IEnumerable<TvShow> GetAll() => _tvShows;

        public TvShow? GetById(int id) => _tvShows.FirstOrDefault(x => x.Id == id);

        public void Add(TvShow tvShow)
        {
            tvShow.Id = _tvShows.Max(x => x.Id) + 1;
            _tvShows.Add(tvShow);
        }

        public void Update(TvShow tvShow)
        {
            var existing = _tvShows.FirstOrDefault(x => x.Id == tvShow.Id);
            if (existing != null)
            {
                existing.Name = tvShow.Name;
                existing.Favorite = tvShow.Favorite;
            }
        }

        public void Delete(int id)
        {
            var tvShow = _tvShows.FirstOrDefault(x => x.Id == id);
            if (tvShow != null)
            {
                _tvShows.Remove(tvShow);
            }
        }
    }
}
