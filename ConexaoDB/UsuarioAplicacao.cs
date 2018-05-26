using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoDB
{
    class UsuarioAplicacao
    {
        private DB db;

        public void insertComando( Usuarios usuarios)
        {
            var query = "";
            query += "INSERT INTO Usuario( nome, cargo, data)";
            query += string.Format(" VALUES( '{0}', '{1}', '{2}')", usuarios.nome, usuarios.cargo, usuarios.data);
            using (db = new DB())
            {
                db.comandoSemRetorno(query);
            }
        }

    }
}
