using crudtarefas.Enums;
using crudtarefas.Models;

namespace crudtarefas.Repositorio.Interfaces
{
    public interface ITarefaRepositorio
    {
        Task<List<TarefaModel>> BuscarTodasTarefas();
        Task<TarefaModel> BuscarPorId(int id);

        Task<List<TarefaModel>> BuscarPorStatus(StatusTarefas status);
        Task<List<TarefaModel>> BuscarPorUsuario(int usuarioId);

        Task<TarefaModel> Adicionar(TarefaModel tarefa);

        Task<TarefaModel> Atualizar(TarefaModel tarefa,int id);

        Task<bool> Apagar(int id);
    }
}
