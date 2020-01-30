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
    public partial class FrmPaciente : Form, ICadform
    {
        Paciente objPaciente;
        Conexao con = new Conexao();

        public FrmPaciente()
        {
            InitializeComponent();
            con = new Conexao();
        }

        public void bloquearCampos()
        {
            txtId.ReadOnly = true;
            txtCpf.ReadOnly = true;
            txtNome.ReadOnly = true;
            txtEndereco.ReadOnly = true;
            txtTelefone.ReadOnly = true;
            dtNasc.Enabled = false;
            chbInstagram.Enabled = false;
            chbFacebook.Enabled = false;
            chbTwitter.Enabled = false;
            chbLinkedIn.Enabled = false;
        }

        public void desbloquearCampos()
        {
            txtId.ReadOnly = false;
            txtCpf.ReadOnly = false;
            txtNome.ReadOnly = false;
            txtEndereco.ReadOnly = false;
            txtTelefone.ReadOnly = false;
            dtNasc.Enabled = true;
            chbInstagram.Enabled = true;
            chbFacebook.Enabled = true;
            chbTwitter.Enabled = true;
            chbLinkedIn.Enabled = true;
        }

        public void lerDados()
        {
            objPaciente = new Paciente();

            objPaciente.Id = int.Parse(txtId.Text.Trim());
            objPaciente.Nome = txtNome.Text;
            objPaciente.Cpf = txtCpf.Text;
            objPaciente.Endereco = txtEndereco.Text;
            objPaciente.Telefone = txtTelefone.Text;
            objPaciente.Dtnasc = dtNasc.Value;

            objPaciente.Instagram = chbInstagram.Checked ? 1 : 0;
            objPaciente.Facebook = chbFacebook.Checked ? 1 : 0;
            objPaciente.Twitter = chbTwitter.Checked ? 1 : 0;
            objPaciente.Linkedin = chbLinkedIn.Checked ? 1 : 0;

            objPaciente.Sexo = rbFeminino.Checked ? "F" : "M";


        }

        public void limparCampos()
        {
            txtNome.Text = "";
            txtId.Text = "";
            txtEndereco.Text = "";
            txtCpf.Text = "";
            txtTelefone.Text = "";
            dtNasc.Value = DateTime.Today;
        }

        public void atualizarGrid()
        {
            List<Paciente> listPaciente = new List<Paciente>();
            con.conectar();
            SqlDataReader reader;
            reader = con.exeConsulta("select * from tb_paciente");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Paciente paciente = new Paciente();
                    paciente.Id = reader.GetInt32(0);
                    paciente.Nome = reader.GetString(1);
                    paciente.Cpf = reader.GetString(2);
                    paciente.Endereco = reader.GetString(3);
                    paciente.Telefone = reader.GetString(4);
                    paciente.Dtnasc = reader.GetDateTime(5);    
                    paciente.Sexo = reader.GetValue(6) == null ? "" : reader.GetValue(6).ToString();
                    paciente.Instagram = reader.GetValue(7).ToString() == "True" ? 1 : 0;
                    paciente.Facebook = reader.GetValue(8).ToString() == "True" ? 1 : 0;
                    paciente.Twitter = reader.GetValue(9).ToString() == "True" ? 1 : 0;
                    paciente.Linkedin = reader.GetValue(10).ToString() == "True" ? 1 : 0;

                    listPaciente.Add(paciente);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }

            dgvPaciente.DataSource = null;
            dgvPaciente.DataSource = listPaciente;
        }

        private void FrmPaciente_Load(object sender, EventArgs e)
        {
            bloquearCampos();
            atualizarGrid();
        }

        private void btn_Novo_Click(object sender, EventArgs e)
        {
            limparCampos();
            con.conectar();
            desbloquearCampos();
        }

        private void btn_Excluir_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Equals(""))
            {
                MessageBox.Show("Selecione um campo primeiro!");
            }
            else
            {
                String sql = "delete from tb_paciente where id = " + txtId.Text;
                con.executar(sql);
                atualizarGrid();
            }

        }

        private void dgvPaciente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dgvPaciente.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dgvPaciente.CurrentRow.Cells[1].Value.ToString();
            txtCpf.Text = dgvPaciente.CurrentRow.Cells[2].Value.ToString();
            txtEndereco.Text = dgvPaciente.CurrentRow.Cells[3].Value.ToString();
            txtTelefone.Text = dgvPaciente.CurrentRow.Cells[4].Value.ToString();
            dtNasc.Text = dgvPaciente.CurrentRow.Cells[5].Value.ToString();
            chbInstagram.Checked = dgvPaciente.CurrentRow.Cells[7].Value.Equals(1);
            chbFacebook.Checked = dgvPaciente.CurrentRow.Cells[8].Value.Equals(1);
            chbTwitter.Checked = dgvPaciente.CurrentRow.Cells[9].Value.Equals(1);
            chbLinkedIn.Checked = dgvPaciente.CurrentRow.Cells[10].Value.Equals(1);

            rbFeminino.Checked = dgvPaciente.CurrentRow.Cells[6].Value.Equals("F") ? true : false;
            rbMasculino.Checked = dgvPaciente.CurrentRow.Cells[6].Value.Equals("M") ? true : false;
        }

        private void btn_Salvar_Click(object sender, EventArgs e)
        {
            lerDados();
            String sql = "insert into tb_paciente " +
                "values(" + objPaciente.Id + ", '" +
                objPaciente.Nome + "', '" +
                objPaciente.Cpf + "', '" +
                objPaciente.Endereco + "', '"+
                objPaciente.Telefone + "', '"+
                objPaciente.Dtnasc + "', '"+
                objPaciente.Sexo + "', " +
                objPaciente.Instagram + ", " +
                objPaciente.Facebook + ", " +
                objPaciente.Twitter + ", " +
                objPaciente.Linkedin + ")";
            

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
    }
}    


    

