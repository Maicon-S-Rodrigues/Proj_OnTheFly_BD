using System;
using System.Data.SqlClient;

namespace Proj_OnTheFly_BD
{
    internal class Program
    {
        #region TELAS
        static void TelaInicial(SqlConnection connection)
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Bem vindo à On The Fly!");
                Console.WriteLine("\nPor Favor, informe a Opção Desejada:\n");
                Console.WriteLine(" 1 - Companhia Aérea\n");
                Console.WriteLine(" 2 - Passageiro\n");
                Console.WriteLine(" 3 - Compras de Passagens\n");
                Console.WriteLine(" 4 - Acesso a Lista de CPF Restritos\n");
                Console.WriteLine(" 5 - Acesso a Lista de CNPJ Restritos\n");
                Console.WriteLine(" 6 - Aeronaves\n");
                Console.WriteLine(" 7 - Voos Realizados\n");
                Console.WriteLine(" 8 - Voos Disponíveis");
                Console.WriteLine("\n 0 - Encerrar Sessão\n");

                try
                {
                    opc = int.Parse(Console.ReadLine());

                    switch (opc)
                    {
                        case 0:
                            connection.Close();
                            Console.WriteLine("Encerrando...");
                            Environment.Exit(0);
                            break;

                        case 1:
                            TelaInicialCompanhiasAereas();
                            break;

                        case 2:
                            TelaInicialPassageiros();
                            break;

                        case 3:
                            TelaVendas();
                            break;

                        case 4:
                            TelaInicialCpfRestritos();
                            break;

                        case 5:
                            TelaInicialCnpjRestritos();
                            break;

                        case 6:
                            TelaVerAeronavesCadastradas();
                            break;

                        case 7:
                            //foreach (var voorealizado in voosrealizados)
                            //{
                            //    Console.WriteLine(voorealizado);
                            //}
                            //Pausa();
                            break;

                        case 8:
                            //foreach (var Voo in listVoo)
                            //{
                            //    if (Voo.Situacao == 'A')
                            //    {
                            //        Console.WriteLine("IDVoo: " + Voo.IDVoo + " Destino: " + Voo.Destino + " Data e Hora do Voo: " + Voo.DataVoo.ToString("dd/MM/yyyy HH:mm"));
                            //    }
                            //}
                            //Pausa();
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Escolha um valor numérico que represente a opção desejada!\n");
                    Pausa();
                }
            } while (opc != 0);
        }
        static void TelaInicialPassageiros()
        {

        }
        static void TelaLoginPassageiro()
        {

        }
        static void TelaCadastrarPassageiro()
        {

        }
        static void TelaEditarPassageiro(Passageiro passageiroAtivo)
        {

        }
        static void TelaEditarCompanhiaAerea(CompanhiaAerea companhiaAerea)
        {

        }
        static void TelaInicialCpfRestritos()
        {

        }
        static void TelaInicialCnpjRestritos()
        {

        }
        static void TelaInicialCompanhiasAereas()
        {

        }
        static void TelaLoginCompanhiaAerea()
        {

        }
        static void TelaCadastrarCompanhiaAerea()
        {

        }
        static void TelaOpcoesCompanhiaAerea(CompanhiaAerea compAtivo)
        {

        }
        static void TelaCadastrarAeronave(CompanhiaAerea compAtivo)
        {

        }
        static void TelaCadastrarVoo(CompanhiaAerea compAtivo)
        {

        }
        static void TelaVerAeronavesCadastradas()
        {

        }
        static void TelaEditarAeronave(CompanhiaAerea compAtivo)
        {

        }
        static void TelaVendas()
        {

        }
        static void TelaVoosDisponiveis(Passageiro passageiroAtivo)
        {

        }
        static void TelaDescricaoVoo(string idvoo, Passageiro passageiroAtivo)
        {

        }
        static void TelaHistoricoVendas()
        {

        }
        static void TelaDescricaoItemVenda()
        {

        }
        static void TelaHistoricoReservadas()
        {

        }
        #endregion

        #region UTILITY
        static void Pausa()
        {
            Console.WriteLine("\nAperte 'ENTER' para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
        static bool PausaMensagem()
        {
            bool repetirdo;
            do
            {
                Console.WriteLine("\nPressione S para informar novamente ou C para cancelar:");
                ConsoleKeyInfo op = Console.ReadKey(true);
                if (op.Key == ConsoleKey.S)
                {
                    Console.Clear();
                    return false;
                }
                else
                {
                    if (op.Key == ConsoleKey.C)
                    {
                        Console.Clear();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Escolha uma opção válida!");
                        repetirdo = true;
                    }
                }
            } while (repetirdo == true);
            return true;
        }
        #endregion
        static void Main(string[] args)
        {
            ConnectBD conn = new ConnectBD();
            SqlConnection connection = new SqlConnection(conn.Caminho());

            TelaInicial(connection);
        }
    }
}
