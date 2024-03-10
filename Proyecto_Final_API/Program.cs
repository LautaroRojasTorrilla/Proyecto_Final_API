using Microsoft.EntityFrameworkCore;
using Proyecto_Final_API.Database;
using Proyecto_Final_API.Service;

namespace Proyecto_Final_API
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
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<ProductoService>();

            //inyecto la conexión con la base de datos
            builder.Services.AddDbContext<CoderContext>(options =>
            {
                options.UseSqlServer("Server=.;Database=coderhouse;Trusted_Connection=True");
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
