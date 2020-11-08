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

namespace UI.Desktop {
    public partial class UsuarioDesktop : ApplicationForm {
        private Usuario usuarioActual;

        public UsuarioDesktop() {
            InitializeComponent();
        }

        public UsuarioDesktop(ModoForm modo): this() {
            this.Modo = modo;
        }

        public UsuarioDesktop(int idUsuario,ModoForm modo): this() {
            this.Modo = modo;
            UsuarioLogic usuarioLogic = new UsuarioLogic();
            UsuarioActual = usuarioLogic.getOne(idUsuario);

            MapearDeDatos();
        }

        public Usuario UsuarioActual { get => usuarioActual; set => usuarioActual = value; }

        public override void MapearADatos() {
            if(Modo == ModoForm.Alta) {
                UsuarioActual = new Usuario();
            }
            UsuarioActual.Habilitado = chkHabilitado.Checked;
            UsuarioActual.NombreUsuario = txtUsuario.Text;
            UsuarioActual.Clave = txtClave.Text;
            UsuarioActual.Persona.Id = int.Parse(txtIdPersona.Text);

            if(Modo == ModoForm.Alta) {
                UsuarioActual.State = BusinessEntity.States.New;
            }
            else {
                UsuarioActual.State = BusinessEntity.States.Modified;
            }
            UsuarioActual.State = (Modo == ModoForm.Alta ? BusinessEntity.States.New : BusinessEntity.States.Modified);
        }
        public override void MapearDeDatos() {
            base.MapearDeDatos();
            txtUsuario.Text = UsuarioActual.NombreUsuario;
            txtId.Text = UsuarioActual.Id.ToString();
            txtClave.Text = UsuarioActual.Clave;
            txtConfirmarClave.Text = UsuarioActual.Clave;
            chkHabilitado.Checked = UsuarioActual.Habilitado;
            txtIdPersona.Text = UsuarioActual.Persona.Id.ToString();

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
            UsuarioLogic usuarioLogic = new UsuarioLogic();
            try {
                usuarioLogic.save(UsuarioActual);
            }
            catch (Exception exc) {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }
        public override bool Validar() {
            if (txtUsuario.TextLength > 0 && txtClave.TextLength > 7 && txtClave.Text == txtConfirmarClave.Text) {
                return true;
            }

            return false;
        }

        private void btnAceptar_Click(object sender, EventArgs e) {
            if(Modo == ModoForm.Alta || Modo == ModoForm.Modificacion) {
                //Modificacion o alta: validamos los datos y despues guardamos
                if (Validar()) {
                    GuardarCambios();
                    Notificar("Operacion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    Notificar("Alguna de sus entradas no es valida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else{
                //Baja: no validamos ni guardamos nada, simplemente borramos
                new UsuarioLogic().delete(UsuarioActual.Id);
                Notificar("Operacion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e) {
            this.Close();
        }
        private void UpdateComboBox() {
            //TODO
        }

        private void btnCrearPersona_Click(object sender, EventArgs e) {
            PersonaDesktop pd = new PersonaDesktop(ModoForm.Alta);
            if(pd.ShowDialog() == DialogResult.OK) {
                UpdateComboBox();
            }
        }
    }
}
