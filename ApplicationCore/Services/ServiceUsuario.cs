using ApplicationCore.ServicesInterface;
using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        IRepositoryUsuario repositoryUsuario = new RepositoryUsuario();
        public void AgregarUsuario(Usuario usuario)
        {
            string encriptarContrasenna = Cryptography.EncrypthAES(usuario.Contrasenna);
            usuario.Contrasenna = encriptarContrasenna;

            repositoryUsuario.AgregarUsuario(usuario);
        }

        public void CambiarEstado(int id)
        {
            repositoryUsuario.CambiarEstado(id);
        }

        public Usuario EditarUsuario(Usuario usuario)
        {
            string encriptarContrasenna = Cryptography.EncrypthAES(usuario.Contrasenna);
            usuario.Contrasenna = encriptarContrasenna;

            return repositoryUsuario.EditarUsuario(usuario);
        }

        public Usuario IniciarSesion(string correoElectronico, string contrasenna)
        {
            string desencriptarContrasenna = Cryptography.EncrypthAES(contrasenna);
            return repositoryUsuario.IniciarSesion(correoElectronico, desencriptarContrasenna);
        }

        public List<Perfil> ListaPerfiles()
        {
            return repositoryUsuario.ListaPerfiles();
        }

        public IEnumerable<Usuario> ListaUsuarios()
        {
            return repositoryUsuario.ListaUsuarios();
        }

        public Usuario ObtenerUsuarioPorCorreo(string correoElectronico)
        {
            return repositoryUsuario.ObtenerUsuarioPorCorreo(correoElectronico);
        }

        public Usuario ObtenerUsuarioPorID(int id)
        {
            return repositoryUsuario.ObtenerUsuarioPorID(id);
        }
    }
}
