using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using Business.Entities;

namespace Data.Database
{
    public class Adapter
    {
        const String consKeyDefaultCnnString = "ConnStringExpress"; //ConnStringLocal || ConnStringExpress
        /*
         * Si estamos trabajando con un MS Sql Server con una instancia sin nombre instalado en la misma PC en la que estamos desarrollando usaremos 
         * ConnStringLocal, en caso que hayamos instalado un Sql Express sin modificar el nombre de instancia usaremos ConnStringExpress y si estamos 
         * usando el serverisi usaremos ConnStringServerISI. Estos los iremos cambiando en la medida que estemos trabajando en otros entornos
        */
        protected SqlConnection sqlConn;

        protected void OpenConnection()
        {
            String connString = ConfigurationManager.ConnectionStrings[consKeyDefaultCnnString].ConnectionString;
            sqlConn = new SqlConnection(connString);
            sqlConn.Open();
        }

        protected void CloseConnection()
        {
            sqlConn.Close();
            sqlConn = null;
        }

        protected SqlDataReader ExecuteReader(String commandText)
        {
            throw new Exception("Metodo no implementado");
        }
    }
}
