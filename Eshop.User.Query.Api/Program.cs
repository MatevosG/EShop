using Eshop.User.Query.Api.Handlers;
using EShop.Infrastructure.EventBus;
using EShop.Infrastructure.Security;
using EShop.User.DataProvider.Repositories;
using EShop.User.DataProvider.Services;
using MassTransit;
using EShop.Infrastructure.Mongo;
using EShop.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEncrypter, Encrypter>();
//builder.Services.AddScoped<IAuthenticationHandler, AuthenticationHandler>();
builder.Services.AddMongoDb(builder.Configuration);
//builder.Services.AddJwt(builder.Configuration);

//var optionss = new JwtOptions();
//builder.Configuration.GetSection("jwt").Bind(optionss);
//builder.Services.Configure<JwtOptions>(x => x = optionss);
//builder.Services.AddSingleton<IAuthenticationHandler, AuthenticationHandler>();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
//                            ValidAudience = AuthOptions.AUDIENCE,
//                            // будет ли валидироваться время существования
//                            ValidateLifetime = true,

//                            // установка ключа безопасности
//                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//                            // валидация ключа безопасности
//                            ValidateIssuerSigningKey = true,
//                        };
//                    });


var rabbitMq = new RabbitMqOption();
builder.Configuration.GetSection("rabbitmq").Bind(rabbitMq);

builder.Services.AddMassTransit(config =>
{

    config.AddConsumer<LoginUserHandler>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(rabbitMq.ConnectionString);

        cfg.ReceiveEndpoint("LoginUser", c =>
        {
            c.ConfigureConsumer<LoginUserHandler>(ctx);
        });
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var dbInitializer = app.Services.GetService<IDatabaseInitializer>();

dbInitializer.InitializeAsync();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
