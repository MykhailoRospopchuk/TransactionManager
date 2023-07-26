
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using TransactionManagement.Database;
using TransactionManagement.Services;

namespace TransactionManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connection = builder.Configuration.GetConnectionString("DbConnectionString");
            builder.Services.AddDbContext<TransactionDbContext>(options => options.UseSqlServer(connection));
            builder.Services.AddDbContext<AdoTransactionDbContext>(options => options.UseSqlServer(connection));


            builder.Services.AddScoped<ICSVService, CSVService>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            //builder.Services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    var supportedCultures = new[] { "en-US", "fr" };
            //    options.SetDefaultCulture(supportedCultures[0])
            //        .AddSupportedCultures(supportedCultures)
            //        .AddSupportedUICultures(supportedCultures);
            //});


            // Add FluentValidation
            builder.Services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<Program>();
                options.ImplicitlyValidateChildProperties = true;
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
        }
    }
}