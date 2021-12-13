using System.Collections.Generic;

namespace DisneyApi.Domain.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }
    }
}
