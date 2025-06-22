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
