using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiendaController : ControllerBase
    {
        private readonly ITiendaService _tiendaService;

        public TiendaController(ITiendaService tiendaService)
        {
            _tiendaService = tiendaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tiendas = await _tiendaService.GetAllAsync();
            return Ok(tiendas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tienda = await _tiendaService.GetByIdAsync(id);
            if (tienda == null) return NotFound();
            return Ok(tienda);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TiendaDto dto)
        {
            var result = await _tiendaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TiendaDto dto)
        {
            if (id != dto.Id) return BadRequest("ID no coincide");
            var updated = await _tiendaService.UpdateAsync(dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _tiendaService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
