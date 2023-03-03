using crudtarefas.Models;
using crudtarefas.Repositorio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crudtarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        protected readonly ITarefaRepositorio _tarefaRepositorio;
        public TarefaController(ITarefaRepositorio tarefaRepositorio) 
        {
            _tarefaRepositorio= tarefaRepositorio;
        }
        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> BuscarTodasTarefas()
        {
            List<TarefaModel> tarefa = await _tarefaRepositorio.BuscarTodasTarefas();
            return Ok(tarefa);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaModel>> BuscarPorID(int id)
        {
            TarefaModel tarefa = await _tarefaRepositorio.BuscarPorId(id);
            return Ok(tarefa);
        }
        [HttpPost]

        public async Task<ActionResult<TarefaModel>> Cadastrar([FromBody] TarefaModel tarefa)
        {
            TarefaModel tarefaModel =  await _tarefaRepositorio.Adicionar(tarefa);
            return Ok(tarefaModel);
        }

        [HttpPut]
        public async Task<ActionResult<TarefaModel>> Atualizar([FromBody] TarefaModel tarefa, int id)
        {
            tarefa.Id = id;
            TarefaModel tarefaModel = await _tarefaRepositorio.Atualizar(tarefa, id);
            return Ok(tarefaModel);
        }
        [HttpDelete]
        public async Task<ActionResult<TarefaModel>> Apagar(int id)
        {
            bool deletado = await _tarefaRepositorio.Apagar(id);
            return Ok(deletado);
        }
    }
}
