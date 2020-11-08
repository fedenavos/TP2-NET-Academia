using Business.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class CursoAdapter:Adapter
    {
        public List<Curso> GetAll()
        {
            List<Curso> cursos = new List<Curso>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdCursos = new SqlCommand("select * from cursos", sqlConn);
                SqlDataReader drCursos = cmdCursos.ExecuteReader();

                while (drCursos.Read())
                {
                    Curso cur = new Curso();

                    cur.Id = (int)drCursos["id_curso"];
                    cur.Cupo = (int)drCursos["cupo"];
                    cur.AnioCalendario = (int)drCursos["anio_calendario"];
                    cur.IdComision = (int)drCursos["id_curso"];
                    cur.IdMateria = (int)drCursos["id_materia"];

                    cursos.Add(cur);
                }
                drCursos.Close();
            }
            catch (Exception exc)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de cursos: " + exc.Message);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return cursos;
        }

        public Curso GetOne(int ID)
        {
            Curso cur = new Curso();

            try
            {
                this.OpenConnection();
                SqlCommand cmdCursos = new SqlCommand("SELECT * FROM cursos WHERE id_curso = @id", sqlConn);
                cmdCursos.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drCursos = cmdCursos.ExecuteReader();

                if (drCursos.Read())
                {
                    cur.Id = (int)drCursos["id_curso"];
                    cur.Cupo = (int)drCursos["cupo"];
                    cur.AnioCalendario = (int)drCursos["anio_calendario"];
                    cur.IdComision = (int)drCursos["id_curso"];
                    cur.IdMateria = (int)drCursos["id_materia"];
                }

                drCursos.Close();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al recuperar datos de cursos: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }

            return cur;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE FROM cursos WHERE id_curso = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al eliminar el curso: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Curso cur)
        {
            if (cur.State == BusinessEntity.States.Deleted)
            {
                this.Delete(cur.Id);
            }
            else if (cur.State == BusinessEntity.States.Modified)
            {
                this.Update(cur);
            }
            else if (cur.State == BusinessEntity.States.New)
            {
                this.Insert(cur);
            }
            cur.State = BusinessEntity.States.Unmodified;
        }

        protected void Update(Curso cur)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdUpdate = new SqlCommand("UPDATE cursos SET cupo = @cupo, id_materia = @idmateria, anio_calendario = @aniocalendario, id_comision = @idcomision WHERE id_curso = @id", sqlConn);
                cmdUpdate.Parameters.Add("@id", SqlDbType.Int).Value = cur.Id;
                cmdUpdate.Parameters.Add("@idmateria", SqlDbType.Int).Value = cur.IdMateria;
                cmdUpdate.Parameters.Add("@idcomision", SqlDbType.Int).Value = cur.IdComision;
                cmdUpdate.Parameters.Add("@aniocalendario", SqlDbType.Int).Value = cur.AnioCalendario;
                cmdUpdate.Parameters.Add("@cupo", SqlDbType.Int).Value = cur.Cupo;

                cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al modificar datos del curso: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Curso cur)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdInsert = new SqlCommand("INSERT INTO cursos(id_materia, id_comision, cupo, anio_calendario) VALUES(@idmateria,@idcomision,@cupo, @aniocalendario) SELECT @@identity", sqlConn);
                cmdInsert.Parameters.Add("@idmateria", SqlDbType.Int).Value = cur.IdMateria;
                cmdInsert.Parameters.Add("@idcomision", SqlDbType.Int).Value = cur.IdComision;
                cmdInsert.Parameters.Add("@aniocalendario", SqlDbType.Int).Value = cur.AnioCalendario;
                cmdInsert.Parameters.Add("@cupo", SqlDbType.Int).Value = cur.Cupo;

                cur.Id = Decimal.ToInt32((decimal)cmdInsert.ExecuteScalar());
            }
            catch (Exception exc)
            {
                throw new Exception("Error al crear el curso: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }
    }
}
