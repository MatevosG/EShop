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

//builder.Services.AddRabbitMq(builder.Configuration);

var rabbitMq = new RabbitMqOption();
builder.Configuration.GetSection("rabbitmq").Bind(rabbitMq);

builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        x.AddConsumer<CreateProductHandler>();

        cfg.Host(new Uri(rabbitMq.ConnectionString), hostCfg =>
        {
            hostCfg.Username(rabbitMq.UserName);
            hostCfg.Password(rabbitMq.Password);
        });

        cfg.ReceiveEndpoint("create_productr", ep => 
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(retrycpnfig => { retrycpnfig.Interval(2, 100); });
            ep.ConfigureConsumer<CreateProductHandler>(provider);
        });
    }));
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

var busControl = app.Services.GetRequiredService<IBusControl>();

busControl.Start();

var dbInitializer = app.Services.GetService<IDatabaseInitializer>();

 dbInitializer.InitializeAsync();

app.MapControllers();

app.Run();
