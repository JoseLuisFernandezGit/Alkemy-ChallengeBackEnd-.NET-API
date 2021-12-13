using DisneyApi.Domain.Dtos;
using DisneyApi.Domain.Entities;
using System.Collections.Generic;

namespace DisneyApi.Domain.Commands
{
    public interface IMoviesRepository
    {
        List<Movie> GetAllMovies();
        List<Movie> GetAllMoviesSortedByDesc();
        Movie GetMovieById(int id);
        MovieDtoForDetails GetMovieWithDetails(int id);
        Movie GetMovieByTitle(string title);
        void Update(Movie movie);
        void Delete(Movie movie);
        void Add(Movie movie);
        List<Movie> GetMoviesByCharacterId(int id);
        List<Movie> GetMoviesByGenreId(int genre);
    }
}
