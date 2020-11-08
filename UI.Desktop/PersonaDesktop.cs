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
    public partial class PersonaDesktop : ApplicationForm
    {
        private Persona personaActual;

        public PersonaDesktop()
        {
            InitializeComponent();
        }

        public PersonaDesktop(ModoForm modo) : this()
        {
            this.Modo = modo;
        }

        public PersonaDesktop(int idPersona, ModoForm modo) : this()
        {
            this.Modo = modo;
            PersonaLogic personaLogic = new PersonaLogic();
            PersonaActual = personaLogic.getOne(idPersona);

            MapearDeDatos();
        }

        public Persona PersonaActual { get => personaActual; set => personaActual = value; }

        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                PersonaActual = new Persona();
            }
            PersonaActual.Legajo = int.Parse(txtLegajo.Text);
            PersonaActual.Nombre = txtNombre.Text;
            PersonaActual.Apellido = txtApellido.Text;
            PersonaActual.Email = txtEmail.Text;
            PersonaActual.Telefono = txtTelefono.Text;
            PersonaActual.Direccion = txtDireccion.Text;
            PersonaActual.FechaNacimiento = txtFechaNac.SelectionRange.Start;
            PersonaActual.IdPlan = int.Parse(txtIdPlan.Text);

            if (Modo == ModoForm.Alta)
            {
                PersonaActual.State = BusinessEntity.States.New;
            }
            else
            {
                PersonaActual.State = BusinessEntity.States.Modified;
            }
            PersonaActual.State = (Modo == ModoForm.Alta ? BusinessEntity.States.New : BusinessEntity.States.Modified);
        }
        public override void MapearDeDatos()
        {
            base.MapearDeDatos();
            txtNombre.Text = PersonaActual.Nombre;
            txtApellido.Text = PersonaActual.Apellido;
            txtId.Text = PersonaActual.Id.ToString();
            txtEmail.Text = PersonaActual.Email;
            txtLegajo.Text = PersonaActual.Legajo.ToString();
            txtTelefono.Text = PersonaActual.Telefono;
            txtDireccion.Text = PersonaActual.Direccion;
            txtFechaNac.SelectionRange.Start = PersonaActual.FechaNacimiento;
            txtIdPlan.Text = PersonaActual.IdPlan.ToString();


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
            PersonaLogic personaLogic = new PersonaLogic();
            try
            {
                personaLogic.save(PersonaActual);
            }
            catch (Exception exc)
            {
                DialogResult result = MessageBox.Show(exc.Message, "Error: " + exc.ToString(), MessageBoxButtons.OK);
            }
        }
        public override bool Validar()
        {
            //String.IsNullOrWhiteSpace(txtUsuario.Text)
            if (txtNombre.TextLength > 0 && txtApellido.TextLength > 0 && txtEmail.TextLength > 0)
            {
                return true;
            }

            return false;
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
                new PersonaLogic().delete(PersonaActual.Id);
                Notificar("Operacion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
