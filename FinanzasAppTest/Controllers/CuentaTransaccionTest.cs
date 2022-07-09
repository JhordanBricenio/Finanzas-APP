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
    public class CuentaTransaccionTest
    {
        [Test]
        public void indexCase01()
        {
            var mockCuentaTrans = new Mock<ICuentaTransacRepo>();
            mockCuentaTrans.Setup(x => x.obtenerPorId(1)).Returns(new List<Transaccion>() {
                new Transaccion() { Id = 1, Fecha = DateTime.Now, Monto = 100 },
                new Transaccion() { Id = 1, Fecha = DateTime.Now, Monto = 100 }

            });
            var cuentaTransacion = new CuentaTransaccionController(mockCuentaTrans.Object, null);
            var result =cuentaTransacion.Index(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateGet()
        {
            var mockCuentaTrans = new Mock<ICuentaTransacRepo>();
            var cuentaTransacion = new CuentaTransaccionController(mockCuentaTrans.Object, null);
            var result = (ViewResult)cuentaTransacion.Create(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreatePostCorrecto()
        {
            var mockCuentaTrans = new Mock<ICuentaTransacRepo>();
            mockCuentaTrans.Setup(x => x.obtenerPorId(1)).Returns(new List<Transaccion>() {
                new Transaccion() { Id = 1, Fecha = DateTime.Now, Monto = 100 },
                new Transaccion() { Id = 1, Fecha = DateTime.Now, Monto = 100 }

            });
            var mockCuenta = new Mock<ICuentaRepository>();
            mockCuenta.Setup(o => o.BuscarPorId(1)).Returns(new Cuenta() { Id = 1, CategoriaId = 2, Monto = 1 });

            var cuentaTransacion = new CuentaTransaccionController(mockCuentaTrans.Object, mockCuenta.Object);
            var result = cuentaTransacion.Create(1, new Transaccion() { Monto = 2 });
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public void CreatePostMontoExcedeCantidadCorrecto()
        {
            var mockCuentaTrans = new Mock<ICuentaTransacRepo>();
                mockCuentaTrans.Setup(x => x.obtenerPorId(1)).Returns(new List<Transaccion>() {
                new Transaccion() { Id = 1, Fecha = DateTime.Now, Monto = 100 },
                new Transaccion() { Id = 1, Fecha = DateTime.Now, Monto = 100 }

            });

            var mockCuenta = new Mock<ICuentaRepository>();
            mockCuenta.Setup(o => o.BuscarPorId(1)).Returns(new Cuenta() { Id = 1, CategoriaId=1 , Monto=1 }) ;

            var cuentaTransacion = new CuentaTransaccionController(mockCuentaTrans.Object, mockCuenta.Object);
            var result = cuentaTransacion.Create(1, new Transaccion() { Monto=2});
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}
