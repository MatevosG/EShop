using EShop.Gateway.Api.Models;
using EvebtBus.Inf.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace EShop.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<LoginUserEvent> _request;

        public UserController(IPublishEndpoint publishEndpoint, IRequestClient<LoginUserEvent> request)
        {
            _publishEndpoint = publishEndpoint;
            _request = request;
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> Add([FromForm] CreateUserEvent user)
        {
            await _publishEndpoint.Publish(user);
            return Accepted("user Created");
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login([FromForm] LoginUserEvent loginUser)
        {
            var userresponse = await _request.GetResponse<JwtAuthTokenEventResponse>(loginUser);
            return Accepted(userresponse.Message);
        }
    }
}
