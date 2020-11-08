using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.logic;
using Business.Entities;

namespace UI.Desktop
{
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();

            generarColumnas();
        }

        private void generarColumnas() {
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "id";
            colId.HeaderText = "ID";
            colId.DataPropertyName = "Id";
            colId.DisplayIndex = 0;
            this.dgvUsuarios.Columns.Add(colId);

            DataGridViewTextBoxColumn colUsuario = new DataGridViewTextBoxColumn();
            colUsuario.Name = "usuario";
            colUsuario.HeaderText = "Nombre de Usuario";
            colUsuario.DataPropertyName = "NombreUsuario";
            colUsuario.DisplayIndex = 1;
            this.dgvUsuarios.Columns.Add(colUsuario);

            DataGridViewCheckBoxColumn colHabilitado = new DataGridViewCheckBoxColumn();
            colHabilitado.Name = "habilitado";
            colHabilitado.HeaderText = "Habilitado";
            colHabilitado.DataPropertyName = "Habilitado";
            colHabilitado.DisplayIndex = 2;
            this.dgvUsuarios.Columns.Add(colHabilitado);

            DataGridViewTextBoxColumn colEstado = new DataGridViewTextBoxColumn();
            colEstado.Name = "estado";
            colEstado.HeaderText = "Estado";
            colEstado.DataPropertyName = "State";
            colEstado.DisplayIndex = 3;
            this.dgvUsuarios.Columns.Add(colEstado);
        }

        public void listar() {
            UsuarioLogic usuarioLogic = new UsuarioLogic();
            dgvUsuarios.DataSource = null;
            try {
                dgvUsuarios.DataSource = usuarioLogic.getAll();
            }
            catch(Exception exc) {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }
        
        private void Usuarios_Load(object sender, EventArgs e) {
            listar();
        }

        private void btnActualizar_Click(object sender, EventArgs e) {
            listar();
        }

        private void btnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tsbNuevo_Click(object sender, EventArgs e) {
            UsuarioDesktop iuUsuarios = new UsuarioDesktop(ApplicationForm.ModoForm.Alta);
            iuUsuarios.ShowDialog();
            listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e) {
            if (dgvUsuarios.GetCellCount(DataGridViewElementStates.Selected) > 0) {
                int idUserSelected = ((Usuario)dgvUsuarios.SelectedRows[0].DataBoundItem).Id;
                UsuarioDesktop iuUsuarios = new UsuarioDesktop(idUserSelected, ApplicationForm.ModoForm.Modificacion);
                iuUsuarios.ShowDialog();
                listar();
            }
            else {
                MessageBox.Show("Debe seleccionar la fila a modificar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e) {
            if(dgvUsuarios.GetCellCount(DataGridViewElementStates.Selected) > 0) {
                int idUserSelected = ((Usuario)dgvUsuarios.SelectedRows[0].DataBoundItem).Id;
                UsuarioDesktop iuUsuarios = new UsuarioDesktop(idUserSelected, ApplicationForm.ModoForm.Baja);
                iuUsuarios.ShowDialog();
                listar();
            }
            else {
                MessageBox.Show("Debe seleccionar la fila a eliminar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
