using System.ComponentModel.DataAnnotations;

namespace APItp6.Models.Dto
{
    public class LoginResponseDto
    {
        
        public int IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Mail { get; set; }

        public bool Autenticado { get; set; }
    }
}
