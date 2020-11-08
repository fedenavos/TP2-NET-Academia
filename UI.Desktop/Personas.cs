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
    public partial class Personas : Form
    {
        public Personas()
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
            colId.DisplayIndex = 0;
            this.dgvPersonas.Columns.Add(colId);

            DataGridViewTextBoxColumn colNombre = new DataGridViewTextBoxColumn();
            colNombre.Name = "nombre";
            colNombre.HeaderText = "Nombre";
            colNombre.DataPropertyName = "Nombre";
            colNombre.DisplayIndex = 1;
            this.dgvPersonas.Columns.Add(colNombre);

            DataGridViewTextBoxColumn colApellido = new DataGridViewTextBoxColumn();
            colApellido.Name = "apellido";
            colApellido.HeaderText = "Apellido";
            colApellido.DataPropertyName = "Apellido";
            colApellido.DisplayIndex = 2;
            this.dgvPersonas.Columns.Add(colApellido);

            DataGridViewTextBoxColumn colEmail = new DataGridViewTextBoxColumn();
            colEmail.Name = "email";
            colEmail.HeaderText = "Dir.Email";
            colEmail.DataPropertyName = "Email";
            colEmail.DisplayIndex = 3;
            this.dgvPersonas.Columns.Add(colEmail);

            DataGridViewTextBoxColumn colEstado = new DataGridViewTextBoxColumn();
            colEstado.Name = "estado";
            colEstado.HeaderText = "Estado";
            colEstado.DataPropertyName = "State";
            colEstado.DisplayIndex = 4;
            this.dgvPersonas.Columns.Add(colEstado);
        }

        public void listar()
        {
            PersonaLogic personaLogic = new PersonaLogic();
            dgvPersonas.DataSource = null;
            try
            {
                dgvPersonas.DataSource = personaLogic.getAll();
            }
            catch (Exception exc)
            {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Personas_Load(object sender, EventArgs e)
        {
            listar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            PersonaDesktop iuPersona = new PersonaDesktop(ApplicationForm.ModoForm.Alta);
            iuPersona.ShowDialog();
            listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvPersonas.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int idUserSelected = ((Business.Entities.Persona)dgvPersonas.SelectedRows[0].DataBoundItem).Id;
                PersonaDesktop iuPersona = new PersonaDesktop(idUserSelected, ApplicationForm.ModoForm.Modificacion);
                iuPersona.ShowDialog();
                listar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar la fila a modificar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPersonas.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                int idUserSelected = ((Persona)dgvPersonas.SelectedRows[0].DataBoundItem).Id;
                PersonaDesktop iuPersona = new PersonaDesktop(idUserSelected, ApplicationForm.ModoForm.Baja);
                iuPersona.ShowDialog();
                listar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
