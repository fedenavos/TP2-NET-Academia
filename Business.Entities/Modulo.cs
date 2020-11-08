using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities {
    public class Modulo:BusinessEntity {
        //Declaración de variables
        String descripcion;

        //Properties
        public String Descripcion {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }
}
