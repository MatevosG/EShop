using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Authentication
{
    public class AuthenticationHandler : IAuthenticationHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly SecurityKey _issuerSigninKey;
        private readonly SigningCredentials _credentials;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public AuthenticationHandler(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _issuerSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _credentials = new SigningCredentials(_issuerSigninKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_credentials);
            _tokenValidationParameters = new TokenValidationParameters 
            {
               ValidateAudience = false,
               ValidateIssuer = _options.Issuer,
               IssuerSigningKey = _issuerSigninKey,
            };
        }
        public JwtAuthToken Create(Guid userId)
        {
            var nowUtc= DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(_options.ExpiryMinutes);
            var centuryBegin = new DateTime(1970,1,1).ToUniversalTime();
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
            var paylod = new JwtPayload
            {
                {"sub",userId },
                { "iss",_options.IsUser},
                { "iat",now},
                { "iss", exp},
                { "unique_name",userId},
            };

            var jwt = new JwtSecurityToken(_jwtHeader, paylod);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);
            var jsonToken = new JwtAuthToken
            {
                Token = token,
                Expires = exp
            };

            return jsonToken;
        }
    }
}
