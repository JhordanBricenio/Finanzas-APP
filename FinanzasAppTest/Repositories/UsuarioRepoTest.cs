using Finanzas_APP.DB;
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
using FinanzasAppTest.Helpers;

namespace TestCuentaBancaria.Repositories
{
    public  class UsuarioRepoTest
    {
        private static IQueryable<Usuario>? data;
        [SetUp]
        public void Setup()
        {

            data = new List<Usuario>
            {
                new Usuario { Id = 1, UserName = "admin", Password = "admin" },
                new Usuario { Id = 2, UserName = "test", Password = "test"  },

            }.AsQueryable();
        }
        [Test]
        public void Guardar()
        {
            var mockBdSetCuenta = new MockDbSet<Usuario>(data);

            var mockBd = new Mock<BbEntities>();
            mockBd.Setup(x => x.Usuarios).Returns(mockBdSetCuenta.Object);

            var cuentaTransRepo = new UsuarioRepo(mockBd.Object);

            cuentaTransRepo.Guardar(new Usuario());

            //Assert.AreEqual(2, result.Count);

        }

        
        [Test]
        public void ObtenerTodosCase01()
        {
            var mockBdSetCuenta = new MockDbSet<Usuario>(data);

            var mockBd = new Mock<BbEntities>();
            mockBd.Setup(x => x.Usuarios).Returns(mockBdSetCuenta.Object);

            var cuentaTransRepo = new UsuarioRepo(mockBd.Object);

            var result=cuentaTransRepo.ObteberTodos();

            Assert.AreEqual(2, result.Count);

        }
        [Test]
        public void ObtenerPorNombre01()
        {
            var mockBdSetCuenta = new MockDbSet<Usuario>(data);
            
            var mockBd = new Mock<BbEntities>();
            mockBd.Setup(x => x.Usuarios).Returns(mockBdSetCuenta.Object);

            var cuentaTransRepo = new UsuarioRepo(mockBd.Object);

            var result=cuentaTransRepo.ObtenerPorNombre("test");

            Assert.AreEqual(false, result.Equals("test"));

        }



    }
}
