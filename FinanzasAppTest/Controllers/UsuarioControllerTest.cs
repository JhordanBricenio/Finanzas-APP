using Finanzas_APP.Controllers;
using Finanzas_APP.Models;
using Finanzas_APP.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanzasAppTest.Controllers
{
    public class UsuarioControllerTest
    {
        [Test]
        public void Index01()
        {
            var mockUsuario = new Mock<IUsuarioRepository>();
            mockUsuario.Setup(x => x.ObteberTodos()).Returns(new List<Usuario>() { new Usuario() });
            var usuario = new UsuarioController(mockUsuario.Object);
            
            var result =(ViewResult) usuario.Index();
            
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((List<Usuario>)result.Model).Count);
        }

        [Test]
        public void CreatePost02()
        {
            var mockUsuario = new Mock<IUsuarioRepository>();
            mockUsuario.Setup(x => x.ObteberTodos()).Returns(new List<Usuario>() { new Usuario() });
            var usuario = new UsuarioController(mockUsuario.Object);

            var result =usuario.Create(new Usuario());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public void CreatePost02Crash()
        {
            var mockUsuario = new Mock<IUsuarioRepository>();
            mockUsuario.Setup(x => x.ObtenerPorNombre("admin"));
          
            var usuario = new UsuarioController(mockUsuario.Object);

            var result = usuario.Create(new Usuario());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

    }
}