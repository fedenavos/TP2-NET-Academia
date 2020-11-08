using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class PersonaAdapter:Adapter
    {
        public List<Persona> GetAll()
        {
            List<Persona> personas = new List<Persona>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdPersona = new SqlCommand("select * from personas", sqlConn);
                SqlDataReader drPersonas = cmdPersona.ExecuteReader();

                while (drPersonas.Read())
                {
                    Persona persona = new Persona();

                    persona.Id = (int)drPersonas["id_persona"];
                    persona.Direccion = (String)drPersonas["direccion"];
                    persona.Email = (String)drPersonas["email"];
                    persona.Nombre = (String)drPersonas["nombre"];
                    persona.Apellido = (String)drPersonas["apellido"];
                    persona.Telefono = (String)drPersonas["telefono"];
                    persona.FechaNacimiento = (DateTime)drPersonas["fecha_nac"];
                    persona.Legajo = (int)drPersonas["legajo"];
                    persona.TipoPersona = (Persona.TiposPersonas)(int)drPersonas["tipo_persona"];
                    persona.IdPlan = (int)drPersonas["id_plan"];

                    personas.Add(persona);
                }
                drPersonas.Close();
            }
            catch (Exception exc)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de personas" + exc.Message);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return personas;
        }

        public Business.Entities.Persona GetOne(int ID)
        {
            Persona persona = new Persona();

            try
            {
                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM personas WHERE id_persona = @id", sqlConn);
                cmdUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                SqlDataReader drPersonas = cmdUsuarios.ExecuteReader();
                if (drPersonas.Read())
                {
                    persona.Id = (int)drPersonas["id_persona"];
                    persona.Direccion = (String)drPersonas["direccion"];
                    persona.Email = (String)drPersonas["email"];
                    persona.Nombre = (String)drPersonas["nombre"];
                    persona.Apellido = (String)drPersonas["apellido"];
                    persona.Telefono = (String)drPersonas["telefono"];
                    persona.FechaNacimiento = (DateTime)drPersonas["fecha_nac"];
                    persona.Legajo = (int)drPersonas["legajo"];
                    persona.TipoPersona = (Persona.TiposPersonas)(int)drPersonas["tipo_persona"];
                    persona.IdPlan = (int)drPersonas["id_plan"];
                }

                drPersonas.Close();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al recuperar datos de personas: ", exc);
            }
            finally
            {
                this.CloseConnection();
            }

            return persona;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE FROM personas WHERE id_persona = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al eliminar la persona: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Persona persona)
        {
            if (persona.State == BusinessEntity.States.Deleted)
            {
                this.Delete(persona.Id);
            }
            else if (persona.State == BusinessEntity.States.Modified)
            {
                this.Update(persona);
            }
            else if (persona.State == BusinessEntity.States.New)
            {
                this.Insert(persona);
            }
            persona.State = BusinessEntity.States.Unmodified;
        }

        protected void Update(Persona persona)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdUpdate = new SqlCommand("UPDATE personas SET direccion = @direccion, id_plan = @idplan, telefono = @telefono" +
                    ",nombre = @nombre,apellido = @apellido,email = @email, fecha_nac = @fechanac, legajo = @legajo," +
                    "tipo_persona = @tipopersona WHERE id_persona = @id", sqlConn);
                cmdUpdate.Parameters.Add("@id", SqlDbType.Int).Value = persona.Id;
                cmdUpdate.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = persona.Nombre;
                cmdUpdate.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = persona.Apellido;
                cmdUpdate.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = persona.Email;
                cmdUpdate.Parameters.Add("@direccion", SqlDbType.VarChar, 50).Value = persona.Direccion;
                cmdUpdate.Parameters.Add("@telefono", SqlDbType.VarChar, 50).Value = persona.Telefono;
                cmdUpdate.Parameters.Add("@idplan", SqlDbType.Int).Value = persona.IdPlan;
                cmdUpdate.Parameters.Add("@fechanac", SqlDbType.DateTime).Value = persona.FechaNacimiento;
                cmdUpdate.Parameters.Add("@legajo", SqlDbType.Int).Value = persona.Legajo;
                cmdUpdate.Parameters.Add("@tipopersona", SqlDbType.Int).Value = persona.TipoPersona;

                cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al modificar datos de la persona: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Persona persona)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdInsert = new SqlCommand("INSERT INTO personas(nombre,apellido,email,direccion,telefono,id_plan,fecha_nac," +
                    "legajo,tipo_persona) VALUES(@nombre,@apellido,@email,@direccion,@telefono,@idplan,@fechanac,@legajo" +
                    ",@tipopersona) SELECT @@identity", sqlConn);
                cmdInsert.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = persona.Nombre;
                cmdInsert.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = persona.Apellido;
                cmdInsert.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = persona.Email;
                cmdInsert.Parameters.Add("@direccion", SqlDbType.VarChar, 50).Value = persona.Direccion;
                cmdInsert.Parameters.Add("@telefono", SqlDbType.VarChar, 50).Value = persona.Telefono;
                cmdInsert.Parameters.Add("@idplan", SqlDbType.Int).Value = persona.IdPlan;
                cmdInsert.Parameters.Add("@fechanac", SqlDbType.DateTime).Value = persona.FechaNacimiento;
                cmdInsert.Parameters.Add("@legajo", SqlDbType.Int).Value = persona.Legajo;
                cmdInsert.Parameters.Add("@tipopersona", SqlDbType.Int).Value = persona.TipoPersona;

                persona.Id = Decimal.ToInt32((decimal)cmdInsert.ExecuteScalar());
            }
            catch (Exception exc)
            {
                throw new Exception("Error al crear persona: " + exc.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }
    }
}
