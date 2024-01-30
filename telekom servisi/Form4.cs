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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace telekom_servisi
{
    
    public partial class Form4 : Form
    {

        
        SqlConnection baglanti = new SqlConnection("Data Source=YASIN;Initial Catalog=1;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=False");
        double gunfark;
        SqlCommand cmd;
        TimeSpan fark;
        DateTime tarihDegeri = DateTime.Today.AddMonths(+1);
        SqlDataReader dr;

       
       

        //public void öde()
        //{
        //    try
        //    {


        //        cmd = new SqlCommand();

        //        baglanti.Open();
        //        cmd = new SqlCommand("Select * from kb ", baglanti);
        //        dr = cmd.ExecuteReader();
        //        gunfark = fark.TotalDays;
        //        while (dr.Read())
        //        {
        //            if (dr["numara"].ToString() == numara)
        //            {
        //                for (int i = 1; i < dataGridView1.Rows.Count; i++)
        //                {
        //                    fark = Convert.ToDateTime(dataGridView1.Rows[i].Cells["Sonödemetarihi"].Value.ToString()) - Convert.ToDateTime(DateTime.Now.ToShortDateString());

        //                    if (gunfark == 0 || gunfark < -10)
        //                    {


        //                        SqlCommand cmd2 = new SqlCommand("update kb set Sonödemetarihi=@s1 where numara=@numara",baglanti);
        //                        cmd2.Parameters.AddWithValue("s1", tarihDegeri);
        //                        cmd2.Parameters.AddWithValue("numara", dr["numara"]);
        //                        cmd2.ExecuteNonQuery();
        //                        SqlCommand cmd3 = new SqlCommand("insert into kasa (ücret) values (@ücret)", baglanti);
        //                        cmd3.Parameters.AddWithValue("ücret", dr["fiyat"]);
        //                        cmd3.ExecuteNonQuery();
        //                        baglanti.Close();

        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Ödeme Günü gelmedi");

        //                        baglanti.Close();

        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}
        public void öde()
        {
            string deneme = Form3.deneme;
            try
            {
                baglanti.Close();
                baglanti.Open();
                cmd = new SqlCommand("Select * from kb ", baglanti);
                dr = cmd.ExecuteReader();
              

                while (dr.Read())
                {
                    if (dr["numara"].ToString() == deneme)
                    {

                       // Döngü indeksi 0'dan başlamalı
                        
                            fark = Convert.ToDateTime(dr["Sonödemetarihi"].ToString()) - Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            gunfark = fark.TotalDays;
                            if (gunfark == 0||gunfark<-10)
                            {
                                DateTime tarihDegeri = DateTime.Today.AddMonths(+1);
                                SqlCommand cmd2 = new SqlCommand("update kb set Sonödemetarihi=@s1 where numara=@numara", baglanti);
                                cmd2.Parameters.AddWithValue("s1", tarihDegeri);
                                cmd2.Parameters.AddWithValue("numara", dr["numara"]);
                                cmd2.ExecuteNonQuery();

                                SqlCommand cmd3 = new SqlCommand("insert into kasa (ücret) values (@ücret)", baglanti);
                                cmd3.Parameters.AddWithValue("ücret", dr["fiyat"]);
                                cmd3.ExecuteNonQuery();
                                MessageBox.Show("İslem basarili");
                              
                            }
                            else
                            {
                                MessageBox.Show("Ödeme Günü gelmedi");
                               
                            }
                          
                        
                      
                    }
                   
                }
                baglanti.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("tarife yok");
            }
            finally
            {
                baglanti.Close(); // Hata oluşsa bile bağlantıyı kapatın.
            }
        }
       
        public void listele()
        {
            baglanti.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select *from kb", baglanti);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }
        public Form4()
        {
            InitializeComponent();
            listele();
        }
        
        public void sil()
        {
            string deneme = Form3.deneme;
            try
            {

                baglanti.Open();

                SqlCommand komut = new SqlCommand("DELETE from kb WHERE numara=@t3", baglanti);

                komut.Parameters.AddWithValue("@t3", deneme);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("İslem Basarılı");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
      
        private void button4_Click(object sender, EventArgs e)
        {
          
        }

        private void Form4_Load(object sender, EventArgs e)
        {


            
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
        }

        private void Form4_Load_1(object sender, EventArgs e)
        {
            string deneme = Form3.deneme;
            MessageBox.Show(deneme);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
    
            baglanti.Close();
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            panel1.Visible = false;
            try
            {
                string deneme = Form3.deneme;
                SqlCommand
             cmd = new SqlCommand("select * from paketler", baglanti);
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int i = 0;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        System.Windows.Forms.TextBox t1 = new System.Windows.Forms.TextBox();
                        System.Windows.Forms.TextBox t2 = new System.Windows.Forms.TextBox();
                        System.Windows.Forms.Button t3 = new System.Windows.Forms.Button();
                        t3.Width = 100;
                        t3.Location = new Point(380, 30 + (i * 20));
                        t3.Text = " Satın al";
                        t2.Text = reader["fiyat"].ToString();
                        t2.Width = 50;
                        t2.Visible = true;
                        t2.Location = new Point(320, 30 + (i * 20));
                        t1.Text = reader["paket"].ToString();
                        t1.Width = 300;
                        t1.Visible = true;
                        t1.Location = new Point(10, 30 + (i * 20));
                        groupBox1.Controls.Add(t1);
                        groupBox1.Controls.Add(t2);
                        groupBox1.Controls.Add(t3);
                        t3.Click += (s, ev) => {


                            string kasa = t2.Text;

                            SqlCommand komut2 = new SqlCommand(" update kb set fiyat=@ücret where numara=@numara", baglanti);
                            komut2.Parameters.AddWithValue("@ücret", kasa);
                            komut2.Parameters.AddWithValue("@numara", deneme);
                            komut2.ExecuteNonQuery();
                            SqlCommand komut = new SqlCommand("UPDATE kb SET   tarife=@t2 WHERE numara = @numara", baglanti);
                            komut.Parameters.AddWithValue("@t2", t1.Text);
                            komut.Parameters.AddWithValue("@numara", deneme);
                            komut.ExecuteNonQuery();

                            string kasa1 = t2.Text;
                            string kasa3 = kasa1.Substring(0, 3);
                            int a = Int16.Parse(kasa3);
                            SqlCommand komut3 = new SqlCommand("insert into kasa (ücret) values (@ücret1)", baglanti);
                            komut3.Parameters.AddWithValue("@ücret1", a);
                            komut3.ExecuteNonQuery();
                            baglanti.Close();
                         
                            baglanti.Open();

                            
                            
                        };
                       
                        i++;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string deneme = Form3.deneme;
            groupBox2.Visible = true;
            groupBox1.Visible = false;
            panel1.Visible = false;
            baglanti.Close();
            try
            {
            
                SqlCommand
             cmd = new SqlCommand("select * from faturalıtarife", baglanti);
                baglanti.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int i = 0;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        System.Windows.Forms.TextBox t1 = new System.Windows.Forms.TextBox();
                        System.Windows.Forms.TextBox t2 = new System.Windows.Forms.TextBox();
                        System.Windows.Forms.Button t3 = new System.Windows.Forms.Button();
                        t3.Width = 100;
                        t3.Location = new Point(380, 30 + (i * 20));
                        t3.Text = " Satın al";
                        t2.Text = reader["fiyat"].ToString();
                        t2.Width = 50;
                        t2.Visible = true;
                        t2.Location = new Point(320, 30 + (i * 20));
                        t1.Text = reader["tarife"].ToString();
                        t1.Width = 300;
                        t1.Visible = true;
                        t1.Location = new Point(10, 30 + (i * 20));
                        groupBox2.Controls.Add(t1);
                        groupBox2.Controls.Add(t2);
                        groupBox2.Controls.Add(t3);
                        t3.Click += (s, ev) => {

                            DialogResult dialogResult = MessageBox.Show("Otomatik Ödeme olmasını ister misiniz?", "Otomatik Ödeme", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                             
                                string yazı = "Otomatik";
                                SqlCommand cmd5 = new SqlCommand("Update kb set ödemeşekli=@t1 where numara =@numara", baglanti);
                                cmd5.Parameters.AddWithValue("t1",yazı);
                                cmd5.Parameters.AddWithValue("numara", deneme);
                               
                                cmd5.ExecuteNonQuery();
                                baglanti.Close();  
                                

                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                
                                SqlCommand cmd5 = new SqlCommand("Update kb set ödemeşekli=@t1 where numara =@numara", baglanti);
                                cmd5.Parameters.AddWithValue("t1", null);
                                cmd5.Parameters.AddWithValue("numara", deneme);
                                cmd5.ExecuteNonQuery();
                                
                            }
                            string kasa = t2.Text;
                            string kasa2 = kasa.Substring(0, 3);
                            Int16.TryParse(kasa2, out Int16 kasa3);
                            baglanti.Open();
                            SqlCommand komut2 = new SqlCommand("update kb set fiyat=@ücret where numara=@numara", baglanti);
                            komut2.Parameters.AddWithValue("@ücret", kasa3);
                            komut2.Parameters.AddWithValue("@numara", deneme);
                            komut2.ExecuteNonQuery();

                            SqlCommand komut = new SqlCommand("UPDATE kb SET tarife=@t2 WHERE numara = @numara", baglanti);
                            komut.Parameters.AddWithValue("@t2", t1.Text);
                            komut.Parameters.AddWithValue("@numara",deneme );
                            komut.ExecuteNonQuery();
                            DateTime tarihDegeri = DateTime.Today.AddMonths(+1);

                            SqlCommand cmd4 = new SqlCommand("UPDATE kb SET Sonödemetarihi=@t1 WHERE numara = @numara", baglanti);
                            cmd4.Parameters.AddWithValue("t1", tarihDegeri);
                            cmd4.Parameters.AddWithValue("numara", deneme);
                            cmd4.ExecuteNonQuery();
                            

                            // Hatalı olan kısım düzeltiliyor
                            SqlCommand komut3 = new SqlCommand("insert into kasa (ücret) values (@ücret1)", baglanti);
                            komut3.Parameters.AddWithValue("@ücret1", kasa2);
                            komut3.ExecuteNonQuery();
                            baglanti.Close();
                            MessageBox.Show("işlem başarılı");

                        };

                        i++;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

            baglanti.Close();
            sil();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            listele();
            baglanti.Close();
            öde();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
