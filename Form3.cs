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
    public partial class Form3 : Form
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=gestaonotas;Trusted_Connection=True;";

        public Form3()
        {
            InitializeComponent();
        }

        private void CarregarEstados()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT nome FROM EstadoNota";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                guna2ComboBox1.DataSource = dt;
                guna2ComboBox1.DisplayMember = "nome";
                guna2ComboBox1.ValueMember = "nome";
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {              
            this.categoriaTableAdapter.Fill(this.gestaonotasCategoria.Categoria);
            CarregarEstados();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO EstadoNota (Nome) VALUES (@nome)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", guna2TextBox1.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Sucesso", "Estado criado com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    guna2TextBox1.Text = "";
                }

                CarregarEstados();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string estadoSelecionado = guna2ComboBox1.SelectedValue?.ToString();

            if (string.IsNullOrEmpty(estadoSelecionado))
            {
                MessageBox.Show("Nenhum estado selecionado.");
                return;
            }

            DialogResult result = MessageBox.Show($"Tem certeza que deseja apagar o estado \"{estadoSelecionado}\"?", "Confirmar Exclusão" ,MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM EstadoNota WHERE Nome = @nome";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", estadoSelecionado);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                CarregarEstados();

                guna2TextBox2.Text = "";
                MessageBox.Show("Estado apagado com sucesso.");
            }
            else
            {
                MessageBox.Show("Exclusão cancelada.");
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string estadoAtual = guna2ComboBox1.SelectedValue?.ToString();
            string novoNome = guna2TextBox2.Text.Trim();

            if (string.IsNullOrEmpty(estadoAtual))
            {
                MessageBox.Show("Nenhum estado selecionado.");
                return;
            }

            if (string.IsNullOrEmpty(novoNome))
            {
                MessageBox.Show("Digite o novo nome do estado.");
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Tem certeza que deseja renomear o estado \"{estadoAtual}\" para \"{novoNome}\"?",
                "Confirmar Alteração",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE EstadoNota SET Nome = @novoNome WHERE Nome = @nomeAtual";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@novoNome", novoNome);
                        cmd.Parameters.AddWithValue("@nomeAtual", estadoAtual);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                CarregarEstados();
                guna2ComboBox1.SelectedValue = novoNome; // Seleciona o novo nome após atualizar

                MessageBox.Show("Estado renomeado com sucesso.");
                guna2TextBox2.Text = "";
            }
            else
            {
                MessageBox.Show("Alteração cancelada.");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}