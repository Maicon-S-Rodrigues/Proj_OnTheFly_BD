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

                string valor = utility.ValidarEntrada("valorpassagem");
                if (valor == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);

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
        public void CancelarVoo(string cnpjAtivo)
        {
            do
            {
                string sql = $"SELECT CompanhiaAerea.Razao_Social, Voo.ID_VOO, Voo.Situacao, Voo.Data_Hora_Voo, Voo.Assentos_Ocupados, Aeronave.INSCRICAO " +
                             $"FROM Aeronave " +
                             $"RIGHT JOIN CompanhiaAerea " +
                             $"ON(CompanhiaAerea.CNPJ = Aeronave.CNPJ) " +
                             $"RIGHT JOIN Voo " +
                             $"ON(Voo.INSCRICAO = Aeronave.INSCRICAO) " +
                             $"WHERE CompanhiaAerea.CNPJ = '{cnpjAtivo}';";
                int opc;

                Console.Clear();
                Console.WriteLine("__________________________________________________________________________________________");
                connection.SqlMostrarVoosDeUmaCompanhia(sql);
                Console.WriteLine("__________________________________________________________________________________________");
                Console.WriteLine("\n1 - Escolher o Voo que deseja CANCELAR: ");
                Console.WriteLine("0 - Voltar");
                opc = int.Parse(utility.ValidarEntrada("menu"));


                switch (opc)
                {
                    case 0:
                        return;
                    case 1:
                        Console.Clear();
                        bool flag = false;
                        string idvoo = utility.ValidarEntrada("idvoo");
                        if (idvoo == null) return;

                        Console.WriteLine("Você tem certeza de que deseja cancelar o Voo " + idvoo + " ?");
                        Console.WriteLine("Escolha [- S -] para confirmar ou [- N -] para cancelar.");
                        do
                        {
                            string confirmacao = Console.ReadLine().ToUpper();

                            switch (confirmacao)
                            {
                                case "S":
                                    string sqlUpdateVoo = $"UPDATE Voo SET Situacao = 'INATIVO' WHERE ID_VOO = '{idvoo}';";
                                    connection.SqlUpdate(sqlUpdateVoo);
                                    Console.WriteLine("Voo Cancelado!");
                                    utility.Pausa();
                                    flag = true;
                                    break;

                                case "N":
                                    flag = true;
                                    break;
                                default:
                                    Console.WriteLine("Escolha [- S -] para confirmar ou [- N -] para cancelar.");
                                    break;
                            }
                        } while (flag == false);
                        break;
                }
            } while (true);
        }

    }
}
