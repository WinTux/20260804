using Microsoft.EntityFrameworkCore;
using Principal.Repositories;
using AutoMapper;

namespace Principal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IProgramadorRepository, ImplProgramadorRepository>();
            builder.Services.AddDbContext<PrincipalDbContext>(o =>
                o.UseSqlServer(builder.Configuration.GetConnectionString("una_conexion")));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
