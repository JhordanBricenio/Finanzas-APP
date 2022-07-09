using Finanzas_APP.DB;
using Finanzas_APP.Models;
using Finanzas_APP.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Finanzas_APP.Controllers
{
    public class CuentaController : Controller
    {
        private readonly ITipoCuentaRepository _tipoCuentaRepository;
        private readonly ICuentaRepository _cuentaRepository;
        private readonly ICuentaTransacRepo cuentaTransacRepo;


        public CuentaController(ITipoCuentaRepository tipoCuentaRepository, ICuentaRepository cuentaRepository, ICuentaTransacRepo cuentaTransacRepo)
        {
            _tipoCuentaRepository = tipoCuentaRepository;
            _cuentaRepository = cuentaRepository;
            this.cuentaTransacRepo = cuentaTransacRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var cuentas = _cuentaRepository.obtenerCuentasPorUsuario(1);

            ViewBag.Total = cuentas.Any()? cuentas.Sum(x => x.Monto):0;

            return View(cuentas);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.TipodeCuentas = _tipoCuentaRepository.ObtenerTodos();
            return View(new Cuenta());
        }
        [HttpPost]
        public ActionResult Create(Cuenta cuenta)
        {
            cuenta.UsuarioId = 1;
            if (cuenta.CategoriaId > 2 || cuenta.CategoriaId < 1)
            {
                ModelState.AddModelError("TipoCuentaId", "Tipo de cuenta no válido");
            }
            var cantidad = _cuentaRepository.contarPorNombre(cuenta);
            if (cantidad > 0)
            {
                ModelState.AddModelError("Nombre", "Ya existe una cuenta con ese nombre");
            }
            if (cuenta.Monto <= 0)
            {
                ModelState.AddModelError("Monto", "Ya existe una cuenta con ese nombre");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.TipodeCuentas = _tipoCuentaRepository.ObtenerTodos();
                return View("create",cuenta);
            }

            _cuentaRepository.Guardar(cuenta);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cuenta = _cuentaRepository.BuscarPorId(id);
            ViewBag.TipodeCuentas = _tipoCuentaRepository.ObtenerTodos();
            return View(cuenta);
        }
        [HttpPost]
        public ActionResult Edit(int id, Cuenta cuenta)
        {


            _cuentaRepository.Editar(id, cuenta);

            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            _cuentaRepository.Eliminar(id);
            return RedirectToAction("Index");
        }
        // GET: CuentaController/Delete/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            decimal contador = 0;
            var transac= cuentaTransacRepo.obtenerPorId(id);
            var cuenta = _cuentaRepository.BuscarPorId(id);
            foreach (var t in transac)
            {
                if (t.Tipo=="gasto")
                {
                    contador=contador+ t.Monto;
                    ViewBag.SaldoGasto = contador;
                }else if (t.Tipo == "Ingreso")
                {
                    contador = contador + t.Monto;
                    ViewBag.SaldoIngreso = contador;
                }              

            }
            if (cuenta.Moneda=="Soles")
            {

            }
            return View(transac);
        }


    }
}
