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
          
        public void SqlInsert(String sql) 
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
        public void SqlUpdate(String sql) 
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
        public void SqlDelete(String sql) 
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
        public void SqlMostrarUmPassageiroAtivo(String sql) 
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*cpf*/
                        Console.WriteLine(" CPF: {0}", reader.GetString(0));
                        /*nome*/
                        Console.WriteLine(" Nome: {0}", reader.GetString(1));
                        /*data_nasc*/
                        Console.WriteLine(" Data de Nascimento: {0}", reader.GetDateTime(2).ToShortDateString());
                        /*sexo*/
                        Console.WriteLine(" Sexo: {0}", reader.GetString(3));
                        /*situacao*/
                        Console.WriteLine(" Situação: {0}", reader.GetString(4));
                        /*data_cadastro*/
                        Console.WriteLine(" Cadastrado des de {0}", reader.GetDateTime(5).ToShortDateString());
                        /*data_ultima_compra*/
                        Console.WriteLine(" Última compra feita em {0}", reader.GetDateTime(6));
                    }
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SqlMostrarUmaCompanhiaAtiva(String sql)  
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*cnpj*/
                        Console.WriteLine(" CNPJ: {0}", reader.GetString(0));
                        /*razaoSocial*/
                        Console.WriteLine(" Razão Social: {0}", reader.GetString(1));
                        /*dataAbertura*/
                        Console.WriteLine(" Existente des de: {0}", reader.GetDateTime(2).ToShortDateString());
                        /*dataCadastro*/
                        Console.WriteLine(" Cadastro Realizado em: {0}", reader.GetDateTime(3));
                        /*ultimoVoo*/
                        Console.WriteLine(" Último Voo Realizado em: {0}", reader.GetDateTime(4));
                        /*situacao*/
                        Console.WriteLine(" Situação do Cadastro: {0}", reader.GetString(5));
                    }
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SqlMostrarVoosDeUmaCompanhia(String sql)  
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*RazaoSocial*/
                        Console.WriteLine(" Empresa: {0}", reader.GetString(0));
                        /*ID_VOO*/
                        Console.WriteLine(" Voo: {0}", reader.GetString(1));
                        /*SituacaoVoo*/
                        Console.WriteLine(" {0}", reader.GetString(2));
                        /*DataPrevistaParaoVoo*/
                        Console.WriteLine(" Data Prevista: {0}", reader.GetDateTime(3));
                        /*AssentosOcupados*/
                        Console.WriteLine(" Quantidade de Assentos ocupados: {0}", reader.GetInt32(4));
                        /*INSCRICAO*/
                        Console.WriteLine("__________________________________________________________________________________________");
                    }
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public bool SqlVerificaCompanhiaAtiva(String sql)
        {
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
        public bool SqlVerificaAeronaveAtiva(String sql)
        {
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
        public bool SqlVerificaListaRestritos(String sql)
        {
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
        public int SqlPegarCapacidadeAeronave(string inscricao) // pega a capacidade da aeronave para determinar quantas passagens seram geradas
        {
            string sql = "SELECT Capacidade FROM Aeronave WHERE INSCRICAO = '" + inscricao + "';";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                int capacidade = 0;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        capacidade = reader.GetInt32(0);
                        connection.Close();
                        return capacidade;
                    }
                }
                return capacidade;
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
        public string SqlPegarInscricaoAeronave(string idVoo) // envia o id do voo e recebe a inscricao da aeronave envolvida nele
        {
            string sql = "SELECT INSCRICAO FROM Voo WHERE ID_VOO = '" + idVoo + "';";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                string inscricao = "";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inscricao = reader.GetString(0);
                        connection.Close();
                        return inscricao;
                    }
                }
                return inscricao;
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
        public int SqlPegarPassagensLivres(string idVoo)
        {
            
            try
            {
                string sql = $"SELECT COUNT (Situacao) FROM Passagem WHERE ID_VOO = '{idVoo}';";
                int passLivres = 0;


                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    passLivres = (int)cmd.ExecuteScalar();
                    connection.Close();
                    return passLivres;
                }
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
        public void SqlMostrarVoosDisponiveis(String sql)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*RazaoSocial*/
                        Console.WriteLine(" Empresa: {0}", reader.GetString(0));
                        /*ID_VOO*/
                        Console.WriteLine(" Voo: {0}", reader.GetString(1));
                        /*SituacaoVoo*/
                        Console.WriteLine(" {0}", reader.GetString(2));
                        /*DataPrevistaParaoVoo*/
                        Console.WriteLine(" Data Prevista: {0}", reader.GetDateTime(3));
                        /*AssentosOcupados*/
                        Console.WriteLine(" Quantidade de Assentos ocupados: {0}", reader.GetInt32(4));
                        /*INSCRICAO*/
                        Console.WriteLine("__________________________________________________________________________________________");
                    }
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SqlMostrarDetahesVoo(String sql)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*RazaoSocial*/
                        Console.WriteLine(" Empresa: {0}", reader.GetString(0));
                        /*ID_VOO*/
                        Console.WriteLine(" Voo: {0}", reader.GetString(1));
                        /*SituacaoVoo*/
                        Console.WriteLine(" {0}", reader.GetString(2));
                        /*DataPrevistaParaoVoo*/
                        Console.WriteLine(" Data Prevista: {0}", reader.GetDateTime(3));
                        /*AssentosOcupados*/
                        Console.WriteLine(" Quantidade de Assentos ocupados: {0}", reader.GetInt32(4));
                        /*INSCRICAO*/
                        Console.WriteLine("__________________________________________________________________________________________");
                    }
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SqlMostrarRestritos(String sql)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*cpf*/
                        Console.WriteLine(" CPF RESTRITO: {0}", reader.GetString(0));
                        Console.WriteLine("----------------------------------");
                    }
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
