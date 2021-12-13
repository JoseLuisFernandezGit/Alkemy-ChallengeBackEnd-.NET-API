using DisneyApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyApi.Domain.Dtos
{
    public interface IUserRepository
    {
        void Add(User user);
        List<User> GetAllUsers();
        void Update(User user);
        void Delete(User usuario);
        void DeleteById(int id);
        User GetUserById(int id);
        User GetUserByEmail(string email);
        User GetUsuarioByEmailAndPassword(string email, string password);
    }
}
