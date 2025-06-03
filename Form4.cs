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
    public partial class Form4 : Form
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=gestaonotas;Trusted_Connection=True;";

        public Form4()
        {
            InitializeComponent();
        }

        private void CarregarCategorias()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT nome FROM Categoria";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                guna2ComboBox1.DataSource = dt;
                guna2ComboBox1.DisplayMember = "nome";
                guna2ComboBox1.ValueMember = "nome";
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Categoria (Nome) VALUES (@nome)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", guna2TextBox1.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Sucesso", "Categoria criada com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    guna2TextBox1.Text = "";
                }

                CarregarCategorias();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string categoriaAtual = guna2ComboBox1.SelectedValue?.ToString();
            string novoNome = guna2TextBox2.Text.Trim();

            if (string.IsNullOrEmpty(categoriaAtual))
            {
                MessageBox.Show("Nenhuma categoria selecionada.");
                return;
            }

            if (string.IsNullOrEmpty(novoNome))
            {
                MessageBox.Show("Digite o novo nome da categoria.");
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Tem certeza que deseja renomear a categoria \"{categoriaAtual}\" para \"{novoNome}\"?",
                "Confirmar Alteração",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Categoria SET Nome = @novoNome WHERE Nome = @nomeAtual";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@novoNome", novoNome);
                        cmd.Parameters.AddWithValue("@nomeAtual", categoriaAtual);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                CarregarCategorias();
                guna2ComboBox1.SelectedValue = novoNome; // Seleciona o novo nome após atualizar

                MessageBox.Show("Categoria renomeada com sucesso.");
                guna2TextBox2.Text = "";
            }
            else
            {
                MessageBox.Show("Alteração cancelada.");
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            CarregarCategorias();
        }
    }
}
