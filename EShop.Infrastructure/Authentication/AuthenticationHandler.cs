//using EShop.Infrastructure.Event.User;
//using Microsoft.Extensions.Configuration;
//using System.Security.Claims;

//namespace EShop.Infrastructure.Authentication
//{
//    public class AuthenticationHandler : IAuthenticationHandler
//    {
//        //private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
//        //private readonly JwtOptions _options;
//        //private readonly SecurityKey _issuerSigninKey;
//        //private readonly SigningCredentials _credentials;
//        //private readonly JwtHeader _jwtHeader;
//        //private readonly TokenValidationParameters _tokenValidationParameters;
//        private readonly IConfiguration _configuration;
//        public AuthenticationHandler(/*IOptions<JwtOptions> options*/ IConfiguration configuration)
//        {
//            //_options = new JwtOptions();
//            _configuration = configuration;
//            //configuration.GetSection("jwt").Bind(_options);
//            ////_options = options.Value;
//            //_issuerSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
//            //_credentials = new SigningCredentials(_issuerSigninKey, SecurityAlgorithms.HmacSha256);
//            //_jwtHeader = new JwtHeader(_credentials);
//            //_tokenValidationParameters = new TokenValidationParameters
//            //{
//            //    ValidateAudience = false,
//            //    //ValidateIssuer = _options.Issuer,
//            //    ValidIssuer = _options.Issuer,
//            //    IssuerSigningKey = _issuerSigninKey,
//            //};
//        }
//        //public JwtAuthToken Create(string userId)
//        //{
//        //    var nowUtc = DateTime.UtcNow;
//        //    var expires = nowUtc.AddMinutes(_options.ExpiryMinutes);
//        //    var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
//        //    var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
//        //    var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
//        //    var paylod = new JwtPayload
//        //    {
//        //        {"sub",userId },
//        //        { "iss",_options.Issuer},
//        //        { "iat",now},
//        //        { "exp", exp},
//        //        { "unique_name",userId},
//        //    };

//        //    var jwt = new JwtSecurityToken(_jwtHeader, paylod);
//        //    var token = _jwtSecurityTokenHandler.WriteToken(jwt);
//        //    var jsonToken = new JwtAuthToken
//        //    {
//        //        Token = token,
//        //        Expires = exp
//        //    };

//        //    return jsonToken;
//        //}
//        public JwtAuthTokenEventResponse Create(UserCreated userCreated )
//        {
//            return new JwtAuthTokenEventResponse { Token = CreateToken(userCreated) };
//        }
//        public string CreateToken(UserCreated request)
//        {
//            var token = new HttpClient().GetAsync("https://localhost:7010/WeatherForecast").Result.Content.ReadAsStringAsync().Result;
//            var ghjk = _configuration.GetValue<string>("AuthSettings:Issuer");
//            var ghjks = _configuration.GetValue<string>("AuthSettings:Audience");
//            var ghjkss = _configuration.GetValue<string>("AuthSettings:Key");
//            var identity = GetIdentity(request);
//            var now = DateTime.UtcNow;

//            //var jwt = new JwtSecurityToken(
//            //        issuer: _configuration.GetValue<string>("AuthSettings:Issuer"),
//            //        audience: _configuration.GetValue<string>("AuthSettings:Audience"),
//            //        notBefore: now,
//            //        claims: identity.Claims,
//            //expires: now.Add(TimeSpan.FromMinutes(20)),
//            //        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("AuthSettings:Key"))), SecurityAlgorithms.HmacSha256));
//            //var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
//            return token;
//        }

//        private ClaimsIdentity GetIdentity(UserCreated request)
//        {
//            var claims = new List<Claim>
//                {
//                    //new Claim(ClaimsIdentity.DefaultNameClaimType, request.EmailId),
//                    //new Claim(ClaimsIdentity.DefaultRoleClaimType, request.UserName),
//                    new Claim(ClaimTypes.Name, request.UserName)
//                };
//            ClaimsIdentity claimsIdentity =
//                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
//                    ClaimsIdentity.DefaultRoleClaimType);
//            return claimsIdentity;
//        }
//    }
//}
