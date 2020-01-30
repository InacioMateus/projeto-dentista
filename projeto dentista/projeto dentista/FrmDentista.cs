using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace projeto_dentista
{
    public partial class FrmDentista : Form, ICadform
    {
        Dentista objDentista;
        Conexao con = new Conexao();

        public FrmDentista()
        {
            InitializeComponent();
            con = new Conexao();
        }               
        

        public void bloquearCampos()
        {
            txtId.ReadOnly = true;
            txtCro.ReadOnly = true;
            txtNome.ReadOnly = true;
        }

        public void desbloquearCampos()
        {
            txtId.ReadOnly = false;
            txtCro.ReadOnly = false;
            txtNome.ReadOnly = false;
        }

        public void lerDados()
        {
            objDentista = new Dentista();

            objDentista.Id = int.Parse(txtId.Text.Trim());
            objDentista.Nome = txtNome.Text;
            objDentista.Cro = txtCro.Text;

            // 13/01/2020

            objDentista.Instagram = chbInstagram.Checked ? 1 : 0;
            objDentista.Facebook = chbFacebook.Checked ? 1 : 0;
            objDentista.Twitter = chbTwitter.Checked ? 1 : 0;
            objDentista.Linkedin = chbLinkedIn.Checked ? 1 : 0;

            // objDentista.Sexo = rbFeminino.Checked ? "F" : "M";

            if (rbFeminino.Checked)
            {
                objDentista.Sexo = "F";

            }
            else
            {
                objDentista.Sexo = "m";
            }
        }

        public void limparCampos()
        {
            txtId.Text = "";
            txtCro.Text = "";
            txtNome.Text = "";
        }
        
        public void atualizarGrid()
        {
            List<Dentista> listDentista = new List<Dentista>();
            con.conectar();
            SqlDataReader reader;
            reader = con.exeConsulta("select * from tb_dentista");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Dentista dentista = new Dentista();
                    dentista.Id = reader.GetInt32(0);
                    dentista.Nome = reader.GetString(1);
                    dentista.Cro = reader.GetString(2);
                    dentista.Sexo = reader.GetValue(3) == null ? "" : reader.GetValue(3).ToString();
                    dentista.Instagram = reader.GetValue(4).ToString() == "True" ? 1 : 0;
                    dentista.Facebook = reader.GetValue(5).ToString() == "True" ? 1 : 0;
                    dentista.Twitter = reader.GetValue(6).ToString() == "True" ? 1 : 0;
                    dentista.Linkedin = reader.GetValue(7).ToString() == "True" ? 1 : 0;

                    listDentista.Add(dentista);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }

            dgvDados.DataSource = null;
            dgvDados.DataSource = listDentista;
        }

        private void FrmDentista_Load(object sender, EventArgs e)
        {
            bloquearCampos();
            atualizarGrid();

        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();   
            con.conectar();
            desbloquearCampos();

        }
        

        private void dgvDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dgvDados.CurrentRow.Cells[0].Value.ToString();            
            txtNome.Text = dgvDados.CurrentRow.Cells[1].Value.ToString();
            txtCro.Text = dgvDados.CurrentRow.Cells[2].Value.ToString();

            chbInstagram.Checked = dgvDados.CurrentRow.Cells[4].Value.Equals(1);
            chbFacebook.Checked = dgvDados.CurrentRow.Cells[5].Value.Equals(1);
            chbTwitter.Checked = dgvDados.CurrentRow.Cells[6].Value.Equals(1);
            chbLinkedIn.Checked = dgvDados.CurrentRow.Cells[7].Value.Equals(1);

             rbFeminino.Checked = dgvDados.CurrentRow.Cells[3].Value.Equals("F")? true : false;
             rbMasculino.Checked = dgvDados.CurrentRow.Cells[3].Value.Equals("M") ? true : false;

            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lerDados();
            String sql = "insert into tb_dentista " +
                "values(" + objDentista.Id + ", '" +
                objDentista.Nome + "', '" +
                objDentista.Cro + "', '" +
                objDentista.Sexo + "', " +
                objDentista.Instagram + ", " +
                objDentista.Facebook + ", " +
                objDentista.Twitter + ", " +
                objDentista.Linkedin + ")";

            if (con.executar(sql) == 1)
            {
                MessageBox.Show("Dados salvos com sucesso!");
            }
            else
            {
                MessageBox.Show("Dados não foram salvos!");
            }

            bloquearCampos();
            atualizarGrid();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Equals(""))
            {
                MessageBox.Show("Selecione um campo primeiro!");
            }
            else
            {
                String sql = "delete from tb_dentista where id = " + txtId.Text;
                con.executar(sql);
                atualizarGrid();

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
            
        }
    }
}

