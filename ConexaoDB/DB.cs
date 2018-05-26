using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ConexaoDB
{
    class DB : IDisposable
    {
        private readonly SqlConnection con;

        public DB()
        {
            this.con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexaoDB"].ConnectionString);
            this.con.Open();
        }

        // metodo que executa uma query simples
        public void comandoSemRetorno(string rowQuery) // aqui é passado o comando por parametro
        {
            var comando = new SqlCommand // aqui é instanciado a variável de comando
            {
                CommandText = rowQuery, // o primeiro parametro é o comando
                CommandType = CommandType.Text, // o segundo é o tipo de comando
                Connection = this.con // o terceiro é a conexao
            };
            comando.ExecuteNonQuery();
        }

        // metodo que executa consultas com retorno tipo um select
        public SqlDataReader comandoComRetorno(string rowQuery)
        {
            var cmdComando = new SqlCommand( rowQuery, this.con);
            return cmdComando.ExecuteReader();
        }

        //Toda vez que a classe for chamada ele irá executar o que está dentro desse metodo por ultimo, para isso é implementado o IDisposable
        public void Dispose()
        {
            if (this.con.State == ConnectionState.Open)
            {
                this.con.Close();
            }
        }
    }
}