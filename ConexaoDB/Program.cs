using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            // cria novo objeto de conexao
            // 1 parametro = Local do banco de dados
            // 2 parametro = Execução do usuário ateuticado com windows // Então só precisa informarn isso, sem usuário e senha
            // 3 parametro = nome do banco de dados
            SqlConnection con = new SqlConnection(@"data source = DESKTOP-K508VPM; Integrated Security = SSPI; Initial Catalog = ExemploDB;");

            // Cria conexao
            con.Open();

            // String que guarda a consulta do banco de dados
            String querySelect = "SELECT * FROM Usuario";

            // armazena as informações da consulta com a classe SqlCommand
            // 1 parametro = variavel do comando
            // 2 parametro = variavel de conexao
            SqlCommand cmdQuerySelect = new SqlCommand(querySelect, con);

            // execucao do comando com as informaçoes armazenadas em cmdQuerySelect
            // quando é select se usa o a funcao executeReader porque retorna dados
            // quando for update, insert, delete e etc se usa o ExecuteNonQuery
            // os dados da consulta ficarao armazenados na variavel dados em forma de array
            SqlDataReader dados = cmdQuerySelect.ExecuteReader();

            // enquanto houver dados na variavel ele fará alguma coisa
            // ou seja o 'while' irá percorrer todo o array
            while (dados.Read())
            {
                Console.WriteLine("ID: {0}, Nome: {1}, Cargo: {2}, Data: {3}", dados["usuarioId"], dados["nome"], dados["cargo"], dados["data"]);
            }

            con.Close();

            con.Open();
            Console.WriteLine("");
            String queryUpdate = "UPDATE Usuario SET nome = 'Nome2' WHERE usuarioId = 1";
            SqlCommand cmdQueryUpdate = new SqlCommand( queryUpdate, con);
            cmdQueryUpdate.ExecuteNonQuery();
            con.Close();

            con.Open();
            // String que guarda a consulta do banco de dados
            String querySelect2 = "SELECT * FROM Usuario";

            // armazena as informações da consulta com a classe SqlCommand
            // 1 parametro = variavel do comando
            // 2 parametro = variavel de conexao
            SqlCommand cmdQuerySelect2 = new SqlCommand(querySelect2, con);

            // execucao do comando com as informaçoes armazenadas em cmdQuerySelect
            // quando é select se usa o a funcao executeReader porque retorna dados
            // quando for update, insert, delete e etc se usa o ExecuteNonQuery
            // os dados da consulta ficarao armazenados na variavel dados em forma de array
            SqlDataReader dados2 = cmdQuerySelect2.ExecuteReader();

            // enquanto houver dados na variavel ele fará alguma coisa
            // ou seja o 'while' irá percorrer todo o array
            while (dados2.Read())
            {
                Console.WriteLine("ID: {0}, Nome: {1}, Cargo: {2}, Data: {3}", dados2["usuarioId"], dados2["nome"], dados2["cargo"], dados2["data"]);
            }

            con.Close();

            con.Open();
            Console.WriteLine("");
            String queryUpdate2 = "UPDATE Usuario SET nome = 'Vitor Hugo' WHERE usuarioId = 1";
            SqlCommand cmdQueryUpdate2 = new SqlCommand(queryUpdate2, con);
            cmdQueryUpdate2.ExecuteNonQuery();
            con.Close();

            con.Open();
            Console.WriteLine("");
            String queryDelete = "DELETE FROM Usuario WHERE usuarioId = 2";
            SqlCommand cmdQueryDelete = new SqlCommand(queryDelete, con);
            cmdQueryDelete.ExecuteNonQuery();
            con.Close();

            Console.WriteLine("");
            Console.WriteLine("Digite o nome:");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o cargo: ");
            string cargo = Console.ReadLine();

            // aqui é feito um insert com a nova classe de conexão
            String queryInsert = string.Format("INSERT INTO Usuario( nome, cargo, data) VALUES( '{0}', '{1}', '26/05/2018')", nome, cargo);
            var DB = new DB();
            DB.comandoSemRetorno(queryInsert);

            // insert com a nova forma
            Console.WriteLine("");
            Console.WriteLine("Digite o nome:");
            string nome2 = Console.ReadLine();
            Console.WriteLine("Digite o cargo: ");
            string cargo2 = Console.ReadLine();
            Console.WriteLine("Digite a Data: ");
            string data2 = Console.ReadLine();
            Usuarios usuarioComando = new Usuarios
            {
                nome = nome2,
                cargo = cargo2,
                data = DateTime.Parse(data2)
            };
            new UsuarioAplicacao().salvarComando( usuarioComando);

            // update com nova forma
            Console.WriteLine("");
            Console.Write("Alteração De Dados");
            Console.WriteLine("Informe o id do Usuario que deseja alterar: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o nome:");
            string nome3 = Console.ReadLine();
            Console.WriteLine("Digite o cargo: ");
            string cargo3 = Console.ReadLine();
            Console.WriteLine("Digite a Data: ");
            string data3 = Console.ReadLine();
            Usuarios usuarioComando2 = new Usuarios
            {
                usuarioId = id,
                nome = nome3,
                cargo = cargo3,
                data = DateTime.Parse(data3)
            };
            new UsuarioAplicacao().salvarComando(usuarioComando2);


            Console.ReadKey();

        }
    }
}
