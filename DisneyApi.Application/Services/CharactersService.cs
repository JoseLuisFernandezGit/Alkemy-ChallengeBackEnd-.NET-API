using AutoMapper;
using DisneyApi.Domain.Commands;
using DisneyApi.Domain.Dtos;
using DisneyApi.Domain.Entities;
using System.Collections.Generic;

namespace DisneyApi.Application.Services
{
    public interface ICharactersService
    {
        List<Character> GetAllCharacters();
        Character GetCharacterById(int id);
        Character GetCharacterByName(string nombre);
        void Update(Character personaje);
        void Delete(Character personaje);
        Character CreatePersonaje(CharacterDtoForCreationOrUpdate personaje);
        List<CharacterDtoForDetails> GetAllCharactersWithDetails();
        CharacterDtoForDetails GetCharacteWithDetails(int id);
        List<Character> GetCharactersByAge(int age);
        List<Character> GetCharacterByMovieId(int movieId);
        List<Character> GetCharactersByWeight(int weight);
    }
    public class CharactersService : ICharactersService
    {
        private readonly ICharacteresRepository _repository;
        private readonly IMapper _mapper;

        public CharactersService(ICharacteresRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Character CreatePersonaje(CharacterDtoForCreationOrUpdate personaje)
        {
            var personajeMapeado = _mapper.Map<Character>(personaje);
            _repository.Add(personajeMapeado);
            

            return personajeMapeado;
        }

        public void Delete(Character personaje)
        {
            _repository.Delete(personaje);
        }

        public List<Character> GetAllCharacters()
        {
            return _repository.GetAllCharacters();
        }

        public List<CharacterDtoForDetails> GetAllCharactersWithDetails()
        {
            return _repository.GetAllCharactersWithDetails();
        }

        public Character GetCharacterById(int id)
        {
            return _repository.GetCharacterById(id);
        }

        public Character GetCharacterByName(string nombre)
        {
            return _repository.GetCharacterByName(nombre);
        }

        public CharacterDtoForDetails GetCharacteWithDetails(int id)
        {
            return _repository.GetCharacteWithDetails(id);
        }

        public List<Character> GetCharactersByAge(int age)
        {
            return _repository.GetCharactersByAge(age);
        }

        public void Update(Character character)
        {
            _repository.Update(character);
        }

        public List<Character> GetCharacterByMovieId(int movieId)
        {
           return _repository.GetCharacterByMovieId(movieId);
        }

        public List<Character> GetCharactersByWeight(int weight)
        {
            return _repository.GetCharactersByWeight(weight);
        }
    }
}
