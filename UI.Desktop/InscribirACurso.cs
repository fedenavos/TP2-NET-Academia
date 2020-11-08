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
    public partial class InscribirACurso : Form
    {
        private Usuario usuarioActual;
        private List<Curso> cursos = new List<Curso>();

        public InscribirACurso()
        {
            InitializeComponent();
        }

        public InscribirACurso(Usuario usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;

            cursos = new CursoLogic().getAll();
            cursos.ForEach(c => cbCursos.Items.Add(c));
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(cbCursos.SelectedItem != null)
            {

                try
                {
                    AlumnoInscripcion inscripcion = new AlumnoInscripcion();
                    inscripcion.Condicion = AlumnoInscripcion.Condiciones.Cursando.ToString();
                    inscripcion.IdAlumno = usuarioActual.Persona.Id;
                    inscripcion.IdCurso = ((Curso)cbCursos.SelectedItem).Id;
                    inscripcion.State = BusinessEntity.States.New;

                    AlumnoInscripcionLogic inscripcionLogic = new AlumnoInscripcionLogic();
                    inscripcionLogic.save(inscripcion);

                    MessageBox.Show("Tu inscripcion se genero correctamente", "Inscripcion confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                catch(Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error al inscribirse", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show("Ningun curso fue seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
