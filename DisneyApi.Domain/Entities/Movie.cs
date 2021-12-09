using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyApi.Domain.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Image { get; set; }
        public string  Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Qualification { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<CharacterMovie> CharacterMovie { get; set; }
    }
}
