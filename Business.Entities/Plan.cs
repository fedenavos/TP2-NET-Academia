using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities {
    public class Plan:BusinessEntity {
        private String descripcion;
        private int idEspecialidad;

        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int IdEspecialidad { get => idEspecialidad; set => idEspecialidad = value; }

        public override String ToString() {
            return Descripcion;
        }
    }
}
