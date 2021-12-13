using System.Collections.Generic;

namespace DisneyApi.Domain.Entities
{
    public class Character
    {
        public int CharacterId;
        public string Image { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public string History { get; set; }
        public ICollection<CharacterMovie> CharacterMovie { get; set; }
        public Character()
        {
            CharacterMovie = new HashSet<CharacterMovie>();
        }
    }
}
