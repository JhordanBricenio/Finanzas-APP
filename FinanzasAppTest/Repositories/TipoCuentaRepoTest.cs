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
    public class TipoCuentaRepoTest
    {
        private IQueryable<Categoria> data;
        private Mock<BbEntities> mockDB;
        
        [SetUp]
        public void Setup()
        {
            data = new List<Categoria>
            {
                new Categoria { Id = 1, Nombre = "Cuenta Corriente" },
                new Categoria { Id = 2, Nombre = "Cuenta Vista" },

            }.AsQueryable();
            var mockBdSetTipoCuenta = new MockDbSet<Categoria>(data);

            mockDB = new Mock<BbEntities>();
            mockDB.Setup(x => x.TipoCuentas).Returns(mockBdSetTipoCuenta.Object);


        }
        [Test]
        public void  ObtenerTodosTestCaso01()
        {


            var tipoRepo = new TipoCuentaRepository(mockDB.Object);
            
            var result = tipoRepo.ObtenerTodos();

            Assert.AreEqual(2, result.Count);
        }
        
        [Test]
        public void ObtenerPorNombreCase01()
        {
            var mockBdSetCuenta = new MockDbSet<Categoria>(data);


            var mockBd = new Mock<BbEntities>();
            mockBd.Setup(x => x.TipoCuentas).Returns(mockBdSetCuenta.Object);

            var cuentaRepo = new TipoCuentaRepository(mockBd.Object);

            var result = cuentaRepo.ObtenerPorNombre("Cuenta Corriente");
            Assert.AreEqual(1, result.Count);
        }
        
        [Test]
        public void ObtenerPorNombreCase02()
        {
            var mockBdSetCuenta = new MockDbSet<Categoria>(data);


            var mockBd = new Mock<BbEntities>();
            mockBd.Setup(x => x.TipoCuentas).Returns(mockBdSetCuenta.Object);

            var cuentaRepo = new TipoCuentaRepository(mockBd.Object);

            var result = cuentaRepo.ObtenerPorNombre("Efectivo");
            Assert.AreEqual(0, result.Count);
        }
    }
}
