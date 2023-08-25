using Microsoft.EntityFrameworkCore;
using TP24.Data;
using TP24.Repositories;
using TP24.Services;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<IDataContext, DataContext>();
}
else
{
    builder.Services.AddDbContext<IDataContext, DataContext>();
}

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IReceivableRepository, ReceivableRepository>();

builder.Services.AddScoped<IReceivableService, ReceivableService>();
builder.Services.AddScoped<ValidationService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.Database.Migrate();
}

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.MapControllers();

app.Run();
