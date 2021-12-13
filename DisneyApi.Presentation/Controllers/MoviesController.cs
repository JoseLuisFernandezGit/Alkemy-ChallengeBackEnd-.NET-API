using AutoMapper;
using DisneyApi.Application.Services;
using DisneyApi.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DisneyApi.Presentation.Controllers
{
    [Route("movies")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _service;
        private readonly IMapper _mapper;

        public MoviesController(IMoviesService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<MovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllMovies(string name = null, int? genre = null, string order = null)
        {
            try
            {
                if(name != null && genre == null && order == null) 
                {
                    var movie = _service.GetMovieByTitle(name);
                    var movieMapped = _mapper.Map<MovieDto>(movie);

                    return Ok(movieMapped);
                }

                else if(name == null && genre != null && order == null) 
                {
                    var movie = _service.GetMoviesByGenreId(genre.Value);
                    var movieMapped = _mapper.Map<List<MovieDto>>(movie);

                    return Ok(movieMapped);
                }

                else if(name == null && genre == null && order != null)
                {
                    var movie = _service.GetMoviesByOrder(order);
                    var movieMapped = _mapper.Map<List<MovieDto>>(movie);

                    return Ok(movieMapped);
                }

                else
                {
                    var movies = _service.GetAllMovies();
                    var movieMapped = _mapper.Map<List<MovieDto>>(movies);

                    return Ok(movieMapped);
                }
                
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult CreateMovie([FromBody] MovieDtoForCreationOrUpdate movie)
        {
            try
            {
                var movieEntity = _service.CreateMovie(movie);

                if (movieEntity != null)
                {
                    var createdMovie = _mapper.Map<MovieDto>(movieEntity);
                    return Created("~movies/", createdMovie);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                var movie = _service.GetMovieById(id);

                if (movie == null)
                {
                    return NotFound();
                }

                _service.Delete(movie);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateMovie(int id, [FromBody] MovieDtoForCreationOrUpdate movie)
        {
            try
            {
                if (movie == null)
                {
                    return BadRequest("Todos los campos deben estar completos para poder realizar la actualización de este elemento.");
                }

                var movieEntity = _service.GetMovieById(id);

                if (movieEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(movie, movieEntity);
                _service.Update(movieEntity);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("{id}/details")]
        [ProducesResponseType(typeof(MovieDtoForDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetMovieWithDetails(int id)
        {
            try
            {
                var movie = _service.GetMovieWithDetails(id);

                return Ok(movie);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
