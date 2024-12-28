using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
namespace Kütüphane
{
    public partial class UserGiris : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source = BARAN\\SQLEXPRESS; Initial Catalog = Library; Integrated Security = True; ");
        SqlCommand cmd;
        public UserGiris()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mail = textBox1.Text;
            string password = textBox2.Text;

            CurrentUser.Mail = mail;


            try
            {
                baglanti.Open();
                string query = "SELECT * FROM Users WHERE mail = @mail AND password = @password";
                cmd = new SqlCommand(query, baglanti);
                cmd.Parameters.AddWithValue("@mail", mail);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    MessageBox.Show("Giriş başarılı!");
                    AnaSayfa form3 = new AnaSayfa(); 
                    form3.Show();
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

        //yeni forma gitme 
        private void button2_Click(object sender, EventArgs e)
        {
            Kayıt form2 = new Kayıt();
            form2.Show();
            this.Hide();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            AdminGiris adminGiris = new AdminGiris();
            adminGiris.Show();
            this.Hide();
        }
    }

}
