using AutoMapper;
using DisneyApi.Domain.Commands;
using DisneyApi.Domain.Dtos;
using DisneyApi.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DisneyApi.AccessData.Commands
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly DisneyApiContext _context;
        private readonly IMapper _mapper;

        public MoviesRepository(DisneyApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Add(Movie movie)
        {
            _context.Movie.Add(movie);
            _context.SaveChanges();
        }

        public void Delete(Movie movie)
        {
            _context.Movie.Remove(movie);
            _context.SaveChanges();
        }

        public List<Movie> GetAllMovies()
        {
            return _context.Movie.OrderBy(movie => movie.CreationDate).ToList();
        }

        public List<Movie> GetMoviesByCharacterId(int id)
        {

            var characterMovies = _context.CharacterMovie.Where(CharacterMovie => CharacterMovie.CharacterId == id).ToList();

            var movies = new List<Movie>();

            foreach (var item in characterMovies)
            {
                var pelicula = _context.Movie.Find(item.MovieId);
                movies.Add(pelicula);
            }

            return movies;
        }

        public Movie GetMovieById(int id)
        {
            return _context.Movie.Find(id);
        }

        public Movie GetMovieByTitle(string title)
        {
            return _context.Movie.SingleOrDefault(Movie => Movie.Title == title);
        }

        public void Update(Movie pelicula)
        {
            _context.Update(pelicula);
            _context.SaveChanges();
        }

        public List<Movie> GetMoviesByGenreId(int genre)
        {
            return _context.Movie.Where(Movie => Movie.GenreId == genre).ToList();
        }

        public List<Movie> GetAllMoviesSortedByDesc()
        {
            return GetAllMovies().OrderByDescending(Pelicula => Pelicula.CreationDate).ToList();
        }



        public MovieDtoForDetails GetMovieWithDetails(int id)
        {
            var movie = GetMovieById(id);
            var movieWithDetails = _mapper.Map<MovieDtoForDetails>(movie);
            var charactersMapped = _mapper.Map<List<CharacterDto>>(GetCharacterByMovieId(movie.MovieId));
            movieWithDetails.Characters = charactersMapped;

            return movieWithDetails;
        }

        public List<Character> GetCharacterByMovieId(int movieId)
        {
            var characterMovie = _context.CharacterMovie.Where(CharacterMovie => CharacterMovie.MovieId == movieId);
            var characters = new List<Character>();

            foreach (var movie in characterMovie)
            {
                var character = _context.Character.Find(movie.CharacterId);
                characters.Add(character);
            }

            return characters;
        }

    }
}