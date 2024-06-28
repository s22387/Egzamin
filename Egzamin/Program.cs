using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<Egzamin.Context.AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Data Source=db-mssql16;Initial Catalog=s22387;Integrated Security=True")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthorization();

app.MapControllers();

app.Run();