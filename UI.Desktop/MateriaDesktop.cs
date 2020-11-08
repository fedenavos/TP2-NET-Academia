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
    public partial class MateriaDesktop : ApplicationForm
    {
        private Materia matActual;
        private List<Plan> planes;
        public MateriaDesktop()
        {
            PlanesLogic planesLogic = new PlanesLogic();
            InitializeComponent();
            planes = planesLogic.getAll();
            foreach (Plan plan in planes)
            {
                cbPlan.Items.Add(plan);
            }
        }

        public MateriaDesktop(ModoForm modo) : this()
        {
            this.Modo = modo;
        }

        public Materia MateriaActual { get => matActual; set => matActual = value; }

        public MateriaDesktop(int idMat, ModoForm modo) : this()
        {
            this.Modo = modo;
            MateriaLogic matLogic = new MateriaLogic();
            MateriaActual = matLogic.getOne(idMat);

            MapearDeDatos();
            this.Modo = modo;
        }

        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                MateriaActual = new Materia();
            }

            MateriaActual.Descripcion = txtDesc.Text;
            MateriaActual.HsSemanales = int.Parse(txtHsSemanales.Text);
            MateriaActual.HsTotales = int.Parse(txtHsTotales.Text);
            MateriaActual.IdPlan = ((Plan)cbPlan.SelectedItem).Id;

            MateriaActual.State = (Modo == ModoForm.Alta ? BusinessEntity.States.New : BusinessEntity.States.Modified);
        }
        public override void MapearDeDatos()
        {
            //PlanesLogic planesLogic = new PlanesLogic();
            base.MapearDeDatos();
            txtDesc.Text = MateriaActual.Descripcion;
            txtHsTotales.Text = MateriaActual.HsTotales.ToString();
            txtHsSemanales.Text = MateriaActual.HsSemanales.ToString();
            cbPlan.SelectedIndex = cbPlan.FindStringExact((planes.Where(p => p.Id == MateriaActual.IdPlan)).ToString());

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
            MateriaLogic matLogic = new MateriaLogic();
            try
            {
                matLogic.save(MateriaActual);
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
                new MateriaLogic().delete(MateriaActual.Id);
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
