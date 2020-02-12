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
    public partial class KGirisForm : Form
    {
        public KGirisForm()
        {
            InitializeComponent();
        }
        OleDbConnection SADEFBaglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SADEF.mdb");
        OleDbCommand komut;
        OleDbCommand komut2;
        OleDbDataReader reader;
        OleDbDataReader reader2;
        public static string Kullaniciadi;
        public static string Kullaniciadi2;

        private void KGirisForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            textBox1.Focus();
        }

        private void KGirisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    SADEFBaglanti.Open();
                    string boslukal = textBox1.Text.Trim();
                    boslukal = boslukal.Replace(" ", "");
                    string KullaniciAdi = "SELECT KullaniciAdi FROM " + boslukal + " WHERE KullaniciAdi='" + boslukal + "'";
                    string sifre = "SELECT Sifre FROM " + boslukal + " WHERE Sifre='" + textBox2.Text + "' AND KullaniciAdi='" + boslukal + "'";
                    komut = new OleDbCommand(KullaniciAdi, SADEFBaglanti);
                    komut2 = new OleDbCommand(sifre, SADEFBaglanti);
                    reader = komut.ExecuteReader();
                    reader2 = komut2.ExecuteReader();
                    reader.Read();
                    reader2.Read();
                    if (reader.HasRows == true && reader2.HasRows == true)
                    {
                        Kullaniciadi = boslukal + "SADEF";
                        Kullaniciadi2 = boslukal;
                        SADEF SADEF = new SADEF();
                        SADEF.Show(); this.Hide();
                    }
                    else
                    {
                        if (reader.HasRows == true && reader2.HasRows == false)
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "Bilgilendirme Mesajı",
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox1.Focus();
                        }
                        else if (reader.HasRows == false && reader2.HasRows == true)
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "Bilgilendirme Mesajı",
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox1.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Kayıtlı Değilsiniz. Kayıt Ol Formundan Kayıt Olabilirsiniz.", "Bilgilendirme Mesajı",
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            textBox1.Clear();
                            textBox2.Clear();
                            linkLabel1.Focus();
                        }
                    }
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                }
                finally
                {
                    SADEFBaglanti.Close();
                }
            }
            else if (textBox1.Text == "" && textBox2.Text != "")
            {
                MessageBox.Show("Kullanıcı Adı Alanı Boş Bırakılamaz!", "Bilgilendirme Mesajı",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox1.Focus();
            }
            else if (textBox2.Text == "" && textBox1.Text != "")
            {
                MessageBox.Show("Şifre Alanı Boş Bırakılamaz!", "Bilgilendirme Mesajı",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox2.Focus();
            }
            else
            {
                MessageBox.Show("Alanları Doldurunuz Lütfen!", "Bilgilendirme Mesajı",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kayit kayit = new Kayit();
            kayit.Show(); this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SifreUnuttumForm SifreUnuttumForm = new SifreUnuttumForm();
            SifreUnuttumForm.Show(); this.Hide();
        }
    }
}
