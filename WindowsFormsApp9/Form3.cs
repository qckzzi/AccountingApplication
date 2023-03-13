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
using System.Text.RegularExpressions;

namespace WindowsFormsApp9
{
    public partial class Form3 : Form
    {
        public int[] ID;
        DBConnect db;
        public Form3()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            db = new DBConnect();
            textBox7.ReadOnly = true;
            for (int i = 0; i < ID.Length-1; i++)
            {
                comboBox2.Items.Add(ID[i]);
            }
            comboBox2.Items.Add("(New)");
            textBox2.MaxLength = 50;
            textBox3.MaxLength = 50;
            textBox4.MaxLength = 50;
            textBox5.MaxLength = 50;
            textBox6.MaxLength = 20;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

        }
        public void Fill()
        {
            
            if (comboBox2.Text != "(New)" && ID.Contains(int.Parse(comboBox2.Text)))
            {
                db.OpenConnection();
                SqlCommand command = new SqlCommand("SELECT LastName FROM Client WHERE ID = @ID", db.GetConnection());
                command.Parameters.AddWithValue("@ID", comboBox2.Text);
                textBox2.Text = command.ExecuteScalar().ToString();
                command = new SqlCommand("SELECT FirstName FROM Client WHERE ID = @ID", db.GetConnection());
                command.Parameters.AddWithValue("@ID", comboBox2.Text);
                textBox3.Text = command.ExecuteScalar().ToString();
                command = new SqlCommand("SELECT Patronymic FROM Client WHERE ID = @ID", db.GetConnection());
                command.Parameters.AddWithValue("@ID", comboBox2.Text);
                textBox4.Text = command.ExecuteScalar().ToString();
                command = new SqlCommand("SELECT Email FROM Client WHERE ID = @ID", db.GetConnection());
                command.Parameters.AddWithValue("@ID", comboBox2.Text);
                textBox5.Text = command.ExecuteScalar().ToString();
                command = new SqlCommand("SELECT Phone FROM Client WHERE ID = @ID", db.GetConnection());
                command.Parameters.AddWithValue("@ID", comboBox2.Text);
                textBox6.Text = command.ExecuteScalar().ToString();
                command = new SqlCommand("SELECT Birthday FROM Client WHERE ID = @ID", db.GetConnection());
                command.Parameters.AddWithValue("@ID", comboBox2.Text);
                dateTimePicker1.Value = DateTime.Parse(command.ExecuteScalar().ToString());
                command = new SqlCommand("SELECT GenderCode FROM Client WHERE ID = @ID", db.GetConnection());
                command.Parameters.AddWithValue("@ID", comboBox2.Text);
                comboBox1.Text = command.ExecuteScalar().ToString();
                command = new SqlCommand("SELECT PhotoPath FROM Client WHERE ID = @ID", db.GetConnection());
                command.Parameters.AddWithValue("@ID", comboBox2.Text);
                try
                {
                    textBox7.Text = $@"D:\{command.ExecuteScalar().ToString()}";
                }
                catch (Exception)
                {
                    textBox7.Text = command.ExecuteScalar().ToString();
                }
                
                
                db.ClosedConnection();
            }
            else
            {
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.Text = "";
                textBox7.Text = "";
                pictureBox2.Image = WindowsFormsApp9.Properties.Resources.Безымянный2;

            }
        }
        public void Save()
        {
            db.OpenConnection();
            SqlCommand command = new SqlCommand("INSERT INTO Client(FirstName, LastName, Patronymic, Email, Phone, Birthday, GenderCode, PhotoPath, RegistrationDate)" +
                " Values(@FirstName, @LastName, @Patronymic, @Email, @Phone, @Birthday, @GenderCode, @PhotoPath, @RegistrationDate)\n" +
                "INSERT INTO Visits(lastDate, amountVisits) VALUES (@lastDate, @amountVisits)", db.GetConnection());
            command.Parameters.AddWithValue("@LastName", textBox2.Text);
            command.Parameters.AddWithValue("@FirstName", textBox3.Text);
            command.Parameters.AddWithValue("@Patronymic", textBox4.Text);
            command.Parameters.AddWithValue("@Email", textBox5.Text);
            command.Parameters.AddWithValue("@Phone", textBox6.Text);
            command.Parameters.AddWithValue("@Birthday", $"{dateTimePicker1.Value.Year}/{dateTimePicker1.Value.Month}/{dateTimePicker1.Value.Day}");
            command.Parameters.AddWithValue("@GenderCode", comboBox1.Text);
            command.Parameters.AddWithValue("@PhotoPath", textBox7.Text);
            command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
            command.Parameters.AddWithValue("@lastDate", DateTime.Now);
            command.Parameters.AddWithValue("@amountVisits", 1);
            command.ExecuteReader();
            db.ClosedConnection();
        }
        public void Edit()
        {
            db.OpenConnection();
            SqlCommand command = new SqlCommand("UPDATE Client SET FirstName = @FirstName, LastName = @LastName," +
                " Patronymic = @Patronymic, Email = @Email," +
                " Phone = @Phone , Birthday = @Birthday, GenderCode = @GenderCode, PhotoPath = @PhotoPath" +
                " WHERE ID = @ID", db.GetConnection());
            command.Parameters.AddWithValue("@LastName", textBox2.Text);
            command.Parameters.AddWithValue("@FirstName", textBox3.Text);
            command.Parameters.AddWithValue("@Patronymic", textBox4.Text);
            command.Parameters.AddWithValue("@Email", textBox5.Text);
            command.Parameters.AddWithValue("@Phone", textBox6.Text);
            command.Parameters.AddWithValue("@Birthday", $"{dateTimePicker1.Value.Year}/{dateTimePicker1.Value.Month}/{dateTimePicker1.Value.Day}");
            command.Parameters.AddWithValue("@GenderCode", comboBox1.Text);
            command.Parameters.AddWithValue("@PhotoPath", textBox7.Text);
            command.Parameters.AddWithValue("@ID", comboBox2.Text);
            command.ExecuteReader();
            db.ClosedConnection();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                pictureBox2.Image = Image.FromFile(textBox7.Text);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {
                    textBox7.Text = file;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (textBox2.Text!="" && textBox3.Text!="" && textBox5.Text.Contains("@") && textBox5.Text.Contains(".") && textBox6.Text!="")
            {
                label11.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                if (comboBox2.Text != "(New)" && ID.Contains(int.Parse(comboBox2.Text)))
                {
                    DialogResult result = MessageBox.Show("Вы уверены, что хотите отредактировать пользователя?", "Редактирование", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result.ToString() == "Yes")
                    {
                        Edit();
                        Close();
                        MessageBox.Show($"Пользователь отредактирован!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    DialogResult result = MessageBox.Show("Вы уверены, что хотите создать пользователя?", "Создание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result.ToString() == "Yes")
                    {
                        Save();
                        Close();
                        MessageBox.Show($"Пользователь создан! (ID: {ID[ID.Length - 2] + 1})", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                
            }
            else
            {
                if (textBox2.Text == "")
                {
                    label11.Visible = true;
                }
                if (textBox3.Text == "")
                {
                    label12.Visible = true;
                }
                if (!textBox5.Text.Contains("@") || !textBox5.Text.Contains("."))
                {
                    label13.Visible = true;
                }
                if (textBox3.Text == "")
                {
                    label14.Visible = true;
                }
            }
            


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            Fill();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[а-яА-Я\b]|[a-zA-Z\b]|[ \b]|[-]").Success)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[а-яА-Я\b]|[a-zA-Z\b]|[ \b]|[-]").Success)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[а-яА-Я\b]|[a-zA-Z\b]|[ \b]|[-]").Success)
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            if (!Regex.Match(Symbol, @"[0-9\b\s\]|[-]|[+()]").Success)
            {
                e.Handled = true;
            }
        }
    }
}
