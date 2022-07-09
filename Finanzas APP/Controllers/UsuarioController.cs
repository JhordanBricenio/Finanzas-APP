using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Finanzas_APP.Models;
using Finanzas_APP.DB;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Finanzas_APP.Repositories;

namespace Finanzas_APP.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
       // private BbEntities bbEnetities;

        private IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        // GET: UsuarioController
        public ActionResult Index()
        {
            var users = _usuarioRepository.ObteberTodos();
            return View(users);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("AuthError", "Usuario o contraseña incorrectos");
                return View("create", usuario);
            }
            _usuarioRepository.Guardar(usuario);
            //usuario.Id = GetLoggerUser().Id;

            return RedirectToAction("Index");
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        private Usuario GetLoggerUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            var username = claim.Value;
            return _usuarioRepository.ObtenerPorNombre(username);
        }
    }
}
