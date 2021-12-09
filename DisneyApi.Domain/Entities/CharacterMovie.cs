using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyApi.Domain.Entities
{
    public class CharacterMovie
    {
        public int CharacterId { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Character Character { get; set; }
    }
}
