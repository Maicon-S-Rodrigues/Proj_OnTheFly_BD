using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_OnTheFly_BD
{
    internal class Voo
    {
        public string IDVoo { get; set; }
        public string InscricaoAeronave { get; set; }
        public string Situacao { get; set; }
        public string Destino { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataVoo { get; set; }
        int AssentosOcupados { get; set; }

        ConnectBD connection = new();
        static Utility utility = new();
        CompanhiaAerea comp = new CompanhiaAerea();

        public Voo () { }
        public Voo(string idVoo, string inscricaoAeronave, string situacao, string destino, DateTime dataVoo, int assentosOcupados)
        {
            this.IDVoo = idVoo;
            this.InscricaoAeronave = inscricaoAeronave;
            this.Situacao = situacao; //Ativo,Cancelado
            this.Destino = destino;
            this.DataCadastro = System.DateTime.Now;
            this.DataVoo = dataVoo;
            this.AssentosOcupados = assentosOcupados;
        }

        public void CadastrarVoo(string cnpjAtivo)
        {
            Console.Clear();
            double valor;
            int capacidadeAeronave;
            List<string> passagens = new List<string>();

            bool permissaoVoo = connection.SqlVerificaCompanhiaAtiva($"SELECT Situacao FROM CompanhiaAerea WHERE Situacao = 'ATIVO' AND CNPJ = '{cnpjAtivo}'"); // verifica se a companhia esta ativa

                if (permissaoVoo == true)
            {
                Console.WriteLine("\nInforme a Inscrição da Aeronave que ira realizar esse Voo");
                string inscricao = utility.ValidarEntrada("aeronave");
                if (inscricao == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);
                bool aeronaveAtiva = connection.SqlVerificarDados(inscricao, "Situacao = 'ATIVA' AND INSCRICAO ", "Aeronave"); // verifica se a aeronave esta ativa PRECISA CORRIGIR SINTAXE IGUAL ACIMA^^^^
                
                if (aeronaveAtiva == false)
                {
                    Console.WriteLine("\nNão é possível cadastrar um novo Voo para essa Aeronave pois ela consta como 'INATIVA' no Sistema.");
                    utility.Pausa();
                    comp.OpcoesCompanhiaAerea(cnpjAtivo);
                }

                bool pegouCapacidade = false;
                do
                {
                    capacidadeAeronave = connection.SqlPegarACapacidaeDaAeronave(inscricao);
                    if (capacidadeAeronave > 0)
                    {
                        pegouCapacidade = true;
                    }
                } while (pegouCapacidade == false);


                this.Destino = utility.ValidarEntrada("destino");
                if (this.Destino == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);
               
                string auxData = utility.ValidarEntrada("datavoo");
                if (auxData == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);
                this.DataVoo = DateTime.Parse(auxData);

                string auxValor = utility.ValidarEntrada("valorpassagem");
                if (auxValor == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);
                valor = double.Parse(auxValor);

                bool gerouID = false;
                do
                {
                    this.IDVoo = utility.GeradorIdVoo();
                    if (this.IDVoo != null)
                    {
                        gerouID = true;
                    }

                } while (gerouID == false);

                this.DataCadastro = System.DateTime.Now;
                DateTime dataUltimaOP = System.DateTime.Now;

                passagens = utility.GeradorIdPassagens(capacidadeAeronave);


                //Insere o Voo
                string sqlTextVoo = $"INSERT INTO Voo (ID_VOO, INSCRICAO, IATA, Data_Cadastro, Data_Hora_Voo)" +
                                     $"values('{this.IDVoo}', '{inscricao}', '{this.Destino}', '{this.DataCadastro}', '{this.DataVoo}');";
                connection.SqlInsert(sqlTextVoo);

                //Preenche as passagens
                foreach (string pass in passagens)
                {
                    string sqlTextPassagens = $"INSERT INTO Passagem (ID_PASSAGEM, ID_VOO, Valor, Data_Cadastro, Data_Ultima_Operacao)" +
                                     $"values('{pass}', '{this.IDVoo}', '{valor}', '{this.DataCadastro}', '{dataUltimaOP}');";
                    connection.SqlInsert(sqlTextPassagens);
                }

                Console.WriteLine("\nNovo Voo Programado com Sucesso!");
                utility.Pausa();
                comp.OpcoesCompanhiaAerea(cnpjAtivo);        
            }
            else
            {
                Console.WriteLine("\nCompanhia INATIVA no sistema. Não tem permissão para Cadastrar um novo Voo!");
                utility.Pausa();
                comp.OpcoesCompanhiaAerea(cnpjAtivo);
            }
        }
    }
}
