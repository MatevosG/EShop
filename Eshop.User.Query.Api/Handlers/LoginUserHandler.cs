using EShop.Infrastructure.Command.User;
using EShop.User.DataProvider.Services;
using MassTransit;

namespace Eshop.User.Query.Api.Handlers
{
    public class LoginUserHandler : IConsumer<LoginUser>
    {
        private readonly IUserService _userService;

        public LoginUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task Consume(ConsumeContext<LoginUser> context)
        {
            //var user = _userService.GetUserBtUserName(context.Message.UserName);
            //if (user != null)
            //{
            //    user.ValidatePassword()
            //}
            return null;
        }
    }
}
