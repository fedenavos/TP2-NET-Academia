using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities {
    public class Curso:BusinessEntity{
        int anioCalendario, cupo, idComision, idMateria;

        public int AnioCalendario { get => anioCalendario; set => anioCalendario = value; }
        public int Cupo { get => cupo; set => cupo = value; }
        public int IdComision { get => idComision; set => idComision = value; }
        public int IdMateria { get => idMateria; set => idMateria = value; }

        public override string ToString()
        {
            return "Curso: " + Id + " - " + AnioCalendario;
        }
    }
}
