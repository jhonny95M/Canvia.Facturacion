using Canvia.Facturacion.Application.Dtos.Request;
using Canvia.Facturacion.Application.Interfaces;
using Canvia.Facturacion.Infraestructure.Commons.Bases.Request;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Canvia.Facturacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaApplication facturaApplication;

        public FacturaController(IFacturaApplication facturaApplication)
        {
            this.facturaApplication = facturaApplication;
        }
        // GET: api/<FacturaController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BaseFiltersRequest filters)
        {
            var response = await facturaApplication.GetAll(filters);
            return Ok(response);
        }

        // GET api/<FacturaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await facturaApplication.GetById(id);
            return Ok(response);
        }

        // POST api/<FacturaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FacturaCabeceraRequestDto request)
        {
            var response=await facturaApplication.RegistroFacturaAsync(request);
            return Ok(response);
        }

        // PUT api/<FacturaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FacturaCabeceraRequestDto request)
        {
            var response = await facturaApplication.EditarFacturaAsync(id,request);
            return Ok(response);
        }

        // DELETE api/<FacturaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await facturaApplication.AnularFacturaAsync(id);
            return Ok(response);
        }
    }
}
