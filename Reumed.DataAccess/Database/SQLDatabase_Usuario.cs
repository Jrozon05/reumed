using Newtonsoft.Json;
using Reumed.DataAccess.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Reumed.DataAccess.Database
{
    internal partial class SQLDatabase
    {
        public List<Usuario> GetUsuarios()
        {
            var results = new List<Usuario>();

            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_GetUsuarios";

                        object response = cmd.ExecuteScalar();

                        if (response != null)
                            results = (List<Usuario>)JsonConvert.DeserializeObject((string)response, typeof(List<Usuario>));
                        else
                            results = null;
                    }

                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return results;
        }

        public Usuario GetUsuario(Guid? usuarioId = null, string nombreUsuario = null)
        {
            Usuario result = new Usuario();

            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_GetUsuarioByIdOrNombreUsuario";

                        IDbDataParameter p = cmd.CreateParameter();
                        p.DbType = DbType.Guid;
                        p.ParameterName = "UsuarioId";
                        p.Value = usuarioId;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "NombreUsuario";
                        p.Value = nombreUsuario;
                        cmd.Parameters.Add(p);

                        object usuario = cmd.ExecuteScalar();

                        if (usuario != null)
                            result = (Usuario)JsonConvert.DeserializeObject((string)usuario, typeof(Usuario));
                        else
                            result = null;
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return result;
        }


        public Usuario CreateUpdateUsuario(Usuario usuario, string clave, int doctorid, int rolid)
        {
            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_CreateUpdateUsuario";

                        IDbDataParameter p = cmd.CreateParameter();
                        p.DbType = DbType.Guid;
                        p.ParameterName = "Guid";
                        p.Value = usuario.Guid;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "NombreUsuario";
                        p.Value = usuario.NombreUsuario;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "Clave";
                        p.Value = clave;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.Binary;
                        p.ParameterName = "ClaveHash";
                        p.Value = usuario.ClaveHash;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.Binary;
                        p.ParameterName = "ClaveSalt";
                        p.Value = usuario.ClaveSalt;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.Int32;
                        p.ParameterName = "DoctorId";
                        p.Value = doctorid;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.Int32;
                        p.ParameterName = "RolId";
                        p.Value = rolid;
                        cmd.Parameters.Add(p);

                        cmd.ExecuteScalar();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }

        public void DeactivateActivateUsuarioById(Guid id, bool activo)
        {
            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_DeactivateActivateUsuarioById";

                        IDbDataParameter p = cmd.CreateParameter();
                        p.DbType = DbType.Guid;
                        p.ParameterName = "Guid";
                        p.Value = id;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.Boolean;
                        p.ParameterName = "Activo";
                        p.Value = activo;
                        cmd.Parameters.Add(p);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario Login(string nombreUsuario, string clave)
        {
            Usuario result = new Usuario();

            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_LoginByNombreUsuarioAndClave";

                        IDbDataParameter p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "NombreUsuario";
                        p.Value = nombreUsuario;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "Clave";
                        p.Value = clave;
                        cmd.Parameters.Add(p);

                        object usuario = cmd.ExecuteScalar();

                        if (usuario != null)
                            result = (Usuario)JsonConvert.DeserializeObject((string)usuario, typeof(Usuario));
                        else
                            result = null;
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
