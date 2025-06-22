using Guna.UI2.WinForms;
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
    public partial class Form5 : Form
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=gestaonotas;Trusted_Connection=True;";

        public Form5()
        {
            InitializeComponent();
        }

        private void CarregarCategorias()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, nome FROM Categoria";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                guna2ComboBox2.DataSource = dt;
                guna2ComboBox2.DisplayMember = "nome";
                guna2ComboBox2.ValueMember = "id";
            }
        }

        private void CarregarEstados()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, nome FROM EstadoNota";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                guna2ComboBox1.DataSource = dt;
                guna2ComboBox1.DisplayMember = "nome";
                guna2ComboBox1.ValueMember = "id";
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            CarregarEstados();
            CarregarCategorias();
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string titulo = guna2TextBox1.Text.Trim();
            string texto = richTextBox1.Text.Trim();

            object categoriaSelecionada = guna2ComboBox2.SelectedValue;
            object estadoSelecionado = guna2ComboBox1.SelectedValue;

            if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(texto))
            {
                MessageBox.Show("O título e o texto da nota devem ser preenchidos.", "Campos Vazios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (categoriaSelecionada == null || estadoSelecionado == null)
            {
                MessageBox.Show("Por favor, selecione uma categoria e um estado.", "Seleção Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (titulo.Length > 150)
            {
                MessageBox.Show("O título não pode ter mais de 150 caracteres.", "Título Demasiado Longo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int categoriaId = Convert.ToInt32(categoriaSelecionada);
                int estadoId = Convert.ToInt32(estadoSelecionado);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Nota (titulo, texto, categoriaId, estadoNotaId) VALUES (@Titulo, @Texto, @CategoriaId, @EstadoNotaId)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Titulo", titulo);
                        cmd.Parameters.AddWithValue("@Texto", texto);
                        cmd.Parameters.AddWithValue("@CategoriaId", categoriaId);
                        cmd.Parameters.AddWithValue("@EstadoNotaId", estadoId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Nota criada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao criar a nota: " + ex.Message, "Erro de Base de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
