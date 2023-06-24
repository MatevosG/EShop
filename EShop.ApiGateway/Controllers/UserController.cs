using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;
using EShop.Infrastructure.Query;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<LoginUser> _request;

        public UserController(IPublishEndpoint publishEndpoint, IRequestClient<LoginUser> request)
        {
            _publishEndpoint = publishEndpoint;
            _request = request;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateUser user)
        {
            await _publishEndpoint.Publish(user);
            return Accepted("user Created");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginUser loginUser)
        {
            var userresponse = await _request.GetResponse<UserCreated>(loginUser);

            return Accepted(userresponse.Message);
        }
    }
}
