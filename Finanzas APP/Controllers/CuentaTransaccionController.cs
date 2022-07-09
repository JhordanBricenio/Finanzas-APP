using Finanzas_APP.DB;
using Finanzas_APP.Models;
using Finanzas_APP.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finanzas_APP.Controllers
{
    public class CuentaTransaccionController : Controller
    {
        private readonly ICuentaTransacRepo _authRepository;
        private readonly ICuentaRepository cuentaRepository;

        public CuentaTransaccionController(ICuentaTransacRepo authRepository, ICuentaRepository cuentaRepository)
        {
            this._authRepository = authRepository;
            this.cuentaRepository = cuentaRepository;
        }
        [HttpGet]
        public ActionResult Index(int cuentaId)
        {
            var items = _authRepository.obtenerPorId(cuentaId);
            ViewBag.CuentaId = cuentaId;
            ViewBag.Total =items.Any()? items.Sum(x => x.Monto):0;
            return View(items);
        }

        [HttpGet]
        public ActionResult Create(int cuentaId)
        {
            ViewBag.CuentaId = cuentaId;
            return View(new Transaccion());
        }

        [HttpPost]
        public ActionResult Create(int cuentaId, Transaccion transaccion)
        {
            var cuenta= cuentaRepository.BuscarPorId(cuentaId);
            transaccion.CuentaId = cuentaId;

            if (transaccion.Monto == 0)
            {

                ModelState.AddModelError("Monto", "El monto debe ser mayor a cero");
            }
            //Categoria ==Propio
            if (cuenta.CategoriaId==1)
            {
                if (transaccion.Monto>cuenta.Monto)
                {
                    ModelState.AddModelError("Monto", "El monto excede la cantidad de la tarjeta");
                }
            }

            if (ModelState.IsValid)
            {
               
                if (transaccion.Tipo == "Gasto")
                {
                    transaccion.Monto = transaccion.Monto * -1;
                }
                _authRepository.insertar(transaccion);
                cuenta.Monto=cuenta.Monto - transaccion.Monto;
                return RedirectToAction("Index", new { cuentaId = cuentaId });


            }

            ViewBag.CuentaId = cuentaId;
            return View(new Transaccion());

        }

    }
}
