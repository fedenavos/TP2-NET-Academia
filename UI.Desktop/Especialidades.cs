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
    public partial class Especialidades : Form {
        public Especialidades() {
            InitializeComponent();
            generarColumnas();
        }
        
        private void generarColumnas() {
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "id";
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
            colId.DisplayIndex = 0;
            this.dgvEspecialidades.Columns.Add(colId);

            DataGridViewTextBoxColumn colDesc = new DataGridViewTextBoxColumn();
            colDesc.Name = "descripcion";
            colDesc.HeaderText = "Descripcion";
            colDesc.DataPropertyName = "Descripcion";
            colDesc.DisplayIndex = 1;
            this.dgvEspecialidades.Columns.Add(colDesc);
        }

        private void tsbNuevo_Click(object sender, EventArgs e) {
            EspecialidadDesktop iuEspecialidades = new EspecialidadDesktop(ApplicationForm.ModoForm.Alta);
            iuEspecialidades.ShowDialog();
            listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e) {
            if (dgvEspecialidades.GetCellCount(DataGridViewElementStates.Selected) > 0) {
                int idEspSelected = ((Especialidad)dgvEspecialidades.SelectedRows[0].DataBoundItem).Id;
                EspecialidadDesktop iuEspecialidades = new EspecialidadDesktop(idEspSelected, ApplicationForm.ModoForm.Modificacion);
                iuEspecialidades.ShowDialog();
                listar();
            }
            else {
                MessageBox.Show("Debe seleccionar la fila a modificar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e) {
            if (dgvEspecialidades.GetCellCount(DataGridViewElementStates.Selected) > 0) {
                int idEspSelected = ((Especialidad)dgvEspecialidades.SelectedRows[0].DataBoundItem).Id;
                EspecialidadDesktop iuEspecialidades = new EspecialidadDesktop(idEspSelected, ApplicationForm.ModoForm.Baja);
                iuEspecialidades.ShowDialog();
                listar();
            }
            else {
                MessageBox.Show("Debe seleccionar la fila a eliminar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void listar() {
            EspecialidadLogic especialidadLogic = new EspecialidadLogic();
            dgvEspecialidades.DataSource = null;
            try {
                dgvEspecialidades.DataSource = especialidadLogic.getAll();
            }
            catch (Exception exc) {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Especialidades_Load(object sender, EventArgs e) {
            listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
