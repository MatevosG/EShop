using EShop.Product.Api.Repositories;
using EShop.Product.Api.Services;
using EShop.Infrastructure.Mongo;
using EShop.Product.Api.Handlers;
using EShop.Infrastructure.EventBus;
using Microsoft.Extensions.Configuration;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddMongoDb(builder.Configuration);


builder.Services.AddScoped<CreateProductHandler>();

var rabbitMq = new RabbitMqOption();
builder.Configuration.GetSection("rabbitmq").Bind(rabbitMq);

builder.Services.AddMassTransit(config =>
{

    config.AddConsumer<CreateProductHandler>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(rabbitMq.ConnectionString);

        cfg.ReceiveEndpoint("CreateProduct", c =>
        {
            c.ConfigureConsumer<CreateProductHandler>(ctx);
        });
    });
});

builder.Services.AddMassTransitHostedService();


var app = builder.Build();


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


