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
using static UI.Desktop.ApplicationForm;

namespace UI.Desktop {
    public partial class ComisionDesktop : ApplicationForm {
        private Comision comActual;
        private List<Plan> planes;
        public ComisionDesktop() {
            PlanesLogic planesLogic = new PlanesLogic();
            InitializeComponent();
            planes = planesLogic.getAll();
            foreach(Plan plan in planes) {
                cbPlan.Items.Add(plan);
            }
        }

        public ComisionDesktop(ModoForm modo) : this() {
            this.Modo = modo;
        }

        public Comision ComisionActual { get => comActual; set => comActual = value; }

        public ComisionDesktop(int idCom,ModoForm modo) : this() {
            this.Modo = modo;
            ComisionesLogic comLogic = new ComisionesLogic();
            ComisionActual = comLogic.getOne(idCom);

            MapearDeDatos();
            this.Modo = modo;
        }

        public override void MapearADatos() {
            if (Modo == ModoForm.Alta) {
                ComisionActual = new Comision();
            }

            ComisionActual.Descripcion = txtDesc.Text;
            ComisionActual.AnioEspecialidad = int.Parse(txtAnioEsp.Text);
            ComisionActual.IdPlan = ((Plan)cbPlan.SelectedItem).Id;

//            if (Modo == ModoForm.Alta) {
//                ComisionActual.State = BusinessEntity.States.New;
//            }
//            else {
//                ComisionActual.State = BusinessEntity.States.Modified;
//            }
            ComisionActual.State = (Modo == ModoForm.Alta ? BusinessEntity.States.New : BusinessEntity.States.Modified);
        }
        public override void MapearDeDatos() {
            //PlanesLogic planesLogic = new PlanesLogic();
            base.MapearDeDatos();
            txtDesc.Text = ComisionActual.Descripcion;
            txtAnioEsp.Text = ComisionActual.AnioEspecialidad.ToString();
            cbPlan.SelectedIndex = cbPlan.FindStringExact((planes.Where(p => p.Id == ComisionActual.IdPlan)).ToString());

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
            ComisionesLogic comLogic = new ComisionesLogic();
            try {
                comLogic.save(ComisionActual);
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
                new ComisionesLogic().delete(ComisionActual.Id);
                Notificar("Operacion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
