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


    public partial class KitapHakkında : Form
    {

        public KitapHakkında(string kitapAdı, string yazar, string tur, string yayınTarihi, string sayfaSayısı)
        {
            InitializeComponent();
            label1.Text = kitapAdı;
            label2.Text = yazar;
            label3.Text = tur;
            label4.Text = yayınTarihi;
            label5.Text = sayfaSayısı;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnaSayfa anaSayfa = new AnaSayfa();
            anaSayfa.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kitapAdi = label1.Text;
            string yazar = label2.Text;
            string tur = label3.Text;
            string yayinTarihi = label4.Text;
            string sayfaSayisi = label5.Text;

            string kullaniciMaili = CurrentUser.Mail;

            DateTime kiralamaTarihi = DateTime.Now;

            DateTime iadeTarihi = kiralamaTarihi.AddDays(7);

            try
            {
                SqlConnection baglanti = new SqlConnection("Data Source = BARAN\\SQLEXPRESS; Initial Catalog = Library; Integrated Security = True;");

                string kiralamaQuery = "INSERT INTO KiralananKitaplar (kitapAdi, kullaniciMaili, kiralamaTarihi, iadeTarihi) VALUES (@kitapAdi, @kullaniciMaili, @kiralamaTarihi, @iadeTarihi)";
                SqlCommand kiralamaKomut = new SqlCommand(kiralamaQuery, baglanti);

                kiralamaKomut.Parameters.AddWithValue("@kitapAdi", kitapAdi);
                kiralamaKomut.Parameters.AddWithValue("@kullaniciMaili", kullaniciMaili); 
                kiralamaKomut.Parameters.AddWithValue("@kiralamaTarihi", kiralamaTarihi);
                kiralamaKomut.Parameters.AddWithValue("@iadeTarihi", iadeTarihi);

                baglanti.Open();
                kiralamaKomut.ExecuteNonQuery();

                string stokQuery = "UPDATE Kitaplar SET stok = stok - 1 WHERE kitapAdı = @kitapAdı";
                SqlCommand stokKomut = new SqlCommand(stokQuery, baglanti);
                stokKomut.Parameters.AddWithValue("@kitapAdı", kitapAdi);

                stokKomut.ExecuteNonQuery();

                baglanti.Close();

                MessageBox.Show($"Keyifli okumalar dileriz. Lütfen belirtilen tarihlerde iade edin. İade Tarihi: {iadeTarihi.ToString("dd/MM/yyyy")}");

                AnaSayfa anaSayfa = new AnaSayfa();
                anaSayfa.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }

    public static class CurrentUser
    {
        public static string Mail { get; set; }
    }

}
