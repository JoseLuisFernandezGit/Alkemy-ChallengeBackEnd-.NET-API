using System.Collections.Generic;

namespace DisneyApi.Domain.Dtos
{
    public class CharacterDtoForDetails
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public string History { get; set; }
        public List<MovieDto> Movies { get; set; }//
    }
}
