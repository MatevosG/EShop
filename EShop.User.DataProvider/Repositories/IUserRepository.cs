using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;

namespace EShop.User.DataProvider.Repositories
{
    public interface IUserRepository
    {
        Task<UserCreated> AddUser(CreateUser createUser);
        Task<UserCreated> GetUser(CreateUser createUser);
        Task<UserCreated> GetUserBtUserName(string name);
    }
}
