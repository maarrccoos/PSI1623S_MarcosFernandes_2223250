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
    public partial class Form6 : Form
    {
        private readonly int _notaId;
        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=gestaonotas;Trusted_Connection=True;";

        public Form6(int notaId)
        {
            InitializeComponent();
            _notaId = notaId;
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string novoTitulo = guna2TextBox1.Text.Trim();
            string novoTexto = richTextBox1.Text.Trim();
            object novaCategoriaIdObj = guna2ComboBox2.SelectedValue;
            object novoEstadoIdObj = guna2ComboBox1.SelectedValue;

            if (string.IsNullOrEmpty(novoTitulo) || string.IsNullOrEmpty(novoTexto))
            {
                MessageBox.Show("O título e o texto da nota não podem estar vazios.", "Campos Vazios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (novaCategoriaIdObj == null || novoEstadoIdObj == null)
            {
                MessageBox.Show("Por favor, selecione uma categoria e um estado.", "Seleção Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (novoTitulo.Length > 150)
            {
                MessageBox.Show("O título não pode ter mais de 150 caracteres.", "Título Demasiado Longo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacao = MessageBox.Show("Deseja guardar as alterações efetuadas a esta nota?", "Guardar Alterações", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    int novaCategoriaId = Convert.ToInt32(novaCategoriaIdObj);
                    int novoEstadoId = Convert.ToInt32(novoEstadoIdObj);

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        const string query = @"
                    UPDATE Nota 
                    SET 
                        titulo = @Titulo, 
                        texto = @Texto, 
                        categoriaId = @CategoriaId, 
                        estadoNotaId = @EstadoNotaId 
                    WHERE 
                        id = @NotaId";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Titulo", novoTitulo);
                            cmd.Parameters.AddWithValue("@Texto", novoTexto);
                            cmd.Parameters.AddWithValue("@CategoriaId", novaCategoriaId);
                            cmd.Parameters.AddWithValue("@EstadoNotaId", novoEstadoId);
                            cmd.Parameters.AddWithValue("@NotaId", _notaId);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Nota atualizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao guardar as alterações: " + ex.Message, "Erro de Base de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CarregarDadosDaNota()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                const string query = "SELECT titulo, texto, categoriaId, estadoNotaId FROM Nota WHERE id = @NotaId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NotaId", _notaId);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                guna2TextBox1.Text = reader["titulo"].ToString();
                                richTextBox1.Text = reader["texto"].ToString();
                                guna2ComboBox2.SelectedValue = reader["categoriaId"];
                                guna2ComboBox1.SelectedValue = reader["estadoNotaId"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar os dados da nota: " + ex.Message);
                    }
                }
            }
        }
        private void CarregarCategorias()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                const string query = "SELECT id, nome FROM Categoria";
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
                const string query = "SELECT id, nome FROM EstadoNota";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2ComboBox1.DataSource = dt;
                guna2ComboBox1.DisplayMember = "nome";
                guna2ComboBox1.ValueMember = "id";
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            CarregarCategorias();
            CarregarEstados();
            CarregarDadosDaNota();
        }
    }
}
