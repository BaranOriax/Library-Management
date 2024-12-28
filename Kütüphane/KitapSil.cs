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
    public partial class KitapSil : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source=BARAN\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;");
        SqlCommand cmd;
        SqlDataAdapter da;

        public KitapSil()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
            this.Hide();
        }

        private void KitapSil_Load(object sender, EventArgs e)
        {
            KitaplariListele();
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

        private void KitaplariListele()
        {
            try
            {
                string query = "SELECT id, kitapAdı, yazarAdı, tür, stok FROM Kitaplar";
                cmd = new SqlCommand(query, baglanti);
                baglanti.Open();
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.Columns["id"].HeaderText = "Kitap ID";
                dataGridView1.Columns["kitapAdı"].HeaderText = "Kitap Adı";
                dataGridView1.Columns["yazarAdı"].HeaderText = "Yazar Adı";
                dataGridView1.Columns["tür"].HeaderText = "Tür";
                dataGridView1.Columns["stok"].HeaderText = "Stok Adedi";
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
                try
                {
                    int kitapID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                    string query = "DELETE FROM Kitaplar WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, baglanti);
                    cmd.Parameters.AddWithValue("@id", kitapID);

                    if (baglanti.State == ConnectionState.Closed)
                        baglanti.Open();

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Kitap başarıyla silindi.");
                    }
                    else
                    {
                        MessageBox.Show("Kitap silinirken bir hata oluştu.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    if (baglanti.State == ConnectionState.Open)
                        baglanti.Close();

                    KitaplariListele();
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kitabı seçin.");
            }
        }


    }
}
