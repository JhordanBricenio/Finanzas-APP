using Finanzas_APP.DB;
using Finanzas_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Finanzas_APP.Repositories
{
    public interface ICuentaRepository 
    {
        Cuenta BuscarPorId(int id);
        List<Cuenta> obtenerCuentasPorUsuario(int id);

        List<Cuenta> obtenerTodos();
        void Guardar(Cuenta cuenta);

        void Editar(int id, Cuenta cuenta);

        void Eliminar(int id);

        int contarPorNombre(Cuenta cuenta);

    }
    public class CuentaRepository : ICuentaRepository
    {
        private BbEntities _dbEntities;
        public CuentaRepository(BbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public Cuenta BuscarPorId(int id)
        {
            return _dbEntities.Cuentas.First(o => o.Id == id);
        }

        public List<Cuenta> obtenerCuentasPorUsuario(int id)
        {
            return _dbEntities.Cuentas
                .Include(o => o.Categoria)
                .Where(o => o.UsuarioId == id).ToList();
        }

        public void Guardar(Cuenta cuenta)
        {
            _dbEntities.Cuentas.Add(cuenta);
            _dbEntities.SaveChanges();
        }
        
        public void Editar(int id, Cuenta cuenta)
        {
            var cuentaDb = BuscarPorId(id);
            cuentaDb.Nombre = cuenta.Nombre;
            _dbEntities.SaveChanges();
        }
        public void Eliminar(int id)
        {
            var cuentaDb = BuscarPorId(id);
            _dbEntities.Cuentas.Remove(cuentaDb);
            _dbEntities.SaveChanges();
        }

        public List<Cuenta> obtenerTodos()
        {
            return _dbEntities.Cuentas.ToList();
        }

        public int contarPorNombre(Cuenta cuenta)
        {
            return _dbEntities.Cuentas.Count(o => o.Nombre == cuenta.Nombre);
        }
    }
}
