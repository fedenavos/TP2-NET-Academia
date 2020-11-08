using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Usuario:BusinessEntity
    {
        private string clave;
        private bool habilitado;
        private string nombreUsuario;
        private Persona persona;

        public Usuario()
        {
            Persona = new Persona();
        }

        public string Clave { get => clave; set => clave = value; }
        public bool Habilitado { get => habilitado; set => habilitado = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public Persona Persona { get => persona; set => persona = value; }
    }
}
