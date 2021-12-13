using AutoMapper;
using DisneyApi.Domain.Commands;
using DisneyApi.Domain.Dtos;
using DisneyApi.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DisneyApi.AccessData.Commands
{
    public class CharactersRepository : ICharacteresRepository
    {
        private readonly DisneyApiContext _context;
        private readonly IMapper _mapper;
        private readonly IMoviesRepository _peliculasRepository;

        public CharactersRepository(DisneyApiContext context, IMapper mapper, IMoviesRepository peliculasRepository)
        {
            _context = context;
            _mapper = mapper;
            _peliculasRepository = peliculasRepository;
        }

        public void Add(Character character)
        {
            _context.Character.Add(character);
            _context.SaveChanges();
        }

        public void Delete(Character character)
        {
            _context.Character.Remove(character);
            _context.SaveChanges();
        }

        public List<Character> GetAllCharacters()
        {
            return _context.Character.ToList();
        }

        public List<CharacterDtoForDetails> GetAllCharactersWithDetails()
        {
            var personajes = GetAllCharacters();
            var personajesConDetalles = new List<CharacterDtoForDetails>();

            foreach (var character in personajes)
            {
                var personajeMapeado = _mapper.Map<CharacterDtoForDetails>(character);
                var peliculasMapeadas = _mapper.Map<List<MovieDto>>(_peliculasRepository.GetMoviesByCharacterId(character.CharacterId));
                personajeMapeado.Movies = peliculasMapeadas;
                personajesConDetalles.Add(personajeMapeado);
            }

            return personajesConDetalles;
        }

        public CharacterDtoForDetails GetCharacteWithDetails(int id)
        {
            var character = GetCharacterById(id);
            var personajeConDetalles = _mapper.Map<CharacterDtoForDetails>(character);
            var movieMapped = _mapper.Map<List<MovieDto>>(_peliculasRepository.GetMoviesByCharacterId(character.CharacterId));
            personajeConDetalles.Movies = movieMapped;

            return personajeConDetalles;
        }

        public Character GetCharacterById(int id)
        {
            return _context.Character.Find(id);
        }

        public Character GetCharacterByName(string name)
        {
            return _context.Character.SingleOrDefault(Personaje => Personaje.Name == name);
        }

        public void Update(Character character)
        {
            _context.Update(character);
            _context.SaveChanges();
        }

        public List<Character> GetCharactersByAge(int age)
        {
            return _context.Character.Where(Character => Character.Age == age).ToList();
        }

        public List<Character> GetCharacterByMovieId(int movieId)
        {
            var personajePeliculas = _context.CharacterMovie.Where(CharacterMovie => CharacterMovie.MovieId == movieId);
            var personajes = new List<Character>();

            foreach (var movie in personajePeliculas)
            {
                var personaje = _context.Character.Find(movie.CharacterId);
                personajes.Add(personaje);
            }

            return personajes;
        }

        public List<Character> GetCharactersByWeight(int weight)
        {
            return _context.Character.Where(Character => Character.Weight == weight).ToList();
        }
    }
}
