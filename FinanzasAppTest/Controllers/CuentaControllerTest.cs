using Finanzas_APP.Controllers;
using Finanzas_APP.Models;
using Finanzas_APP.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;

namespace FinanzasAppTest.Controllers
{
    public class CuentaControllerTest
    {
        [Test]
        public void obtenerTodosCreate()
        {
            var mockTipo = new Mock<ITipoCuentaRepository>();
            var controller = new CuentaController(mockTipo.Object, null, null);
            var view = controller.Create();

            Assert.IsNotNull(view);
        }

        [Test]
        public void editarCuentaVerificarId()
        {
            var mockCuenta = new Mock<ICuentaRepository>();
            var mockTipo = new Mock<ITipoCuentaRepository>();
            mockCuenta.Setup(x => x.BuscarPorId(1)).Returns(new Cuenta { Id = 1, Nombre = "Banco", CategoriaId = 1 });

            var controller = new CuentaController(mockTipo.Object, mockCuenta.Object, null);
            var view = (ViewResult)controller.Edit(1);


            Assert.IsNotNull(view.Model);//Modelo  enviado hacia la vsita no es nulo
            Assert.IsNotNull(view);//verifcar si retorna una vista
        }

        [Test]
        public void IndexTest01()
        {
            
          
            
            var mockCuentaRepo = new Mock<ICuentaRepository>();
            mockCuentaRepo.Setup(o => o.obtenerCuentasPorUsuario(1)).Returns(new List<Cuenta>
            {
                new Cuenta()
            });

            var controller = new CuentaController(null, mockCuentaRepo.Object, null);
            controller.ControllerContext = new ControllerContext();
            var view =(ViewResult)controller.Index();

            //Validamos que la vista no sea nula
            Assert.IsNotNull(view);
            //Validamos que estamos esperando una cuenta como minimo
            Assert.AreEqual(1, ((List<Cuenta>)view.Model).Count);
        }

        [Test]
        public void CreatePostCorrecto()
        {
            
            
            var mockCuenta = new Mock<ICuentaRepository>();


            var controller = new CuentaController(null, mockCuenta.Object, null);
            controller.ControllerContext = new ControllerContext();
            

            var result=controller.Create(new Cuenta() {CategoriaId=2 });
            //Validamos que la vista no sea nula
            Assert.IsNotNull(result);
            //Preguntamos si es el resultado es una instancia de la clase RedirectToActionResult
            //ya que retorna 2 tipos de datos viewResult y RedirectToActionResult
            Assert.IsInstanceOf<RedirectToActionResult>(result);

        }
        [Test]
        public void CreatePostCrash()
        {
            

            var mockCuenta = new Mock<ICuentaRepository>();

            var mockTipo = new Mock<ITipoCuentaRepository>();


            var controller = new CuentaController(mockTipo.Object, mockCuenta.Object, null);
            controller.ControllerContext = new ControllerContext();
           
            var result = controller.Create(new Cuenta() { CategoriaId = 0 });
            //Validamos que la vista no sea nula
            Assert.IsNotNull(result);
            //Preguntamos si es el resultado es una instancia de la clase RedirectToActionResult
            //ya que retorna 2 tipos de datos viewResult y RedirectToActionResult
            Assert.IsInstanceOf<ViewResult>(result);

        }
        
        [Test]
        public void EditarPostCase01ModelValid02()
        {
            var mockCuentaRepo = new Mock<ICuentaRepository>();
            var mockTipo = new Mock<ITipoCuentaRepository>();
            var controller = new CuentaController(mockTipo.Object, mockCuentaRepo.Object, null);
            var result = controller.Edit(1, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public void DeleteGet01()
        {
            var mockCuentaRepo = new Mock<ICuentaRepository>();
            var controller = new CuentaController(null, mockCuentaRepo.Object, null);
            var result = controller.Delete(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public void DetailsGet01()
        {
            var mockCuentaTrans = new Mock<ICuentaTransacRepo>();
            mockCuentaTrans.Setup(x => x.obtenerPorId(1)).Returns(new List<Transaccion>() {
                new Transaccion() { Id = 1, Monto = 100 }

            });
            var mockCuentaRepo = new Mock<ICuentaRepository>();
            mockCuentaRepo.Setup(o=>o.BuscarPorId(1)).Returns(new Cuenta());
            var controller = new CuentaController(null, mockCuentaRepo.Object, mockCuentaTrans.Object);
            var result = controller.Details(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}
