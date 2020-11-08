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
    public class MateriaAdapter : Adapter
    {
        public List<Materia> GetAll()
        {
            List<Materia> materias = new List<Materia>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdMaterias = new SqlCommand("select * from materias", sqlConn);
                SqlDataReader drMaterias = cmdMaterias.ExecuteReader();

                while (drMaterias.Read())
                {
                    Materia mat = new Materia();

                    mat.Id = (int)drMaterias["id_materia"];
                    mat.Descripcion = (String)drMaterias["desc_materia"];
                    mat.HsSemanales = (int)drMaterias["hs_semanales"];
                    mat.HsTotales = (int)drMaterias["hs_totales"];
                    mat.IdPlan = (int)drMaterias["id_plan"];

                    materias.Add(mat);
                }
                drMaterias.Close();
            }
            catch (Exception exc)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de materias: " + exc.Message);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return materias;
        }

        public Materia GetOne(int ID)
        {
            Materia mat = new Materia();

            try
            {
                this.OpenConnection();
                SqlCommand cmdMaterias = new SqlCommand("SELECT * FROM materias WHERE id_materia = @id", sqlConn);
                cmdMaterias.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drMaterias = cmdMaterias.ExecuteReader();

                if (drMaterias.Read())
                {
                    mat.Id = (int)drMaterias["id_materia"];
                    mat.Descripcion = (String)drMaterias["desc_materia"];
                    mat.HsSemanales = (int)drMaterias["hs_semanales"];
                    mat.HsTotales = (int)drMaterias["hs_totales"];
                    mat.IdPlan = (int)drMaterias["id_plan"];
                }

                drMaterias.Close();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al recuperar datos de la materia: " + exc.Message);
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

                SqlCommand cmdDelete = new SqlCommand("DELETE FROM materias WHERE id_materia = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al eliminar la materia: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Materia mat)
        {
            if (mat.State == BusinessEntity.States.Deleted)
            {
                this.Delete(mat.Id);
            }
            else if (mat.State == BusinessEntity.States.Modified)
            {
                this.Update(mat);
            }
            else if (mat.State == BusinessEntity.States.New)
            {
                this.Insert(mat);
            }
            mat.State = BusinessEntity.States.Unmodified;
        }

        protected void Update(Materia mat)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdUpdate = new SqlCommand("UPDATE materias SET desc_materia = @descripcion, hs_semanales = @hssemanales, hs_totales = @hstotales, id_plan = @idplan WHERE id_materia = @id", sqlConn);
                cmdUpdate.Parameters.Add("@id", SqlDbType.Int).Value = mat.Id;
                cmdUpdate.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = mat.Descripcion;
                cmdUpdate.Parameters.Add("@hssemanales", SqlDbType.Int).Value = mat.HsSemanales;
                cmdUpdate.Parameters.Add("@hstotales", SqlDbType.Int).Value = mat.HsTotales;
                cmdUpdate.Parameters.Add("@idplan", SqlDbType.Int).Value = mat.IdPlan;

                cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al modificar datos de la materia: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Materia mat)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdInsert = new SqlCommand("INSERT INTO materias(desc_materia, hs_semanales, hs_totales ,id_plan) VALUES(@descripcion, @hssemanales, @hstotales,@idplan) SELECT @@identity", sqlConn);
                cmdInsert.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = mat.Descripcion;
                cmdInsert.Parameters.Add("@hssemanales", SqlDbType.Int).Value = mat.HsSemanales;
                cmdInsert.Parameters.Add("@hstotales", SqlDbType.Int).Value = mat.HsTotales;
                cmdInsert.Parameters.Add("@idplan", SqlDbType.Int).Value = mat.IdPlan;

                mat.Id = Decimal.ToInt32((decimal)cmdInsert.ExecuteScalar());
            }
            catch (Exception exc)
            {
                throw new Exception("Error al crear la materia: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }
    }
}
