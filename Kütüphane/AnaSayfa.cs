using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane
{
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Roman roman = new Roman();
            roman.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Şiir şiir = new Şiir();
            şiir.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BilimKurgu bilimKurgu = new BilimKurgu();
            bilimKurgu.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Fantastik fantastik = new Fantastik();
            fantastik.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Felsefe felsefe = new Felsefe();
            felsefe.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Tarih tarih = new Tarih();
            tarih.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            İade iade = new İade();
            iade.Show();
            this.Hide();
        }

        private void AnaSayfa_Load(object sender, EventArgs e)
        {

        }
    }
}
