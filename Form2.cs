using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotasRapidas
{
    public partial class Form2 : Form
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=gestaonotas;Trusted_Connection=True;";

        public Form2()
        {
            InitializeComponent();
        }

        private void CarregarNotas()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                const string query = "SELECT id, titulo FROM Nota ORDER BY titulo ASC";

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    guna2ComboBox1.DataSource = dt;
                    guna2ComboBox1.DisplayMember = "titulo";
                    guna2ComboBox1.ValueMember = "id";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Falha ao carregar as notas: " + ex.Message);
                }
            }   
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CarregarNotas();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            this.Hide();
            form5.ShowDialog();
            this.Show();
            CarregarNotas();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecione uma nota para apagar.", "Nenhuma Nota Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacao = MessageBox.Show("Tem a certeza que deseja apagar a nota selecionada? Esta ação é irreversível.", "Confirmar Eliminação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    int notaId = Convert.ToInt32(guna2ComboBox1.SelectedValue);

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        const string query = "DELETE FROM Nota WHERE id = @NotaId";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@NotaId", notaId);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Nota eliminada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CarregarNotas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao apagar a nota: " + ex.Message, "Erro de Base de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecione uma nota para editar.", "Nenhuma Nota Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int notaIdSelecionada = Convert.ToInt32(guna2ComboBox1.SelectedValue);

                Form6 formEdicao = new Form6(notaIdSelecionada);

                formEdicao.Show();       
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar editar a nota: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
