
using HospitalManagementSystem.API.Data;
using HospitalManagementSystem.API.Repositories;
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

            builder.Services.AddSingleton<PatientRepository>();

            builder.Services.AddSingleton<DoctorRepository>();

            builder.Services.AddSingleton<AppointmentRepository>();
            
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
