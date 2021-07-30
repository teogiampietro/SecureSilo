using Microsoft.AspNetCore.Identity;
using Moq;
using SecureSilo.Server.Controllers;
using SecureSilo.Server.Data;
using SecureSilo.Shared;
using SecureSilo.Shared.Identity;
using System.Linq;
using Xunit;

namespace SecureSilo.Tests
{
    public class Test_CamposController
    {
        [Fact]
        public void Test_PostCampo()
        {           
            var mockRepo = new Mock<ApplicationDbContext>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>();
            var service = new CamposController(mockRepo.Object, mockUserManager.Object);

            Campo campoCrear = new Campo() { Id = 1, Descripcion = "Test", UserId = "UsuarioPrueba", LocalidadId = 1, Ubicacion = "Km10" };

            _ = service.Post(campoCrear);
            var campo = service.context.Campos.FirstOrDefault();

            Assert.Equal(campoCrear, campo);
        }
        [Fact]
        public void Test_UpdateCampo()
        {
            var mockRepo = new Mock<ApplicationDbContext>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>();
            var service = new CamposController(mockRepo.Object, mockUserManager.Object);

            Campo campoCrear = new Campo() { Id = 1, Descripcion = "Test", UserId = "UsuarioPrueba", LocalidadId = 1, Ubicacion = "Km10" };
            Campo campoModificado = new Campo() { Id = 1, Descripcion = "Modificado", UserId = "Cambio2", LocalidadId = 1, Ubicacion = "Km10" };

            _ = service.Post(campoCrear);
            _ = service.Put(campoModificado);

            var campoInDb = service.context.Campos.Where(x=>x.Id==1).FirstOrDefault();

            Assert.Equal(campoModificado, campoInDb);
        }

        [Fact]
        public void Test_DeleteCampo()
        {
            var mockRepo = new Mock<ApplicationDbContext>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>();
            var service = new CamposController(mockRepo.Object, mockUserManager.Object);

            Campo campoCrear = new Campo() { Id = 1, Descripcion = "Test", UserId = "UsuarioPrueba", LocalidadId = 1, Ubicacion = "Km10" };

            _ = service.Delete(campoCrear.Id);
            var campo = service.context.Campos.FirstOrDefault();

            Assert.Null(campo);
        }
    }

}
