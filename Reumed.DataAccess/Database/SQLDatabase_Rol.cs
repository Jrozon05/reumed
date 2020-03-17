using Newtonsoft.Json;
using Reumed.DataAccess.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Reumed.DataAccess.Database
{
    internal partial class SQLDatabase
    {
        public ICollection<Rol> GetRoles()
        {
            var results = new List<Rol>();

            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_GetRoles";

                        object response = cmd.ExecuteScalar();

                        if (response != null)
                            results = (List<Rol>)JsonConvert.DeserializeObject((string)response, typeof(List<Rol>));
                        else
                            results = null;
                    }

                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return results;
        }

        public Rol GetRol(int usuarioId)
        {
            Rol result = new Rol();
            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_GetRolByUsuarioId";

                        IDbDataParameter p = cmd.CreateParameter();
                        p.DbType = DbType.Int32;
                        p.ParameterName = "UsuarioId";
                        p.Value = usuarioId;
                        cmd.Parameters.Add(p);

                        object usuario = cmd.ExecuteScalar();

                        if (usuario != null)
                            result = (Rol)JsonConvert.DeserializeObject((string)usuario, typeof(Rol));
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
