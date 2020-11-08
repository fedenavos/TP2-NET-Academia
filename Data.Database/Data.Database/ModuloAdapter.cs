using Business.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class ModuloAdapter:Adapter
    {
        public List<Modulo> GetAll()
        {
            List<Modulo> modulos = new List<Modulo>();
            try
            {
                this.OpenConnection();
                SqlCommand sqlCommand = new SqlCommand("select * from modulos", sqlConn);
                SqlDataReader drModulos = sqlCommand.ExecuteReader();

                while (drModulos.Read())
                {
                    Modulo modulo = new Modulo();
                    modulo.Descripcion = (string) drModulos["desc_modulo"];
                    modulo.Id = (int) drModulos["id_modulo"];

                    modulos.Add(modulo);
                }
                drModulos.Close(); 
            }
            catch(Exception exc)
            {
                Exception exception = new Exception("Error al recuperar los modulos: " + exc.Message);
                throw exception;
            }
            finally
            {
                this.CloseConnection();
            }
            return modulos;
        }

        public Modulo GetOne(int idModulo)
        {
            Modulo modulo = new Modulo();
            try
            {
                this.OpenConnection();
                SqlCommand sqlCommand = new SqlCommand("select * from modulos where id_modulo = @idModulo", sqlConn);
                sqlCommand.Parameters.Add(new SqlParameter("@idModulo", System.Data.SqlDbType.Int)).Value = idModulo;
                SqlDataReader drModulos = sqlCommand.ExecuteReader();

                if (drModulos.Read())
                {
                    modulo.Descripcion = (string)drModulos["desc_modulo"];
                    modulo.Id = (int)drModulos["id_modulo"];
                }
                drModulos.Close();
            }
            catch (Exception exc)
            {
                Exception exception = new Exception("Error al recuperar el modulo: " + exc.Message);
                throw exception;
            }
            finally
            {
                this.CloseConnection();
            }
            return modulo;
        }

        public void Insert(Modulo modulo)
        {
            try
            {
                this.OpenConnection();
                SqlCommand sqlCommand = new SqlCommand("insert into modulos(desc_modulo) values (@descModulo) select @@Identity", sqlConn);
                sqlCommand.Parameters.Add(new SqlParameter("@descModulo", System.Data.SqlDbType.VarChar)).Value = modulo.Descripcion;
                modulo.Id = Decimal.ToInt32((decimal)sqlCommand.ExecuteScalar());
            }
            catch (Exception exc)
            {
                Exception exception = new Exception("Error al agregar el modulo: " + exc.Message);
                throw exception;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Update(Modulo modulo)
        {
            try
            {
                this.OpenConnection();
                SqlCommand sqlCommand = new SqlCommand("update modulos set desc_modulo = @descModulo where id_modulo = @idModulo", sqlConn);
                sqlCommand.Parameters.Add(new SqlParameter("@descModulo", System.Data.SqlDbType.VarChar)).Value = modulo.Descripcion;
                sqlCommand.Parameters.Add(new SqlParameter("@idModulo", System.Data.SqlDbType.Int)).Value = modulo.Id;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                Exception exception = new Exception("Error al modificar el modulo: " + exc.Message);
                throw exception;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Delete(int idModulo)
        {
            try
            {
                this.OpenConnection();
                SqlCommand sqlCommand = new SqlCommand("delete from modulos where id_modulo = @idModulo", sqlConn);
                sqlCommand.Parameters.Add(new SqlParameter("@idModulo", System.Data.SqlDbType.Int)).Value = idModulo;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                Exception exception = new Exception("Error al eliminar el modulo: " + exc.Message);
                throw exception;
            }
            finally
            {
                this.CloseConnection();
            }
        }
    }
}
