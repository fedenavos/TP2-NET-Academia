using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.logic;

namespace UI.Desktop
{
    public partial class Planes : Form
    {
        public Planes()
        {
            InitializeComponent();
            generarColumnas();
        }

        private void generarColumnas()
        {
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "id";
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
//            colId.DisplayIndex = 0;
            this.dgvPlanes.Columns.Add(colId);

            DataGridViewTextBoxColumn colDesc = new DataGridViewTextBoxColumn();
            colDesc.Name = "descripcion";
            colDesc.HeaderText = "Descripcion";
            colDesc.DataPropertyName = "Descripcion";
//            colDesc.DisplayIndex = 1;
            this.dgvPlanes.Columns.Add(colDesc);

            DataGridViewTextBoxColumn colIdEspecialidad = new DataGridViewTextBoxColumn();
            colIdEspecialidad.Name = "id_especialidad";
            colIdEspecialidad.HeaderText = "Id Especialidad";
            colIdEspecialidad.DataPropertyName = "IdEspecialidad";
//            colDesc.DisplayIndex = 1;
            this.dgvPlanes.Columns.Add(colIdEspecialidad);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            PlanDesktop iuPlanes = new PlanDesktop(ApplicationForm.ModoForm.Alta);
            iuPlanes.ShowDialog();
            listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvPlanes.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int idPlanSelected = ((Plan)dgvPlanes.SelectedRows[0].DataBoundItem).Id;
                PlanDesktop iuPlanes = new PlanDesktop(idPlanSelected, ApplicationForm.ModoForm.Modificacion);
                iuPlanes.ShowDialog();
                listar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar la fila a modificar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPlanes.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int idPlanSelected = ((Plan)dgvPlanes.SelectedRows[0].DataBoundItem).Id;
                PlanDesktop iuPlanes = new PlanDesktop(idPlanSelected, ApplicationForm.ModoForm.Baja);
                iuPlanes.ShowDialog();
                listar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void listar()
        {
            PlanesLogic planesLogic = new PlanesLogic();
            dgvPlanes.DataSource = null;
            try
            {
                dgvPlanes.DataSource = planesLogic.getAll();
            }
            catch (Exception exc)
            {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Planes_Load(object sender, EventArgs e)
        {
            listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e) {
            listar();
        }
    }
}
