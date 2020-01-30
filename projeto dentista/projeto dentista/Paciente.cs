using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_dentista
{
    class Paciente:Pessoa
    {     

        public String Cpf { get; set; }

        public String Endereco { get; set;}

        public String Telefone { get; set; }

        public DateTime Dtnasc { get; set; }

        public String Sexo { get; set; }

        public int Instagram { get; set; }

        public int Facebook { get; set; }

        public int Twitter { get; set; }

        public int Linkedin { get; set; }
    }
}
