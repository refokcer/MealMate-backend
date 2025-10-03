
using MealMate_backend.Application.Interfaces;
using MealMate_backend.Application.Services;
using MealMate_backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MealMateDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IDishProductService, DishProductService>();
builder.Services.AddScoped<IMealGroupService, MealGroupService>();
builder.Services.AddScoped<IMealGroupDishService, MealGroupDishService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
