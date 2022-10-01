using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_OnTheFly_BD
{
    internal class Passageiro
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public DateTime DataUltimaCompra { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Situacao { get; set; }

        ConnectBD connection = new();
        static Utility utility = new();

        public Passageiro()
        {

        }
        public Passageiro(string cpf, string nome, DateTime dataNascimento, char sexo, DateTime UltimaCompra, DateTime DataCadastro, string Situacao)
        {
            this.Cpf = cpf;
            this.Nome = nome;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.DataUltimaCompra = System.DateTime.Now;//Data do sistema
            this.DataCadastro = System.DateTime.Now;
            this.Situacao = Situacao; // Ativo,Inativo
        }

        public void CadastroPassageiro() // ok
        {
            Console.Clear();
            do
            {
                string strDataNascimento;
                try
                {
                    this.Nome = utility.ValidarEntrada("nome");
                    if (this.Nome == null) return;

                    this.Cpf = utility.ValidarEntrada("cpf");
                    if (this.Cpf == null) return;

                    strDataNascimento = utility.ValidarEntrada("datanascimento");
                    if (strDataNascimento == null) return;

                    this.DataNascimento = DateTime.Parse(strDataNascimento);

                    this.Sexo = char.Parse(utility.ValidarEntrada("sexo"));
                    if (this.Sexo.Equals(null)) return;

                    this.DataCadastro = System.DateTime.Now;
                    this.DataUltimaCompra = System.DateTime.Now;
                    this.Situacao = "ATIVO";

                    string sqlText = $"INSERT INTO Passageiro (CPF, Situacao, Sexo, Nome, Data_Cadastro, Data_Nascimento, Data_Ultima_Compra) VALUES ('{this.Cpf}', '{this.Situacao}', '{this.Sexo}', " +
                                     $"'{this.Nome}', '{this.DataCadastro}', '{this.DataNascimento}', '{this.DataUltimaCompra}');";

                    try
                    {
                        connection.SqlInsert(sqlText);
                        Console.WriteLine("\nPassageiro Cadastrado com Sucesso!");
                        utility.Pausa();
                        return;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\nHouve um erro ao cadastrar..." + e.Message);
                        utility.Pausa();
                        throw;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("\nHouve um erro ao cadastrar..." + e.Message);
                    utility.Pausa();
                    throw;
                }
            } while (true);
        }
        public void LoginPassageiro() // ok
        {
            do
            {
                try
                {
                    Console.Clear();

                    this.Cpf = utility.ValidarEntrada("cpfexiste");
                    if (this.Cpf == null) { return; }

                        EditarPassageiro(this.Cpf);                  
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nHouve um erro ao tentar Login..." + e.Message);
                    utility.Pausa();
                    throw;
                }

            } while (true);
        }
        public void EditarPassageiro(string cpfAtivo) // ok
        {
            do
            {
                int opc;
                string sqlUpdate = "";
                string sql = $"SELECT CPF, Nome, Data_Nascimento, Sexo, Situacao, Data_Cadastro, Data_Ultima_Compra from Passageiro Where CPF = '{cpfAtivo}';";

                Console.Clear();
                Console.WriteLine("\nEDTAR DADOS DE CADASTRO");
                Console.WriteLine("______________________________________________________");
                connection.SqlMostrarUmPassageiroAtivo(sql);
                Console.WriteLine("______________________________________________________");
                Console.WriteLine("\nEscolha qual Dado deseja Editar: ");
                Console.Write("\n 1 - Nome");
                Console.Write("\n 2 - Data de Nascimento");
                Console.Write("\n 3 - Sexo");
                Console.Write("\n 4 - Situação (Ativo / Inativo)");
                Console.Write("\n\n 0 - Sair");
                try
                {
                    opc = int.Parse(utility.ValidarEntrada("menu"));

                    switch (opc)
                    {
                        case 0:
                            return;


                        case 1: // nome
                            Console.Write("\n\nInforme o Novo Nome");
                            utility.Pausa();
                            string novoNome = utility.ValidarEntrada("nome");
                            if (novoNome == null) return; 
                           
                            sqlUpdate = $"UPDATE Passageiro SET Nome = '{novoNome}' WHERE CPF = '{cpfAtivo}';";

                            connection.SqlUpdate(sqlUpdate);

                            Console.WriteLine("\nNome Alterado com Sucesso!");
                            utility.Pausa();
                            break;

                        case 2: // data nascimento
                            Console.Write("\nInforme a Nova Data de Nascimento");
                            utility.Pausa();
                            string novaDataNascimento = utility.ValidarEntrada("datanascimento");
                            if (novaDataNascimento == null) return;

                            sqlUpdate = $"UPDATE Passageiro SET Data_Nascimento = '{novaDataNascimento}' WHERE CPF = '{cpfAtivo}';";

                            connection.SqlUpdate(sqlUpdate);

                            Console.WriteLine("\nData de Nascimento Alterada com Sucesso!");
                            utility.Pausa();
                            break;

                        case 3: // sexo
                            Console.Write("\nInforme o Novo Sexo");
                            utility.Pausa();
                            char novoSexo = char.Parse(utility.ValidarEntrada("sexo"));
                            if (novoSexo.Equals(null)) return;

                            sqlUpdate = $"UPDATE Passageiro SET Sexo = '{novoSexo}' WHERE CPF = '{cpfAtivo}';";

                            connection.SqlUpdate(sqlUpdate);

                            Console.WriteLine("\nSexo Alterado com Sucesso!");
                            utility.Pausa();
                            break;


                        case 4: // situacao
                            Console.Write("\nInforme a Nova Situação para o Cadastro");
                            utility.Pausa();
                            string novaSituacao = utility.ValidarEntrada("situacao");
                            if (novaSituacao.Equals(null)) return;

                            sqlUpdate = $"UPDATE Passageiro SET Situacao = '{novaSituacao}' WHERE CPF = '{cpfAtivo}';";

                            connection.SqlUpdate(sqlUpdate);

                            Console.WriteLine("\nSituação de Cadastro Alterada com Sucesso!");
                            utility.Pausa();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nHouve um erro ao editar..." + e.Message);
                    utility.Pausa();
                    throw;
                }

            } while (true);
        }

    }
}
