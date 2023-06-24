using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;
using EShop.Infrastructure.Security;
using EShop.User.DataProvider.Extension;
using EShop.User.DataProvider.Repositories;

namespace EShop.User.DataProvider.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public async Task<UserCreated> AddUser(CreateUser createUser)
        {
            var user = await _userRepository.GetUser(createUser);
            if (user == null)
            {
                createUser.SetPassword(_encrypter);
            }
            else 
            {
                throw new Exception("UserName already exist");
            }
            return await _userRepository.AddUser(createUser);
        }

        public async Task<UserCreated> GetUser(CreateUser createUser)
        {
            return await _userRepository.GetUser(createUser);
        }

        public async Task<UserCreated> GetUserBtUserName(string name)
        {
            return await _userRepository.GetUserBtUserName(name);
        }
    }
}
