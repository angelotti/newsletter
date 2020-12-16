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
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Text.RegularExpressions;

namespace Newsletter_System_2._0
{
    public partial class NewsletterForm : Form
    {
        public NewsletterForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregarGrid();
            //Email e Senha
            txtEmail.Text = "MEU EMAIL";
            txtSenha.Text = "MINHA SENHA";
        }
        private void EnviarEmail()
        {
            try
            {

                bool bValidaEmail = ValidaEnderecoEmail(txtEmail.Text);

                if (bValidaEmail == false)
                    MessageBox.Show("Endereço de e-mail inválido");

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(txtEmail.Text);

                string[] Para = txtPara.Text.Split(';');

                foreach (string Muitos in Para)
                {
                    mm.To.Add(new MailAddress(Muitos));
                }

                mm.Subject = txtAssunto.Text;

                AlternateView aw = AlternateView.CreateAlternateViewFromString(txtMensagem.Text, null, "text/html");
                mm.AlternateViews.Add(aw);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.live.com";
                smtp.Port = 587;
                NetworkCredential nc = new NetworkCredential(txtEmail.Text, txtSenha.Text);
                smtp.EnableSsl = true;
                smtp.Credentials = nc;
                smtp.Send(mm);

                MessageBox.Show("Email enviado para " + txtPara.Text + " [" + DateTime.Now.ToString() + "].");

                txtPara.Clear();
            }
            catch
            {

                if (DialogResult.OK == MessageBox.Show("Ocorreu um erro ao enviar o e-mail. Recomenda-se fechar a aplicação", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error))
                {

                    Application.Exit();

                }
            }

            

        }

        private void CarregarGrid()
        {
            BLL.Newsletter newsletter = new BLL.Newsletter();
            dgvEmail.DataSource = newsletter.Listar().Tables[0];
            rbtAtivos.Checked = true;

        }

        public static string RemoveDuplicados(string v)
        {

            var d = new Dictionary<string, bool>();

            StringBuilder b = new StringBuilder();

            string[] a = v.Split(new char[] {';'},
            StringSplitOptions.RemoveEmptyEntries);

            foreach (string current in a)
            {

                string lower = current.ToLower();

                if (!d.ContainsKey(lower))
                {
                    b.Append(current).Append(';');
                    d.Add(lower, true);
                }
            }

            return b.ToString().Trim();
        }


        public static bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {

                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LimparCheck()
        {
            foreach(DataGridViewRow gvrow in dgvEmail.Rows)
            {
                gvrow.Cells["selecionar"].Value = false;
            }
        }

        ArrayList aAnexosEmail;

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Insira o remetente.", "Campo em branco");
                return;
            }

            if (String.IsNullOrEmpty(txtSenha.Text))
            {
                MessageBox.Show("Insira a senha do remetente.", "Campo em branco");
                return;
            }

            if (String.IsNullOrEmpty(txtPara.Text))
            {
                MessageBox.Show("Insira o destinatário.", "Campo em branco");
                return;
            }

            if (String.IsNullOrEmpty(txtAssunto.Text))
            {
                MessageBox.Show("Insira o Assunto.", "Campo em branco");
                return;
            }

            if (String.IsNullOrEmpty(txtMensagem.Text))
            {
                MessageBox.Show("Insira a mensagem.", "Campo em branco");
                return;
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("Tem certeza que as informações estão corretas?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {

                    txtPara.Text = txtPara.Text.TrimEnd(';');

                    EnviarEmail();

                }
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtPara.Clear(); 
        }
        private void btnAtivar_Click(object sender, EventArgs e)
        {
            if (dgvEmail.CurrentRow.Index == dgvEmail.Rows.Count - 0)
            {
                MessageBox.Show("SELECIONE ALGUM CLIENTE", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                for (int i = 0; i < dgvEmail.Rows.Count - 0;)
                {
                    BLL.Newsletter newsletter = new BLL.Newsletter();
                    newsletter.situacao = "Ativo";
                    newsletter.id = Convert.ToInt32(dgvEmail.SelectedRows[i].Cells[0].Value);
                    newsletter.Atualizar();
                    MessageBox.Show("CLIENTE ATIVO!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rbtAtivos.Checked = true;
                    break;

                }
            }
        }

        private void btnDesativar_Click(object sender, EventArgs e)
        {
            if (dgvEmail.CurrentRow.Index == dgvEmail.Rows.Count - 0)
            {
                MessageBox.Show("SELECIONE ALGUM CLIENTE", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                for (int i = 0; i < dgvEmail.Rows.Count - 0;)
                {
                    BLL.Newsletter newsletter = new BLL.Newsletter();
                    newsletter.situacao = "Inativo";
                    newsletter.id = Convert.ToInt32(dgvEmail.SelectedRows[i].Cells[0].Value);
                    newsletter.Atualizar();
                    MessageBox.Show("CLIENTE INATIVO!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rbtInativos.Checked = true;
                    break;
                }

                foreach (DataGridViewRow row in dgvEmail.Rows)
                {
                    string[] array = txtPara.Text.Split(';');
                    foreach (string Percorra in array)
                    {
                        string source = txtPara.Text;
                        string Remove = row.Cells["dgv_email"].Value.ToString() + ';';
                        string Result = string.Empty;
                        int i = source.IndexOf(Remove);
                        if (i >= 0)
                        {
                            Result = source.Remove(i, Remove.Length);
                            txtPara.Text = Result;
                        }
                    }
                }

            }
        }

        private void rbtAtivos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtAtivos.Checked)
            {
                BLL.Newsletter newsletter = new BLL.Newsletter();
                dgvEmail.DataSource = newsletter.ListarAtivos().Tables[0];
                foreach (DataGridViewRow row in dgvEmail.Rows)
                {
                    string para = row.Cells[3].Value.ToString() + ";";
                    txtPara.Text += para;
                    
                    string a = RemoveDuplicados(txtPara.Text);
                    txtPara.Text = a;

                }

            }
        }

        private void rbtInativos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtInativos.Checked)
            {
                BLL.Newsletter newsletter = new BLL.Newsletter();
                dgvEmail.DataSource = newsletter.ListarInativos().Tables[0];
            }
        }

        private void rbtTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtTodos.Checked)
            {
                BLL.Newsletter newsletter = new BLL.Newsletter();
                dgvEmail.DataSource = newsletter.Listar().Tables[0];
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvEmail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEmail.SelectedRows.Count == 1)
            {
                DataRowView dr = (DataRowView)dgvEmail.Rows[dgvEmail.SelectedRows[0].Index].DataBoundItem;
                string[] email = txtPara.Text.Split(';');
                foreach (string Adiciona in email)
                {
                    if (dr["Email"].ToString().Equals("Ativo"))
                    {
                        txtPara.Text += dr["Email"].ToString() + ';';
                    }
                    else if(dr["Email"].ToString().Equals("Inativo"))
                    {
                    }
                    
                }
                string a = RemoveDuplicados(txtPara.Text);
                txtPara.Text = a;
            }
        }
    }
}
