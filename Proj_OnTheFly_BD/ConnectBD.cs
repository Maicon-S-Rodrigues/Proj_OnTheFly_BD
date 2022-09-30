using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_OnTheFly_BD
{
    internal class ConnectBD
    {
        private static string PathWay = "Data Source=localhost; Initial Catalog=OnTheFly; User ID=sa; password=Sol@2905;";

        private static SqlConnection connection = new SqlConnection(PathWay);

        public ConnectBD() { }

        public void SqlInsert(String sql) // ok
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                connection.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
            }
        }

        public void SqlUpdate(String sql) // ok
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
            }
        }

        public bool SqlLoginPassageiro(String sql) // pesquisa se o cpf existe, se existir retorna true
        {
            string encontrado = "";
            bool acesso = false;

            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        encontrado = reader.GetString(0);
                    }
                }
                if (encontrado.Equals(null) || encontrado.Equals(""))
                {
                    Console.WriteLine("\nCPF Não Cadastrado!");
                    Utility utility = new Utility();
                    utility.Pausa();
                }
                else
                {
                    acesso = true;
                }
                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
            }
            return acesso;
        }
        public String SqlPesquisarExistente(String sql)
        {

            String recebe = "";

            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                    }
                }
                if (recebe.Equals(null) || recebe.Equals(""))
                {
                    Console.WriteLine("\n\t*** Passageiro Não Localizado ****\n");
                }
                else
                {
                    Console.WriteLine("\n\t*** Passageiro Localizado ****\n");
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            /*cpf*/
                            Console.Write(" {0}", reader.GetString(0));
                            /*nome*/
                            Console.Write(" {0}", reader.GetString(1));
                            /*data_nasc*/
                            Console.Write(" {0}", reader.GetDateTime(2).ToShortDateString());
                            /*sexo*/
                            Console.Write(" {0}", reader.GetString(3));
                            /*situacao*/
                            Console.Write(" {0}", reader.GetString(4));
                            /*data_cadastro*/
                            Console.Write(" {0}", reader.GetDateTime(5));
                            /*data_ultima_compra*/
                            Console.Write(" {0}", reader.GetDateTime(6));
                        }
                    }
                }
                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return recebe;
        }

        public bool SqlVerificarDados(string dado, string campo, string tabela)
        {
            string sql = $"SELECT {campo} FROM {tabela} WHERE {campo} = '{dado}'";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        connection.Close();
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
                Utility utility = new Utility();
                utility.Pausa();
                return false;
            }
        }

        public DateTime SqlPegarDataNascimento(string cpf) // para calcular idadade passageiro e proibir venda para -18
        {
            string sql = "select Data_Nascimento from Passageiro where CPF = '" + cpf + "' and Situacao = 'ATIVO';";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                DateTime datanascimento = System.DateTime.Now;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        datanascimento = reader.GetDateTime(0);
                        connection.Close();
                        return datanascimento;
                    }
                }
                return datanascimento;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
                Utility utility = new Utility();
                utility.Pausa();
                throw;
            }
        }

    }
}
