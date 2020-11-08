using System;
using System.Collections.Generic;
using System.Text;
using Data.Database;
using Business.Entities;

namespace Business.logic
{
    public class PersonaLogic:BusinessLogic
    {
        PersonaAdapter personaData;

        public PersonaLogic()
        {
            personaData = new PersonaAdapter();
        }

        PersonaAdapter PersonaData
        {
            get { return personaData; }
            set { personaData = value; }
        }

        public Persona getOne(int id)
        {
            try
            {
                return personaData.GetOne(id);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
        public List<Persona> getAll()
        {
            try
            {
                return personaData.GetAll();
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        public void save(Persona persona)
        {
            try
            {
                personaData.Save(persona);
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
                personaData.Delete(id);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
    }
}
