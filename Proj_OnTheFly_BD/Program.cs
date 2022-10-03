using System;
using System.Data.SqlClient;

namespace Proj_OnTheFly_BD
{
    internal class Program
    {
        static Utility utility = new Utility();
        static ConnectBD connect = new ConnectBD();
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
                            TelaInicialCpfRestritos();
                            break;

                        case 5:
                            TelaInicialCnpjRestritos();
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
        static void TelaInicialCpfRestritos()// ok 
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("\n'CPF' RESTRITOS");
                Console.WriteLine("\nInforme a Opção Desejada:\n");
                Console.WriteLine(" 1 - Ver a Lista de 'CPF' Restritos\n");
                Console.WriteLine(" 2 - Adicionar um 'CPF' à Lista de Restritos\n");
                Console.WriteLine(" 3 - Remover um 'CPF' da Lista de Restritos\n");
                Console.WriteLine("\n 0 - Sair\n");

                opc = int.Parse(utility.ValidarEntrada("menu"));
                Console.Clear();

                switch (opc)
                {
                    case 0:

                        TelaInicial();

                        break;

                    case 1:

                        string MostrarListaRestritos = $"SELECT CPF_RESTRITO FROM Restritos;";
                        bool listaTem = connect.SqlVerificaListaRestritos(MostrarListaRestritos);
                        if (listaTem == false)
                        {
                            Console.WriteLine("Sem Registros!");
                            utility.Pausa();
                            TelaInicialCpfRestritos();
                        }
                        else
                        {
                            connect.SqlMostrarRestritos(MostrarListaRestritos);
                            utility.Pausa();
                            TelaInicialCpfRestritos();
                        }
                        break;
                    case 2:
                        Console.WriteLine("Informe o CPF a ser Inserido na Lista de Restritos");
                        utility.Pausa();

                        string addCpf = utility.ValidarEntrada("cpfjarestrito");
                        if (addCpf == null) TelaInicialCpfRestritos();

                        string sqlBloquearCpf = $"INSERT INTO Restritos VALUES ('{addCpf}');";
                        connect.SqlInsert(sqlBloquearCpf);
                        Console.Clear();
                        Console.WriteLine("CPF bloqueado com sucesso");
                        utility.Pausa();
                        TelaInicialCpfRestritos();
                        break;

                    case 3:
                        bool flag = false;
                        do
                        {
                            string cpfRetirar;
                            Console.Clear();
                            Console.Write("Informe o CPF que deseja retirar da Lista de Restritos: ");
                            cpfRetirar = Console.ReadLine();

                            string verificarSeEstaREstrito = $"SELECT CPF_RESTRITO FROM Restritos WHERE CPF_RESTRITO = '{cpfRetirar}';";

                            bool estaRestrito = connect.SqlVerificaListaRestritos(verificarSeEstaREstrito);

                            if (estaRestrito == false)
                            {
                                Console.WriteLine("Este CPF não foi encontrado na Lista de Restritos!");
                                flag = true;
                                utility.Pausa();
                                TelaInicialCpfRestritos();
                            }
                            else
                            {
                                
                                Console.WriteLine("Você tem certeza de que deseja Desbloquear o CPF " + cpfRetirar + " ?");
                                Console.WriteLine("Escolha [- S -] para confirmar ou [- N -] para cancelar.");
                                do
                                {
                                    string confirmacao = Console.ReadLine().ToUpper();

                                    switch (confirmacao)
                                    {
                                        case "S":
                                            string deletarCpfRestrito = $"DELETE FROM Restritos WHERE CPF_RESTRITO = {cpfRetirar};";
                                            connect.SqlDelete(deletarCpfRestrito);
                                            Console.WriteLine("CPF Retirado da Lista de Restrição!");
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
                                utility.Pausa();
                                TelaInicialCpfRestritos();
                            }
                        } while (flag == false);
                        
                        break;
                }

            } while (true);
        }
        static void TelaInicialCnpjRestritos()// ok 
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("\n'CNPJ' RESTRITOS");
                Console.WriteLine("\nInforme a Opção Desejada:\n");
                Console.WriteLine(" 1 - Ver a Lista de 'CNPJ' Bloqueados\n");
                Console.WriteLine(" 2 - Adicionar um 'CNPJ' à Lista de Bloqueados\n");
                Console.WriteLine(" 3 - Remover um 'CNPJ' da Lista de Bloqueados\n");
                Console.WriteLine("\n 0 - Sair\n");

                opc = int.Parse(utility.ValidarEntrada("menu"));
                Console.Clear();

                switch (opc)
                {
                    case 0:

                        TelaInicial();

                        break;

                    case 1:

                        string MostrarListaRestritos = $"SELECT CNPJ_RESTRITO FROM Bloqueados;";
                        bool listaTem = connect.SqlVerificaListaRestritos(MostrarListaRestritos);
                        if (listaTem == false)
                        {
                            Console.WriteLine("Sem Registros!");
                            utility.Pausa();
                            TelaInicialCnpjRestritos();
                        }
                        else
                        {
                            connect.SqlMostrarRestritos(MostrarListaRestritos);
                            utility.Pausa();
                            TelaInicialCnpjRestritos();
                        }
                        break;
                    case 2:
                        Console.WriteLine("Informe o CNPJ a ser Inserido na Lista de Restritos");
                        utility.Pausa();

                        string addCnpj = utility.ValidarEntrada("cnpjjarestrito");
                        if (addCnpj == null) TelaInicialCnpjRestritos();

                        string sqlBloquearCpf = $"INSERT INTO Bloqueados VALUES ('{addCnpj}');";
                        connect.SqlInsert(sqlBloquearCpf);
                        Console.Clear();
                        Console.WriteLine("CNPJ bloqueado com sucesso");
                        utility.Pausa();
                        TelaInicialCnpjRestritos();
                        break;

                    case 3:
                        bool flag = false;
                        do
                        {
                            string cnpjRetirar;
                            Console.Clear();
                            Console.Write("Informe o CNPJ que deseja retirar da Lista de Restritos: ");
                            cnpjRetirar = Console.ReadLine();

                            string verificarSeEstaREstrito = $"SELECT CNPJ_RESTRITO FROM Bloqueados WHERE CNPJ_RESTRITO = '{cnpjRetirar}';";

                            bool estaRestrito = connect.SqlVerificaListaRestritos(verificarSeEstaREstrito);

                            if (estaRestrito == false)
                            {
                                Console.WriteLine("Este CNPJ não foi encontrado na Lista de Restritos!");
                                flag = true;
                                utility.Pausa();
                                TelaInicialCnpjRestritos();
                            }
                            else
                            {

                                Console.WriteLine("Você tem certeza de que deseja Desbloquear o CNPJ " + cnpjRetirar + " ?");
                                Console.WriteLine("Escolha [- S -] para confirmar ou [- N -] para cancelar.");
                                do
                                {
                                    string confirmacao = Console.ReadLine().ToUpper();

                                    switch (confirmacao)
                                    {
                                        case "S":
                                            string deletarCpfRestrito = $"DELETE FROM BLoqueados WHERE CNPJ_RESTRITO = {cnpjRetirar};";
                                            connect.SqlDelete(deletarCpfRestrito);
                                            Console.WriteLine("CNPJ Retirado da Lista de Restrição!");
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
                                utility.Pausa();
                                TelaInicialCnpjRestritos();
                            }
                        } while (flag == false);

                        break;
                }

            } while (true);
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
