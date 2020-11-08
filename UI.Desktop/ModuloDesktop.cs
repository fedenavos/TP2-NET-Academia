using Business.Entities;
using Business.logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Desktop
{
    public partial class ModuloDesktop : ApplicationForm
    {
        private Modulo moduloActual;

        public ModuloDesktop(ModoForm modo)
        {
            InitializeComponent();
            this.Modo = modo;
        }

        public ModuloDesktop(int id, ModoForm modo)
        {
            InitializeComponent();
            this.Modo = modo;
            ModulosLogic modulosLogic = new ModulosLogic();
            moduloActual = modulosLogic.GetOne(id);
            MapearDeDatos();
        }

        public override void MapearADatos()
        {
            if(Modo == ModoForm.Alta)
            {
                moduloActual = new Modulo();
            }
            moduloActual.Descripcion = txtDescripcion.Text;
            moduloActual.State = (Modo == ModoForm.Alta ? BusinessEntity.States.New : BusinessEntity.States.Modified);
        }

        public override void MapearDeDatos()
        {
            base.MapearDeDatos();
            this.txtDescripcion.Text = moduloActual.Descripcion;
            this.txtIdModulo.Text = moduloActual.Id.ToString();

            switch (Modo)
            {
                case ModoForm.Baja:
                    btnAceptar.Text = "Eliminar";
                    break;
                case ModoForm.Consulta:
                    btnAceptar.Text = "Aceptar";
                    break;
                default:
                    btnAceptar.Text = "Guardar";
                    break;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ModulosLogic modulosLogic = new ModulosLogic();
            if(this.Modo == ModoForm.Alta)
            {
                MapearADatos();
                modulosLogic.AgregarModulo(moduloActual);
            } else if(this.Modo == ModoForm.Baja)
            {
                modulosLogic.EliminarModulo(moduloActual.Id);
            } else if(this.Modo == ModoForm.Modificacion)
            {
                MapearADatos();
                modulosLogic.ModificarModulo(moduloActual);
            }
            this.Close();
        }
    }
}
