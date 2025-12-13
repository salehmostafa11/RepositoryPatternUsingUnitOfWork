
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternUsingUOW.Core.Assets;
using RepositoryPatternUsingUOW.Core.Interfaces;
using RepositoryPatternUsingUOW.EF;
using RepositoryPatternUsingUOW.EF.Repositories;
using AutoMapper;

namespace RepositoryPatternUsingUOW.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    //to say for ef that the location of migrations is in the assembly which contains ApplicationDbContext
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName) 
                    );
            });

            // Registeration of services and repos
            //builder.Services.AddScoped(ty
            //peof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                    options.SwaggerEndpoint("/openapi/v1.json", "v1")
                );
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
