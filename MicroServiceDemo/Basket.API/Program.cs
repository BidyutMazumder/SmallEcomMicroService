using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Basket.API.Repositories.Abstraction;
using Discount.Grpc.Protos;
using MassTransit;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("BasketDB");
});

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
    (options => options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountGrpcUrl")));
builder.Services.AddScoped<DiscountGrpcService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBusketRepository, BusketRepository>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// rabbitmq configuration
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:RabbitMQHost"]);
    });
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

app.MapControllers();

app.Run();
