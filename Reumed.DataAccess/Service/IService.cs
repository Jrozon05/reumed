using Reumed.DataAccess.BusinessObjects;
using Reumed.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reumed.DataAccess.Service
{
    public interface IService
    {
        #region Usuario

        Task<List<Usuario>> GetUsuariosAsync();

        List<Usuario> GetUsuarios();

        Usuario CreateUpdateUsuario(Usuario usuario, string clave, int doctorid, int rolid);

        Usuario GetUsuarioById(Guid id);

        Usuario GetUsuarioByIdOrNombreUsuario(Guid? usuarioid = null, string nombreUsuario = null);

        Usuario Login(string nombreUsuario, string clave);

        void DeactivateActivateUsuarioById(Guid id, bool activo);

        bool UsuarioExiste(string nombreUsuario);

        #endregion

        #region Doctor
        ICollection<Doctor> GetDoctores();

        Doctor GetDoctorByUsuarioId(int usuarioId);

        #endregion

        #region Roles
        ICollection<Rol> GetRoles();

        Rol GetRolesByUsuarioId(int usuarioId);
        #endregion
    }
}
