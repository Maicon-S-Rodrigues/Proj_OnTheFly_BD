using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_OnTheFly_BD
{
    internal class Venda
    {
        public string IDVenda { get; set; }
        public string CpfPassageiro { get; set; }
        public DateTime DataVenda { get; set; }
        public float ValorTotal { get; set; }

        ConnectBD connection = new();
        static Utility utility = new();

        public Venda() { }
        public Venda(string idVenda, DateTime dataVenda, string cpfPassageiro, float valorTotal)
        {
            this.IDVenda = idVenda;
            this.DataVenda = dataVenda;
            this.CpfPassageiro = cpfPassageiro;
            this.ValorTotal = valorTotal;
        }

        public void TelaVendas() // ok
        {

            int opc;
            Console.Clear();
            Console.WriteLine("Informe a opção desejada: \n");
            Console.WriteLine("1 - Compra de Passagem\n");
            Console.WriteLine("2 - Ver Passagens Vendidas\n");
            Console.WriteLine("3 - Ver Passagens Reservadas\n");
            Console.WriteLine("\n0 -  SAIR\n");
            opc = int.Parse(utility.ValidarEntrada("menu"));
            Console.Clear();

            switch (opc)
            {
                case 0:
                    return;

                case 1:
                    this.CpfPassageiro = utility.ValidarEntrada("cpflogin");
                    if (this.CpfPassageiro == null) return;
                    Console.Clear();
                    VoosDisponiveis(this.CpfPassageiro);
                    break;

                case 2:
                    HistoricoVendas();
                    break;

                case 3:
                    HistoricoReservadas();
                    break;
            }
        }
        public void VoosDisponiveis(string cpfAtivo) // ok 
        {
            string sql = $"SELECT CompanhiaAerea.Razao_Social, Voo.ID_VOO, Voo.Situacao, Voo.Data_Hora_Voo, Voo.Assentos_Ocupados, Aeronave.INSCRICAO " +
                         $"FROM Aeronave " +
                         $"RIGHT JOIN CompanhiaAerea " +
                         $"ON(CompanhiaAerea.CNPJ = Aeronave.CNPJ) " +
                         $"RIGHT JOIN Voo " +
                         $"ON(Voo.INSCRICAO = Aeronave.INSCRICAO) " +
                         $"WHERE Voo.Situacao = 'ATIVO'; ";
            int opc = 0;

            Console.Clear();
            Console.WriteLine("VOOS DISPONÍVEIS:");
            Console.WriteLine("__________________________________________________________________________________________");
            connection.SqlMostrarVoosDisponiveis(sql);
            Console.WriteLine("__________________________________________________________________________________________");
            Console.WriteLine("\n1 - Escolher o Voo Desejado: ");
            Console.WriteLine("0 - Voltar");
            opc = int.Parse(utility.ValidarEntrada("menu"));
            switch (opc)
            {
                case 0:
                    TelaVendas();
                    break;
                case 1:
                    string idvoo = utility.ValidarEntrada("idvoo");
                    if (idvoo == null) VoosDisponiveis(cpfAtivo);
                    DescricaoVoo(idvoo, cpfAtivo);
                    break;
            }
        }

        public void DescricaoVoo(string idVoo, string cpfAtivo)
        {
            string inscricao;
            int passLivres = 0;

            bool pegouInscricao = false;
            do
            {
                inscricao = connection.SqlPegarInscricaoAeronave(idVoo);
                if (inscricao != null)
                    pegouInscricao = true;

            } while (pegouInscricao == false);


            bool pegouPassagensLivres = false;
            do
            {
                passLivres = connection.SqlPegarPassagensLivres(inscricao);
                if (passLivres == 0)
                {
                    Console.WriteLine("Passagens Esgotadas para este Voo!");
                    utility.Pausa();
                    VoosDisponiveis(cpfAtivo);
                }
                else if (passLivres > 0)
                {
                    pegouPassagensLivres = true;
                }

            } while (pegouPassagensLivres == false);
            Console.WriteLine("PASSAGENS LIVRES: " + passLivres);


            //string sql = $"SELECT CompanhiaAerea.Razao_Social, Voo.ID_VOO, Voo.Situacao, Voo.Data_Hora_Voo, Voo.Assentos_Ocupados, Aeronave.INSCRICAO " +
            //            $"FROM Aeronave " +
            //            $"RIGHT JOIN CompanhiaAerea " +
            //            $"ON(CompanhiaAerea.CNPJ = Aeronave.CNPJ) " +
            //            $"RIGHT JOIN Voo " +
            //            $"ON(Voo.INSCRICAO = Aeronave.INSCRICAO) " +
            //            $"WHERE Voo.ID_VOO = '{idVoo}'; ";
            //int opc = 0;
            //Console.Clear();
            //Console.WriteLine("VOO " + idVoo + ":");
            //Console.WriteLine("__________________________________________________________________________________________");
            //connection.SqlMostrarDetahesVoo(sql);
            //Console.WriteLine("__________________________________________________________________________________________");
            //Console.WriteLine("1 - Comprar: ");
            //Console.WriteLine("2 - Reservar: ");
            //Console.WriteLine("0 - Voltar: ");
            //opc = int.Parse(utility.ValidarEntrada("menu"));
            //switch (opc)
            //{
            //    case 0:
            //        VoosDisponiveis(cpfAtivo);
            //        break;
            //    case 1:
            //        int cont = 0;
            //        bool retornar = false;
            //        int quantPassagem;
            //        do
            //        {
            //            Console.Clear();
            //            Console.WriteLine("\nInforme a quantidade de passagens (máximo 4): \n1  2  3  4");
            //            quantPassagem = int.Parse(utility.ValidarEntrada("menu"));
            //            if (quantPassagem > 0 && quantPassagem <= 4)
            //            {
            //                foreach (var passagem in listPassagem)
            //                {
            //                    if (passagem.IDVoo == idvoo && passagem.Situacao == 'L')
            //                    {
            //                        cont++;
            //                    }
            //                }
            //                if (cont >= quantPassagem)
            //                {
            //                    cont = 0;
            //                    PassagemVoo p = null;
            //                    foreach (var passagem in listPassagem)
            //                    {
            //                        if (passagem.IDVoo == idvoo && passagem.Situacao == 'L')
            //                        {
            //                            p = passagem;
            //                            passagem.Situacao = 'P';
            //                            passagem.DataUltimaOperacao = System.DateTime.Now;
            //                            GravarPassagem();
            //                            ItemVenda item = new ItemVenda(GeradorId("iditemvenda"), passagem.IDPassagem, passagem.Valor, passageiroAtivo.Cpf, passageiroAtivo.Nome);
            //                            listItemVenda.Add(item);
            //                            GravarItemVenda();
            //                            cont++;
            //                        }

            //                        if (cont == quantPassagem)
            //                        {
            //                            retornar = true;
            //                            Venda venda = new Venda(GeradorId("idvenda"), System.DateTime.Now, passageiroAtivo.Cpf, (p.Valor * quantPassagem));
            //                            listVenda.Add(venda);
            //                            GravarVenda();
            //                            passageiroAtivo.DataUltimaCompra = System.DateTime.Now;
            //                            GravarPassageiro();

            //                            string idAeronave = null;
            //                            foreach (var voo in listVoo)
            //                            {
            //                                if (idvoo == voo.IDVoo)
            //                                {
            //                                    idAeronave = voo.IDVoo;
            //                                    break;
            //                                }
            //                            }
            //                            foreach (var aeronave in listAeronaves)
            //                            {
            //                                if (aeronave.Inscricao == idAeronave)
            //                                {
            //                                    aeronave.AssentosOcupados = aeronave.AssentosOcupados + quantPassagem;
            //                                    aeronave.UltimaVenda = System.DateTime.Now;
            //                                }
            //                            }
            //                            GravarAeronaves();

            //                            Console.WriteLine("Compra realizada com sucesso!");
            //                            Pausa();
            //                            TelaVendas();
            //                            break;
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    Console.WriteLine("Não possui esta quantidade de passagens disponíveis: ");
            //                    retornar = PausaMensagem();
            //                }
            //            }
            //            else
            //            {
            //                Console.WriteLine("Só é possível comprar [4] passagens por venda");
            //                retornar = PausaMensagem();
            //            }
            //        } while (retornar == true);
            //        TelaHistoricoVendas();
            //        break;
            //    case 2:

            //        cont = 0;
            //        retornar = false;
            //        do
            //        {
            //            Console.WriteLine("\nInforme a quantidade de passagens para reserva (máximo 4): \n1  2  3  4");
            //            quantPassagem = int.Parse(ValidarEntrada("menu"));
            //            if (quantPassagem > 0 && quantPassagem <= 4)
            //            {
            //                foreach (var passagem in listPassagem)
            //                {
            //                    if (passagem.IDVoo == idvoo && passagem.Situacao == 'L')
            //                    {
            //                        cont++;
            //                    }
            //                }
            //                if (cont >= quantPassagem)
            //                {
            //                    cont = 0;
            //                    PassagemVoo p = null;
            //                    foreach (var passagem in listPassagem)
            //                    {
            //                        if (passagem.IDVoo == idvoo && passagem.Situacao == 'L')
            //                        {
            //                            p = passagem;
            //                            passagem.Situacao = 'R';
            //                            passagem.DataUltimaOperacao = System.DateTime.Now;
            //                            GravarPassagem();
            //                            cont++;
            //                        }
            //                        if (cont == quantPassagem) break;
            //                    }
            //                    Console.Clear();
            //                    Console.WriteLine("Reserva realizada com sucesso!");
            //                    Pausa();
            //                    TelaVendas();
            //                }
            //                else
            //                {
            //                    Console.WriteLine("Não possui esta quantidade de passagens disponíveis: ");
            //                    retornar = PausaMensagem();
            //                }
            //            }
            //            else
            //            {
            //                Console.WriteLine("Só é possível reservar [4] passagens por venda");
            //                retornar = PausaMensagem();
            //            }
            //        } while (retornar == true);
            //        TelaHistoricoReservadas();
            //        break;
            //}
        }
        public void HistoricoVendas()
        {

        }
        public void HistoricoReservadas()
        {

        }
    }
}
