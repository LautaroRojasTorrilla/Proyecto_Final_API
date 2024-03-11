using Microsoft.EntityFrameworkCore;
using Proyecto_Final_API.Database;
using Proyecto_Final_API.DTO;
using Proyecto_Final_API.Mapper;
using Proyecto_Final_API.Models;

namespace Proyecto_Final_API.Service
{
    public class UsuarioService
    {
        private CoderContext context;

        public UsuarioService(CoderContext coderContext)
        {
            this.context = coderContext;
        }

        private bool ValidarCorreo(string mail)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(mail);
                return mailAddress.Address == mail;
            }
            catch
            {
                return false;
            }
        }

        private bool UsuarioExiste(string mail)
        {
            return this.context.Usuarios.Any(u => u.Mail == mail);
        }

        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            try
            {
                return this.context.Usuarios.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener todos los usuarios: {ex.Message}", ex);
            }
        }

        public Usuario ObtenerUsuarioporID(int id)
        {
            try
            {
                Usuario? usuarioBuscado = this.context.Usuarios.FirstOrDefault(p => p.Id == id)
                    ?? throw new Exception($"No se encontró al usuario con ID {id}");

                return usuarioBuscado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public bool AgregarUsuario(UsuarioDTO dto)
        {
            try
            {
                if(!ValidarCorreo(dto.Mail))
                {
                    throw new ArgumentException("El formato del correo electrónico no es válido.");

                }

                if (UsuarioExiste(dto.Mail))
                {
                    throw new ArgumentException("El usuario ya existe con este correo electrónico.");
                }

                Usuario usuario = UsuarioMapper.MapearAProducto(dto);

                this.context.Usuarios.Add(usuario);
                context.SaveChanges();
                return true;

            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error al agregar al usuario: {ex.Message}", ex);
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Error de validación: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool IniciarSesion(string usuario, string contrasena)
        {
            try
            {
                return this.context.Usuarios.Any(u => u.NombreUsuario == usuario && u.Contraseña == contrasena);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar iniciar sesión", ex);
            }
        }

        public static bool ModificarUsuarioPorId(Usuario usuario, int id)
        {

            using (CoderContext contexto = new CoderContext())
            {
                Usuario? usuarioBuscado = contexto.Usuarios.Where(u => u.Id == id).FirstOrDefault();

                usuarioBuscado.Nombre = usuario.Nombre;
                usuarioBuscado.NombreUsuario = usuario.NombreUsuario;
                usuarioBuscado.Apellido = usuario.Apellido;
                usuarioBuscado.Mail = usuario.Mail;

                contexto.Usuarios.Update(usuarioBuscado);

                contexto.SaveChanges();

                return true;
            }
        }

        public static bool EliminarUsuarioPorID(int id)
        {
            using (CoderContext contexto = new CoderContext())
            {
                Usuario? usuarioAEliminar = contexto.Usuarios.FirstOrDefault(u => u.Id == id);

                if (usuarioAEliminar is not null)
                {
                    contexto.Usuarios.Remove(usuarioAEliminar);
                    contexto.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
