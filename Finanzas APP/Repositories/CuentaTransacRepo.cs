using Finanzas_APP.DB;
using Finanzas_APP.Models;

namespace Finanzas_APP.Repositories
{
    public interface ICuentaTransacRepo
    {
        List<Transaccion> obtenerPorId(int idCuenta);

        List<Transaccion> obtenerPorTipo(String nombre);
        void insertar(Transaccion transaccion);



    }
    public class CuentaTransacRepo : ICuentaTransacRepo
    {
        private BbEntities _dbEntities;
        public CuentaTransacRepo(BbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public void insertar(Transaccion transaccion)
        {
            _dbEntities.Transaccions.Add(transaccion);
            _dbEntities.SaveChanges();
        }

        public List<Transaccion> obtenerPorId(int idCuenta)
        {
            return _dbEntities.Transaccions
                .Where(x => x.CuentaId == idCuenta)
                .ToList();
        }

        public List<Transaccion> obtenerPorTipo(string tipo)
        {
            return _dbEntities.Transaccions.Where(x => x.Tipo == tipo).ToList();
        }
    }
}
