using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using Data.Database;

namespace Business.logic {
    public class EspecialidadLogic:BusinessLogic {
        EspecialidadAdapter especialidadData;
        public EspecialidadLogic() {
            especialidadData = new EspecialidadAdapter();
        }

        EspecialidadAdapter EspecialidadData {
            get { return especialidadData; }
            set { especialidadData = value; }
        }

        public Especialidad getOne(int id) {
            try {
                return especialidadData.GetOne(id);
            }
            catch (Exception exc) {
                throw;
            }
        }
        public List<Especialidad> getAll() {
            try {
                return especialidadData.GetAll();
            }
            catch (Exception exc) {
                throw;
            }
        }
        public void save(Especialidad esp) {
            try {
                especialidadData.Save(esp);
            }
            catch (Exception exc) {
                throw;
            }
        }
        public void delete(int id) {
            try {
                especialidadData.Delete(id);
            }
            catch (Exception exc) {
                throw;
            }
        }
    }
}
