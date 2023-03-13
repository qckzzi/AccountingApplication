using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace WindowsFormsApp9
{
    public partial class Form2 : Form
    {
        DBConnect db = new DBConnect();
        Login log;
        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        

        private void Form2_Load(object sender, EventArgs e)
        {

            pictureBox1.Size = new Size(50, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label4.Visible = false;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            textBox2.PasswordChar = '*';
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //кнопка Login
        {
            log = new Login(textBox1.Text, textBox2.Text);
            if (log.Enter() == true)
            {
                Form1 form1 = new Form1();
                Hide();
                form1.ShowDialog();
                label4.Visible = false;
                panel2.BackColor = Color.FromArgb(4, 160, 255);
                panel4.BackColor = Color.FromArgb(4, 160, 255);
                Show();

            }
            else
            {
                label4.Visible = true;
                panel2.BackColor = Color.Red;
                panel4.BackColor = Color.Red;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
