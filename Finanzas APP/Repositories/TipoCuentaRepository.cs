using Finanzas_APP.DB;
using Finanzas_APP.Models;

namespace Finanzas_APP.Repositories
{
    public interface ITipoCuentaRepository 
    {
        List<Categoria> ObtenerTodos();
        List<Categoria> ObtenerPorNombre(String nombre);

        void Guardar(Categoria tipoCuenta);

    }
    public class TipoCuentaRepository : ITipoCuentaRepository
    {
        private BbEntities _dbEntities;

        public TipoCuentaRepository(BbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public List<Categoria> ObtenerTodos()
        {
            return _dbEntities.TipoCuentas.ToList();
        }


        public List<Categoria> ObtenerPorNombre(string nombre)
        {
            return _dbEntities
                .TipoCuentas.Where(x => x.Nombre.Contains(nombre))
                .ToList();
        }

        public void Guardar(Categoria tipoCuenta)
        {
            _dbEntities.TipoCuentas.Add(tipoCuenta);
        }
    }
}
