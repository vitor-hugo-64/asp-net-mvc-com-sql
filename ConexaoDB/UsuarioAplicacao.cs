using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoDB
{
    class UsuarioAplicacao
    {
        private DB db;

        private void insertComando( Usuarios usuarios)
        {
            var query = "";
            query += "INSERT INTO Usuario( nome, cargo, data)";
            query += string.Format(" VALUES( '{0}', '{1}', '{2}')", usuarios.nome, usuarios.cargo, usuarios.data);
            using (db = new DB())
            {
                db.comandoSemRetorno(query);
            }
        }

        private void alterarComando(Usuarios usuarios)
        {
            var query = "";
            query += "UPDATE Usuario SET ";
            query += string.Format("nome = '{0}', ", usuarios.nome);
            query += string.Format("cargo = '{0}', ", usuarios.cargo);
            query += string.Format("data = '{0}' ", usuarios.data);
            query += string.Format("WHERE usuarioId = {0} ", usuarios.usuarioId);
            using (db = new DB())
            {
                db.comandoSemRetorno(query);
            }
        }

        // Essa linha verifica se é pra fazer um insert ou update
        // o id do usuário só é necessário em casos de update
        // portanto o if irá analisar se é necessário fazer update baseado no valor do id
        // se o id for menor que 0 então ele fará um insert
        public void salvarComando(Usuarios usuarios)
        {
            if (usuarios.usuarioId > 0)
            {
                this.alterarComando(usuarios);
            } else
            {
                this.insertComando(usuarios);
            }
        }

        public void deletarComando(int id)
        {
            using (db = new DB())
            {
                var query = string.Format("DELETE FROM Usuario WHERE usuarioId = {0}", id);
                db.comandoSemRetorno(query);
            }
        }

        public List<Usuarios> comandoListarTodos()
        {
            using (db = new DB())
            {
                var query = "SELECT * FROM Usuario";
                var dados =  db.comandoComRetorno(query);
                return RenderEmLista(dados);
            }
        }

        // função que irá retornar todos os registros do banco em forma de array
        public List<Usuarios> RenderEmLista(SqlDataReader reader) // função que retorna uma lista
        {
            var usuarios = new List<Usuarios>(); // aqui é criado uma variável de lista
            while (reader.Read()) // enquanto existir registros o while executara
            {
                var tempoObjeto = new Usuarios() // os dados do loop serão armazenados dentro do objeto do tipo Usuario
                {
                    usuarioId = int.Parse(reader["usuarioId"].ToString()),
                    nome = reader["nome"].ToString(),
                    cargo = reader["cargo"].ToString(),
                    data = DateTime.Parse(reader["data"].ToString())
                };

                // mais tarde esse objeto será armazenado dentro da lista
                usuarios.Add(tempoObjeto);
            }

            reader.Close(); // a conexao será fechada
            return usuarios; // retornara a lista
        }

    }
}
