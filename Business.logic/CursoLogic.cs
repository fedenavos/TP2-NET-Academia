using Business.Entities;
using Data.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.logic
{
    public class CursoLogic
    {
        CursoAdapter curData;
        public CursoLogic()
        {
            curData = new CursoAdapter();
        }

        CursoAdapter CurData
        {
            get { return curData; }
            set { curData = value; }
        }

        public Curso getOne(int id)
        {
            try
            {
                return curData.GetOne(id);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
        public List<Curso> getAll()
        {
            try
            {
                return curData.GetAll();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public void save(Curso cur)
        {
            try
            {
                curData.Save(cur);
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
                curData.Delete(id);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
    }
}
