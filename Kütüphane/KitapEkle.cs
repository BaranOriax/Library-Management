using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane
{
    public partial class KitapEkle : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=BARAN\\SQLEXPRESS; Initial Catalog=Library; Integrated Security=True;");
        SqlCommand cmd;

        public KitapEkle()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kitapAdi = textBox1.Text;
            string yazarAdi = textBox2.Text;
            string tur = comboBox1.SelectedItem.ToString();
            string yayimYili = textBox4.Text;
            string sayfaSayisi = textBox3.Text;
            string stok = textBox6.Text;

            if (string.IsNullOrEmpty(kitapAdi) || string.IsNullOrEmpty(yazarAdi) || string.IsNullOrEmpty(tur) || string.IsNullOrEmpty(yayimYili) || string.IsNullOrEmpty(sayfaSayisi) || string.IsNullOrEmpty(stok))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {

                baglanti.Open();

                string query = "INSERT INTO Kitaplar (kitapAdı, yazarAdı, tür, yayımYılı, sayfaSayısı, stok) " +
                                  "VALUES (@kitapAdı, @yazarAdı, @tür, @yayımYılı, @sayfaSayısı, @stok)";

                cmd = new SqlCommand(query, baglanti);
                cmd.Parameters.AddWithValue("@kitapAdı", kitapAdi);
                cmd.Parameters.AddWithValue("@yazarAdı", yazarAdi);
                cmd.Parameters.AddWithValue("@tür", tur);
                cmd.Parameters.AddWithValue("@yayımYılı", yayimYili);
                cmd.Parameters.AddWithValue("@sayfaSayısı", sayfaSayisi);
                cmd.Parameters.AddWithValue("@stok", stok);

                int ekle = cmd.ExecuteNonQuery();

                if (ekle > 0)
                {
                    MessageBox.Show("Kitap başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox6.Clear();
                    comboBox1.SelectedIndex = -1;

                }
                else
                {
                    MessageBox.Show("Kitap eklenemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                baglanti.Close();
            }

        }

        private void KitapEkle_Load(object sender, EventArgs e)
        {

        }
    }
}
