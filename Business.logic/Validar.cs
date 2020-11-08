using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Business.Entities;

namespace Business.logic {
    public class Validar {

        public static bool IsMailValido(String mail) {
            return Regex.IsMatch(mail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9]
                                        (?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public static Usuario Login(String user, String pass)
        {
            UsuarioLogic usuarioLogic = new UsuarioLogic();
            List<Usuario> usuarios = usuarioLogic.getAll();
            foreach(Usuario usuario in usuarios)
            {
                if(usuario.NombreUsuario == user && usuario.Clave == pass)
                {
                    return usuario;
                }
            }
            return null;
        }
    }

}
