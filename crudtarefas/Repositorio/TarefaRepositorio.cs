using crudtarefas.Data;
using crudtarefas.Models;
using crudtarefas.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace crudtarefas.Repositorio
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SistemadeTarefasDBContext _dbContext;
        public TarefaRepositorio(SistemadeTarefasDBContext sistemadeTarefasDBContext) 
        {
            _dbContext = sistemadeTarefasDBContext;
        }
        public async Task<TarefaModel> BuscarPorId(int id)
        {
            return await _dbContext.Tarefas.
                Include(x => x.Usuario).
                FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas.
                Include(x => x.Usuario).
                ToListAsync();
        }
        public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();
            return tarefa;
        }
        public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaporId = await BuscarPorId(id);
            if(tarefaporId == null)
            {
                throw new Exception($"Tarefa com o ID:{id} enviado não foi encontrado"); 
            }
            tarefaporId.Name = tarefa.Name;
            tarefaporId.Description = tarefa.Description;
            tarefaporId.Status = tarefa.Status;
            tarefaporId.UsuarioId = tarefa.UsuarioId;

            _dbContext.Tarefas.Update(tarefaporId);
            await _dbContext.SaveChangesAsync();
            return tarefaporId;
        }

        public async Task<bool> Apagar(int id)
        {
            TarefaModel tarefaporId = await BuscarPorId(id);
            if (tarefaporId == null)
            {
                throw new Exception($"Usuario com o ID:{id} enviado não foi encontrado");
            }

            _dbContext.Tarefas.Remove(tarefaporId);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        

       
    }
}
