using Business.Entities;
using Data.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.logic
{
    public class AlumnoInscripcionLogic
    {
        AlumnoInscripcionAdapter insData;
        public AlumnoInscripcionLogic()
        {
            insData = new AlumnoInscripcionAdapter();
        }

        AlumnoInscripcionAdapter InsData
        {
            get { return insData; }
            set { insData = value; }
        }

        public AlumnoInscripcion getOne(int id)
        {
            try
            {
                return insData.GetOne(id);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
        public List<AlumnoInscripcion> getAll()
        {
            try
            {
                return insData.GetAll();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public void save(AlumnoInscripcion ins)
        {
            try
            {
                insData.Save(ins);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
        public void delete(int id)
        {
            try
            {
                insData.Delete(id);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
    }
}
