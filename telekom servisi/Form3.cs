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

namespace telekom_servisi
{
    public partial class Form3 : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=YASIN;Initial Catalog=1;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=False");
        SqlDataReader dr;
        SqlCommand komut;
        SqlCommand cmd;
        TimeSpan fark;
        double gunfark;
        DateTime tarihDegeri = DateTime.Today.AddMonths(+1);
        public static string deneme;

        private void sorgu()
        {

            try
            {
                cmd = new SqlCommand();

                baglanti.Open();
                cmd = new SqlCommand("Select * from kb ", baglanti);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    if (dr["ödemeşekli"] != null)
                    {

                        fark = Convert.ToDateTime(dr["Sonödemetarihi"].ToString()) - Convert.ToDateTime(DateTime.Today.ToShortDateString());
                        gunfark = fark.TotalDays;

                        if (gunfark == 0)
                        {
                            MessageBox.Show("Ödeme Günü geldi..." + dr["numara"]);
                            SqlCommand komut2 = new SqlCommand("update kb set Sonödemetarihi=@t1 where numara=@numara ", baglanti);
                            komut2.Parameters.AddWithValue("t1", tarihDegeri);
                            komut2.Parameters.AddWithValue("numara", dr["numara"]);
                            SqlCommand komut3 = new SqlCommand("insert into kasa (ücret) values (@ücret)", baglanti);
                            komut3.Parameters.AddWithValue("ücret", dr["fiyat"]);
                            komut3.ExecuteNonQuery();
                            komut2.ExecuteNonQuery();
                            baglanti.Close();
                        }

                    }
                    if (dr["ödemeşekli"] == null)
                    {
                        fark = Convert.ToDateTime(dr["Sonödemetarihi"].ToString()) - Convert.ToDateTime(DateTime.Today.ToShortDateString());
                        gunfark = fark.TotalDays;
                        if (gunfark == 0)
                        {
                            MessageBox.Show("Ödeme Günü geldi" + dr["numara"]);
                            baglanti.Close();
                        }
                        if (gunfark <= -10)
                        {
                            cmd = new SqlCommand("delete from kb where numara=@numara");
                            cmd.Parameters.AddWithValue("numara", dr["numara"]);
                            cmd.ExecuteNonQuery();
                            SqlCommand cmd2 = new SqlCommand("update kb set numara=@numara ", baglanti);
                            cmd2.Parameters.AddWithValue("numara", dr["numara"]);
                            cmd2.ExecuteNonQuery();
                            baglanti.Close();
                        }
                    }
                    baglanti.Close();
                }
            }
            catch (Exception ex)
            {
                baglanti.Close();
               MessageBox.Show (ex.Message);
            }
        }
       
        public void giris1()
        {
            try
            {
                
                SqlCommand  komut = new SqlCommand("SELECT* FROM  admin WHERE kullanıcıadı=@t1 AND sifre=@t2", baglanti);
                komut.Parameters.AddWithValue("t1", textBox1.Text);
                komut.Parameters.AddWithValue("t2", textBox2.Text);
                baglanti.Open();
                dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Basarıyla giris yapıldı");
                    Form2 form2 = new Form2();
                   this.Hide();
                    form2.Show();
                     

                }
                else
                {
                    MessageBox.Show("Sifre yada kullanıcı adı yanlıış");
                   
                }
                baglanti.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            
            }
        }
      
        public void olustur()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into admin (kullanıcıadı,sifre) values (@t1,@t2)", baglanti);
                komut.Parameters.AddWithValue("t1", textBox3.Text);
                komut.Parameters.AddWithValue("t2", textBox4.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Başarıyla kayıt oldunuz");
                panel1.Visible = false;

            }catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }



        }
        public Form3()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible = false;
            panel1.Visible = false;
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
        
            try
            {
             
                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT* FROM  admin WHERE kullanıcıadı=@t1 AND sifre=@sifre", baglanti);
                komut.Parameters.AddWithValue("@t1", textBox1.Text);
                komut.Parameters.AddWithValue("sifre", textBox2.Text);
                dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Basarıyla giris yapıldı");
                    Form2 f2 = new Form2();
                    this.Hide();
                   f2.Show();
                }
                else
                {
                    MessageBox.Show("Geçerli bir numara giriniz");
                }


                baglanti.Close();
            }
            catch (Exception ex)

            {
                
                baglanti.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            olustur();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        
            try
            {

                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT* FROM  kb WHERE  numara=@numara", baglanti);
                komut.Parameters.AddWithValue("@numara", textBox5.Text);
                dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Basarıyla giris yapıldı");
                    Form4 f4 = new Form4();
                    this.Hide();
                    deneme = textBox5.Text;
                    f4.ShowDialog();
                    
                }
                else
                {
                    MessageBox.Show("Geçerli bir numara giriniz");
                }


                baglanti.Close();
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
            Form4 form4 = new Form4();
          
        }

        private void Form3_Load(object sender, EventArgs e)
        {
         
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
