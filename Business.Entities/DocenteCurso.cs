using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities {
    class DocenteCurso:BusinessEntity{
        //private TiposCargos cargo; No sabemos donde esta la clase TiposCargos
        int idCurso, idDocente;

        public int IdCurso { get => idCurso; set => idCurso = value; }
        public int IdDocente { get => idDocente; set => idDocente = value; }
    }
}
