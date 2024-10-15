using APItp6.Context;
using APItp6.Models;
using APItp6.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APItp6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbcontext _context;

        public UsuarioController(AppDbcontext context)
        {
            _context = context;
        }

        [HttpGet(Name = "ObtenerUsuarios")]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var lista = await _context.Usuario.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerUsuarioPorId/{usuarioId:int}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute(Name = "usuarioId")] int id)
        {
            try
            {
                var item = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Crear([FromBody] Usuario usuario)
        {
            try
            {
                //producto.RutaImagen = "Imagen/";
                await _context.Usuario.AddAsync(usuario);
                var result = await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{usuarioId:int}")]
        public async Task<IActionResult> Borrar([FromRoute] int usuarioId)
        {
            try
            {
                var usuarioExistente = await _context.Usuario.FindAsync(usuarioId);

                if (usuarioExistente != null)
                {
                    _context.Usuario.Remove(usuarioExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{usuarioId:int}")]
        public async Task<IActionResult> Modificar([FromBody] Usuario usuario, [FromRoute] int usuarioId)
        {
            try
            {
                var usuarioExistente = await _context.Usuario.FindAsync(usuarioId);

                if (usuarioExistente != null)
                {
                    if (!usuario.NombreUsuario.IsNullOrEmpty()) usuarioExistente.NombreUsuario = usuario.NombreUsuario;
                    if (!usuario.Apellido.IsNullOrEmpty()) usuarioExistente.Apellido = usuario.Apellido;
                    if (!usuario.Mail.IsNullOrEmpty()) usuarioExistente.Mail = usuario.Mail;
                    if (usuario.NumTelefono != null) usuarioExistente.NumTelefono = usuario.NumTelefono;
                    if (!usuario.Username.IsNullOrEmpty()) usuarioExistente.Username = usuario.Username;
                    if (!usuario.Contrasena.IsNullOrEmpty()) usuarioExistente.Contrasena = usuario.Contrasena;

                    //if (!producto.RutaImagen.IsNullOrEmpty()) productoExistente.RutaImagen = producto.RutaImagen;
                    //if (usuario.PrecioProducto != null) usuarioExistente.PrecioProducto = usuario.PrecioProducto;
                    //if (producto.Stock != null) productoExistente.Stock = producto.Stock;

                    _context.Usuario.Update(usuarioExistente);
                    await _context.SaveChangesAsync();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ValidarCredencial")]
        public async Task<IActionResult> ValidarCredencial([FromBody] UsuarioLoginDto usuario)
        {
            var existeLogin = await _context.Usuario
                .AnyAsync(x => x.Username.Equals(usuario.Username) && x.Contrasena.Equals(usuario.Contrasena));

            Usuario usuarioLogin = await _context.Usuario.FirstOrDefaultAsync(x => x.Username.Equals(usuario.Username) && x.Contrasena.Equals(usuario.Contrasena));


            if (!existeLogin)
            {
                return NotFound("Usuario No Existe");
            }

            LoginResponseDto loginReponse = new LoginResponseDto()
            {
                Autenticado = existeLogin,
                Mail = existeLogin ? usuarioLogin.Mail : "",
                NombreUsuario = existeLogin ? usuarioLogin.NombreUsuario : "",
                IdUsuario = existeLogin ? usuarioLogin.IdUsuario : 0
            };

            return Ok(loginReponse);
        }
    }
}
