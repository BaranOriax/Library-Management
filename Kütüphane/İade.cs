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
    public partial class İade : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source = BARAN\\SQLEXPRESS; Initial Catalog = Library; Integrated Security = True;");
        SqlCommand cmd = new SqlCommand();
        public İade()
        {
            InitializeComponent();
        }


        private void İade_Load(object sender, EventArgs e)
        {
            KullaniciKitaplariniGoruntule();
        }
        private void KullaniciKitaplariniGoruntule()
        {
            try
            {
                string kullaniciMaili = CurrentUser.Mail;

                string query = "SELECT id, kitapAdi, kiralamaTarihi, iadeTarihi FROM KiralananKitaplar WHERE kullaniciMaili = @kullaniciMaili AND iadeTarihi > GETDATE()";
                cmd = new SqlCommand(query, baglanti);
                cmd.Parameters.AddWithValue("@kullaniciMaili", kullaniciMaili);

                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                dataGridView1.Columns[0].HeaderText = "Kitap ID";
                dataGridView1.Columns[1].HeaderText = "Kitap Adı";
                dataGridView1.Columns[2].HeaderText = "Kiralama Tarihi";
                dataGridView1.Columns[3].HeaderText = "İade Tarihi";

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); 
                string kitapAdı = dataGridView1.SelectedRows[0].Cells[1].Value.ToString(); 

                try
                {
                    baglanti.Open();
                    string stokQuery = "UPDATE Kitaplar SET stok = stok + 1 WHERE kitapAdı = @kitapAdı";
                    SqlCommand stokKomut = new SqlCommand(stokQuery, baglanti);
                    stokKomut.Parameters.AddWithValue("@kitapAdı", kitapAdı);
                    stokKomut.ExecuteNonQuery();

                    string iadeQuery = "DELETE FROM KiralananKitaplar WHERE id = @id";
                    SqlCommand iadeKomut = new SqlCommand(iadeQuery, baglanti);
                    iadeKomut.Parameters.AddWithValue("@id", id);
                    iadeKomut.ExecuteNonQuery();

                    baglanti.Close();
                    MessageBox.Show("Kitap başarıyla iade edildi.");

                    KullaniciKitaplariniGoruntule();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen iade etmek için bir kitap seçin.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnaSayfa anaSayfa = new AnaSayfa();
            anaSayfa.Show();
            this.Hide();
        }
    }
}
