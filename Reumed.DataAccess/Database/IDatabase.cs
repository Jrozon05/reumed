using Reumed.DataAccess.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reumed.DataAccess.Database
{
    public interface IDatabase
    {
        #region Usuario

        List<Usuario> GetUsuarios();

        Usuario GetUsuario(Guid? usuarioId, string nombreUsuario = null);

        Usuario CreateUpdateUsuario(Usuario usuario, string clave, int doctorid, int rolid);

        void DeactivateActivateUsuarioById(Guid id, bool activo);

        #endregion

        #region Doctor
        ICollection<Doctor> GetDoctores();

        Doctor GetDoctor(int usuarioId);
        #endregion

        #region Roles
        ICollection<Rol> GetRoles();

        Rol GetRol(int usuarioId);
        #endregion
    }
}
