using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Data.Database
{
    public class PersonaAdapter : Adapter
    {
        public Personas GetOne(int ID)
        {
            Personas persona = new Personas();
            OpenConnection();
            SqlCommand cmdPersonas = new SqlCommand("select * from personas where id_persona = @id", sqlConn);
            cmdPersonas.Parameters.Add("@id", SqlDbType.Int).Value = ID;
            SqlDataReader drPersonas = cmdPersonas.ExecuteReader();

            try
            {
                if (drPersonas.Read())
                {
                    persona.ID = (int)drPersonas["id_persona"];
                    persona.Legajo = (int)drPersonas["legajo"];
                    persona.Apellido = (string)drPersonas["apellido"];
                    persona.Nombre = (string)drPersonas["nombre"];
                    persona.Direccion = (string)drPersonas["direccion"];
                    persona.Email = (string)drPersonas["email"];
                    persona.Telefono = (string)drPersonas["telefono"];
                    persona.FechaNacimiento = Convert.ToDateTime(drPersonas["fecha_nac"]);
                    persona.IdPlan = (int)drPersonas["id_plan"];
                    persona.TPersona = (Personas.TipoPersona)(drPersonas["tipo_persona"]);
                }
                else persona = null;
                drPersonas.Close();
            }

            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de persona", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return persona;
        }

        public List<Personas> GetAll()
        {
            List<Personas> personas = new List<Personas>();
            try
            {
                OpenConnection();
                SqlCommand cmdPersonas = new SqlCommand("select * from personas", sqlConn);
                SqlDataReader drPersonas = cmdPersonas.ExecuteReader();
                while (drPersonas.Read())
                {
                    Personas persona = new Personas();
                    persona.ID = (int)drPersonas["id_persona"];
                    persona.Legajo = (int)drPersonas["legajo"];
                    persona.Apellido = (string)drPersonas["apellido"];
                    persona.Nombre = (string)drPersonas["nombre"];
                    persona.Direccion = (string)drPersonas["direccion"];
                    persona.Email = (string)drPersonas["email"];
                    persona.Telefono = (string)drPersonas["telefono"];
                    persona.FechaNacimiento = Convert.ToDateTime(drPersonas["fecha_nac"]);
                    persona.IdPlan = (int)drPersonas["id_plan"];
                    persona.TPersona = (Personas.TipoPersona)(drPersonas["tipo_persona"]);

                    personas.Add(persona);
                }
                drPersonas.Close();
            }
            catch
            {
                Exception Ex = new Exception("Error al recuperar personas");
                throw Ex;
            }
            finally
            {
                CloseConnection();
            }
            return personas;
        }

        public void Delete(int ID)
        {
            try
            {
                OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("delete personas where id_persona=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch
            {
                Exception Ex = new Exception("No se pudo eliminar a la persona");
                throw Ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Insert(Personas persona)
        {
            try
            {
                OpenConnection();
                SqlCommand cmdInsert = new SqlCommand("insert into personas(nombre,apellido,email,telefono,legajo,direccion,fecha_nac,id_plan,tipo_persona)" +
                    "values(@nombre,@apellido,@email,@telefono,@legajo,@direccion,@fechaNac,@idPlan,@tipoPersona)" +
                    "select @@identity", sqlConn);

                cmdInsert.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = persona.Nombre;
                cmdInsert.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = persona.Apellido;
                cmdInsert.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = persona.Email;
                cmdInsert.Parameters.Add("@telefono", SqlDbType.VarChar, 50).Value = persona.Telefono;
                cmdInsert.Parameters.Add("@legajo", SqlDbType.Int).Value = persona.Legajo;
                cmdInsert.Parameters.Add("@direccion", SqlDbType.VarChar, 50).Value = persona.Direccion;
                cmdInsert.Parameters.Add("@fechaNac", SqlDbType.DateTime).Value = persona.FechaNacimiento;
                cmdInsert.Parameters.Add("@idPlan", SqlDbType.Int).Value = persona.IdPlan;
                cmdInsert.Parameters.Add("@tipoPersona", SqlDbType.Int).Value = Convert.ToInt32(persona.TPersona);
                persona.ID = Decimal.ToInt32((decimal)cmdInsert.ExecuteScalar());

            }
            catch
            {
                Exception ExcepcionManejada = new Exception("Error al crear la persona");
                throw ExcepcionManejada;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(Personas persona)
        {
            try
            {
                OpenConnection();
                SqlCommand cmdUpdate = new SqlCommand("update personas set nombre = @nombre, apellido = @apellido, " +
                    "fecha_nac = @fechaNac, direccion = @direccion, legajo = @legajo, telefono = @telefono, id_plan = @idPlan, email = @email, tipo_persona = @tipoPersona " +
                    "where id_persona = @id", sqlConn);

                cmdUpdate.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = persona.Nombre;
                cmdUpdate.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = persona.Apellido;
                cmdUpdate.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = persona.Email;
                cmdUpdate.Parameters.Add("@telefono", SqlDbType.VarChar, 50).Value = persona.Telefono;
                cmdUpdate.Parameters.Add("@legajo", SqlDbType.Int).Value = persona.Legajo;
                cmdUpdate.Parameters.Add("@direccion", SqlDbType.VarChar, 50).Value = persona.Direccion;
                cmdUpdate.Parameters.Add("@fechaNac", SqlDbType.DateTime).Value = persona.FechaNacimiento;
                cmdUpdate.Parameters.Add("@idPlan", SqlDbType.Int).Value = persona.IdPlan;
                cmdUpdate.Parameters.Add("@tipoPersona", SqlDbType.Int).Value = Convert.ToInt32(persona.TPersona);
                cmdUpdate.Parameters.Add("@id", SqlDbType.Int).Value = persona.ID;

                cmdUpdate.ExecuteNonQuery();

            }
            catch
            {
                Exception ExcepcionManejada = new Exception("Error al actualizar la persona");
                throw ExcepcionManejada;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Save(Personas persona)
        {
            if (persona.State == BusinessEntity.States.Deleted)
            {
                this.Delete(persona.ID);
            }
            else if (persona.State == BusinessEntity.States.New)
            {
                this.Insert(persona);
            }
            else if (persona.State == BusinessEntity.States.Modified)
            {
                this.Update(persona);
            }
            persona.State = BusinessEntity.States.Unmodified;

        }

    }
}