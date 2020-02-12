using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace SADEF
{
    public partial class SADEF : Form
    {
        public SADEF()
        {
            InitializeComponent();
        }

        OleDbConnection SADEFBaglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SADEF.mdb");
        OleDbCommand komut;
        OleDbDataReader reader;
        public static byte kaydetbuton = 0;

        private void resimguncelle()
        {
            try
            {
                komut = new OleDbCommand();
                SADEFBaglanti.Open();
                komut.Connection = SADEFBaglanti;
                komut.CommandText = "SELECT * FROM " + KGirisForm.Kullaniciadi2 + "";
                reader = komut.ExecuteReader();
                if (reader.Read())
                    if ("" != reader["ResimYolu"].ToString())
                    { panel1.BackgroundImage = Image.FromFile(reader["ResimYolu"].ToString()); }
                SADEFBaglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void textclear(Control ctl)
        {
            foreach (Control item in ctl.Controls)
            {
                if (item is TextBox) ((TextBox)item).Clear();
                if (item.Controls.Count > 0) textclear(item);
            }
        }

        private void SADEF_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            resimguncelle();
            tabControl1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            pictureBox6.Visible = false;
            label12.Text = KGirisForm.Kullaniciadi2;
            label17.Text = KGirisForm.Kullaniciadi;
            try
            {
                komut = new OleDbCommand();
                SADEFBaglanti.Open();
                komut.Connection = SADEFBaglanti;
                komut.CommandText = "SELECT * FROM " + KGirisForm.Kullaniciadi + "";
                reader = komut.ExecuteReader();
                while (reader.Read())
                    listBox1.Items.Add(reader["AdSoyad"]);
                komut.Dispose();
                komut = new OleDbCommand();
                komut.Connection = SADEFBaglanti;
                komut.CommandText = "SELECT * FROM " + KGirisForm.Kullaniciadi2 + "";
                reader = komut.ExecuteReader();
                if (reader.Read())
                {
                    label15.Text = reader["Ad"].ToString();
                    label16.Text = reader["Soyad"].ToString();
                }
                SADEFBaglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
}

        private void SADEF_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SADEFBaglanti.Open();
                string aktar = Convert.ToString(listBox1.SelectedItem);
                komut = new OleDbCommand("SELECT * FROM " + KGirisForm.Kullaniciadi + " WHERE AdSoyad='" + aktar + "'", SADEFBaglanti);
                komut.Connection = SADEFBaglanti;
                reader = komut.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["AdSoyad"].ToString();
                    textBox2.Text = reader["YSehir"].ToString();
                    textBox3.Text = reader["Semt"].ToString();
                    textBox4.Text = reader["CTel"].ToString();
                    textBox5.Text = reader["ETel"].ToString();
                    textBox6.Text = reader["Burc"].ToString();
                    maskedTextBox1.Text = reader["DGunu"].ToString();
                    richTextBox1.Text = reader["Adres"].ToString();
                    richTextBox2.Text = reader["Bilgi"].ToString();
                }
                SADEFBaglanti.Close();
                button2.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            listBox1.SetSelected(0, false);
            if (listBox1.FindString(textBox7.Text) != -1)
            {
                listBox1.SetSelected(listBox1.FindString(textBox7.Text), true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kaydetbuton = 1;
            tabControl1.Enabled = true;
            textclear(this);
            maskedTextBox1.Clear();
            richTextBox1.Clear();
            richTextBox2.Clear();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kaydetbuton = 0;
            tabControl1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (kaydetbuton == 1)
            {
                DialogResult cevap = MessageBox.Show("Kişi Kaydedilsin mi?", "Bilgilendirme Mesajı",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (cevap == DialogResult.Yes)
                {
                    if (textBox1.Text != "")
                    {
                        string ad = textBox1.Text.ToString();
                        try
                        {
                            tabControl1.Enabled = false;
                            textBox1.Enabled = true;
                            SADEFBaglanti.Open();
                            string ekle = "INSERT INTO " + KGirisForm.Kullaniciadi + " (AdSoyad,YSehir,Semt,CTel,ETel,Burc,DGunu,Adres,Bilgi) VALUES (@AdSoyad,@YSehir,@Semt,@CTel,@ETel,@Burc,@DGunu,@Adres,@Bilgi)";
                            OleDbCommand komut = new OleDbCommand(ekle, SADEFBaglanti);
                            komut.Parameters.AddWithValue("@AdSoyad", textBox1.Text);
                            komut.Parameters.AddWithValue("@YSehir", textBox2.Text);
                            komut.Parameters.AddWithValue("@Semt", textBox3.Text);
                            komut.Parameters.AddWithValue("@CTel", textBox4.Text);
                            komut.Parameters.AddWithValue("@ETel", textBox5.Text);
                            komut.Parameters.AddWithValue("@Burc", textBox6.Text);
                            komut.Parameters.AddWithValue("@DGunu", maskedTextBox1.Text);
                            komut.Parameters.AddWithValue("@Adres", richTextBox1.Text);
                            komut.Parameters.AddWithValue("@Bilgi", richTextBox2.Text);
                            komut.ExecuteNonQuery();
                            komut.Connection = SADEFBaglanti;
                            komut.CommandText = "SELECT AdSoyad FROM " + KGirisForm.Kullaniciadi + "";
                            reader = komut.ExecuteReader();
                            listBox1.Items.Clear();
                            while (reader.Read())
                            {
                                listBox1.Items.Add(reader["AdSoyad"]);
                            }
                            komut.Dispose();
                            SADEFBaglanti.Close();
                            button1.Enabled = true;
                            button2.Enabled = false;
                            button3.Enabled = false;
                            button4.Enabled = false;
                            button5.Enabled = false;
                            SADEFBaglanti.Close();
                        }
                        catch (Exception hata)
                        {
                            MessageBox.Show(hata.Message);
                        }
                        listBox1.SelectedItem = ad;
                    }
                    else
                    {
                        MessageBox.Show("Ad Soyad Kısmı Boş Bırakılamaz!", "Bilgilendirme Mesajı",
                            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        textBox1.Focus();
                    }
                }
                else if (cevap == DialogResult.No)
                {
                    tabControl1.Enabled = false;
                    textclear(this);
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    listBox1.SelectedIndex = 0;
                }
            }
            else
            {

                DialogResult cevap = MessageBox.Show("Kişi Güncellesin mi?", "Bilgilendirme Mesajı",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (cevap == DialogResult.Yes)
                {
                    try
                    {
                        tabControl1.Enabled = false;
                        textBox1.Enabled = false;
                        komut = new OleDbCommand();
                        SADEFBaglanti.Open();
                        komut.Connection = SADEFBaglanti;
                        komut.CommandText = "UPDATE " + KGirisForm.Kullaniciadi + " SET YSehir=@YSehir,Semt=@Semt,CTel=@CTel,ETel=@ETel,Burc=@Burc,DGunu=@DGunu,Adres=@Adres,Bilgi=@Bilgi WHERE AdSoyad='" + listBox1.SelectedItem.ToString() + "'";
                        komut.Parameters.AddWithValue("@YSehir", textBox2.Text);
                        komut.Parameters.AddWithValue("@Semt", textBox3.Text);
                        komut.Parameters.AddWithValue("@CTel", textBox4.Text);
                        komut.Parameters.AddWithValue("@ETel", textBox5.Text);
                        komut.Parameters.AddWithValue("@Burc", textBox6.Text);
                        komut.Parameters.AddWithValue("@DGunu", maskedTextBox1.Text);
                        komut.Parameters.AddWithValue("@Adres", richTextBox1.Text);
                        komut.Parameters.AddWithValue("@Bilgi", richTextBox2.Text);
                        komut.ExecuteNonQuery();
                        SADEFBaglanti.Close();
                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = false;
                        button4.Enabled = true;
                        button5.Enabled = true;
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show(hata.Message);
                    }
                }
                else
                {
                    tabControl1.Enabled = false;
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    komut.Connection = SADEFBaglanti;
                    SADEFBaglanti.Open();
                    komut.CommandText = "SELECT AdSoyad FROM " + KGirisForm.Kullaniciadi + "";
                    reader = komut.ExecuteReader();
                    listBox1.Items.Clear();
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader["AdSoyad"]);
                    }
                    komut.Dispose();
                    SADEFBaglanti.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap = MessageBox.Show("Kişi Silinsin mi?", "Bilgilendirme Mesajı",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (cevap == DialogResult.Yes)
                {
                    textclear(this);
                    SADEFBaglanti.Open();
                    komut = new OleDbCommand();
                    komut.Connection = SADEFBaglanti;
                    komut.CommandText = "SELECT * FROM " + KGirisForm.Kullaniciadi + " WHERE AdSoyad='" + listBox1.SelectedItem + "'";
                    reader = komut.ExecuteReader();
                    if (reader.Read())
                    {
                        komut.Dispose();
                        komut = new OleDbCommand();
                        komut.Connection = SADEFBaglanti;
                        komut.CommandText = "DELETE FROM " + KGirisForm.Kullaniciadi + " WHERE AdSoyad='" + listBox1.SelectedItem + "'";
                        komut.ExecuteNonQuery();

                        button1.Enabled = true;
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button4.Enabled = false;
                        button5.Enabled = false;

                        komut.CommandText = "SELECT AdSoyad FROM " + KGirisForm.Kullaniciadi + "";
                        reader = komut.ExecuteReader();
                        listBox1.Items.Clear();
                        while (reader.Read())
                        {
                            listBox1.Items.Add(reader["AdSoyad"]);
                        }
                    }
                }
                SADEFBaglanti.Close();
                listBox1.SelectedIndex = 0;
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        int i = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            SADEFBaglanti.Open();
            Font baslik_fontu = new Font("Tahoma", 14, FontStyle.Bold);
            Font yazi_fontu = new Font("Tahoma", 9, FontStyle.Regular);
            Font kalinyazi_fontu = new Font("Tahoma", 9, FontStyle.Bold);
            int x = 125, y = 125, say = listBox1.Items.Count;
            System.Drawing.Printing.PageSettings p = printDocument1.DefaultPageSettings;
            e.Graphics.DrawString("Defter Bilgileri", baslik_fontu, Brushes.Black, 340, 60);
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), kalinyazi_fontu, Brushes.Black, 680, 80);
            e.Graphics.DrawLine(new Pen(Color.Black, 20), p.Margins.Left - 30, 115, p.PaperSize.Width + 30 - p.Margins.Right, 115);
            e.Graphics.DrawString("Adı Soyadı", kalinyazi_fontu, Brushes.White, 90, 108);
            e.Graphics.DrawString("Ev Telefonu", kalinyazi_fontu, Brushes.White, 2 * 160, 108);
            e.Graphics.DrawString("Cep Telefonu", kalinyazi_fontu, Brushes.White, 3 * 155, 108);
            e.Graphics.DrawString("Doğum Günü", kalinyazi_fontu, Brushes.White, 4 * 150, 108);

            while (i < say)
            {
                x += 25;
                string ad = listBox1.Items[i].ToString();
                e.Graphics.DrawString(ad, kalinyazi_fontu, Brushes.Black, 90, x - 20);
                komut = new OleDbCommand("SELECT ETel, CTel, DGunu FROM " + KGirisForm.Kullaniciadi + " WHERE AdSoyad='" + ad + "'", SADEFBaglanti);
                komut.Connection = SADEFBaglanti;
                reader = komut.ExecuteReader();
                if (reader.Read())
                {
                    e.Graphics.DrawString(reader["ETel"].ToString(), yazi_fontu, Brushes.Black, 2 * 160, x - 20);
                    e.Graphics.DrawString(reader["CTel"].ToString(), yazi_fontu, Brushes.Black, 3 * 155, x - 20);
                    e.Graphics.DrawString(reader["DGunu"].ToString(), yazi_fontu, Brushes.Black, 4 * 150, x - 20);
                }
                e.Graphics.DrawLine(new Pen(Color.Black, 2), p.Margins.Left - 30, x, p.PaperSize.Width + 30 - p.Margins.Right, x);
                i++;

                if ((x + y + 20) > (p.PaperSize.Height + 80 - p.Margins.Bottom + 80))
                {
                    e.HasMorePages = true;
                    break;
                }
            }

            if (i >= say)
            {
                e.HasMorePages = false;
                i = 0;
            }
            e.Graphics.DrawLine(new Pen(Color.Black, 2), p.Margins.Left - 29, 125, p.Margins.Left - 29, x);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), p.Margins.Left + 656, 125, p.Margins.Left + 656, x);
            SADEFBaglanti.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Width = Convert.ToInt16(834);
            pictureBox6.Enabled = true;
            pictureBox6.Visible = true;
            pictureBox5.Enabled = false;
            pictureBox5.Visible = false;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Width = Convert.ToInt16(1188);
            pictureBox6.Enabled = false;
            pictureBox6.Visible = false;
            pictureBox5.Enabled = true;
            pictureBox5.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Ayarlar Ayarlar = new Ayarlar();
            Ayarlar.Show(); this.Hide();
        }
    }
}
