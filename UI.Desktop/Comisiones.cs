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
    public partial class Comisiones : Form {
        public Comisiones() {
            InitializeComponent();
            generarColumnas();
        }

        private void generarColumnas() {
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "id";
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
            this.dgvComisiones.Columns.Add(colId);

            DataGridViewTextBoxColumn colAnio = new DataGridViewTextBoxColumn();
            colAnio.Name = "anio";
            colAnio.HeaderText = "Año especialidad";
            colAnio.DataPropertyName = "AnioEspecialidad";
            this.dgvComisiones.Columns.Add(colAnio);

            DataGridViewTextBoxColumn colIdPlan = new DataGridViewTextBoxColumn();
            colIdPlan.Name = "idPlan";
            colIdPlan.HeaderText = "ID Plan";
            colIdPlan.DataPropertyName = "IdPlan";
            this.dgvComisiones.Columns.Add(colIdPlan);

            DataGridViewTextBoxColumn colDescripcion = new DataGridViewTextBoxColumn();
            colDescripcion.Name = "descripcion";
            colDescripcion.HeaderText = "Descripcion";
            colDescripcion.DataPropertyName = "Descripcion";
            this.dgvComisiones.Columns.Add(colDescripcion);
        }

        private void tsbNuevo_Click(object sender, EventArgs e) {
            ComisionDesktop iuComisiones = new ComisionDesktop(ApplicationForm.ModoForm.Alta);
            iuComisiones.ShowDialog();
            listar();
        }

        private void listar() {
            ComisionesLogic comLogic = new ComisionesLogic();
            dgvComisiones.DataSource = null;
            try {
                dgvComisiones.DataSource = comLogic.getAll();
            }catch(Exception exc) {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e) {
            if (dgvComisiones.GetCellCount(DataGridViewElementStates.Selected) > 0) {
                int idComSelected = ((Comision)dgvComisiones.SelectedRows[0].DataBoundItem).Id;
                ComisionDesktop iuComisiones = new ComisionDesktop(idComSelected, ApplicationForm.ModoForm.Modificacion);
                iuComisiones.ShowDialog();
                listar();
            }
            else {
                MessageBox.Show("Debe seleccionar la fila a modificar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e) {
            if (dgvComisiones.GetCellCount(DataGridViewElementStates.Selected) > 0) {
                int idComSelected = ((Comision)dgvComisiones.SelectedRows[0].DataBoundItem).Id;
                ComisionDesktop iuComisiones = new ComisionDesktop(idComSelected, ApplicationForm.ModoForm.Baja);
                iuComisiones.ShowDialog();
                listar();
            }
            else {
                MessageBox.Show("Debe seleccionar la fila a eliminar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e) {
            listar();
        }

        private void Comisiones_Load(object sender, EventArgs e) {
            listar();
        }

        private void btnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
