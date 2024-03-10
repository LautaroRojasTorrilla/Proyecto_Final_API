using Proyecto_Final_API.Database;
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

        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            return this.context.Usuarios.ToList();
        }

        public static Usuario ObtenerUsuarioporID(int id)
        {
            using (CoderContext contexto = new CoderContext())
            {
                //añ where le paso una fn criterio
                Usuario? usuarioBuscado = contexto.Usuarios.Where(u => u.Id == id).FirstOrDefault();

                return usuarioBuscado;
            }
        }

        //public static Usuario ObtenerUsuarioporID2(int id)
        //{
        //    List<Usuario> usuarios = UsuarioService.ObtenerTodosLosUsuarios();

        //    foreach (Usuario item in usuarios)
        //    {
        //        if (item.Id == id)
        //        {
        //            return item;
        //        }
        //    }

        //    return null;
        //}

        public static bool AgregarUsuario(Usuario usuario)
        {
            using (CoderContext contexto = new CoderContext())
            {
                contexto.Usuarios.Add(usuario);

                //si no hago esta linea no tiene impacto en la base de datos.
                contexto.SaveChanges();
                return true;
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
