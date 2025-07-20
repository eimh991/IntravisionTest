
using IntravisionTest.Data;
using IntravisionTest.Hubs;
using IntravisionTest.Interface;
using IntravisionTest.Interfaces;
using IntravisionTest.Repositories;
using IntravisionTest.Service;
using Microsoft.EntityFrameworkCore;

namespace IntravisionTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddAuthorization();

            var connectionString = builder.Configuration.GetConnectionString("DataBase");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));


            builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<ICoinRepository, CoinRepository>();
            builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
            builder.Services.AddScoped<IOrderRepository,OrderRepository>();
            builder.Services.AddScoped<IDrinkService, DrinkService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ICartItemService, CartItemService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IImportService, ImportService>();
            builder.Services.AddScoped<IPaymentServicecs, PaymentService>();
            builder.Services.AddScoped<ICartPaymentService, CartPaymentService>();



            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();
           

            app.UseCors("AllowFrontend");


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapHub<ShopHub>("/shopHub");

            app.Run();
        }
    }
}
