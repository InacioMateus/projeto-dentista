using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projeto_dentista
{
    public partial class MenuDentibao : Form
    {
        public MenuDentibao()
        {
            InitializeComponent();
        }

        private void btnDentistas_Click(object sender, EventArgs e)
        {
            FrmDentista dentista = new FrmDentista();
            dentista.Show();
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            FrmPaciente paciente = new FrmPaciente();
            paciente.Show();
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            FrmConsulta consultas = new FrmConsulta();
            consultas.Show();
        }
    }
}
