//using MassTransit.Serialization;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authentication.OAuth;
//using Microsoft.AspNetCore.DataProtection.KeyManagement;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace EShop.Infrastructure.Authentication
//{
//    public static class Extension
//    {
//        public static IServiceCollection AddJwt(this IServiceCollection collection, IConfiguration configuration)
//        {
//            var optionss = new JwtOptions();
//            configuration.GetSection("jwt").Bind(optionss);
//            collection.Configure<JwtOptions>(x => x = optionss);
//            collection.AddSingleton<IAuthenticationHandler, AuthenticationHandler>();
//            var authenticationProviderKey = "EShopAuthenticationKey";

//            //collection.AddAuthentication()
//            //    .AddJwtBearer(authenticationProviderKey, cfg =>
//            //    {
//            //        cfg.RequireHttpsMetadata = false;
//            //        cfg.SaveToken = true;
//            //        cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//            //        {
//            //            ValidateAudience = false,
//            //            ValidIssuer = options.Issuer,
//            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey))
//            //        };
//            //    });

          

//            collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                    .AddJwtBearer(options =>
//                    {
//                        options.RequireHttpsMetadata = false;
//                        options.TokenValidationParameters = new TokenValidationParameters
//                        {
//                            // укзывает, будет ли валидироваться издатель при валидации токена
//                            ValidateIssuer = true,
//                            // строка, представляющая издателя
//                            ValidIssuer = AuthOptions.ISSUER,

//                            // будет ли валидироваться потребитель токена
//                            ValidateAudience = true,
//                            // установка потребителя токена
//                            //ValidAudience = AuthOptions.AUDIENCE,
//                            // будет ли валидироваться время существования
//                            ValidateLifetime = true,

//                            // установка ключа безопасности
//                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//                            // валидация ключа безопасности
//                            ValidateIssuerSigningKey = true,
//                        };
//                    });


//            return collection;
//        }
//    }
//}
