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
    public partial class InscripcionDesktop : ApplicationForm
    {
        private AlumnoInscripcion insActual;

        public InscripcionDesktop()
        {
            InitializeComponent();
        }

        public InscripcionDesktop(ModoForm modo) : this()
        {
            this.Modo = modo;
        }

        public AlumnoInscripcion InscripcionActual { get => insActual; set => insActual = value; }

        public InscripcionDesktop(int idIns, ModoForm modo) : this()
        {
            this.Modo = modo;
            AlumnoInscripcionLogic insLogic = new AlumnoInscripcionLogic();
            InscripcionActual = insLogic.getOne(idIns);

            MapearDeDatos();
            this.Modo = modo;
        }

        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                InscripcionActual = new AlumnoInscripcion();
            }

            InscripcionActual.IdAlumno = int.Parse(txtIdAlumno.Text);
            InscripcionActual.IdCurso = int.Parse(txtIdCurso.Text);
            InscripcionActual.Nota = int.Parse(txtNota.Text);
            InscripcionActual.Condicion = txtCondicion.Text;

            InscripcionActual.State = (Modo == ModoForm.Alta ? BusinessEntity.States.New : BusinessEntity.States.Modified);
        }
        public override void MapearDeDatos()
        {
            txtCondicion.Text = InscripcionActual.Condicion;
            txtIdAlumno.Text = InscripcionActual.IdAlumno.ToString();
            txtIdCurso.Text = InscripcionActual.IdCurso.ToString();
            txtNota.Text = InscripcionActual.Nota.ToString();
            txtId.Text = InscripcionActual.Id.ToString();

            switch (Modo)
            {
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

        public override void GuardarCambios()
        {
            MapearADatos();
            AlumnoInscripcionLogic insLogic = new AlumnoInscripcionLogic();
            try
            {
                insLogic.save(InscripcionActual);
            }
            catch (Exception exc)
            {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }

        public override bool Validar()
        {
            return txtCondicion.TextLength > 0 && txtIdAlumno.TextLength > 0 && txtIdCurso.TextLength > 0;

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                //Modificacion o alta: validamos los datos y despues guardamos
                if (Validar())
                {
                    GuardarCambios();
                    Notificar("Operacion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Notificar("Alguna de sus entradas no es valida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //Baja: no validamos ni guardamos nada, simplemente borramos
                new AlumnoInscripcionLogic().delete(InscripcionActual.Id);
                Notificar("Operacion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
