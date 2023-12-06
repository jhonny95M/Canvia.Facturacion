using Canvia.Facturacion.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Canvia.Facturacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteApplication clienteApplication;

        public ClienteController(IClienteApplication clienteApplication)
        {
            this.clienteApplication = clienteApplication;
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await clienteApplication.GetAll(null);
            return Ok(response);
        }
        [HttpGet("select")]
        public async Task<IActionResult> GetSelect()
        {
            var response = await clienteApplication.SelectClientes();
            return Ok(response);
        }
    }
}
