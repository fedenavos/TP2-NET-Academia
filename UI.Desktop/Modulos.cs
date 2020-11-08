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
    public partial class Modulos : Form
    {
        public Modulos()
        {
            InitializeComponent();
            generarColumnas();
            listar();
        }

        private void generarColumnas()
        {
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.HeaderText = "ID";
            colId.Name = "id";
            colId.DataPropertyName = "Id";
            this.dgvModulos.Columns.Add(colId);

            DataGridViewTextBoxColumn colDescripcion = new DataGridViewTextBoxColumn();
            colDescripcion.HeaderText = "Descripcion";
            colDescripcion.Name = "descripcion";
            colDescripcion.DataPropertyName = "descripcion";
            this.dgvModulos.Columns.Add(colDescripcion);
        }

        private void listar()
        {
            ModulosLogic moduloLogic = new ModulosLogic();
            dgvModulos.DataSource = null;
            try
            {
                dgvModulos.DataSource = moduloLogic.GetAll();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tsbAgregar_Click(object sender, EventArgs e)
        {

            ModuloDesktop iuModulo = new ModuloDesktop(ApplicationForm.ModoForm.Alta);
            iuModulo.ShowDialog();
            listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvModulos.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int idModulo = ((Modulo)dgvModulos.SelectedRows[0].DataBoundItem).Id;
                ModuloDesktop iuModulo = new ModuloDesktop(idModulo, ApplicationForm.ModoForm.Modificacion);
                iuModulo.ShowDialog();
                listar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar la fila a modificar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (dgvModulos.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int idModulo = ((Modulo)dgvModulos.SelectedRows[0].DataBoundItem).Id;
                ModuloDesktop iuModulo = new ModuloDesktop(idModulo, ApplicationForm.ModoForm.Baja);
                iuModulo.ShowDialog();
                listar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar la fila a modificar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
