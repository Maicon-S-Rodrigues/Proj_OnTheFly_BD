using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_OnTheFly_BD
{
    internal class CompanhiaAerea
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime UltimoVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Situacao { get; set; }

        ConnectBD connection = new();
        static Utility utility = new();
        public CompanhiaAerea() { } 
        public CompanhiaAerea(string cnpj, string razaoSocial, DateTime DataAbertura, DateTime UltimoVoo, DateTime DataCadastro, string Situacao)
        {
            this.Cnpj = cnpj;
            this.RazaoSocial = razaoSocial;
            this.DataAbertura = DataAbertura;
            this.UltimoVoo = System.DateTime.Now;
            this.DataCadastro = System.DateTime.Now;
            this.Situacao = Situacao; //Ativo,Inativo
        }

        public void LoginCompanhia() // ok 
        {
            do
            {
                try
                {
                    Console.Clear();

                    this.Cnpj = utility.ValidarEntrada("cnpjexiste");
                    if (this.Cnpj == null) { return; }

                    EditarCompanhia(this.Cnpj);
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nHouve um erro ao tentar Login..." + e.Message);
                    utility.Pausa();
                    throw;
                }

            } while (true);
        }
        public void CadastroCompanhia() // ok 
        {
            Console.Clear();
            do
            {
                string strDataAbertura;
                try
                {
                    this.Cnpj = utility.ValidarEntrada("cnpj");
                    if (this.Cnpj == null) return;

                    this.RazaoSocial = utility.ValidarEntrada("nome");
                    if (this.RazaoSocial == null) return;

                    strDataAbertura = utility.ValidarEntrada("dataabertura");
                    if (strDataAbertura == null) return;
                    this.DataAbertura = DateTime.Parse(strDataAbertura);

                    this.DataCadastro = System.DateTime.Now;
                    this.UltimoVoo = System.DateTime.Now;

                    string sqlText = $"INSERT INTO CompanhiaAerea (CNPJ, Razao_Social, Data_Cadastro, Ultimo_Voo, Data_Abertura)" +
                                     $"values('{this.Cnpj}', '{this.RazaoSocial}', '{this.DataCadastro}', '{this.UltimoVoo}', '{DataAbertura}');";

                    try
                    {
                        connection.SqlInsert(sqlText);
                        Console.WriteLine("\nCompanhia Aérea Cadastrada com Sucesso!");
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
        public void EditarCompanhia(string cnpjAtivo) // ok 
        {
            do
            {
                int opc;
                string sqlUpdate = "";
                string sql = $"SELECT CNPJ, Razao_Social, Data_Abertura, Data_Cadastro, Ultimo_Voo, Situacao FROM CompanhiaAerea Where CNPJ = '{cnpjAtivo}';";

                Console.Clear();
                Console.WriteLine("\nEDTAR DADOS DE CADASTRO");
                Console.WriteLine("______________________________________________________");
                connection.SqlMostrarUmaCompanhiaAtiva(sql);
                Console.WriteLine("______________________________________________________");
                Console.WriteLine("\nEscolha qual Dado deseja Editar: ");
                Console.Write("\n 1 - Razão Social");
                Console.Write("\n 2 - Data de Abertura");
                Console.Write("\n 3 - Situação (Ativo / Inativo)");
                Console.Write("\n\n 0 - Sair");
                try
                {
                    opc = int.Parse(utility.ValidarEntrada("menu"));

                    switch (opc)
                    {
                        case 0:
                            return;


                        case 1: // razao social
                            Console.Write("\n\nInforme o Novo Nome/Razão Social");
                            utility.Pausa();
                            string novoNome = utility.ValidarEntrada("nome");
                            if (novoNome == null) return;

                            sqlUpdate = $"UPDATE CompanhiaAerea SET Razao_Social = '{novoNome}' WHERE CNPJ = '{cnpjAtivo}';";

                            connection.SqlUpdate(sqlUpdate);

                            Console.WriteLine("\nRazão Social Alterada com Sucesso!");
                            utility.Pausa();
                            break;

                        case 2: // data de abertura
                            Console.Write("\nInforme a Data de Abertura");
                            utility.Pausa();
                            string novaDataAbertura = utility.ValidarEntrada("dataabertura");
                            if (novaDataAbertura == null) return;

                            sqlUpdate = $"UPDATE CompanhiaAerea SET Data_Abertura = '{novaDataAbertura}' WHERE CNPJ = '{cnpjAtivo}';";

                            connection.SqlUpdate(sqlUpdate);

                            Console.WriteLine("\nData de Abertura Alterada com Sucesso!");
                            utility.Pausa();
                            break;

                        case 3: // situacao
                            Console.Write("\nInforme a Nova Situação para o Cadastro");
                            utility.Pausa();
                            string novaSituacao = utility.ValidarEntrada("situacao");
                            if (novaSituacao.Equals(null)) return;

                            sqlUpdate = $"UPDATE CompanhiaAerea SET Situacao = '{novaSituacao}' WHERE CNPJ = '{cnpjAtivo}';";

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
