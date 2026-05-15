using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw7_s32101.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Rejestrujemy DbContext w kontenerze DI.
// Domyślny cykl życia to Scoped — jeden DbContext na jedno żądanie HTTP.
builder.Services.AddDbContext<ComputerMgmtDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ComputerMgmtDb")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();