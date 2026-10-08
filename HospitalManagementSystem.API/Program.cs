
using HospitalManagementSystem.API.Data;
using HospitalManagementSystem.API.Repositories;
using HospitalManagementSystem.API.Services;
namespace HospitalManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSingleton<DbConnection>();

            builder.Services.AddScoped<PatientRepository>();
            builder.Services.AddScoped<PatientService>();

            builder.Services.AddScoped<DoctorRepository>();
            builder.Services.AddScoped<DoctorService>();

            builder.Services.AddScoped<AppointmentRepository>();
            builder.Services.AddScoped<AppointmentService>();
            
            builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
