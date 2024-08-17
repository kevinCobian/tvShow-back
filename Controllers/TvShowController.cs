using Microsoft.AspNetCore.Mvc;
using TvShowApi.Models;
using TvShowApi.Repositories;
using TvShowApi.DTOs;

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
        public ActionResult<IEnumerable<TvShow>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<TvShow> GetById(int id)
        {
            var tvShow = _repository.GetById(id);
            if (tvShow == null)
                return NotFound();
            return Ok(tvShow);
        }

        [HttpPost]
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
