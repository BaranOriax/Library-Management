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
    public partial class Kullanıcılar : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source = BARAN\\SQLEXPRESS; Initial Catalog = Library; Integrated Security = True;");

        public Kullanıcılar()
        {
            InitializeComponent();
        }

        private void Kullanıcılar_Load(object sender, EventArgs e)
        {
            KullanıcılarıGörüntüle();
            DataGridViewStilAyarla();
        }

        private void KullanıcılarıGörüntüle()
        {
            try
            {

                string query = "SELECT name, surname, mail, password, resim FROM Users";
                SqlCommand cmd = new SqlCommand(query, baglanti);

                baglanti.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.Columns["mail"].Visible = false;
                dataGridView1.Columns["password"].Visible = false;
                dataGridView1.Columns["resim"].Visible = false;

                dataGridView1.Columns[0].HeaderText = "İsim";
                dataGridView1.Columns[1].HeaderText = "Soyisim";

                dataGridView1.Columns[0].Width = 150;
                dataGridView1.Columns[1].Width = 150;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private void DataGridViewStilAyarla()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
            this.Hide();
        }

        private int KullaniciKitapSayisi(string mail)
        {
            int kitapSayisi = 0;
            try
            {
                SqlConnection baglanti = new SqlConnection("Data Source = BARAN\\SQLEXPRESS; Initial Catalog = Library; Integrated Security = True;");
                string query = "SELECT COUNT(*) FROM KiralananKitaplar WHERE kullaniciMaili = @mail AND iadeTarihi > GETDATE()";
                SqlCommand cmd = new SqlCommand(query, baglanti);
                cmd.Parameters.AddWithValue("@mail", mail);

                baglanti.Open();

                kitapSayisi = Convert.ToInt32(cmd.ExecuteScalar());
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            return kitapSayisi;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                string name = row.Cells["name"].Value.ToString();
                string surname = row.Cells["surname"].Value.ToString();
                string mail = row.Cells["mail"].Value.ToString();
                string password = row.Cells["password"].Value.ToString();

                label1.Text = "İsim: " + name;
                label2.Text = "Soyisim: " + surname;
                label3.Text = "Mail: " + mail;
                label4.Text = "Şifre: " + password;

                int kiralananKitapSayisi = KullaniciKitapSayisi(mail);
                label5.Text = "Toplam Kiralanan Kitap: " + kiralananKitapSayisi;

                byte[] imageData = row.Cells["resim"].Value as byte[];
                if (imageData != null)
                {
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir kullanıcı seçin.");
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
