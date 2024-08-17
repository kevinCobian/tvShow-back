using System.ComponentModel.DataAnnotations;

namespace TvShowApi.DTOs
{
    public class TvShowDto
    {
        [Required(ErrorMessage = "The name of the show is required.")]
        public string Name { get; set; } = string.Empty;

        public bool Favorite { get; set; }
    }
}
