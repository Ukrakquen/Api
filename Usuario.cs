using System.ComponentModel.DataAnnotations;

namespace Peliculas.Api.Models
{
    public class Usuario
    {

        public int Id { get; set; }

        [Required, StringLength(120)]

        public string NombreCompleto { get; set; } = string.Empty;

        [Required, StringLength(80)]

        public string UsuarioLogin { get; set; } = string.Empty;

        [Required]

        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(20)]
        public string Rol { get; set; } = "Usuario";
    }
}
