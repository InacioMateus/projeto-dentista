using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_dentista
{
    public interface ICadform
    {
        void bloquearCampos();

        void desbloquearCampos();

        void limparCampos();

        void lerDados();

        void atualizarGrid();
    }
}
