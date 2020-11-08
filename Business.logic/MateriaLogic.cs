using Business.Entities;
using Data.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.logic
{
    public class MateriaLogic
    {
        MateriaAdapter matData;
        public MateriaLogic()
        {
            matData = new MateriaAdapter();
        }

        MateriaAdapter MatData
        {
            get { return matData; }
            set { matData = value; }
        }

        public Materia getOne(int id)
        {
            try
            {
                return matData.GetOne(id);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
        public List<Materia> getAll()
        {
            try
            {
                return matData.GetAll();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public void save(Materia cur)
        {
            try
            {
                matData.Save(cur);
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
                matData.Delete(id);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
    }
}
