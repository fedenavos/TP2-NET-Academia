using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities {
    public class Persona : BusinessEntity {
        String nombre, apellido, direccion, email, telefono;
        int idPlan, legajo;
        DateTime fechaNacimiento;
        TiposPersonas tipoPersona;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Email { get => email; set => email = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public int IdPlan { get => idPlan; set => idPlan = value; }
        public int Legajo { get => legajo; set => legajo = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public TiposPersonas TipoPersona { get => tipoPersona; set => tipoPersona = value; }

        public enum TiposPersonas{
            admin,
            alumno,
            docente
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
