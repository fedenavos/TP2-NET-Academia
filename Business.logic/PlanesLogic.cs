using System;
using System.Collections.Generic;
using System.Text;
using Data.Database;
using Business.Entities;

namespace Business.logic
{
    public class PlanesLogic
    {
        PlanAdapter planData;
        public PlanesLogic()
        {
            planData = new PlanAdapter();
        }

        PlanAdapter PlanData
        {
            get { return planData; }
            set { planData = value; }
        }

        public Plan getOne(int id)
        {
            try
            {
                return planData.GetOne(id);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
        public List<Plan> getAll()
        {
            try
            {
                return planData.GetAll();
            }
            catch (Exception exc)
            {
                throw;
            }
        }
        public void save(Plan plan)
        {
            try
            {
                planData.Save(plan);
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
                planData.Delete(id);
            }
            catch (Exception exc)
            {
                throw;
            }
        }
    }

}

