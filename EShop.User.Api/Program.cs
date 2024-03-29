using EShop.Infrastructure.EventBus;
using EShop.Infrastructure.Mongo;
using EShop.Infrastructure.Security;
using EShop.User.Api.Handlers;
using EShop.User.DataProvider.Repositories;
using EShop.User.DataProvider.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongoDb(builder.Configuration);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>(); 
builder.Services.AddScoped<IEncrypter, Encrypter>();





var rabbitMq = new RabbitMqOption();
builder.Configuration.GetSection("rabbitmq").Bind(rabbitMq);

builder.Services.AddMassTransit(config =>
{

    config.AddConsumer<CreateUserHandler>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(rabbitMq.ConnectionString);

        cfg.ReceiveEndpoint("CreateUser", c =>
        {
            c.ConfigureConsumer<CreateUserHandler>(ctx);
        });
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

var dbInitializer = app.Services.GetService<IDatabaseInitializer>();

dbInitializer.InitializeAsync();

app.MapControllers();

app.Run();
