using DisneyApi.Domain.Dtos;
using DisneyApi.Domain.Entities;

namespace DisneyApi.Application.Services
{
    public interface IUserService
    {
        User RegisterUser(UserDtoForCreation user);
        User LoginUser(UserLoginDto user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User LoginUser(UserLoginDto user)
        {
            return _repository.GetUsuarioByEmailAndPassword(user.Email, user.Password);
        }

        public User RegisterUser(UserDtoForCreation userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                RoleId = 2
            };

            _repository.Add(user);
            
            return user;
        }
    }
}
