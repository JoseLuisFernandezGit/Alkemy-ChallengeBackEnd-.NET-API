using System;
using System.Collections.Generic;

namespace DisneyApi.Domain.Entities
{
    public class Character
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public string History { get; set; }
        public ICollection<CharacterMovie> CharacterMovie { get; set; }
    }
}
