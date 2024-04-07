using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.RepositoryInterface
{
    public interface IRepositoryUsuario
    {
        IEnumerable<Usuario> ListaUsuarios();
        Usuario ObtenerUsuarioPorID(int id);
        void Agregar (Usuario usuario);
        void CambiarEstado(int id);
        List<Perfil> ListaPerfiles();
        Usuario IniciarSesion(string correoElectronico, string contrasenna);
        Usuario ObtenerUsuarioPorCorreo(string correoElectronico);
    }
}
