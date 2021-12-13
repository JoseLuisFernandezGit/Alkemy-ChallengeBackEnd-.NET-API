using System.Collections.Generic;

namespace DisneyApi.Domain.Entities
{
    public class Role
    {
        public Role()
        {
            Usuarios = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<User> Usuarios { get; set; }
    }
}
