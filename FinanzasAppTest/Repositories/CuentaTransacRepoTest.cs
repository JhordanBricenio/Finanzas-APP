using Finanzas_APP.DB;
using Finanzas_APP.Models;
using Finanzas_APP.Repositories;
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
    public  class CuentaTransacRepoTest
    {
        private static IQueryable<Transaccion>? data;
        [SetUp]
        public void Setup()
        {
            new Cuenta()
            {
                Id = 1,
                Monto = 200,
                Nombre = "Cuenta01"
            };
            data = new List<Transaccion>
            {
                new Transaccion { Id = 1, CuentaId=1, Monto=2000, Tipo="Ingreso" },
                new Transaccion { Id = 2, CuentaId=1,Monto=200, Tipo="Egreso" },

            }.AsQueryable();
        }
        [Test]
        public void Guardar()
        {
            var mockBdSetCuenta = new MockDbSet<Transaccion>(data);

           


            var mockBd = new Mock<BbEntities>();
            mockBd.Setup(x => x.Transaccions).Returns(mockBdSetCuenta.Object);

            var cuentaTransRepo = new CuentaTransacRepo(mockBd.Object);

            cuentaTransRepo.insertar(new Transaccion());

            //Assert.AreEqual(2, result.Count);

        }


        [Test]
        public void ObtenerPorIdTest01()
        {
            var mockBdSetCuenta = new MockDbSet<Transaccion>(data);

            var mockBd = new Mock<BbEntities>();
            mockBd.Setup(x => x.Transaccions).Returns(mockBdSetCuenta.Object);

            var cuentaTransRepo = new CuentaTransacRepo(mockBd.Object);

            var result=cuentaTransRepo.obtenerPorId(1);

            Assert.AreEqual(2, result.Count);


        }

        [Test]
        public void ObtenerPorNombreTest01()
        {
            var mockBdSetCuenta = new MockDbSet<Transaccion>(data);

            var mockBd = new Mock<BbEntities>();
            mockBd.Setup(x => x.Transaccions).Returns(mockBdSetCuenta.Object);

            var cuentaTransRepo = new CuentaTransacRepo(mockBd.Object);

            var result = cuentaTransRepo.obtenerPorTipo("Ingreso");

            Assert.AreEqual(1, result.Count);


        }
    }
}
