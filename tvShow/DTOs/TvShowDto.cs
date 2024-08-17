using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace TvShowApi.DTOs
{
    public class TvShowDto
    {
        [Required(ErrorMessage = "The name of the show is required.")]
        [SwaggerSchema(Description = "The name of the TV show.")]
        public string Name { get; set; } = string.Empty;

        [SwaggerSchema(Description = "Indicates if the TV show is a favorite.")]
        public bool Favorite { get; set; }
    }
}
