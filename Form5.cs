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

        private void CarregarCategorias()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT nome FROM Categoria";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                guna2ComboBox2.DataSource = dt;
                guna2ComboBox2.DisplayMember = "nome";
                guna2ComboBox2.ValueMember = "nome";
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

        }
    }
}
