using Eshop.Product.Query.Api.Handlers;
using EShop.Infrastructure.EventBus;
using EShop.Infrastructure.Mongo;
using EShop.Product.DataProvide.Repositories;
using EShop.Product.DataProvide.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var rabbitMq = new RabbitMqOption();
builder.Configuration.GetSection("rabbitmq").Bind(rabbitMq);



builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddMongoDb(builder.Configuration);

builder.Services.AddMassTransit(config =>
{

    config.AddConsumer<GetProductByIdHandler>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(rabbitMq.ConnectionString);

        cfg.ReceiveEndpoint("GetProductById", c =>
        {
            c.ConfigureConsumer<GetProductByIdHandler>(ctx);
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
