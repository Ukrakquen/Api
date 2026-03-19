using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Peliculas.Api.DTOs;
using Peliculas.Api.Services;

namespace Peliculas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    // referencia
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaService _service;

        // 0 referencias
        public PeliculasController(IPeliculaService service)
        {
            _service = service;
        }

        [HttpGet]
        // 0 referencias
        public async Task<ActionResult<List<PeliculaDto>>> ListarPeliculas()
            => Ok(await _service.ListarPeliculasAsync());

        [HttpGet("{id:int}")]
        // 1 referencia
        public async Task<ActionResult<PeliculaDto>> ObtenerPeliculaPorId(int id)
        {
            var pelicula = await _service.ObtenerPeliculaPorIdAsync(id);
            if (pelicula is null) return NotFound(new { mensaje = "Película no encontrada." });
            return Ok(pelicula);
        }

        [HttpGet("buscar")]
        // 0 referencias
        public async Task<ActionResult<List<PeliculaDto>>> BuscarPeliculas([FromQuery] string texto)
    => Ok(await _service.BuscarPeliculasAsync(texto));

        [HttpPost]
        // 0 referencias
        public async Task<ActionResult<PeliculaDto>> CrearPelicula([FromBody] PeliculaCreateDto dto)
        {
            var (ok, error, pelicula) = await _service.CrearPeliculaAsync(dto);
            if (!ok) return BadRequest(new { mensaje = error });

            return CreatedAtAction(
                nameof(ObtenerPeliculaPorId),
                new { id = pelicula!.Id },
                pelicula
            );
        }

        [HttpPut("{id:int}")]
        // 0 referencias
        public async Task<IActionResult> ActualizarPelicula(int id, [FromBody] PeliculaUpdateDto dto)
        {
            var (ok, error) = await _service.ActualizarPeliculaAsync(id, dto);
            if (!ok) return BadRequest(new { mensaje = error });
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        // 0 referencias
        public async Task<IActionResult> EliminarPelicula(int id)
        {
            var (ok, error) = await _service.EliminarPeliculaAsync(id);
            if (!ok) return BadRequest(new { mensaje = error });
            return NoContent();
        }
    }
}