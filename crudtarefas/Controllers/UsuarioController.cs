using crudtarefas.Models;
using crudtarefas.Repositorio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crudtarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        protected readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio) 
        {
            _usuarioRepositorio= usuarioRepositorio;
        }
        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarTodosUsuarios()
        {
            List<UsuarioModel> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> BuscarPorID(int id)
        {
            UsuarioModel usuarios = await _usuarioRepositorio.BuscarPorId(id);
            return Ok(usuarios);
        }
        [HttpPost]

        public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody] UsuarioModel usuario)
        {
            UsuarioModel usuarioModel =  await _usuarioRepositorio.Adicionar(usuario);
            return Ok(usuarioModel);
        }

        [HttpPut]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuario, int id)
        {
            usuario.Id = id;
            UsuarioModel usuarioModel = await _usuarioRepositorio.Atualizar(usuario,id);
            return Ok(usuarioModel);
        }
        [HttpDelete]
        public async Task<ActionResult<UsuarioModel>> Apagar(int id)
        {
            bool deletado = await _usuarioRepositorio.Apagar(id);
            return Ok(deletado);
        }
    }
}
