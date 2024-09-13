using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Core.Domain;

namespace WorkoutTracker.Infrasctructure.Data
{
    public class WorkoutTrackerContext : DbContext
    {
        public WorkoutTrackerContext(DbContextOptions<WorkoutTrackerContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workout>()
                .HasOne(w => w.User)
                .WithMany(w => w.Workouts)
                .HasForeignKey(w => w.UserId);

            //modelBuilder.Entity<Workout>()
            //    .HasMany(w => w.Exercises)
            //    .WithMany(w => w.Workouts);

            modelBuilder.Entity<WorkoutExercise>()
            .HasKey(we => new { we.WorkoutId, we.ExerciseId });

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Workout)
                .WithMany(w => w.WorkoutExercises)
                .HasForeignKey(we => we.WorkoutId);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Exercise)
                .WithMany(e => e.WorkoutExercises)
                .HasForeignKey(we => we.ExerciseId);
        }
    }
}
