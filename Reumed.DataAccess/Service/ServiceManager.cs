using Reumed.DataAccess.BusinessObjects;
using Reumed.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Reumed.DataAccess.Service
{
    public class ServiceManager : IService
    {
        #region Usuario
        public Task<List<Usuario>> GetUsuariosAsync()
        {
            return Task.Factory.StartNew(() => { return Factory.GetDatabase().GetUsuarios(); });
        }

        public List<Usuario> GetUsuarios()
        {
            return Factory.GetDatabase().GetUsuarios();
        }

        public Usuario GetUsuarioById(Guid id)
        {
            return Factory.GetDatabase().GetUsuario(id, null);
        }

        public Usuario GetUsuarioByIdOrNombreUsuario(Guid? usuarioid = null, string nombreUsuario = null)
        {
            return Factory.GetDatabase().GetUsuario(usuarioid, nombreUsuario);
        }

        public Usuario CreateUpdateUsuario(Usuario usuario, string Clave, int doctorid, int rolid)
        {
            string errMsg = $"";
            var _service = Factory.GetDatabase();

            try
            {
                byte[] ClaveHash;
                byte[] ClaveSalt;
                CreateClaveHash(Clave, out ClaveHash, out ClaveSalt);

                usuario.ClaveHash = ClaveHash;
                usuario.ClaveSalt = ClaveSalt;

                usuario = _service.CreateUpdateUsuario(usuario, Clave, doctorid, rolid);
            }
            catch(Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();

                throw new Exception("Error en salvar las informaciones del usuario", ex);
            }

            return usuario;
        }

        public void DeactivateActivateUsuarioById(Guid id, bool activo)
        {
            var _service = Factory.GetDatabase();
            _service.DeactivateActivateUsuarioById(id, activo);
        }

        public Usuario Login (string nombreUsuario, string clave)
        {
            var usuario = GetUsuarioByIdOrNombreUsuario(null, nombreUsuario);

            if (usuario == null)
                return null;

            if (!VerificarClaveHash(clave, usuario.ClaveHash, usuario.ClaveSalt))
                return null;

            return usuario;
        }

        public bool UsuarioExiste(string nombreUsuario)
        {
            var usuario = GetUsuarioByIdOrNombreUsuario(null, nombreUsuario);

            if (usuario == null)
                return false;

            return true;
        }

        private void CreateClaveHash(string clave, out byte[] claveHash, out byte[] claveSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                claveSalt = hmac.Key;
                claveHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(clave));
            }
        }

        private bool VerificarClaveHash(string clave, byte[] claveHash, byte[] claveSalt)
        {
            using (var hmac = new HMACSHA512(claveSalt))
            {
                var hashCreado = hmac.ComputeHash(Encoding.UTF8.GetBytes(clave));

                for (int i = 0; i < hashCreado.Length; i++)
                {
                    if (hashCreado[i] != claveHash[i])
                        return false;
                }
            }

            return true;
        }

        #endregion

        #region Doctor
        public ICollection<Doctor> GetDoctores()
        {
            return Factory.GetDatabase().GetDoctores();
        }

        public Doctor GetDoctorByUsuarioId(int usuarioId)
        {
            return Factory.GetDatabase().GetDoctor(usuarioId);
        }
        #endregion

        #region Roles
        public ICollection<Rol> GetRoles()
        {
            return Factory.GetDatabase().GetRoles();
        }

        public Rol GetRolesByUsuarioId(int usuarioId)
        {
            return Factory.GetDatabase().GetRol(usuarioId);
        }
        #endregion
    }
}
