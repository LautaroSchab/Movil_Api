using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APItp6.Context;
using APItp6.Models;
using Microsoft.IdentityModel.Tokens;

namespace APItp6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbcontext _context;

        public ProductoController(AppDbcontext context)
        {
            _context = context;
        }


        [HttpGet(Name = "ObtenerTodos")]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var lista = await _context.Producto.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerPorId/{productoId:int}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute(Name = "productoId")] int id)
        {
            try
            {
                var item = await _context.Producto.FirstOrDefaultAsync(x => x.IdProducto == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Crear([FromBody] Producto producto)
        {
            try
            {
                //producto.RutaImagen = "Imagen/";
                await _context.Producto.AddAsync(producto);
                var result = await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{productoId:int}")]
        public async Task<IActionResult> Borrar([FromRoute] int productoId)
        {
            try
            {
                var productoExistente = await _context.Producto.FindAsync(productoId);

                if (productoExistente != null)
                {
                    _context.Producto.Remove(productoExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{productoId:int}")]
        public async Task<IActionResult> Modificar([FromBody] Producto producto, [FromRoute] int productoId)
        {
            try
            {
                var productoExistente = await _context.Producto.FindAsync(productoId);

                if (productoExistente != null)
                {
                    if (!producto.Descripcion.IsNullOrEmpty()) productoExistente.Descripcion = producto.Descripcion;
                    if (!producto.NombreProducto.IsNullOrEmpty()) productoExistente.NombreProducto = producto.NombreProducto;
                    //if (!producto.RutaImagen.IsNullOrEmpty()) productoExistente.RutaImagen = producto.RutaImagen;
                    if (producto.PrecioProducto != null) productoExistente.PrecioProducto = producto.PrecioProducto;
                    //if (producto.Stock != null) productoExistente.Stock = producto.Stock;

                    _context.Producto.Update(productoExistente);
                    await _context.SaveChangesAsync();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}