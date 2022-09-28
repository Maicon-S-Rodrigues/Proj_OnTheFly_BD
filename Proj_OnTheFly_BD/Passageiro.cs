using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_OnTheFly_BD
{
    internal class Passageiro
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public DateTime DataUltimaCompra { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        public Passageiro(string cpf, string nome, DateTime dataNascimento, char sexo, DateTime UltimaCompra, DateTime DataCadastro, char Situacao)
        {
            this.Cpf = cpf;
            this.Nome = nome;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.DataUltimaCompra = System.DateTime.Now;//Data do sistema
            this.DataCadastro = System.DateTime.Now;
            this.Situacao = Situacao; // Ativo,Inativo
        }

        


    }
}
