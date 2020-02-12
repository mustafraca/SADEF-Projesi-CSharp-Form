using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace SADEF
{
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }

        OleDbConnection SADEFBaglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SADEF.mdb");
        public static string resimyolu;

        string[] WherePictures;
        int whichPict = 0;
        int i;
        void resimleriGetir()
        {
            WherePictures = Directory.GetFiles("../../ArkaPlan", "*.jpg");
            foreach (string myPictures in WherePictures)
            {
                Button buton = new Button();
                buton.Width = buton.Height = 68;
                buton.Margin = new Padding(5);
                buton.BackgroundImage = Image.FromFile(myPictures);
                buton.BackgroundImageLayout = ImageLayout.Stretch;
                buton.Tag = i++;
                buton.Cursor = Cursors.Hand;
                flowLayoutPanel1.Controls.Add(buton);
                buton.Click += buton_Click;
            }
        }

        private void Ayarlar_Load(object sender, EventArgs e)
        {
            resimleriGetir();
        }

        void buton_Click(object sender, EventArgs e)
        {
            Button tiklanan = sender as Button;
            resimyolu = Convert.ToString(WherePictures[Convert.ToInt32(tiklanan.Tag)]);
            pictureBox1.Image = Image.FromFile(WherePictures[Convert.ToInt32(tiklanan.Tag)]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SADEFBaglanti.Open();
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = SADEFBaglanti;
                komut.CommandText = "UPDATE " + KGirisForm.Kullaniciadi2 + " SET ResimYolu=@ResimYolu";
                komut.Parameters.AddWithValue("@ResimYolu", resimyolu);
                komut.ExecuteNonQuery();
                SADEFBaglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SADEF SADEF = new SADEF();
            SADEF.Show(); this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            whichPict--;
            if (whichPict < 0) whichPict = WherePictures.Length - 1;
            pictureBox1.ImageLocation = WherePictures[whichPict];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            whichPict++;
            if (whichPict > WherePictures.Length - 1) whichPict = 0;
            pictureBox1.ImageLocation = WherePictures[whichPict];
        }
    }
}
