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
    public partial class Materias : Form
    {
        public Materias()
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
            this.dgvMaterias.Columns.Add(colId);

            DataGridViewTextBoxColumn colHsSemanales = new DataGridViewTextBoxColumn();
            colHsSemanales.Name = "hsSemanales";
            colHsSemanales.HeaderText = "Horas semanales";
            colHsSemanales.DataPropertyName = "HsSemanales";
            this.dgvMaterias.Columns.Add(colHsSemanales);

            DataGridViewTextBoxColumn colHsTotales = new DataGridViewTextBoxColumn();
            colHsTotales.Name = "hsTotales";
            colHsTotales.HeaderText = "Horas totales";
            colHsTotales.DataPropertyName = "HsTotales";
            this.dgvMaterias.Columns.Add(colHsTotales);

            DataGridViewTextBoxColumn colIdPlan = new DataGridViewTextBoxColumn();
            colIdPlan.Name = "idPlan";
            colIdPlan.HeaderText = "ID Plan";
            colIdPlan.DataPropertyName = "IdPlan";
            this.dgvMaterias.Columns.Add(colIdPlan);

            DataGridViewTextBoxColumn colDescripcion = new DataGridViewTextBoxColumn();
            colDescripcion.Name = "descripcion";
            colDescripcion.HeaderText = "Descripcion";
            colDescripcion.DataPropertyName = "Descripcion";
            this.dgvMaterias.Columns.Add(colDescripcion);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            MateriaDesktop iuMaterias = new MateriaDesktop(ApplicationForm.ModoForm.Alta);
            iuMaterias.ShowDialog();
            listar();
        }

        private void listar()
        {
            MateriaLogic matLogic = new MateriaLogic();
            dgvMaterias.DataSource = null;
            try
            {
                dgvMaterias.DataSource = matLogic.getAll();
            }
            catch (Exception exc)
            {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvMaterias.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int idMatSelected = ((Materia)dgvMaterias.SelectedRows[0].DataBoundItem).Id;
                MateriaDesktop iuMaterias = new MateriaDesktop(idMatSelected, ApplicationForm.ModoForm.Modificacion);
                iuMaterias.ShowDialog();
                listar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar la fila a modificar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMaterias.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int idMatSelected = ((Materia)dgvMaterias.SelectedRows[0].DataBoundItem).Id;
                MateriaDesktop iuMaterias = new MateriaDesktop(idMatSelected, ApplicationForm.ModoForm.Baja);
                iuMaterias.ShowDialog();
                listar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void Materias_Load(object sender, EventArgs e)
        {
            listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
