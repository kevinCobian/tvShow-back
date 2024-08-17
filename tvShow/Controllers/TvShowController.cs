using Microsoft.AspNetCore.Mvc;
using TvShowApi.Models;
using TvShowApi.Repositories;
using TvShowApi.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace TvShowApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TvShowController : ControllerBase
    {
        private readonly ITvShowRepository _repository;

        public TvShowController(ITvShowRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieve all TV shows", Description = "Returns a list of all available TV shows.")]
        [SwaggerResponse(200, "List of TV shows retrieved successfully.", typeof(IEnumerable<TvShow>))]
        public ActionResult<IEnumerable<TvShow>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieve a specific TV show by ID", Description = "Returns a single TV show based on its ID.")]
        [SwaggerResponse(200, "TV show retrieved successfully.", typeof(TvShow))]
        [SwaggerResponse(404, "TV show not found.")]
        public ActionResult<TvShow> GetById(int id)
        {
            var tvShow = _repository.GetById(id);
            if (tvShow == null)
                return NotFound();
            return Ok(tvShow);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add a new TV show", Description = "Adds a new TV show to the list.")]
        [SwaggerResponse(201, "TV show created successfully.", typeof(TvShow))]
        public ActionResult Add([FromBody] TvShowDto tvShowDto)
        {
            var tvShow = new TvShow
            {
                Name = tvShowDto.Name,
                Favorite = tvShowDto.Favorite
            };
            _repository.Add(tvShow);
            return CreatedAtAction(nameof(GetById), new { id = tvShow.Id }, tvShow);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing TV show", Description = "Updates the details of a TV show based on its ID.")]
        [SwaggerResponse(204, "TV show updated successfully.")]
        [SwaggerResponse(404, "TV show not found.")]
        public ActionResult Update(int id, [FromBody] TvShowDto tvShowDto)
        {
            var tvShow = _repository.GetById(id);
            if (tvShow == null)
                return NotFound();

            tvShow.Name = tvShowDto.Name;
            tvShow.Favorite = tvShowDto.Favorite;

            _repository.Update(tvShow);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a TV show", Description = "Deletes a TV show based on its ID.")]
        [SwaggerResponse(204, "TV show deleted successfully.")]
        [SwaggerResponse(404, "TV show not found.")]
        public ActionResult Delete(int id)
        {
            var tvShow = _repository.GetById(id);
            if (tvShow == null)
                return NotFound();

            _repository.Delete(id);
            return NoContent();
        }
    }
}
