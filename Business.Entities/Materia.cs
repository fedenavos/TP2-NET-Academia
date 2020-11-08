using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities {
    public class Materia:BusinessEntity{
        String descripcion;
        int hsSemanales, hsTotales, idPlan;

        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int HsSemanales { get => hsSemanales; set => hsSemanales = value; }
        public int HsTotales { get => hsTotales; set => hsTotales = value; }
        public int IdPlan { get => idPlan; set => idPlan = value; }

        public override string ToString()
        {
            return Id + " - " + Descripcion ;
        }
    }
}
