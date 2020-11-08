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
    public partial class PlanDesktop : ApplicationForm
    {
        private Plan planActual;
        private List<Especialidad> especialidades;
        public PlanDesktop()
        {
            EspecialidadLogic especialidadLogic = new EspecialidadLogic();
            InitializeComponent();
            especialidades = especialidadLogic.getAll();
            foreach (Especialidad esp in especialidades)
            {
                cbEspecialidad.Items.Add(esp);
            }    
        }

        public PlanDesktop(ModoForm modo) : this()
        {
            this.Modo = modo;
        }

        public Plan PlanActual { get => planActual; set => planActual = value; }

        public PlanDesktop(int idPlan, ModoForm modo) : this()
        {
            this.Modo = modo;
            PlanesLogic planLogic = new PlanesLogic();
            PlanActual = planLogic.getOne(idPlan);

            MapearDeDatos();
            this.Modo = modo;
        }

        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                PlanActual = new Plan();
            }
            PlanActual.Descripcion = txtDesc.Text;
            PlanActual.IdEspecialidad = ((Especialidad)cbEspecialidad.SelectedItem).Id;

            if (Modo == ModoForm.Alta)
            {
                PlanActual.State = BusinessEntity.States.New;
            }
            else
            {
                PlanActual.State = BusinessEntity.States.Modified;
            }
            PlanActual.State = (Modo == ModoForm.Alta ? BusinessEntity.States.New : BusinessEntity.States.Modified);
        }
        public override void MapearDeDatos()
        {
            //EspecialidadLogic especialidadLogic = new EspecialidadLogic();
            base.MapearDeDatos();
            txtDesc.Text = PlanActual.Descripcion;
            //cbEspecialidad.SelectedItem = (Especialidad)especialidadLogic.getOne(PlanActual.IdEspecialidad);
            cbEspecialidad.SelectedIndex = cbEspecialidad.FindStringExact((especialidades.Where(e => e.Id == PlanActual.IdEspecialidad)).ToString());
                //cbEspecialidad.Items[cbEspecialidad.Items.IndexOf((especialidades.Where(e => e.Id == PlanActual.IdEspecialidad)).ToString())];

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
            PlanesLogic planesLogic = new PlanesLogic();
            try
            {
                planesLogic.save(PlanActual);
            }
            catch (Exception exc)
            {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }

        public override bool Validar()
        {
            return txtDesc.TextLength > 0;

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
                new PlanesLogic().delete(PlanActual.Id);
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
