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

namespace Kütüphane
{
    public partial class AdminGiris : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source = BARAN\\SQLEXPRESS; Initial Catalog = Library; Integrated Security = True; ");
        SqlCommand cmd;

        public AdminGiris()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserGiris userGiris = new UserGiris();
            userGiris.Show();
            this.Hide();
        }

        private void AdminGiris_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adminMail = textBox1.Text;
            string adminPassword = textBox2.Text;


            try
            {
                baglanti.Open();
                string query = "SELECT * FROM Admin WHERE adminMail = @adminMail AND adminPassword = @adminPassword";
                cmd = new SqlCommand(query, baglanti);
                cmd.Parameters.AddWithValue("@adminMail", adminMail);
                cmd.Parameters.AddWithValue("@adminPassword", adminPassword);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Hoşgeldiniz");
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hatalı mail veya şifre!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }
    }
}
