using System;
using System.Data.SqlClient;

namespace Proj_OnTheFly_BD
{
    internal class Program
    {
        static Utility utility = new Utility();
        #region TELAS
        static void TelaInicial()
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
                Console.WriteLine(" 8 - Voos Disponíveis\n");
                Console.WriteLine(" 0 - Encerrar Sessão\n");

                try
                {
                    opc = int.Parse(utility.ValidarEntrada("menu"));

                    switch (opc)
                    {
                        case 0:
                            Console.WriteLine("Encerrando...");
                            Environment.Exit(0);
                            break;

                        case 1:
                            TelaInicialCompanhiasAereas();
                            break;

                        case 2:
                            TelaInicialPassageiro();
                            break;

                        case 3:
                            //TelaVendas();
                            break;

                        case 4:
                            //TelaInicialCpfRestritos();
                            break;

                        case 5:
                            //TelaInicialCnpjRestritos();
                            break;

                        case 6:
                            //TelaVerAeronavesCadastradas();
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
                    utility.Pausa();
                }
            } while (opc != 0);
        }
        static void TelaInicialPassageiro() // ok 
        {
            Passageiro passageiro = new Passageiro();
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("\nInforme a Opção Desejada:\n");
                Console.WriteLine(" 1 - Passageiro já cadastrado\n");
                Console.WriteLine(" 2 - Cadastrar um novo Passageiro\n");
                Console.WriteLine("\n 0 - SAIR\n");
                try
                {
                    opc = int.Parse(utility.ValidarEntrada("menu"));
                    Console.Clear();

                    switch (opc)
                    {
                        case 0:
                            TelaInicial();
                            break;

                        case 1:
                            passageiro.LoginPassageiro();
                            break;

                        case 2:
                            passageiro.CadastroPassageiro();
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Escolha um valor numérico que represente a opção desejada!\n");
                    utility.Pausa();
                }
            } while (opc != 0);
        }
        static void TelaInicialCompanhiasAereas() // ok 
        {
            CompanhiaAerea companhia = new CompanhiaAerea();
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("\nInforme a Opção Desejada:\n");
                Console.WriteLine(" 1 - Companhia Aérea já cadastrado\n");
                Console.WriteLine(" 2 - Cadastrar uma nova Companhia Aérea\n");
                Console.WriteLine("\n 0 - SAIR\n");
                try
                {
                    opc = int.Parse(utility.ValidarEntrada("menu"));
                    Console.Clear();

                    switch (opc)
                    {
                        case 0:
                            TelaInicial();
                            break;

                        case 1:
                            companhia.LoginCompanhia();
                            break;

                        case 2:
                            companhia.CadastroCompanhia();
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Escolha um valor numérico que represente a opção desejada!\n");
                    utility.Pausa();
                }
            } while (opc != 0);
        }
        static void TelaInicialAeronaves()
        {

        }
        #endregion
        static void Main(string[] args)
        {
            TelaInicial();
            //OBS ___ Quando os RESTRITOS e BLOQUEADOS estiverem prontos
            //Precisa TEstar se funciona a validação de entrada CPF Login e CNPJ Login
        }
    }
}
