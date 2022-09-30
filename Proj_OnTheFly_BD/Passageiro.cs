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

        public void Cadastro() // ok
        {
            Console.Clear();
            do
            {
                string strDataNascimento;
                try
                {
                    Console.WriteLine("nome");
                    this.Nome = Console.ReadLine();
                    //this.Nome = utility.ValidarEntrada("nome");
                    if (this.Nome == null) return;

                    Console.WriteLine("cpf");
                    this.Cpf = utility.ValidarEntrada("cpf");
                    if (this.Cpf == null) return;

                    Console.WriteLine("datanasc");
                    strDataNascimento = Console.ReadLine();
                    //strDataNascimento = ValidarEntrada("datanascimento");
                    if (strDataNascimento == null) return;

                    this.DataNascimento = DateTime.Parse(strDataNascimento);

                    Console.WriteLine("sex");
                    this.Sexo = char.Parse(Console.ReadLine());
                    //this.Sexo = char.Parse(ValidarEntrada("sexo"));
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
                    string sql;
                    bool acesso = false;
                    Console.Clear();

                    Console.WriteLine("CPF LOGIN");
                    this.Cpf = Console.ReadLine();

                    if (connection.SqlVerificarDados(this.Cpf, "CPF", "Passageiro"))
                    {
                        Console.WriteLine("Cpf já Cadastrado");
                        Console.ReadKey();
                        return;
                    }

                    //this.Cpf = ValidarEntrada("cpfexiste");
                    if (this.Cpf == null) { return; }

                    sql = $"SELECT (CPF) from Passageiro Where CPF = '{this.Cpf}';";

                    acesso = connection.SqlLoginPassageiro(sql);

                    if (acesso == true)
                    {
                        Editar(this.Cpf);                  
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nHouve um erro ao tentar Login..." + e.Message);
                    utility.Pausa();
                    throw;
                }

            } while (true);
        }
        public void Editar(string cpfAtivo)
        {
            ConnectBD connect = new ConnectBD();


            do
            {
                int opc;
                string nome = "";
                string sqlUpdate = "";
                string sql = $"SELECT (Nome) from Passageiro Where CPF = '{cpfAtivo}';";
                Console.Clear();

                SqlCommand cmd = new SqlCommand(sql);

                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nome = reader.GetString(0);
                    }
                }
                Console.WriteLine("\nEDTAR DADOS DE: " + nome + ", CPF: " + cpfAtivo);
                Console.WriteLine("\nEscolha qual Dado deseja Editar: ");
                Console.Write("\n 1 - Nome");
                Console.Write("\n 2 - Data de Nascimento");
                Console.Write("\n 3 - Sexo");
                Console.Write("\n 4 - Situação (Ativo / Inativo)");
                Console.Write("\n\n 0 - Sair");
                try
                {
                    opc = int.Parse(Console.ReadLine());
                    //opc = int.Parse(utility.ValidarEntrada("menu"));

                    switch (opc)
                    {
                        case 0:
                            return;


                        case 1:
                            string novoNome;
                            Console.Clear();
                            Console.WriteLine("\nNome Atual: " + nome);
                            Console.Write("\n\nInforme o Novo Nome");
                            utility.Pausa();
                            novoNome = Console.ReadLine();
                            //novoNome = utility.ValidarEntrada("nome");
                            if (novoNome == null) { return; }

                            Console.Clear();
                            sqlUpdate = $"UPDATE Passageiro SET Nome = '{novoNome}';";

                            connect.SqlUpdate(sqlUpdate);

                            Console.WriteLine("\nNome Alterado com Sucesso!");
                            utility.Pausa();
                            Editar(cpfAtivo);

                            break;

                        //case 2:

                        //    Console.Clear();
                        //    Console.WriteLine("\nData de nascimento Atual: " + passageiroAtivo.DataNascimento.ToShortDateString());
                        //    Console.Write("\n\nInforme a Nova Data de Nascimento");
                        //    Pausa();
                        //    novaDataNascimento = ValidarEntrada("datanascimento");
                        //    if (novaDataNascimento == null) TelaEditarPassageiro(passageiroAtivo);

                        //    data = DateConverter(novaDataNascimento);
                        //    passageiroAtivo.DataNascimento = data;
                        //    GravarPassageiro();
                        //    Console.Clear();
                        //    Console.WriteLine("\nData de Nascimento Alterada com Sucesso!");
                        //    Pausa();
                        //    TelaEditarPassageiro(passageiroAtivo);

                        //    break;

                        //case 3:
                        //    Console.Clear();
                        //    Console.WriteLine("\nSexo Atual: " + passageiroAtivo.Sexo);
                        //    Console.Write("\n\nInforme o Novo Sexo");
                        //    Pausa();
                        //    novoSexo = char.Parse(ValidarEntrada("sexo"));
                        //    if (novoSexo.Equals(null)) TelaInicialPassageiros();

                        //    passageiroAtivo.Sexo = novoSexo;
                        //    GravarPassageiro();
                        //    Console.Clear();
                        //    Console.WriteLine("\nSexo Alterado com Sucesso!");
                        //    Pausa();
                        //    TelaEditarPassageiro(passageiroAtivo);
                        //    break;


                        //case 4:

                        //    Console.Clear();
                        //    Console.WriteLine("\nPASSAGEIRO: " + passageiroAtivo.Nome);
                        //    if (passageiroAtivo.Situacao == 'A')
                        //    { Console.WriteLine("\nSituação Atual: ATIVO"); }

                        //    if (passageiroAtivo.Situacao == 'I')
                        //    { Console.WriteLine("\nSituação Atual: INATIVO"); }

                        //    Pausa();

                        //    novaSituacao = char.Parse(ValidarEntrada("situacao"));
                        //    if (novaSituacao.Equals(null)) TelaInicialPassageiros();

                        //    passageiroAtivo.Situacao = novaSituacao;
                        //    GravarPassageiro();
                        //    Console.Clear();
                        //    Console.WriteLine("\nSituação de Cadastro Alterada com Sucesso!");
                        //    Pausa();
                        //    TelaEditarPassageiro(passageiroAtivo);
                        //    break;
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
