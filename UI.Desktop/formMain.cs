using Business.Entities;
using Business.logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace UI.Desktop {
    public partial class formMain : Form {

        private Usuario UsuarioActual;

        public formMain() {
            InitializeComponent();
        }

        private void SetMode()
        {
            switch (UsuarioActual.Persona.TipoPersona)
            {
                case Persona.TiposPersonas.admin:
                    mnuStripPrincipal.Enabled = true;
                    pnlAlumnos.Enabled = true;
                    pnlDocentes.Enabled = true;
                    break;
                case Persona.TiposPersonas.alumno:
                    pnlAlumnos.Enabled = true;
                    break;
                case Persona.TiposPersonas.docente:
                    pnlDocentes.Enabled = true;
                    break;
            }
        }

        private void mnuSalir_Click(object sender, EventArgs e) {
            this.Dispose();
        }

        private void formMain_shown(object sender, EventArgs e) {
            formLogin appLogin = new formLogin();
            if(appLogin.ShowDialog() != DialogResult.OK) {
                this.Dispose();
            }
            UsuarioActual = appLogin.UsuarioActual;
            lblBienvenida.Visible = true;
            lblBienvenida.Text = "Bienvenido " + UsuarioActual.NombreUsuario + "!";
            SetMode();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e) {
            Usuarios appUsuarios = new Usuarios();
            appUsuarios.ShowDialog();
        }

        private void especialidadesToolStripMenuItem_Click(object sender, EventArgs e) {
            Especialidades appEspecialidades = new Especialidades();
            appEspecialidades.ShowDialog();
        }

        private void planesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Planes appPlanes = new Planes();
            appPlanes.ShowDialog();
        }

        private void comisionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Comisiones appComisiones = new Comisiones();
            appComisiones.ShowDialog();
        }

        private void personasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Personas appPersonas = new Personas();
            appPersonas.ShowDialog();
        }

        private void modulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modulos appModulos = new Modulos();
            appModulos.ShowDialog();
        }

        private void cursosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cursos appCursos = new Cursos();
            appCursos.ShowDialog();
        }

        private void materiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Materias appMaterias = new Materias();
            appMaterias.ShowDialog();
        }

        private void inscripcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inscripciones appInscripciones = new Inscripciones();
            appInscripciones.ShowDialog();
        }

        private void btnInscribirCurso_Click(object sender, EventArgs e)
        {
            InscribirACurso appInscribirACurso = new InscribirACurso(UsuarioActual);
            appInscribirACurso.ShowDialog();
        }
    }
}
