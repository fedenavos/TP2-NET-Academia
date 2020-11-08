using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using Data.Database;
namespace Business.logic {
    public class UsuarioLogic:BusinessLogic {
        UsuarioAdapter usuarioData;

        public UsuarioLogic() {
            usuarioData = new UsuarioAdapter();
        }

        UsuarioAdapter UsuarioData {
            get { return usuarioData; }
            set { usuarioData = value; }
        }

        public Usuario getOne(int id) {
            try {
                return usuarioData.GetOne(id);
            }
            catch (Exception exc) {
                throw;
            }
        }
        public List<Usuario> getAll() {
            try {
                return usuarioData.GetAll();
            }
            catch(Exception exc) {
                throw;
            }
        }
        
        public void save(Usuario user) {
            try {
                usuarioData.Save(user);
            }
            catch(Exception exc) {
                throw;
            }
        }
        public void delete(int id) {
            try {
                usuarioData.Delete(id);
            }
            catch (Exception exc) {
                throw;
            }
        }
    }
}
