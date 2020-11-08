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
    public class AlumnoInscripcionAdapter:Adapter
    {
        public List<AlumnoInscripcion> GetAll()
        {
            List<AlumnoInscripcion> alumnos_inscripciones = new List<AlumnoInscripcion>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdInscripciones = new SqlCommand("select * from alumnos_inscripciones", sqlConn);
                SqlDataReader drInscripciones = cmdInscripciones.ExecuteReader();

                while (drInscripciones.Read())
                {
                    AlumnoInscripcion ins = new AlumnoInscripcion();

                    ins.Id = (int)drInscripciones["id_inscripcion"];
                    ins.Condicion = (String)drInscripciones["condicion"];
                    ins.Nota = (int)drInscripciones["nota"];
                    ins.IdCurso = (int)drInscripciones["id_curso"];
                    ins.IdAlumno = (int)drInscripciones["id_alumno"];

                    alumnos_inscripciones.Add(ins);
                }
                drInscripciones.Close();
            }
            catch (Exception exc)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de alumnos_inscripciones: " + exc.Message);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return alumnos_inscripciones;
        }

        public AlumnoInscripcion GetOne(int ID)
        {
            AlumnoInscripcion ins = new AlumnoInscripcion();

            try
            {
                this.OpenConnection();
                SqlCommand cmdInscripciones = new SqlCommand("SELECT * FROM alumnos_inscripciones WHERE id_inscripcion = @id", sqlConn);
                cmdInscripciones.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drInscripciones = cmdInscripciones.ExecuteReader();

                if (drInscripciones.Read())
                {
                    ins.Id = (int)drInscripciones["id_inscripcion"];
                    ins.Condicion = (String)drInscripciones["condicion"];
                    ins.Nota = (int)drInscripciones["nota"];
                    ins.IdCurso = (int)drInscripciones["id_curso"];
                    ins.IdAlumno = (int)drInscripciones["id_alumno"];
                }

                drInscripciones.Close();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al recuperar datos de alumnos_inscripciones: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }

            return ins;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE FROM alumnos_inscripciones WHERE id_inscripcion = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al eliminar la inscripcion: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(AlumnoInscripcion ins)
        {
            if (ins.State == BusinessEntity.States.Deleted)
            {
                this.Delete(ins.Id);
            }
            else if (ins.State == BusinessEntity.States.Modified)
            {
                this.Update(ins);
            }
            else if (ins.State == BusinessEntity.States.New)
            {
                this.Insert(ins);
            }
            ins.State = BusinessEntity.States.Unmodified;
        }

        protected void Update(AlumnoInscripcion ins)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdUpdate = new SqlCommand("UPDATE alumnos_inscripciones SET condicion = @condicion, nota = @nota, id_curso = @idcurso, id_alumno = @idalumno WHERE id_inscripcion = @id", sqlConn);
                cmdUpdate.Parameters.Add("@id", SqlDbType.Int).Value = ins.Id;
                cmdUpdate.Parameters.Add("@idalumno", SqlDbType.Int).Value = ins.IdAlumno;
                cmdUpdate.Parameters.Add("@idcurso", SqlDbType.Int).Value = ins.IdCurso;
                cmdUpdate.Parameters.Add("@condicion", SqlDbType.VarChar, 50).Value = ins.Condicion;
                cmdUpdate.Parameters.Add("@nota", SqlDbType.Int).Value = ins.Nota;
                cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al modificar datos de la inscripcion: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(AlumnoInscripcion ins)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdInsert = new SqlCommand("INSERT INTO alumnos_inscripciones(id_alumno, id_curso, condicion, nota) VALUES(@idalumno, @idcurso, @condicion, @nota) SELECT @@identity", sqlConn);
                cmdInsert.Parameters.Add("@idalumno", SqlDbType.Int).Value = ins.IdAlumno;
                cmdInsert.Parameters.Add("@idcurso", SqlDbType.Int).Value = ins.IdCurso;
                cmdInsert.Parameters.Add("@condicion", SqlDbType.VarChar, 50).Value = ins.Condicion;
                cmdInsert.Parameters.Add("@nota", SqlDbType.Int).Value = ins.Nota;

                ins.Id = Decimal.ToInt32((decimal)cmdInsert.ExecuteScalar());
            }
            catch (Exception exc)
            {
                throw new Exception("Error al crear la inscripcion: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }
    }
}
