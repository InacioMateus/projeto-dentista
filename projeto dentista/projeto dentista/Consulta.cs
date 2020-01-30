using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_dentista
{
    class Consulta
    {
        public int IdCon { get; set; }

        public String Motivo { get; set; }

        public DateTime dt_consulta { get; set; }

        public String Diagnostico { get; set; }

        public String Receita { get; set; }

        public DateTime dt_retorno { get; set; }

        public String  MotivoR { get; set; }

        public int IdPaciente { get; set; }

        public int IdDentista { get; set; }
    }
}
