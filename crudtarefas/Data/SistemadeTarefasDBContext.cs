using crudtarefas.Models;
using Microsoft.EntityFrameworkCore;
using crudtarefas.Data.Map;

namespace crudtarefas.Data
{
    public class SistemadeTarefasDBContext:DbContext
    {
        public SistemadeTarefasDBContext(DbContextOptions<SistemadeTarefasDBContext> options) 
            : base(options) 
        {
        }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<TarefaModel> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new TarefaMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
