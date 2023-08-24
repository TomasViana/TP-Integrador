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
    public class CursoAdapter : Adapter
    {
        public List<Curso> GetAll()
        {
            List<Curso> cursos = new List<Curso>();
            try
            {
                OpenConnection();
                SqlCommand cmdCursos = new SqlCommand("select * from cursos", sqlConn);
                SqlDataReader drCursos = cmdCursos.ExecuteReader();
                while (drCursos.Read())
                {
                    Curso curso = new Curso();
                    curso.ID = (int)drCursos["id_curso"];
                    curso.IdMateria = (int)drCursos["id_materia"];
                    curso.IdMateria = (int)drCursos["id_comision"];
                    curso.AnioCalendario = (int)drCursos["anio_calendario"];
                    curso.Cupo = (int)drCursos["cupo"];

                    cursos.Add(curso);
                }
                drCursos.Close();
            }

            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar cursos", Ex);
                //throw ExcepcionManejada;
            }

            finally
            {
                CloseConnection();
            }
            return cursos;
        }

        public Curso GetOne(int ID)
        {
            Curso curso = new Curso();
            OpenConnection();
            SqlCommand cmdCurso = new SqlCommand("select * from cursos where id_curso=@id", sqlConn);
            cmdCurso.Parameters.Add("@id", SqlDbType.Int).Value = ID;
            SqlDataReader drCursos = cmdCurso.ExecuteReader();

            try
            {
                if (drCursos.Read())
                {
                    curso.ID = (int)drCursos["id_curso"];
                    curso.IdMateria = (int)drCursos["id_materia"];
                    curso.IdMateria = (int)drCursos["id_comision"];
                    curso.AnioCalendario = (int)drCursos["anio_calendario"];
                    curso.Cupo = (int)drCursos["cupo"];
                }
                else curso = null;
                drCursos.Close();
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
            return curso;
        }

        public void Delete(int ID)
        {
            try
            {
                OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("delete cursos where id_curso=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }

            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("No se pudo eliminar el curso", Ex);
                throw ExcepcionManejada;
            }

            finally
            {
                CloseConnection();
            }

        }

        public void Save(Curso curso)
        {
            if (curso.State == BusinessEntity.States.Deleted)
            {
                this.Delete(curso.ID);
            }

            if (curso.State == BusinessEntity.States.New)
            {
                this.Insert(curso);
            }

            if (curso.State == BusinessEntity.States.Modified)
            {
                this.Update(curso);
            }
            curso.State = BusinessEntity.States.Unmodified;
        }

        public void Insert(Curso curso)
        {
            try
            {
                OpenConnection();
                SqlCommand cmdInsert = new SqlCommand("insert into cursos(id_materia, id_comision, anio_calendario, cupo)" +
                    "values(@idmateria, @idcomision, @aniocalendario, @cupo)" +
                    "select @@identity", sqlConn);
                cmdInsert.Parameters.Add("@idmateria", SqlDbType.Int).Value = curso.IdMateria;
                cmdInsert.Parameters.Add("@idcomision", SqlDbType.Int).Value = curso.IdComision;
                cmdInsert.Parameters.Add("@aniocalendario", SqlDbType.Int).Value = curso.AnioCalendario;
                cmdInsert.Parameters.Add("@cupo", SqlDbType.Int).Value = curso.Cupo;
                curso.ID = Decimal.ToInt32((decimal)cmdInsert.ExecuteScalar());
            }

            catch (Exception Ex)
            {
                Exception ExcepctionManejada = new Exception("Error al crear curso", Ex);
                throw ExcepctionManejada;
            }

            finally
            {
                CloseConnection();
            }
        }

        public void Update(Curso curso)
        {
            try
            {
                OpenConnection();
                SqlCommand cmdUpdate = new SqlCommand("UPDATE cursos SET id_materia = @idmateria, " +
                    "id_comision = @idcomision," +
                    "anio_calendario = @aniocalendario, cupo = @cupo where id_curso = @id", sqlConn);

                cmdUpdate.Parameters.Add("@idmateria", SqlDbType.Int).Value = curso.IdMateria;
                cmdUpdate.Parameters.Add("@idcomision", SqlDbType.Int).Value = curso.IdComision;
                cmdUpdate.Parameters.Add("@aniocalendario", SqlDbType.Int).Value = curso.AnioCalendario;
                cmdUpdate.Parameters.Add("@cupo", SqlDbType.Int).Value = curso.Cupo;
                cmdUpdate.Parameters.Add("@id", SqlDbType.Int).Value = curso.ID;

                cmdUpdate.ExecuteNonQuery();
            }

            catch (Exception Ex)
            {
                Exception ExceptionManejada = new Exception("Error al actualizar el curso", Ex);
                throw ExceptionManejada;
            }

            finally
            {
                CloseConnection();
            }
        }
    }
}
