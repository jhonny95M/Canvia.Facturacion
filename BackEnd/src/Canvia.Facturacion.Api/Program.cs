using Canvia.Facturacion.Api.Middlewares;
using Canvia.Facturacion.Application.Extensions;
using Canvia.Facturacion.Infraestructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var cors = "cors";
// Add services to the container.
builder.Services.AddInjectionInfraestructure(configuration)
    .AddInjectionApplication(configuration);

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: cors,
        builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors(cors);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
