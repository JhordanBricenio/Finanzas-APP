using Finanzas_APP.DB;
using Finanzas_APP.Models;

namespace Finanzas_APP.Repositories
{
    public interface IUsuarioRepository
    {
        List<Usuario> ObteberTodos();

        Usuario ObtenerPorNombre(String nombre);

        void Guardar(Usuario usuario);
   
    }
    public class UsuarioRepo : IUsuarioRepository
    {
        private BbEntities _dbEntities;
        public UsuarioRepo(BbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }
        public void Guardar(Usuario usuario)
        {
            _dbEntities.Usuarios.Add(usuario);
            _dbEntities.SaveChanges();
        }

        public List<Usuario> ObteberTodos()
        {
            return _dbEntities.Usuarios.ToList();
        }

        public Usuario ObtenerPorNombre(string nombre )//admin
        {
            return _dbEntities.Usuarios.First(o => o.UserName == nombre);
        }
    }
}
