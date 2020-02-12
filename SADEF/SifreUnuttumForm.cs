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

namespace SADEF
{
    public partial class SifreUnuttumForm : Form
    {
        OleDbConnection SADEFBaglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SADEF.mdb");
        OleDbCommand komut;
        OleDbDataReader reader;

        public SifreUnuttumForm()
        {
            InitializeComponent();
        }

        private void SifreUnuttumForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            textBox1.Focus();
        }

        private void SifreUnuttumForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KGirisForm KGirisForm = new KGirisForm();
            KGirisForm.Show(); this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    SADEFBaglanti.Open();
                    string boslukal = textBox1.Text.Trim();
                    boslukal = boslukal.Replace(" ", "");
                    string sifreunuttum = "SELECT KullaniciAdi, Ad, Soyad, Sifre FROM " + boslukal + " WHERE KullaniciAdi='" + boslukal + "'";
                    komut = new OleDbCommand(sifreunuttum, SADEFBaglanti);
                    reader = komut.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows == true)
                    {
                        groupBox1.Enabled = true;
                    }
                    else
                    {
                        if (reader.HasRows == true)
                        {
                            MessageBox.Show("Girilen bilgiler doğru değil. Kontrol ediniz.", "Bilgilendirme Mesajı",
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    SADEFBaglanti.Close();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                }
            }
            else
            {
                MessageBox.Show("Alanları Doldurunuz Lütfen!", "Bilgilendirme Mesajı",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox1.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "" && textBox7.Text != "")
            {
                try
                {
                    SADEFBaglanti.Open();
                    string boslukal = textBox1.Text.Trim();
                    boslukal = boslukal.Replace(" ", "");
                    komut.Connection = SADEFBaglanti;
                    if (textBox6.Text == textBox7.Text)
                    {
                        komut.CommandText = "UPDATE " + boslukal + " SET Sifre=@pSifre WHERE KullaniciAdi='" + boslukal + "'";
                        komut.Parameters.AddWithValue("@pSifre", textBox6.Text);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Şifreniz Değiştirildi.", "Bilgilendirme Mesajı",
                            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        groupBox1.Enabled = false;
                        textBox6.Clear();
                        textBox7.Clear();
                        button1.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Şifreler Birbiriyle Eşleşmiyor!", "Bilgilendirme Mesajı",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    SADEFBaglanti.Close();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                }
            }
        }
    }
}
