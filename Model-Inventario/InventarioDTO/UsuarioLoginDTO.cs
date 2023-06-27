using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Inventario.InventarioDTO
{
    public class UsuariosDTO
    {
        public string nombreusuario { get; set; }
        public string contrasena { get; set; }
        public string? nombres { get; set; }
        public string? correoelectronico { get; set; }
    }
    public class UsuarioLoginDTO
    {
        public string nombreusuario { get; set; }
        public string contrasena { get; set; }
        public string? empresa { set; get; }
    }
    public class UsuariosReturnPortalDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Empresa { get; set; }
        public DateTime Expira { get; set; } = DateTime.Now;
        public string Token { get; set; }
        public bool Rol { get; set; }
        public string Observations { get; set; }
        [NotMapped]
        public string correo { get; set; }
    }
}
