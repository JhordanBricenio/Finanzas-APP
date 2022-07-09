using Finanzas_APP.DB;
using Finanzas_APP.Models;
using Finanzas_APP.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanzasAppTest.Helpers;

namespace FinanzasAppTest.Repositories
{
    public class CuentaRepoTest
    {
        private static IQueryable<Cuenta>? data;
        [SetUp]
        public void Setup()
        {
            data = new List<Cuenta>
            {
                new Cuenta { Id = 1, Nombre = "Cuenta 01" },
                new Cuenta { Id = 2, Nombre = "Cuenta 02" },

            }.AsQueryable();
        }
        [Test]
        public void  ObtenerTodosTestCaso01()
        {
            var mockBdSetCuenta = new MockDbSet<Cuenta>(data);

            //mockBdSetCuenta.As<IQueryable<Cuenta>>().Setup(m => m.Provider).Returns(data.Provider);
           // mockBdSetCuenta.As<IQueryable<Cuenta>>().Setup(m => m.Expression).Returns(data.Expression);
            //mockBdSetCuenta.As<IQueryable<Cuenta>>().Setup(m => m.ElementType).Returns(data.ElementType);
          // mockBdSetCuenta.As<IQueryable<Cuenta>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var mockBd = new Mock<BbEntities>();
            mockBd.Setup(x => x.Cuentas).Returns(mockBdSetCuenta.Object);

            var cuentaRepo = new CuentaRepository(mockBd.Object);

            var result = cuentaRepo.obtenerTodos();

            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void Guardar()
        {
            var mockBdSetCuenta = new MockDbSet<Cuenta>(data);

            var mockBd = new Mock<BbEntities>();
            mockBd.Setup(x => x.Cuentas).Returns(mockBdSetCuenta.Object);

            var cuentaRepo = new CuentaRepository(mockBd.Object);

            cuentaRepo.Guardar(new Cuenta());

            //Assert.Throws<NullReferenceException>(() => cuentaRepo.Guardar(new Cuenta() { Id = 1, Nombre = "Cuenta 01" }));
            //Assert.ThrowsException<ArgumentOutOfRangeException>(() => cuentaRepo.Guardar(new Cuenta()));
            
            Assert.AreEqual(2, data.Count());

        }
        [Test]
        public void Editar()
        {
            var mockBdSetCuenta = new MockDbSet<Cuenta>(data);

            var mockBd = new Mock<BbEntities>();
            mockBd.Setup(x => x.Cuentas).Returns(mockBdSetCuenta.Object);

            var cuentaRepo = new CuentaRepository(mockBd.Object);

            cuentaRepo.Editar(1,new Cuenta());

            


        }
        [Test]
        public void Eliminar()
        {
            var mockBdSetCuenta = new MockDbSet<Cuenta>(data);

            var mockBd = new Mock<BbEntities>();
            mockBd.Setup(x => x.Cuentas).Returns(mockBdSetCuenta.Object);

            var cuentaRepo = new CuentaRepository(mockBd.Object);

            cuentaRepo.Eliminar(1);


        }

    }
}
