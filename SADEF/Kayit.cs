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
    public partial class Kayit : Form
    {
        public Kayit()
        {
            InitializeComponent();
        }

        OleDbConnection SADEFBaglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SADEF.mdb");
        OleDbCommand komut;
        OleDbCommand tabloolustur;
        OleDbCommand tabloolustur2;

        private void Kayit_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            textBox1.Focus();
        }

        private void Kayit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                try
                {
                    SADEFBaglanti.Open();
                    string tabloadi = textBox1.Text.Trim();
                    tabloadi = tabloadi.Replace(" ", "");
                    string tabloadi2 = tabloadi + "SADEF";
                    string olustur = "CREATE TABLE " + tabloadi + "(KullaniciAdi varchar(14), Ad varchar(20), Soyad varchar(20), Sifre varchar(20), ResimYolu MEMO)";
                    string olustur2 = "CREATE TABLE " + tabloadi2 + "(AdSoyad varchar(20), YSehir varchar(30), Semt varchar(30), CTel varchar(11), ETel varchar(11), Burc varchar(7), DGunu varchar(10), Adres MEMO, Bilgi MEMO)";
                    tabloolustur = new OleDbCommand(olustur, SADEFBaglanti);
                    tabloolustur.ExecuteNonQuery();
                    tabloolustur2 = new OleDbCommand(olustur2, SADEFBaglanti);
                    tabloolustur2.ExecuteNonQuery();
                    string ekle = "INSERT INTO " + tabloadi + "(KullaniciAdi, Ad, Soyad, Sifre) VALUES (@KullaniciAdi, @Ad, @Soyad, @Sifre)";
                    komut = new OleDbCommand(ekle, SADEFBaglanti);
                    komut.Parameters.AddWithValue("@KullaniciAdi", tabloadi);
                    komut.Parameters.AddWithValue("@Ad", textBox2.Text);
                    komut.Parameters.AddWithValue("@Soyad", textBox3.Text);
                    komut.Parameters.AddWithValue("@Sifre", textBox4.Text);
                    komut.ExecuteNonQuery();
                    SADEFBaglanti.Close();
                    MessageBox.Show("SADEF Kaydınız Yapıldı. Giriş Yapabilirsiniz.", "Bilgilendirme Mesajı",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    button2.Focus();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                }
            }
            else
            {
                MessageBox.Show("Boş Alan Bırakılamaz!", "Bilgilendirme Mesajı",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KGirisForm KGirisForm = new KGirisForm();
            KGirisForm.Show(); this.Hide();
        }
    }
}
