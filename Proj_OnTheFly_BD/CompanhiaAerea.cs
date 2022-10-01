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

        public void LoginCompanhia()
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
        public void CadastroCompanhia()
        {

        }
        public void EditarCompanhia(string cnpjAtivo)
        {
            Console.WriteLine("deu certo"); //// parei aquiiiiiiiiiiiiiiiii
            Console.ReadKey();
        }
    }
}
