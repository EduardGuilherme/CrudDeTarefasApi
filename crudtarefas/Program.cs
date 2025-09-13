using crudtarefas.Data;
using crudtarefas.Repositorio;
using crudtarefas.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace crudtarefas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DbContext e Repositorios
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<SistemadeTarefasDBContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
                );
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();

            // ?? Habilitar CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200") // Angular Dev Server
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Criar banco e aplicar migrations automaticamente
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SistemadeTarefasDBContext>();
                db.Database.Migrate(); // aplica migrations
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // ?? Aplicar CORS ANTES do MapControllers
            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}