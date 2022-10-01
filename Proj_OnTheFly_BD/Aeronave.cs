using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_OnTheFly_BD
{
    internal class Aeronave
    {
        public string Inscricao { get; set; }
        public string Cnpj { get; set; }
        public int Capacidade { get; set; }
        public DateTime UltimaVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Situacao { get; set; }

        ConnectBD connection = new();
        static Utility utility = new();
        CompanhiaAerea comp = new CompanhiaAerea();

        public Aeronave() { }
        public Aeronave(string inscricao, string cnpj, int capacidade, DateTime ultimaVenda, string situacao)
        {
            this.Inscricao = inscricao;
            this.Cnpj = cnpj;
            this.Capacidade = capacidade;
            this.UltimaVenda = ultimaVenda;
            this.Situacao = situacao;
        }

        public void CadastrarAeronave(string cnpjAtivo) // ok 
        {
            do
            {
                try
                {
                    this.Cnpj = cnpjAtivo;

                    this.Inscricao = utility.ValidarEntrada("idaeronave");
                    if (this.Inscricao == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);

                    this.Capacidade = int.Parse(utility.ValidarEntrada("capacidade"));
                    if (this.Capacidade.Equals(null)) comp.OpcoesCompanhiaAerea(cnpjAtivo);

                    this.Situacao = utility.ValidarEntrada("situacao");
                    if (this.Situacao == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);

                    this.UltimaVenda = System.DateTime.Now;
                    this.DataCadastro = System.DateTime.Now;

                    string sqlText = $"INSERT INTO Aeronave (Inscricao, CNPJ, Capacidade, Ultima_Venda, Situacao, Data_cadastro)" +
                                     $"values('{this.Inscricao}', '{this.Cnpj}', '{this.Capacidade}', '{this.UltimaVenda}', '{this.Situacao}', '{this.DataCadastro}');";

                    try
                    {
                        connection.SqlInsert(sqlText);
                        Console.WriteLine("\nAeronave Cadastrada com Sucesso!");
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
        public void EditarAeronave(string cnpjAtivo) // ok 
        {
            do
            {
                try
                {
                    string inscricao = utility.ValidarEntrada("aeronaveeditar");
                    if (inscricao == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);

                    string novaSituacao = utility.ValidarEntrada("situacao");
                    if (novaSituacao == null) comp.OpcoesCompanhiaAerea(cnpjAtivo);

                    string sqlUpdate = $"UPDATE Aeronave SET Situacao = '{novaSituacao}' WHERE INSCRICAO = '{inscricao}';";

                    connection.SqlUpdate(sqlUpdate);

                    Console.WriteLine("\nSituação de Cadastro Alterada com Sucesso!");
                    utility.Pausa();
                    return;

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
