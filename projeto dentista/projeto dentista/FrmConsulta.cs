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
    public partial class FrmConsulta : Form, ICadform
    {
        Consulta objconsulta = new Consulta();
        Conexao con = new Conexao();

        public FrmConsulta()
        {

            con = new Conexao();
            InitializeComponent();
        }

        public void bloquearCampos()
        {
            txtIdConsulta.ReadOnly = true;
            txtDiag.ReadOnly = true;
            txtMotivo.ReadOnly = true;
            txtReceita.ReadOnly = true;
            txtRetorno.ReadOnly = true;
            dt_consulta.Enabled = false;
            dt_retorno.Enabled = false;
            cmbDentista.Enabled = false;
            cmbPaciente.Enabled = false;
            btn_Salvar.Enabled = false;
            btn_Cancelar.Enabled = false;
            btn_Novo.Enabled = true;
            btn_Excluir.Enabled = false;

        }

        public void desbloquearCampos()
        {
            txtIdConsulta.ReadOnly = false;
            txtDiag.ReadOnly = false;
            txtMotivo.ReadOnly = false;
            txtReceita.ReadOnly = false;
            dt_consulta.Enabled = true;
            cmbDentista.Enabled = true;
            cmbPaciente.Enabled = true;
            btn_Salvar.Enabled = true;
            btn_Cancelar.Enabled = true;
            btn_Novo.Enabled = false;
            btn_Excluir.Enabled = false;
        }

        private void rbRs_CheckedChanged(object sender, EventArgs e)
        {
            txtRetorno.ReadOnly = false;
            dt_retorno.Enabled = true;
        }

        private void rbRn_CheckedChanged(object sender, EventArgs e)
        {
            dt_retorno.Enabled = false;
            txtRetorno.ReadOnly = true;
            txtRetorno.Text = "";
        }

        private void FrmConsulta_Load(object sender, EventArgs e)
        {
            bloquearCampos();
            atualizarGrid();
            comboxPaciente();
            comboxDentista();
        }

        public void lerDados()
        {
            objconsulta = new Consulta();

            objconsulta.IdCon = int.Parse(txtIdConsulta.Text.Trim());
            objconsulta.Motivo = txtMotivo.Text;
            objconsulta.Receita = txtReceita.Text;
            objconsulta.Diagnostico = txtDiag.Text;
            objconsulta.dt_consulta = dt_consulta.Value;
            objconsulta.MotivoR = txtRetorno.Text;
            objconsulta.dt_retorno = dt_retorno.Value;

            objconsulta.IdDentista = int.Parse(cmbDentista.SelectedValue.ToString());
            objconsulta.IdPaciente = int.Parse(cmbPaciente.SelectedValue.ToString());
            cmbPaciente.DisplayMember = "Nome";

        }

        public void limparCampos()
        {
            txtIdConsulta.Text = "";
            txtMotivo.Text = "";
            txtReceita.Text = "";
            txtDiag.Text = "";
            txtRetorno.Text = "";
            dt_consulta.Value = DateTime.Today;
            dt_retorno.Value = DateTime.Today;
            cmbPaciente.Text = "";
            cmbDentista.Text = "";
        }

        public void atualizarGrid()
        {
            List<Consulta> listConsulta = new List<Consulta>();
            con.conectar();
            SqlDataReader reader;
            reader = con.exeConsulta("select * from tb_consulta");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Consulta consulta = new Consulta();
                    consulta.IdCon = reader.GetInt32(0);
                    consulta.Motivo = reader.GetString(1);
                    consulta.dt_consulta = reader.GetDateTime(2);
                    consulta.Diagnostico = reader.GetString(3);
                    consulta.Receita = reader.GetString(4);
                    consulta.dt_retorno = reader.GetDateTime(5);
                    consulta.MotivoR = reader.GetString(6);
                    consulta.IdPaciente = reader.GetInt32(7);
                    consulta.IdDentista = reader.GetInt32(8);

                    listConsulta.Add(consulta);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }

            dgvConsulta.DataSource = null;
            dgvConsulta.DataSource = listConsulta;
        }

        private void dgvConsulta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_Excluir.Enabled = true;

            txtIdConsulta.Text = dgvConsulta.CurrentRow.Cells[0].Value.ToString();
            txtMotivo.Text = dgvConsulta.CurrentRow.Cells[1].Value.ToString();
            dt_consulta.Value = DateTime.Parse(dgvConsulta.CurrentRow.Cells[2].Value.ToString());
            txtDiag.Text = dgvConsulta.CurrentRow.Cells[3].Value.ToString();
            txtReceita.Text = dgvConsulta.CurrentRow.Cells[4].Value.ToString();
            txtRetorno.Text = dgvConsulta.CurrentRow.Cells[6].Value.ToString();
            cmbPaciente.Text = dgvConsulta.CurrentRow.Cells[7].Value.ToString();
            cmbDentista.Text = dgvConsulta.CurrentRow.Cells[8].Value.ToString();                   

            if (!dgvConsulta.CurrentRow.Cells[5].Value.ToString().Equals("01/01/0001 00:00:00"))
            {
                dt_retorno.Value = DateTime.Parse(dgvConsulta.CurrentRow.Cells[5].Value.ToString());
            }

            //rbRs.Checked = dgvConsulta.CurrentRow.Cells[6].Value !=null ? true : false;
            //rbRn.Checked = dgvConsulta.CurrentRow.Cells[6].Value.Equals("") ? true : false;

        }

        private void btn_Novo_Click(object sender, EventArgs e)
        {
            limparCampos();
            con.conectar();
            desbloquearCampos();
            txtIdConsulta.Focus();
        }

        private void btn_Salvar_Click(object sender, EventArgs e)
        {
            lerDados();
            String sql = "insert into tb_consulta " +
                "values(" + objconsulta.IdCon + ", '" +
                objconsulta.Motivo + "', '" +
                objconsulta.dt_consulta + "', '" +
                objconsulta.Diagnostico + "', '" +
                objconsulta.Receita + "', '" +
                objconsulta.dt_retorno + "', '" +
                objconsulta.MotivoR + "', " +
                objconsulta.IdPaciente + ", " +
                objconsulta.IdDentista + ")";


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

        private void btn_Excluir_Click(object sender, EventArgs e)
        {
            if (txtIdConsulta.Text.Equals(""))
            {
                MessageBox.Show("Selecione um campo primeiro!");
            }
            else
            {
                String sql = "delete from tb_consulta where id = " + txtIdConsulta.Text;
                con.executar(sql);
                atualizarGrid();
            }
        }

        private void comboxPaciente()
        {
            List<Paciente> listPaciente = new List<Paciente>();
            con.conectar();
            SqlDataReader reader;
            reader = con.exeConsulta("select id, nome from tb_paciente");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Paciente paciente = new Paciente();
                    paciente.Id = reader.GetInt32(0);
                    paciente.Nome = reader.GetString(1);                
                    listPaciente.Add(paciente);
                }
                
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }
            reader.Close();
            cmbPaciente.DataSource = listPaciente;
            cmbPaciente.DisplayMember = "Nome";
            cmbPaciente.ValueMember = "Id";
        }

        private void comboxDentista()
        {
            List<Dentista> listDentista = new List<Dentista>();
            con.conectar();
            SqlDataReader reader;
            reader = con.exeConsulta("select id, nome from tb_dentista");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Dentista dentista = new Dentista();
                    dentista.Id = reader.GetInt32(0);
                    dentista.Nome = reader.GetString(1);
                    listDentista.Add(dentista);
                }
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }
            reader.Close();
            cmbDentista.DataSource = listDentista;
            cmbDentista.DisplayMember = "Nome";
            cmbDentista.ValueMember = "Id";
        }




    }
}


