using System.ComponentModel.DataAnnotations;

namespace APItp6.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public decimal PrecioProducto { get; set; }
        public string? Categoria { get; set; }
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }

    }
}
