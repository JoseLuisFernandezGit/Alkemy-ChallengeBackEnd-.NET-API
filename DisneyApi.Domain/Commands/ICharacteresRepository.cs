using DisneyApi.Domain.Entities;
using DisneyApi.Domain.Dtos;
using System.Collections.Generic;

namespace DisneyApi.Domain.Commands
{
    public interface ICharacteresRepository
    {
        List<Character> GetAllCharacters();
        Character GetCharacterById(int id);
        Character GetCharacterByName(string name);
        List<Character> GetCharacterByMovieId(int movieId);
        List<Character> GetCharactersByAge(int age);
        List<CharacterDtoForDetails> GetAllCharactersWithDetails();
        List<Character> GetCharactersByWeight(int weight);
        CharacterDtoForDetails GetCharacteWithDetails(int id);
        void Update(Character Character);
        void Delete(Character Character);
        void Add(Character Character);
    }
}
