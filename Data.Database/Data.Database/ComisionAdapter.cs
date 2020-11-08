using Business.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database {
    public class ComisionAdapter:Adapter {
        public List<Comision> GetAll() {
            List<Comision> comisiones = new List<Comision>();
            try {
                this.OpenConnection();
                SqlCommand cmdComisiones = new SqlCommand("select * from comisiones", sqlConn);
                SqlDataReader drComisiones = cmdComisiones.ExecuteReader();

                while (drComisiones.Read()) {
                    Comision com = new Comision();

                    com.Id = (int)drComisiones["id_comision"];
                    com.Descripcion = (String)drComisiones["desc_comision"];
                    com.AnioEspecialidad = (int)drComisiones["anio_especialidad"];
                    com.IdPlan = (int)drComisiones["id_plan"];

                    comisiones.Add(com);
                }
                drComisiones.Close();
            }
            catch (Exception exc) {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de comisiones: " + exc.Message);
                throw ExcepcionManejada;
            }
            finally {
                this.CloseConnection();
            }

            return comisiones;
        }

        public Comision GetOne(int ID) {
            Comision com = new Comision();

            try {
                this.OpenConnection();
                SqlCommand cmdComisiones = new SqlCommand("SELECT * FROM comisiones WHERE id_comision = @id", sqlConn);
                cmdComisiones.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drComisiones = cmdComisiones.ExecuteReader();

                if (drComisiones.Read()) {
                    com.Id = (int)drComisiones["id_comision"];
                    com.Descripcion = (String)drComisiones["desc_comision"];
                    com.AnioEspecialidad = (int)drComisiones["anio_especialidad"];
                    com.IdPlan = (int)drComisiones["id_plan"];
                }

                drComisiones.Close();
            }
            catch (Exception exc) {
                throw new Exception("Error al recuperar datos de comisiones: " + exc.Message);
            }
            finally {
                this.CloseConnection();
            }

            return com;
        }

        public void Delete(int ID) {
            try {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE FROM comisiones WHERE id_comision = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception exc) {
                throw new Exception("Error al eliminar la comision: " + exc.Message);
            }
            finally {
                this.CloseConnection();
            }
        }

        public void Save(Comision com) {
            if (com.State == BusinessEntity.States.Deleted) {
                this.Delete(com.Id);
            }
            else if (com.State == BusinessEntity.States.Modified) {
                this.Update(com);
            }
            else if (com.State == BusinessEntity.States.New) {
                this.Insert(com);
            }
            com.State = BusinessEntity.States.Unmodified;
        }

        protected void Update(Comision com) {
            try {
                this.OpenConnection();

                SqlCommand cmdUpdate = new SqlCommand("UPDATE comisiones SET desc_comision = @descripcion, anio_especialidad = @anioespecialidad, id_plan = @idplan WHERE id_comision = @id", sqlConn);
                cmdUpdate.Parameters.Add("@id", SqlDbType.Int).Value = com.Id;
                cmdUpdate.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = com.Descripcion;
                cmdUpdate.Parameters.Add("@anioespecialidad", SqlDbType.Int).Value = com.AnioEspecialidad;
                cmdUpdate.Parameters.Add("@idplan", SqlDbType.Int).Value = com.IdPlan;

                cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception exc) {
                throw new Exception("Error al modificar datos de la comision: " + exc.Message);
            }
            finally {
                this.CloseConnection();
            }
        }

        protected void Insert(Comision com) {
            try {
                this.OpenConnection();

                SqlCommand cmdInsert = new SqlCommand("INSERT INTO comisiones(desc_comision,anio_especialidad,id_plan) VALUES(@descripcion, @anioespecialidad,@idplan) SELECT @@identity", sqlConn);
                cmdInsert.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = com.Descripcion;
                cmdInsert.Parameters.Add("@anioespecialidad", SqlDbType.Int).Value = com.AnioEspecialidad;
                cmdInsert.Parameters.Add("@idplan", SqlDbType.Int).Value = com.IdPlan;

                com.Id = Decimal.ToInt32((decimal)cmdInsert.ExecuteScalar());
            }
            catch (Exception exc) {
                throw new Exception("Error al crear la comision: " + exc.Message);
            }
            finally {
                this.CloseConnection();
            }
        }
    }
}
