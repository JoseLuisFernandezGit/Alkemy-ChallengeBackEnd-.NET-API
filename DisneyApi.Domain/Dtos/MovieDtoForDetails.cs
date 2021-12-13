using System;
using System.Collections.Generic;

namespace DisneyApi.Domain.Dtos
{
    public class MovieDtoForDetails
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Qualification { get; set; }
        public int GenreId { get; set; }
        public List<CharacterDto> Characters { get; set; }//
    }
}
