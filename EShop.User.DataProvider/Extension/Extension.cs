using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;
using EShop.Infrastructure.Security;

namespace EShop.User.DataProvider.Extension
{
    public static class Extension
    {
        public static CreateUser SetPassword(this CreateUser user, IEncrypter encrypter)
        {
            user.Password = encrypter.GetHash(user.Password);
            return user;
        }

        public static bool ValidatePassword(this UserCreated user, LoginUser savedUser, IEncrypter encrypter)
        {
            return user.Password.Equals(encrypter.GetHash(savedUser.Password));
        }
    }
}
