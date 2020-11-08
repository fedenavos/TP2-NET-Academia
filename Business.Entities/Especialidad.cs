using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities {
    public class Especialidad:BusinessEntity{
        String descripcion;

        public string Descripcion { get => descripcion; set => descripcion = value; }

        public override String ToString()
        {
            return Descripcion;
        }
    }
}
