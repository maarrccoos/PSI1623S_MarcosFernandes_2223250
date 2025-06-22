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
                string query = "SELECT titulo FROM Nota";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                guna2ComboBox1.DataSource = dt;
                guna2ComboBox1.DisplayMember = "titulo";
                guna2ComboBox1.ValueMember = "titulo";
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

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
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
