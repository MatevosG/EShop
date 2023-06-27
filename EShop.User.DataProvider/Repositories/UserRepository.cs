using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;
using MongoDB.Driver;

namespace EShop.User.DataProvider.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IMongoDatabase _database;
        private IMongoCollection<CreateUser> _collection => _database.GetCollection<CreateUser>("user");

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<UserCreated> AddUser(CreateUser createUser)
        {
            await _collection.InsertOneAsync(createUser);
            return new UserCreated
            {
                ContactNo = createUser.ContactNo,
                EmailId = createUser.EmailId,
                Password = createUser.Password,
                UserId = createUser.UserId,
                UserName = createUser.UserName,
            };
        }

        public async Task<UserCreated> GetUser(CreateUser createUser)
        {
            var user =  _collection.AsQueryable().Where(x=>x.UserName == createUser.UserName).FirstOrDefault();

            await Task.CompletedTask;

            if (user == null)
                return null;

            return new UserCreated
            {
                UserName = user.UserName,
                ContactNo = user.ContactNo,
                EmailId = user.EmailId,
                Password = user.Password,
                UserId = user.UserId
            };
        }

        public async Task<UserCreated> GetUserBtUserName(string name)
        {
            var user = _collection.AsQueryable().Where(x => x.UserName == name).FirstOrDefault();

            if (user == null)
                throw new Exception("user not found");
            await Task.CompletedTask;

            return new UserCreated
            {
                UserName = user.UserName,
                ContactNo = user.ContactNo,
                EmailId = user.EmailId,
                Password = user.Password,
                UserId = user.UserId
            };
        }
    }
}
