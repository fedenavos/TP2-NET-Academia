using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;

namespace Data.Database {
    public class EspecialidadAdapter:Adapter {
        public List<Especialidad> GetAll() {
            List<Especialidad> especialidades = new List<Especialidad>();
            try {
                this.OpenConnection();
                SqlCommand cmdEspecialidades = new SqlCommand("select * from especialidades", sqlConn);
                SqlDataReader drEspecialidades = cmdEspecialidades.ExecuteReader();

                while (drEspecialidades.Read()) {
                    Especialidad esp = new Especialidad();

                    esp.Id = (int)drEspecialidades["id_especialidad"];
                    esp.Descripcion = (String)drEspecialidades["desc_especialidad"];

                    especialidades.Add(esp);
                }
                drEspecialidades.Close();
            }
            catch (Exception exc) {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de especialidades: "+ exc);
                throw ExcepcionManejada;
            }
            finally {
                this.CloseConnection();
            }

            return especialidades;
        }

        public Business.Entities.Especialidad GetOne(int ID) {
            Especialidad esp = new Especialidad();

            try {
                this.OpenConnection();
                SqlCommand cmdEspecialidades = new SqlCommand("SELECT * FROM especialidades WHERE id_especialidad = @id", sqlConn);
                cmdEspecialidades.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drEspecialidades = cmdEspecialidades.ExecuteReader();

                if (drEspecialidades.Read()) {
                    esp.Id = (int)drEspecialidades["id_especialidad"];
                    esp.Descripcion = (String)drEspecialidades["desc_especialidad"];
                }

                drEspecialidades.Close();
            }
            catch (Exception exc) {
                throw new Exception("Error al recuperar datos de especialidades: ", exc);
            }
            finally {
                this.CloseConnection();
            }

            return esp;
        }

        public void Delete(int ID) {
            try {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE FROM especialidades WHERE id_especialidad = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception exc) {
                throw new Exception("Error al eliminar la especialidad: "+ exc.Message);
            }
            finally {
                this.CloseConnection();
            }
        }

        public void Save(Especialidad especialidad) {
            if (especialidad.State == BusinessEntity.States.Deleted) {
                this.Delete(especialidad.Id);
            }
            else if (especialidad.State == BusinessEntity.States.Modified) {
                this.Update(especialidad);
            }
            else if (especialidad.State == BusinessEntity.States.New) {
                this.Insert(especialidad);
            }
            especialidad.State = BusinessEntity.States.Unmodified;
        }

        protected void Update(Especialidad especialidad) {
            try {
                this.OpenConnection();

                SqlCommand cmdUpdate = new SqlCommand("UPDATE especialidades SET desc_especialidad = @descripcion WHERE id_especialidad = @id", sqlConn);
                cmdUpdate.Parameters.Add("@id", SqlDbType.Int).Value = especialidad.Id;
                cmdUpdate.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = especialidad.Descripcion;

                cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception exc) {
                throw new Exception("Error al modificar datos de la especialidad: "+ exc.Message);
            }
            finally {
                this.CloseConnection();
            }
        }

        protected void Insert(Especialidad especialidad) {
            try {
                this.OpenConnection();

                SqlCommand cmdInsert = new SqlCommand("INSERT INTO especialidades(desc_especialidad) VALUES(@descripcion) SELECT @@identity", sqlConn);
                cmdInsert.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = especialidad.Descripcion;

                especialidad.Id = Decimal.ToInt32((decimal)cmdInsert.ExecuteScalar());
            }
            catch (Exception exc) {
                throw new Exception("Error al crear especialidad: "+ exc.Message);
            }
            finally {
                this.CloseConnection();
            }
        }
    }
}
