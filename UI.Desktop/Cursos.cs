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
    public partial class Cursos : Form
    {
        public Cursos()
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
            this.dgvCursos.Columns.Add(colId);

            DataGridViewTextBoxColumn colAnio = new DataGridViewTextBoxColumn();
            colAnio.Name = "anio";
            colAnio.HeaderText = "Año calendario";
            colAnio.DataPropertyName = "AnioCalendario";
            this.dgvCursos.Columns.Add(colAnio);

            DataGridViewTextBoxColumn colIdMateria = new DataGridViewTextBoxColumn();
            colIdMateria.Name = "idMateria";
            colIdMateria.HeaderText = "ID Materia";
            colIdMateria.DataPropertyName = "IdMateria";
            this.dgvCursos.Columns.Add(colIdMateria);

            DataGridViewTextBoxColumn colIdComision = new DataGridViewTextBoxColumn();
            colIdComision.Name = "idComision";
            colIdComision.HeaderText = "ID Comision";
            colIdComision.DataPropertyName = "IdComision";
            this.dgvCursos.Columns.Add(colIdComision);

            DataGridViewTextBoxColumn colCupo = new DataGridViewTextBoxColumn();
            colCupo.Name = "cupo";
            colCupo.HeaderText = "Cupo";
            colCupo.DataPropertyName = "Cupo";
            this.dgvCursos.Columns.Add(colCupo);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            CursoDesktop iuCursos = new CursoDesktop(ApplicationForm.ModoForm.Alta);
            iuCursos.ShowDialog();
            listar();
        }

        private void listar()
        {
            CursoLogic curLogic = new CursoLogic();
            dgvCursos.DataSource = null;
            try
            {
                dgvCursos.DataSource = curLogic.getAll();
            }
            catch (Exception exc)
            {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvCursos.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int idCurSelected = ((Curso)dgvCursos.SelectedRows[0].DataBoundItem).Id;
                CursoDesktop iuCursos = new CursoDesktop(idCurSelected, ApplicationForm.ModoForm.Modificacion);
                iuCursos.ShowDialog();
                listar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar la fila a modificar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCursos.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int idCurSelected = ((Curso)dgvCursos.SelectedRows[0].DataBoundItem).Id;
                CursoDesktop iuCursos = new CursoDesktop(idCurSelected, ApplicationForm.ModoForm.Baja);
                iuCursos.ShowDialog();
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

        private void Comisiones_Load(object sender, EventArgs e)
        {
            listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
