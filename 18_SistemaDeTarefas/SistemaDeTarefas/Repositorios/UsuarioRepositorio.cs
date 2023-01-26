using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public readonly SistemaTarefasDBContext _DbContext;

        public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _DbContext = sistemaTarefasDBContext;
        }

        public async Task<UsuarioModel> BuscarUsuarioPorId(int id)
        {
            return await _DbContext.Usuarios.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _DbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _DbContext.Usuarios.AddAsync(usuario);
            await _DbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarUsuarioPorId(id); //busca o usuario por ID
            
            if(usuarioPorId == null) //se o ID for nulo, lança essa exeção
            {
                throw new Exception($"Usuario para o ID {id} não foi encontrado no banco de dados!");
            }

            usuarioPorId.Nome= usuario.Nome; //traz essas informações referente ao ID buscado
            usuarioPorId.Email= usuario.Email;

            _DbContext.Usuarios.Update(usuarioPorId); //atualiza o banco de dados
            await _DbContext.SaveChangesAsync(); 

            return usuarioPorId; //retorna o usuario propriamente dito
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarUsuarioPorId(id); //busca o usuario por ID

            if (usuarioPorId == null) //se o ID for nulo, lança essa exeção
            {
                throw new Exception($"Usuario para o ID {id} não foi encontrado no banco de dados!");
            }

            _DbContext.Usuarios.Remove(usuarioPorId);
            await _DbContext.SaveChangesAsync();
            return true;
        }




    }
}
