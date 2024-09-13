using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.Data;
using WorkoutTracker.Infrasctructure.Repositories;

namespace WorkoutTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
            builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();

            builder.Services.AddDbContext<WorkoutTrackerContext>(x => x.UseSqlServer
            (@"Server=DESKTOP-G3PD29J\SQLEXPRESS;database=WorkoutTracker;
            Trusted_Connection=True;TrustServerCertificate=True;"));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
