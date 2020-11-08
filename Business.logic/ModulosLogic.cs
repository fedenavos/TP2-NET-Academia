using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using Data.Database;

namespace Business.logic
{
    public class ModulosLogic
    {
        ModuloAdapter moduloAdapter;

        public ModulosLogic()
        {
            moduloAdapter = new ModuloAdapter();
        }

        public Modulo GetOne(int id)
        {
            try
            {
                return moduloAdapter.GetOne(id);
            }
            catch
            {
                throw;
            }
        }

        public List<Modulo> GetAll()
        {
            try
            {
                return moduloAdapter.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public void AgregarModulo(Modulo modulo)
        {
            try
            {
                moduloAdapter.Insert(modulo);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void EliminarModulo(int id)
        {
            try
            {
                moduloAdapter.Delete(id);
            }
            catch
            {
                throw;
            }
        }

        public void ModificarModulo(Modulo modulo)
        {
            try
            {
                moduloAdapter.Update(modulo);
            }
            catch
            {
                throw;
            }
        }
    }
}
