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
    public class ComisionAdapter : Adapter
    {
        public List<Comision> GetAll()
        {
            List<Comision> comisiones = new List<Comision>();

            try
            {
                this.OpenConnection();
                SqlCommand cmdComisiones = new SqlCommand("select * from comisiones", sqlConn);
                SqlDataReader drComisiones = cmdComisiones.ExecuteReader();
                while (drComisiones.Read())
                {
                    Comision com = new Comision();
                    com.ID = (int)drComisiones["id_comision"];
                    com.Descripcion = (string)drComisiones["desc_comision"];
                    com.AnioEspecialidad = (int)drComisiones["anio_especialidad"];
                    com.IdPlan = (int)drComisiones["id_plan"];

                    comisiones.Add(com);
                }

                drComisiones.Close();
            }

            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de comisiones", Ex);
                throw Ex;
            }
            finally { this.CloseConnection(); }

            return comisiones;
        }

        public Comision GetOne(int ID)
        {
            Comision com = new Comision();
            try
            {
                this.OpenConnection();
                SqlCommand cmdComision = new SqlCommand("select * from comisiones where id_comision = @id", sqlConn);
                cmdComision.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drComision = cmdComision.ExecuteReader();
                if (drComision.Read())
                {
                    com.ID = (int)drComision["id_comision"];
                    com.Descripcion = (string)drComision["desc_comision"];
                    com.AnioEspecialidad = (int)drComision["anio_especialidad"];
                    com.IdPlan = (int)drComision["id_plan"];
                }
                else com = null;
                drComision.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar comision", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return com;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("delete comisiones where id_comision = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar la comision");
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Comision comision)
        {
            if (comision.State == BusinessEntity.States.Deleted)
            {
                this.Delete(comision.ID);
            }
            else if (comision.State == BusinessEntity.States.New)
            {
                this.Insert(comision);
            }
            else if (comision.State == BusinessEntity.States.Modified)
            {
                this.Update(comision);
            }
            comision.State = BusinessEntity.States.Unmodified;
        }

        protected void Update(Comision comision)
        {
            try
            {

                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("UPDATE comisiones SET desc_comision = @descripcion," +
                    "anio_especialidad = @anioEspecialidad, id_plan = @idPlan " +
                    "WHERE id_comision = @id", sqlConn);
                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = comision.Descripcion;
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = comision.ID;
                cmdSave.Parameters.Add("@anioEspecialidad", SqlDbType.Int).Value = comision.AnioEspecialidad;
                cmdSave.Parameters.Add("@idPlan", SqlDbType.Int).Value = comision.IdPlan;
                cmdSave.ExecuteNonQuery();
            }
            catch
            {
                Exception ExcepcionManejada = new Exception("Error al modificar comision");
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
        }

        protected void Insert(Comision comision)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand("insert into comisiones(desc_comision,anio_especialidad,id_plan)" +
                    "values(@descripcion,@anioEspecialidad,@idPlan)" +
                    "select @@identity", sqlConn);
                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = comision.Descripcion;
                cmdSave.Parameters.Add("@idPlan", SqlDbType.Int).Value = comision.IdPlan;
                cmdSave.Parameters.Add("@anioEspecialidad", SqlDbType.Int).Value = comision.AnioEspecialidad;
                comision.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch
            {
                Exception Ex = new Exception("Error al crear comision");
                throw Ex;
            }
            finally { this.CloseConnection(); }
        }

        public DataTable GetComisiones()
        {
            DataTable comisiones = new DataTable();
            try
            {
                OpenConnection();
                SqlCommand cmdComisiones = new SqlCommand("select id_comision, desc_comision from comisiones", sqlConn);
                SqlDataAdapter daComisiones = new SqlDataAdapter(cmdComisiones);
                daComisiones.Fill(comisiones);
            }

            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar las comisiones", Ex);
                throw ExcepcionManejada;
            }

            finally
            {
                CloseConnection();
            }

            return comisiones;
        }
    }
}