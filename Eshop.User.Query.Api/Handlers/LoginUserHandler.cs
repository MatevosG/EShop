using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;
using EShop.Infrastructure.Security;
using EShop.User.DataProvider.Extension;
using EShop.User.DataProvider.Services;
using MassTransit;

namespace Eshop.User.Query.Api.Handlers
{
    public class LoginUserHandler : IConsumer<LoginUser>
    {
        private readonly IUserService _userService;
        private readonly IEncrypter _encrypter;

        public LoginUserHandler(IUserService userService, IEncrypter encrypter) 
        {
            _encrypter = encrypter;
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<LoginUser> context)
        {
            var userResult = new UserCreated();
            var user = await _userService.GetUserBtUserName(context.Message.UserName);
            if (user != null)
            {
                var isAllowed = user.ValidatePassword(context.Message,_encrypter);
                if (isAllowed)
                    userResult = user;
            }
            await context.RespondAsync(userResult);
        }
    }
}
