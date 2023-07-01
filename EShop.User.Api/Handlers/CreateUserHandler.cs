using EShop.Infrastructure.Command.User;
using EShop.User.DataProvider.Services;
using EvebtBus.Inf.Models;
using MassTransit;

namespace EShop.User.Api.Handlers
{
    public class CreateUserHandler : IConsumer<CreateUserEvent>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<CreateUserEvent> context)
        {
            var createdUser = await _userService.AddUser(new CreateUser 
            {
                ContactNo = context.Message.ContactNo,
                EmailId = context.Message.EmailId,
                Password = context.Message.Password,
                UserName = context.Message.UserName,
            });
        }
    }
}
