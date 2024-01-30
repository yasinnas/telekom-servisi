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
    public partial class Form2 : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=YASIN;Initial Catalog=1;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=False");
        SqlDataReader dr;
        SqlCommandBuilder commandBuilder;
        SqlCommandBuilder commandBuilder2;
        SqlDataAdapter adtr;
        SqlDataAdapter adtr2;
        DataTable dt = new DataTable();
      DataTable dt2 = new DataTable();
        public Form2()
        {
            InitializeComponent();
            
            listele();
           
        }
        void listele()
        {
            dt.Clear();   
             adtr = new SqlDataAdapter("Select *from paketler", baglanti);
          
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;
            dt2.Clear();
            adtr2 = new SqlDataAdapter("Select *from faturalıtarife", baglanti);
            adtr2.Fill(dt2);
            dataGridView2.DataSource = dt2;

        }
       
        public void olustur()
        {
            string numara = textBox3.Text;
            string b = numara.Substring(0, 1);


            try
            {
                if (b == "5" && textBox3.Text.Length == 10)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into kb (ad,soyad,numara) values (@t1,@t2,@t3)", baglanti);
                    komut.Parameters.AddWithValue("t1", textBox1.Text);
                    komut.Parameters.AddWithValue("t2", textBox2.Text);
                    komut.Parameters.AddWithValue("t3", textBox3.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("İŞlem başarılı");
                    
                }else
                {
                    MessageBox.Show("Lütfen geçerli  bilgiler giriniz.");
                    textBox3.Clear();
                }


            }
            catch (Exception ex)
            {
             MessageBox.Show(ex.Message);
               
            }


        }
        private void kasadurumu()
        {
            try
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("Select  sum (ücret) from kasa", baglanti);  
             label3.Text = cmd.ExecuteScalar().ToString();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
            panel5.Visible = false;
            panel3.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel5.Visible = false;
            panel3.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel2.Visible = false;
            panel1.Visible = false;
            panel5.Visible = false;
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
       
            kasadurumu();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("delete from kasa ", baglanti);
            cmd2.ExecuteNonQuery();
            baglanti.Close() ;
            kasadurumu();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            olustur();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            commandBuilder = new SqlCommandBuilder(adtr);
            adtr.Update(dt);
            listele();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            commandBuilder2= new SqlCommandBuilder(adtr2);
            commandBuilder2.GetUpdateCommand().UpdatedRowSource = UpdateRowSource.None;
            adtr2.Update(dt2);
            listele();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            panel2.Visible = false;
            panel1.Visible = false;
          
            panel3.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }
    }
}
