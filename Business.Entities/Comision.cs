using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities {
    public class Comision:BusinessEntity {
        int anioEspecialidad, idPlan;
        String descripcion;

        public int AnioEspecialidad { get => anioEspecialidad; set => anioEspecialidad = value; }
        public int IdPlan { get => idPlan; set => idPlan = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public override string ToString()
        {
            return Id + " - " + Descripcion;
        }
    }
}
