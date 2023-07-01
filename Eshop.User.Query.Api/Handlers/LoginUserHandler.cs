using EShop.Infrastructure.Authentication;
using EShop.Infrastructure.Event.User;
//using EShop.Infrastructure.Command.User;
//using EShop.Infrastructure.Event.User;
using EShop.Infrastructure.Security;
using EShop.User.DataProvider.Extension;
using EShop.User.DataProvider.Services;
using EvebtBus.Inf.Models;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eshop.User.Query.Api.Handlers
{
    public class LoginUserHandler : IConsumer<LoginUserEvent>
    {
        private readonly IUserService _userService;
        private readonly IEncrypter _encrypter;
        //private readonly IAuthenticationHandler _authenticationHandler;
       // private readonly JwtOptions _jwtOptions;
       private readonly IConfiguration _configuration;

        public LoginUserHandler(IUserService userService, IEncrypter encrypter, /*IAuthenticationHandler authenticationHandler,*/ IConfiguration configuration) 
        {
            //_jwtOptions = new JwtOptions();
            //configuration.GetSection("jwt").Bind(_jwtOptions);
            _encrypter = encrypter;
            _userService = userService;
            _configuration = configuration;
            //_authenticationHandler = authenticationHandler;
        }

        public async Task Consume(ConsumeContext<LoginUserEvent> context)
        {
            var userResult = new UserCreatedEvent();
            JwtAuthTokenEventResponse token = new JwtAuthTokenEventResponse();
            var user = await _userService.GetUserBtUserName(context.Message.UserName);
            if (user != null)
            {
                var realUser = new EShop.Infrastructure.Command.User.LoginUser { 
                    Password = context.Message.Password ,
                    UserName = context.Message.UserName ,
                };
                var isAllowed = user.ValidatePassword(realUser, _encrypter);
                if (isAllowed)
                    token.Token = CreateToken(user);// _authenticationHandler.Create(user); // ;;
                //userResult = user;
            }
            await context.RespondAsync<JwtAuthTokenEventResponse>(token);
        }
        private string CreateToken(UserCreated request)
        {
            var identity = GetIdentity(request);
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: _configuration.GetValue<string>("AuthSettings:Issuer"),
                    audience: _configuration.GetValue<string>("AuthSettings:Audience"),
                    notBefore: now,
                    claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(20)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("AuthSettings:Key"))), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(UserCreated request)
        {
            var claims = new List<Claim>
                {
                    //new Claim(ClaimsIdentity.DefaultNameClaimType, request.EmailId),
                    //new Claim(ClaimsIdentity.DefaultRoleClaimType, request.UserName),
                    new Claim(ClaimTypes.Name, request.UserName)
                };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        //SymmetricSecurityKey GetSymmetricSecurityKey() =>
        //    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        //private string GetHash(string source)
        //{
        //    using (var sha256 = System.Security.Cryptography.SHA256.Create())
        //    {
        //        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(source));

        //        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        //    }
        //}
    }
}
