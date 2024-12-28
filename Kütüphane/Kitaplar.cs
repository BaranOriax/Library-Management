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
    public partial class Kitaplar : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source = BARAN\\SQLEXPRESS; Initial Catalog = Library; Integrated Security = True;");
        SqlDataAdapter da;
        SqlCommand cmd;

        public Kitaplar()
        {
            InitializeComponent();
        }

        private void Kitaplar_Load(object sender, EventArgs e)
        {
            KitaplarıYukle();
            toplamStokAdedi();
            DataGridViewStilAyarla();
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

        private void KitaplarıYukle()
        {
            try
            {
                string query = "SELECT kitapAdı, stok FROM Kitaplar";
                cmd = new SqlCommand(query, baglanti);
                da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                dataGridView1.Columns[0].HeaderText = "Kitap Adı";
                dataGridView1.Columns[1].HeaderText = "Stok";

                for (int i = 2; i < dataGridView1.Columns.Count; i++)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string kitapAdı = dataGridView1.SelectedRows[0].Cells["kitapAdı"].Value.ToString();
                int yeniStok = Convert.ToInt32(textBox1.Text);

                try
                {
                    baglanti.Open();
                    string updateQuery = "UPDATE Kitaplar SET stok = @stok WHERE kitapAdı = @kitapAdı";
                    cmd = new SqlCommand(updateQuery, baglanti);
                    cmd.Parameters.AddWithValue("@stok", yeniStok);
                    cmd.Parameters.AddWithValue("@kitapAdı", kitapAdı);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Stok güncellendi.");
                    KitaplarıYukle();
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

            toplamStokAdedi();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
            this.Hide();
        }

        private void toplamStokAdedi()
        {
            try
            {
                baglanti.Open();
                string query = "SELECT SUM(CAST(stok AS INT)) AS ToplamStok FROM Kitaplar";
                cmd = new SqlCommand(query, baglanti);
                var toplamStok = cmd.ExecuteScalar();

                label2.Text = toplamStok.ToString();
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
