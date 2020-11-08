using Business.Entities;
using Data.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.logic {
    public class ComisionesLogic {
        ComisionAdapter comData;
        public ComisionesLogic() {
            comData = new ComisionAdapter();
        }

        ComisionAdapter ComData {
            get { return comData; }
            set { comData = value; }
        }

        public Comision getOne(int id) {
            try {
                return comData.GetOne(id);
            }
            catch (Exception exc) {
                throw;
            }
        }
        public List<Comision> getAll() {
            try {
                return comData.GetAll();
            }
            catch (Exception exc) {
                throw exc;
            }
        }
        public void save(Comision com) {
            try {
                comData.Save(com);
            }
            catch (Exception exc) {
                throw;
            }
        }
        public void delete(int id) {
            try {
                comData.Delete(id);
            }
            catch (Exception exc) {
                throw;
            }
        }
    }
}
