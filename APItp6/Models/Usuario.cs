using System.ComponentModel.DataAnnotations;

namespace APItp6.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Apellido { get; set; }
        public string? Mail { get; set; }
        public int NumTelefono { get; set; }
        public string? Username { get; set; }
        public string? Contrasena { get; set; }
    }
}
