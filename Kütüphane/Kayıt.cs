using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Kütüphane
{
    public partial class Kayıt : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();
        string resimDosyaYolu = "";

        public Kayıt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string isim = textBox1.Text;
            string soyisim = textBox2.Text;
            string sifre = textBox3.Text;
            string mail = textBox4.Text;

            if (string.IsNullOrWhiteSpace(isim) || string.IsNullOrWhiteSpace(soyisim) ||
                string.IsNullOrWhiteSpace(sifre) || string.IsNullOrWhiteSpace(mail))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            try
            {
                //resim ekleme
                byte[] resimVerisi = null;
                if (resimDosyaYolu != "")
                {
                    FileStream fs = new FileStream(resimDosyaYolu, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    resimVerisi = br.ReadBytes((int)fs.Length);
                    fs.Close();
                }

                // databaseye ekleme
                string query = "INSERT INTO Users(name, surname, mail, password, resim) VALUES(@isim, @soyisim, @mail, @sifre, @resim)";
                cmd = new SqlCommand(query, baglanti);
                cmd.Parameters.AddWithValue("@isim", isim);
                cmd.Parameters.AddWithValue("@soyisim", soyisim);
                cmd.Parameters.AddWithValue("@mail", mail);
                cmd.Parameters.AddWithValue("@sifre", sifre);
                cmd.Parameters.AddWithValue("@resim", resimVerisi ?? (object)DBNull.Value);

                baglanti.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt başarılı");

                UserGiris form1 = new UserGiris();
                form1.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserGiris form1 = new UserGiris();
            form1.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; 

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                resimDosyaYolu = openFileDialog.FileName; 
                pictureBox1.Image = Image.FromFile(resimDosyaYolu); 

                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void Kayıt_Load(object sender, EventArgs e)
        {

        }
    }
}
