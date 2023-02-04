using Poq.Api.Endpoints;
using Poq.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddHttpClient<ProductService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapProductEndpoints();

app.Run();
