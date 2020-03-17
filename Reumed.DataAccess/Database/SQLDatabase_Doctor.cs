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
        public ICollection<Doctor> GetDoctores()
        {
            var list = new List<Doctor>();

            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_GetDoctores";

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var item = new Doctor();
                                item.DoctorId = dr.GetInt32(dr.GetOrdinal("doctorid"));
                                item.Guid = dr.GetGuid(dr.GetOrdinal("guid"));
                                item.Nombre = dr.GetString(dr.GetOrdinal("nombre"));
                                item.Apellido = dr.GetString(dr.GetOrdinal("apellido"));
                                item.Posicion = dr.GetString(dr.GetOrdinal("posicion"));
                                item.Activo = dr.GetBoolean(dr.GetOrdinal("activo"));
                                list.Add(item);
                            }

                            dr.Close();
                        }
                    }

                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public Doctor GetDoctor(int usuarioId)
        {
            Doctor result = new Doctor();
            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_GetDoctorByUsuarioId";

                        IDbDataParameter p = cmd.CreateParameter();
                        p.DbType = DbType.Int32;
                        p.ParameterName = "UsuarioId";
                        p.Value = usuarioId;
                        cmd.Parameters.Add(p);

                        object usuario = cmd.ExecuteScalar();

                        if (usuario != null)
                            result = (Doctor)JsonConvert.DeserializeObject((string)usuario, typeof(Doctor));
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
