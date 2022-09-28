using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_OnTheFly_BD
{
    internal class ConnectBD
    {
        string Conexao = "Data Source=localhost; Initial Catalog=OnTheFly; User ID=sa; password=Sol@2905;";

        public string Caminho()
        {
            return Conexao;
        }
    }
}
