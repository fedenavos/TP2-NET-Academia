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

namespace UI.Desktop {
    public partial class EspecialidadDesktop : ApplicationForm {
        private Especialidad especialidadActual;
        public EspecialidadDesktop() {
            InitializeComponent();
        }
        public EspecialidadDesktop(ModoForm modo):this() {
            this.Modo = modo;
        }

        public Especialidad EspecialidadActual { get => especialidadActual; set => especialidadActual = value; }

        public EspecialidadDesktop(int idEsp,ModoForm modo) : this() {
            this.Modo = modo;
            EspecialidadLogic especialidadLogic = new EspecialidadLogic();
            EspecialidadActual = especialidadLogic.getOne(idEsp);

            MapearDeDatos();
            this.Modo = modo;
        }

        public override void MapearADatos() {
            if (Modo == ModoForm.Alta) {
                EspecialidadActual = new Especialidad();
            }
            EspecialidadActual.Descripcion = txtDesc.Text;

            if (Modo == ModoForm.Alta) {
                EspecialidadActual.State = BusinessEntity.States.New;
            }
            else {
                EspecialidadActual.State = BusinessEntity.States.Modified;
            }
            EspecialidadActual.State = (Modo == ModoForm.Alta ? BusinessEntity.States.New : BusinessEntity.States.Modified);
        }
        public override void MapearDeDatos() {
            base.MapearDeDatos();
            txtDesc.Text = EspecialidadActual.Descripcion;

            switch (Modo) {
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

        public override void GuardarCambios() {
            MapearADatos();
            EspecialidadLogic especialidadLogic = new EspecialidadLogic();
            try {
                especialidadLogic.save(EspecialidadActual);
            }
            catch (Exception exc) {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }

        public override bool Validar() {
            return txtDesc.TextLength > 0;

        }

        private void btnAceptar_Click(object sender, EventArgs e) {
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion) {
                //Modificacion o alta: validamos los datos y despues guardamos
                if (Validar()) {
                    GuardarCambios();
                    Notificar("Operacion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    Notificar("Alguna de sus entradas no es valida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                //Baja: no validamos ni guardamos nada, simplemente borramos
                new EspecialidadLogic().delete(EspecialidadActual.Id);
                Notificar("Operacion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
