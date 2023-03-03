using crudtarefas.Data;
using crudtarefas.Models;
using crudtarefas.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace crudtarefas.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemadeTarefasDBContext _dbContext;
        public UsuarioRepositorio(SistemadeTarefasDBContext sistemadeTarefasDBContext) 
        {
            _dbContext = sistemadeTarefasDBContext;
        }
        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }
        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioporId = await BuscarPorId(id);
            if(usuarioporId == null)
            {
                throw new Exception($"Usuario com o ID:{id} enviado não foi encontrado"); 
            }
            usuarioporId.Name = usuario.Name;
            usuarioporId.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioporId);
            await _dbContext.SaveChangesAsync();
            return usuarioporId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioporId = await BuscarPorId(id);
            if (usuarioporId == null)
            {
                throw new Exception($"Usuario com o ID:{id} enviado não foi encontrado");
            }

            _dbContext.Usuarios.Remove(usuarioporId);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        

       
    }
}
