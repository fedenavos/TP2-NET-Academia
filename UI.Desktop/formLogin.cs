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

namespace UI.Desktop{
    public partial class formLogin : Form{

        private Usuario usuarioActual;


        public formLogin()
        {
            InitializeComponent();
        }

        public Usuario UsuarioActual { get => usuarioActual; set => usuarioActual = value; }

        private void btnIngresar_Click(object sender, EventArgs e) {
            try
            {
                UsuarioActual = Validar.Login(txtUsuario.Text, txtPass.Text);
                if (UsuarioActual != null)
                {
                    if (UsuarioActual.Habilitado)
                    {
                        MessageBox.Show("Usted ha ingresado correctamente", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("El usuario no esta habilitado para ingresar", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña incorrectos", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Ha ocurrido un error, intente de nuevo mas tarde.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkOlvidaPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            MessageBox.Show("Es usted un usuario muy descuidado, haga memoria!", "Olvide mi contraseña", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
