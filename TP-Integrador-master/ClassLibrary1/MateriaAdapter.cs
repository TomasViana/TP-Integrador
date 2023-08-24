using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Entidades;

namespace Data.Database
{
    public class MateriaAdapter : Adapter
    {
        public List<Materia> GetAll()
        {
            List<Materia> materias = new List<Materia>();

            try
            {
                this.OpenConnection();
                SqlCommand cmdMateria = new SqlCommand("select * from materias", sqlConn);
                SqlDataReader drMateria = cmdMateria.ExecuteReader();
                while (drMateria.Read())
                {
                    Materia mat = new Materia();
                    mat.ID = (int)drMateria["id_materia"];
                    mat.Descripcion = (string)drMateria["desc_materia"];
                    mat.HsSemanales = (int)drMateria["hs_semanales"];
                    mat.HsTotales = (int)drMateria["hs_totales"];
                    mat.IdPlan = (int)drMateria["id_plan"];

                    materias.Add(mat);
                }

                drMateria.Close();
            }

            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de materias", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }

            return materias;
        }

        public Entidades.Materia GetOne(int ID)
        {
            Materia mat = new Materia();
            try
            {
                this.OpenConnection();
                SqlCommand cmdMateria = new SqlCommand("select * from materias where id_materia = @id", sqlConn);
                cmdMateria.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drMateria = cmdMateria.ExecuteReader();
                if (drMateria.Read())
                {
                    mat.ID = (int)drMateria["id_materia"];
                    mat.Descripcion = (string)drMateria["desc_materia"];
                    mat.HsSemanales = (int)drMateria["hs_semanales"];
                    mat.HsTotales = (int)drMateria["hs_totales"];
                    mat.IdPlan = (int)drMateria["id_plan"];
                }
                else mat = null;
                drMateria.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de la materia", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return mat;
        }


        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdDelete =
                    new SqlCommand("delete materias where id_materia=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar materia", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Materia materia)
        {
            if (materia.State == BusinessEntity.States.Deleted)
            {
                this.Delete(materia.ID);
            }
            else if (materia.State == BusinessEntity.States.New)
            {
                this.Insert(materia);
            }
            else if (materia.State == BusinessEntity.States.Modified)
            {
                this.Update(materia);
            }
            materia.State = BusinessEntity.States.Unmodified;
        }

        protected void Update(Materia materia)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(
                    "UPDATE materias SET desc_materia = @descripcion, hs_semanales = @hsSemanales," +
                    "hs_totales = @hsTotales, id_plan = @idPlan " +
                    "WHERE id_materia = @id", sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = materia.ID;
                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = materia.Descripcion;
                cmdSave.Parameters.Add("@hsSemanales", SqlDbType.Int).Value = materia.HsSemanales;
                cmdSave.Parameters.Add("@hsTotales", SqlDbType.Int).Value = materia.HsTotales;
                cmdSave.Parameters.Add("@idPlan", SqlDbType.Int).Value = materia.IdPlan;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar los datos de la materia", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Materia materia)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(
                    "insert into materias(desc_materia,hs_semanales,hs_totales,id_plan)" +
                    "values(@descripcion,@hsSemanales,@hsTotales,@idPlan)" +
                    "select @@identity", sqlConn);

                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = materia.Descripcion;
                cmdSave.Parameters.Add("@hsSemanales", SqlDbType.Int).Value = materia.HsSemanales;
                cmdSave.Parameters.Add("@hsTotales", SqlDbType.Int).Value = materia.HsTotales;
                cmdSave.Parameters.Add("@idPlan", SqlDbType.Int).Value = materia.IdPlan;

                materia.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear la materia", Ex);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public DataTable GetMaterias()
        {
            DataTable materias = new DataTable();
            try
            {
                OpenConnection();
                SqlCommand cmdMaterias = new SqlCommand("select id_materia, desc_materia from materias", sqlConn);
                SqlDataAdapter daMaterias = new SqlDataAdapter(cmdMaterias);
                daMaterias.Fill(materias);
            }

            catch (Exception Ex)
            {
                Exception ExceptionManejada = new Exception("No se pudo cargar las materias", Ex);
                throw ExceptionManejada;
            }

            finally
            {
                CloseConnection();
            }
            return materias;
        }
    }
}

