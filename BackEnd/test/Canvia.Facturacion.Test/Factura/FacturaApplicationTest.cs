using Canvia.Facturacion.Application.Dtos.Request;
using Canvia.Facturacion.Application.Interfaces;
using Canvia.Facturacion.Utilities.Static;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Canvia.Facturacion.Test.Factura
{
    [TestClass]
    public class FacturaApplicationTest
    {
        private static WebApplicationFactory<Program>? factory = null;
        private static IServiceScopeFactory? scopeFactory = null;
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            factory = new FacturaWebApplicationFactory();
            scopeFactory = factory.Services.GetService<IServiceScopeFactory>();
        }
        [TestMethod]
        public async Task RegisterFactura_WhenSendingItemsNullValuesOrEmpty_ValidationErrors()
        {
            using var scope = scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<IFacturaApplication>();
            //Arrage
            var request = new FacturaCabeceraRequestDto
            {
                ClienteID=1,
                FechaEmision=DateTime.Now,
                Items=null,
            };
            var expected = ReplyMessage.MESSAGE_VALIDATE;
            //Act
            var result = await context!.RegistroFacturaAsync(request);
            var current = result.Message;
            //Assert
            Assert.AreEqual(expected, current);
        }
        [TestMethod]
        public async Task RegisterCategory_WhenSendingCorrectValues_RegisteredSuccesfully()
        {
            using var scope = scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<IFacturaApplication>();
            //Arrage
            var request = new FacturaCabeceraRequestDto
            {
                ClienteID = 1,
                FechaEmision = DateTime.Now,
                Descripcion="descripcion de test",
                Items = new List<FacturaDetalleRequestDto>
                {
                    new FacturaDetalleRequestDto
                    {
                        Cantidad=1,
                        Descripcion="item de test",
                        PrecioUnitario=2.3M
                    }
                },
            };
            var expected = ReplyMessage.MESSAGE_SAVE;
            //Act
            var result = await context!.RegistroFacturaAsync(request);
            var current = result.Message;
            //Assert
            Assert.AreEqual(expected, current);
        }
    }
}
