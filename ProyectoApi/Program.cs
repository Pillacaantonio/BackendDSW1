using ProyectoApi.Helpers;
using ProyectoApi.Repositories;
using ProyectoApi.Repositories.interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDatabaseExecutor>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    return new DatabaseExecutor(config.GetConnectionString("Default"));
});

// Add services to the container.

builder.Services.AddScoped<IClienteRepository,ClienteRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var app = builder.Build();
builder.Services.AddCors(option =>
{
    option.AddPolicy("HabilitarCors", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
var app =builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//activando politica
app.UseCors("HabilitarCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
