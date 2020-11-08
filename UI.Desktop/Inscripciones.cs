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
    public partial class Inscripciones : Form
    {
        public Inscripciones()
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
            this.dgvInscripciones.Columns.Add(colId);

            DataGridViewTextBoxColumn colCondicion = new DataGridViewTextBoxColumn();
            colCondicion.Name = "condicion";
            colCondicion.HeaderText = "Condicion";
            colCondicion.DataPropertyName = "Condicion";
            this.dgvInscripciones.Columns.Add(colCondicion);

            DataGridViewTextBoxColumn colNota = new DataGridViewTextBoxColumn();
            colNota.Name = "nota";
            colNota.HeaderText = "Nota";
            colNota.DataPropertyName = "nota";
            this.dgvInscripciones.Columns.Add(colNota);

            DataGridViewTextBoxColumn colIdAlumno = new DataGridViewTextBoxColumn();
            colIdAlumno.Name = "idAlumno";
            colIdAlumno.HeaderText = "ID Alumno";
            colIdAlumno.DataPropertyName = "IdAlumno";
            this.dgvInscripciones.Columns.Add(colIdAlumno);

            DataGridViewTextBoxColumn colIdCurso = new DataGridViewTextBoxColumn();
            colIdCurso.Name = "idCurso";
            colIdCurso.HeaderText = "ID Curso";
            colIdCurso.DataPropertyName = "IdCurso";
            this.dgvInscripciones.Columns.Add(colIdCurso);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            InscripcionDesktop iuInscripciones = new InscripcionDesktop(ApplicationForm.ModoForm.Alta);
            iuInscripciones.ShowDialog();
            listar();
        }

        private void listar()
        {
            AlumnoInscripcionLogic insLogic = new AlumnoInscripcionLogic();
            dgvInscripciones.DataSource = null;
            try
            {
                dgvInscripciones.DataSource = insLogic.getAll();
            }
            catch (Exception exc)
            {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvInscripciones.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int idInsSelected = ((AlumnoInscripcion)dgvInscripciones.SelectedRows[0].DataBoundItem).Id;
                InscripcionDesktop iuInscripciones = new InscripcionDesktop(idInsSelected, ApplicationForm.ModoForm.Modificacion);
                iuInscripciones.ShowDialog();
                listar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar la fila a modificar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (dgvInscripciones.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int idInsSelected = ((AlumnoInscripcion)dgvInscripciones.SelectedRows[0].DataBoundItem).Id;
                InscripcionDesktop iuInscripciones = new InscripcionDesktop(idInsSelected, ApplicationForm.ModoForm.Baja);
                iuInscripciones.ShowDialog();
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
