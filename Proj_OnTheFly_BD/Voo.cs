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

            bool permissaoVoo = connection.SqlVerificarDados(cnpjAtivo, "Situacao = 'ATIVA' AND CNPJ ", "CompanhiaAerea"); // verifica se a companhia esta ativa

            if (permissaoVoo == true)
            {
                Console.WriteLine("\nInforme a Inscrição da Aeronave que ira realizar esse Voo");
                string inscricao = utility.ValidarEntrada("aeronave");
                if (inscricao == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);
                bool aeronaveAtiva = connection.SqlVerificarDados(inscricao, "Situacao = 'ATIVA' AND INSCRICAO ", "Aeronave"); // verifica se a aeronave esta ativa
                
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
                valor = double.Parse(auxValor);

                this.IDVoo = utility.GeradorId("idvoo");
                if (this.IDVoo == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);

                Voo novoVoo = new Voo(idVoo, destino, idAeronave, dataVoo, System.DateTime.Now, 'A');
                listVoo.Add(novoVoo);
                GravarVoo();
                Aeronave a = null;
                foreach (var aeronave in listAeronaves)
                {
                    if (aeronave.Inscricao == idAeronave)
                    {
                        a = aeronave;
                        break;
                    }
                }
                //Gerador de passagens
                List<string> idsPassagem = GeradorIdPassagens(a.Capacidade);
                for (int i = 0; i < a.Capacidade; i++)
                {
                    PassagemVoo passagem = new PassagemVoo(idsPassagem[i], idVoo, System.DateTime.Now, valor, 'L');
                    listPassagem.Add(passagem);
                    GravarPassagem();
                }
                Console.WriteLine("\nCadastro Realizado com Sucesso!");
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
