using Model_Inventario.InventarioDTO;
using Repositorio_Inventario;

namespace InventarioService.Services.Interface
{
    public interface ICustomAuthenticationManagerService
    {
        UsuariosReturnPortalDTO Authenticate(UsuarioLoginDTO usuarioLog, SqlDbContext context);
        IDictionary<string, string> Tokens { get; }
    }
}
