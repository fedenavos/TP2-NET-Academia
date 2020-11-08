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
    public partial class CursoDesktop : ApplicationForm
    {
        private Curso curActual;
        private List<Comision> comisiones;
        private List<Materia> materias;

        public CursoDesktop()
        {
            InitializeComponent();
            ComisionesLogic comisionesLogic = new ComisionesLogic();
            MateriaLogic materiaLogic = new MateriaLogic();
            
            comisiones = comisionesLogic.getAll();
            foreach (Comision com in comisiones)
            {
                cbComision.Items.Add(com);
            }
            materias = materiaLogic.getAll();
            foreach (Materia mat in materias)
            {
                cbMateria.Items.Add(mat);
            }
        }

        public CursoDesktop(ModoForm modo) : this()
        {
            this.Modo = modo;
        }

        public Curso CursoActual { get => curActual; set => curActual = value; }

        public CursoDesktop(int idCur, ModoForm modo) : this()
        {
            this.Modo = modo;
            CursoLogic curLogic = new CursoLogic();
            CursoActual = curLogic.getOne(idCur);

            MapearDeDatos();
            this.Modo = modo;
        }

        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                CursoActual = new Curso();
            }

            CursoActual.Cupo = int.Parse(txtCupo.Text);
            CursoActual.AnioCalendario = int.Parse(txtAnioCal.Text);
            CursoActual.IdComision = ((Comision)cbComision.SelectedItem).Id;
            CursoActual.IdMateria = ((Materia)cbMateria.SelectedItem).Id;

            CursoActual.State = (Modo == ModoForm.Alta ? BusinessEntity.States.New : BusinessEntity.States.Modified);
        }
        public override void MapearDeDatos()
        {
            txtCupo.Text = CursoActual.Cupo.ToString();
            txtAnioCal.Text = CursoActual.AnioCalendario.ToString();
            cbComision.SelectedIndex = cbComision.FindStringExact((comisiones.Where(c => c.Id == CursoActual.IdComision)).ToString());
            cbMateria.SelectedIndex = cbMateria.FindStringExact((materias.Where(m => m.Id == CursoActual.IdMateria)).ToString());

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
            CursoLogic curLogic = new CursoLogic();
            try
            {
                curLogic.save(CursoActual);
            }
            catch (Exception exc)
            {
                DialogResult result = MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
            }
        }

        public override bool Validar()
        {
            return txtCupo.TextLength > 0 && txtAnioCal.TextLength > 0 && cbComision.SelectedIndex >= 0 && cbMateria.SelectedIndex >= 0;

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
                new CursoLogic().delete(CursoActual.Id);
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
