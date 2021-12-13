using AutoMapper;
using DisneyApi.Domain.Commands;
using DisneyApi.Domain.Dtos;
using DisneyApi.Domain.Entities;
using System.Collections.Generic;

namespace DisneyApi.Application.Services
{
    public interface IMoviesService
    {
        List<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        Movie GetMovieByTitle(string title);
        MovieDtoForDetails GetMovieWithDetails(int id);
        void Update(Movie pelicula);
        void Delete(Movie pelicula);
        Movie CreateMovie(MovieDtoForCreationOrUpdate pelicula);
        List<Movie> GetMovieByCharacterId(int id);
        List<Movie> GetMoviesByOrder(string order);
        List<Movie> GetMoviesByGenreId(int genre);
    }
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesRepository _repository;
        private readonly IMapper _mapper;

        public MoviesService(IMoviesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Movie CreateMovie(MovieDtoForCreationOrUpdate movie)
        {
            var movieMapped = _mapper.Map<Movie>(movie);
            _repository.Add(movieMapped);

            return movieMapped;
        }

        public void Delete(Movie pelicula)
        {
            _repository.Delete(pelicula);
        }

        public List<Movie> GetAllMovies()
        {
            return _repository.GetAllMovies();
        }

        public MovieDtoForDetails GetMovieWithDetails(int id)
        {
            return _repository.GetMovieWithDetails(id);
        }

        public Movie GetMovieById(int id)
        {
            return _repository.GetMovieById(id);
        }

        public Movie GetMovieByTitle(string title)
        {
            return _repository.GetMovieByTitle(title);
        }

        public List<Movie> GetMovieByCharacterId(int id)
        {
            return _repository.GetMoviesByCharacterId(id);
        }

        public List<Movie> GetMoviesByGenreId(int genre)
        {
            return _repository.GetMoviesByGenreId(genre);
        }

        public List<Movie> GetMoviesByOrder(string order)
        {
            return order == "DESC" ? _repository.GetAllMoviesSortedByDesc() : GetAllMovies();
        }

        public void Update(Movie pelicula)
        {
            _repository.Update(pelicula);
        }
    }
}
