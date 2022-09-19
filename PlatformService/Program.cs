using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PlatformService.Interfaces;
using PlatformService.Repositories;
using PlatformService.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* ------------------------- Inject my own services ------------------------- */
// builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(
//     options => 
//     {
//         options.UseNpgsql(
//             builder.Configuration.GetConnectionString("conn")
//         );
//     }
// );
builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseInMemoryDatabase("InMem")
);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
/* -------------------------------------------------------------------------- */

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
