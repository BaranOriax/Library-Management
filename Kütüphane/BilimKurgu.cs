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
    public partial class BilimKurgu : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source = BARAN\\SQLEXPRESS; Initial Catalog = Library; Integrated Security = True;");
        SqlDataAdapter da;
        SqlCommand cmd;

        public BilimKurgu()
        {
            InitializeComponent();
        }

        private void BilimKurgu_Load(object sender, EventArgs e)
        {
            verileriYukle();
            DataGridViewStilAyarla();
        }

        public void verileriYukle()
        {
            try
            {
                string query = "SELECT kitapAdı FROM Kitaplar WHERE tür = 'Bilim'";
                cmd = new SqlCommand(query, baglanti);
                baglanti.Open();
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "Kitap adı";
                dataGridView1.Columns[0].Visible = true;
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Visible = false;
                }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0) 
            {
                string kitapAd = dataGridView1.Rows[rowIndex].Cells["kitapAdı"].Value?.ToString();

                if (!string.IsNullOrEmpty(kitapAd))
                {
                    try
                    {
                        string query = "SELECT yazarAdı, tür, yayımYılı, sayfaSayısı FROM Kitaplar WHERE kitapAdı = @kitapAdı";
                        cmd = new SqlCommand(query, baglanti);
                        cmd.Parameters.AddWithValue("@kitapAdı", kitapAd);
                        baglanti.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string yazar = reader["yazarAdı"].ToString();
                            string tur = reader["tür"].ToString();
                            string yayinTarihi = reader["yayımYılı"].ToString();
                            string sayfaSayisi = reader["sayfaSayısı"].ToString();

                            KitapHakkında kitapBilgi = new KitapHakkında(kitapAd, yazar, tur, yayinTarihi, sayfaSayisi);
                            kitapBilgi.Show();
                            this.Hide(); 
                        }
                        else
                        {
                            MessageBox.Show("Seçilen kitabın bilgileri bulunamadı.");
                        }
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnaSayfa anasayfa = new AnaSayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
