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

        public Voo() { }
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
            int capacidade;
            float valor;
            bool permissaoVoo = connection.SqlVerificaCompanhiaAtiva($"SELECT Situacao FROM CompanhiaAerea WHERE Situacao = 'ATIVO' AND CNPJ = '{cnpjAtivo}'"); // verifica se a companhia esta ativa

            if (permissaoVoo == true)
            {
                Console.WriteLine("\nInforme a Inscrição da Aeronave que ira realizar esse Voo");
                string inscricao = utility.ValidarEntrada("aeronave");
                if (inscricao == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);

                bool aeronaveAtiva = connection.SqlVerificaAeronaveAtiva($"SELECT Situacao FROM Aeronave WHERE Situacao = 'ATIVO' AND INSCRICAO = '{inscricao}'"); // verifica se a aeronave esta ativa
                
                if (aeronaveAtiva == false)
                {
                    Console.WriteLine("\nNão é possível cadastrar um novo Voo para essa Aeronave pois ela consta como 'INATIVA' no Sistema.");
                    utility.Pausa();
                    comp.OpcoesCompanhiaAerea(cnpjAtivo);
                }

                this.Destino = utility.ValidarEntrada("destino");
                if (this.Destino == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);

                string auxData = utility.ValidarEntrada("datavoo");
                if (auxData == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);
                this.DataVoo = DateTime.Parse(auxData);

                string auxValor = utility.ValidarEntrada("valorpassagem");
                if (auxValor == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);
                valor = float.Parse(auxValor);

                bool gerou = false;
                do
                {
                    this.IDVoo = utility.GeradorIdVoo();
                    if (this.IDVoo != null)
                        gerou = true;

                } while (gerou == false);

                this.DataCadastro = System.DateTime.Now;

                string sqlCadastrarVoo = $"INSERT INTO Voo (ID_VOO, INSCRICAO, IATA, Data_Cadastro, Data_Hora_Voo) " +
                                         $"VALUES ('{this.IDVoo}', '{inscricao}', '{this.Destino}', '{this.DataCadastro}', '{this.DataVoo}');";
                connection.SqlInsert(sqlCadastrarVoo);

                //Gerador de passagens
                bool pegouCapacidade = false;
                do
                {
                    capacidade = connection.SqlPegarCapacidadeAeronave(inscricao);
                    if (capacidade > 0)
                        pegouCapacidade = true;

                } while (pegouCapacidade == false);


                List<string> passagens = new List<string>();

                passagens = utility.GeradorIdPassagens(capacidade);


                foreach (string pass in passagens)
                {
                    string sqlInserirPassagens = $"INSERT INTO Passagem (ID_PASSAGEM, ID_VOO, Valor, Data_Cadastro, Data_Ultima_Operacao) " +
                                                 $"VALUES ('{pass}', '{this.IDVoo}', '{valor}', '{this.DataCadastro}', '{this.DataCadastro}');";
                    connection.SqlInsert(sqlInserirPassagens);
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
