using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Core.Domain;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.Data;
using WorkoutTracker.Infrasctructure.DTO;
using WorkoutTracker.Infrasctructure.DTO.Validators;
using WorkoutTracker.Infrasctructure.Repositories;
using WorkoutTracker.Infrasctructure.Services;

namespace WorkoutTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
            builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
            var scope = builder.Services.AddScoped<DbSeeder>();
            builder.Services.AddDbContext<WorkoutTrackerContext>(x => x.UseSqlServer
            (@"Server=DESKTOP-G3PD29J\SQLEXPRESS;database=WorkoutTracker;
            Trusted_Connection=True;TrustServerCertificate=True;"));
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();

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

            var services = scope.BuildServiceProvider();
            var context = services.GetRequiredService<WorkoutTrackerContext>();
            var seeder = new DbSeeder(context);
            seeder.Seed();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
