using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities {
    public class ModuloUsuario:Modulo {
        //Declaración de variables
        int idUsuario, idModulo;
        Boolean permiteAlta, permiteBaja, permiteModificacion, permiteConsulta;

        //Properties
        public int IdUsuario {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
        public int IdModulo {
            get { return idModulo; }
            set { idModulo = value; }
        }
        public Boolean PermiteAlta {
            get { return permiteAlta; }
            set { permiteAlta = value; }
        }
        public Boolean PermiteBaja {
            get { return permiteBaja; }
            set { permiteBaja = value; }
        }
        public Boolean PermiteModificacion {
            get { return permiteModificacion; }
            set { permiteModificacion = value; }
        }
        public Boolean PermiteConsulta {
            get { return permiteConsulta; }
            set { permiteConsulta = value; }
        }
    }
}
