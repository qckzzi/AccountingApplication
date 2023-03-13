using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        DBConnect db = new DBConnect();
        List<string[]> data = new List<string[]>();
        double pagesCount;
        int pageIndex = 0;
        int dataOutputIndex = 0;
        public int[] ID;
        public void Delete(int ID)//Удаление клиента по его ID
        {
            Query($"DELETE Client WHERE ID = {ID}");
        }
        public void Search()
        {
            if (comboBox2.Text != "" && comboBox2.Text != "All")
            {
                Query($"SELECT * FROM Client JOIN Visits ON Visits.idClient = Client.ID WHERE GenderCode in ('{comboBox2.Text}')" +
                    $"{((textBox1.Text != "") ? $"AND FirstName like '%{textBox1.Text}%' " : "")}" +
                    $"{((textBox2.Text != "") ? $"AND LastName like '%{textBox2.Text}%' " : "")}" +
                    $"{((textBox3.Text != "") ? $"AND Patronymic like '%{textBox3.Text}%' " : "")}" +
                    $"{((textBox4.Text != "") ? $"AND Email like '%{textBox4.Text}%' " : "")}" +
                    $"{((textBox5.Text != "") ? $"AND Phone like '%{textBox5.Text}%' " : "")}");
            }
            else
            {
                Query($"SELECT * FROM Client JOIN Visits ON Visits.idClient = Client.ID " +
                $"{((textBox1.Text != "") ? $"WHERE FirstName Like '%{textBox1.Text}%' " : "")}" +
                $"{((textBox2.Text != "") ? $"{(textBox1.Text != "" ? "AND" : "WHERE")} LastName Like '%{textBox2.Text}%' " : "")}" +
                $"{((textBox3.Text != "") ? $"{(textBox2.Text != "" ? "AND" : "WHERE")} Patronymic Like '%{textBox3.Text}%' " : "")}" +
                $"{((textBox4.Text != "") ? $"{(textBox3.Text != "" ? "AND" : "WHERE")} Email Like '%{textBox4.Text}%' " : "")}" +
                $"{((textBox5.Text != "") ? $"{(textBox4.Text != "" ? "AND" : "WHERE")} Phone Like '%{textBox5.Text}%' " : "")}");
            }
        }
        public void Select(string str)//Поиск или сортировка
        {
            if (str=="Search")
            {
                if (comboBox2.Text != "" && comboBox2.Text != "All")
                {
                    Query($"SELECT * FROM Client JOIN Visits ON Visits.idClient = Client.ID WHERE GenderCode in ('{comboBox2.Text}')" +
                        $"{((textBox1.Text != "") ? $"AND FirstName like '%{textBox1.Text}%' " : "")}" +
                        $"{((textBox2.Text != "") ? $"AND LastName like '%{textBox2.Text}%' " : "")}" +
                        $"{((textBox3.Text != "") ? $"AND Patronymic like '%{textBox3.Text}%' " : "")}" +
                        $"{((textBox4.Text != "") ? $"AND Email like '%{textBox4.Text}%' " : "")}" +
                        $"{((textBox5.Text != "") ? $"AND Phone like '%{textBox5.Text}%' " : "")}");
                }
                else
                {
                    Query($"SELECT * FROM Client JOIN Visits ON Visits.idClient = Client.ID " +
                    $"{((textBox1.Text != "") ? $"WHERE FirstName Like '%{textBox1.Text}%' " : "")}" +
                    $"{((textBox2.Text != "") ? $"{(textBox1.Text != "" ? "AND" : "WHERE")} LastName Like '%{textBox2.Text}%' " : "")}" +
                    $"{((textBox3.Text != "") ? $"{(textBox2.Text != "" ? "AND" : "WHERE")} Patronymic Like '%{textBox3.Text}%' " : "")}" +
                    $"{((textBox4.Text != "") ? $"{(textBox3.Text != "" ? "AND" : "WHERE")} Email Like '%{textBox4.Text}%' " : "")}" +
                    $"{((textBox5.Text != "") ? $"{(textBox4.Text != "" ? "AND" : "WHERE")} Phone Like '%{textBox5.Text}%' " : "")}");
                }
            }
            else if (str == "Sort")
            {
                if (comboBox3.Text == "фамилии")
                {
                    Query($"SELECT * FROM Client JOIN Visits ON Visits.idClient = Client.ID " +
                    $"{(comboBox2.Text == "" ? "" : $"{(comboBox2.Text == "All" ? "" : $"WHERE Client.GenderCode in ('{(comboBox2.Text == "м" ? 'м' : 'ж')}') ")}")}" +
                    $"ORDER BY (Client.LastName)");
                }
                else if (comboBox3.Text == "дате последнего посещения")
                {
                    Query($"SELECT * FROM Client JOIN Visits ON Visits.idClient = Client.ID " +
                    $"{(comboBox2.Text == "" ? "" : $"{(comboBox2.Text == "All" ? "" : $"WHERE Client.GenderCode in ('{(comboBox2.Text == "м" ? 'м' : 'ж')}') ")}")}" +
                    $"ORDER BY (Visits.LastDate) DESC");
                }
                else if (comboBox3.Text == "количеству посещений")
                {
                    Query($"SELECT * FROM Client JOIN Visits ON Visits.idClient = Client.ID " +
                    $"{(comboBox2.Text == "" ? "" : $"{(comboBox2.Text == "All" ? "" : $"WHERE Client.GenderCode in ('{(comboBox2.Text == "м" ? 'м' : 'ж')}') ")}")}" +
                    $"ORDER BY (Visits.amountVisits) DESC");
                }
                else
                {
                    Query($"SELECT * FROM Client JOIN Visits ON Visits.idClient = Client.ID " +
                    $"{(comboBox2.Text == "" ? "" : $"{(comboBox2.Text == "All" ? "" : $"WHERE Client.GenderCode in ('{(comboBox2.Text == "м" ? 'м' : 'ж')}') ")}")}");
                }
            }
        }
        public void Query(string query)//Запонение List<string[]> data строками из таблицы БД
        {
            db.OpenConnection();
            SqlCommand command = new SqlCommand(query, db.GetConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                data.Add(new string[12]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[8].ToString();
                data[data.Count - 1][2] = reader[1].ToString();
                data[data.Count - 1][3] = reader[2].ToString();
                data[data.Count - 1][4] = reader[3].ToString();
                data[data.Count - 1][5] = reader[4].ToString();
                data[data.Count - 1][6] = reader[7].ToString();
                data[data.Count - 1][7] = reader[6].ToString();
                data[data.Count - 1][8] = reader[5].ToString();
                data[data.Count - 1][9] = reader[11].ToString();
                data[data.Count - 1][10] = reader[12].ToString();


            }
            db.ClosedConnection();
        }
        public void FillTable()//Заполнение DataGridView строками из List<string[]> data
        {
            if (comboBox1.Text != "All")
            {
                pagesCount = Math.Ceiling((double)data.Count / int.Parse(comboBox1.Text));
                dataOutputIndex = pageIndex * int.Parse(comboBox1.Text);
            }
            dataGridView1.Rows.Clear();
            if (comboBox1.Text == "10")
            {
                if ((data.Count-dataOutputIndex) < 10)
                {
                    for (int i = dataOutputIndex; i < (data.Count - dataOutputIndex) + dataOutputIndex; i++)
                    {
                        dataGridView1.Rows.Add(data[i]);
                    }
                }
                else
                {
                    for (int i = dataOutputIndex; i <= dataOutputIndex + 9; i++)
                    {
                        dataGridView1.Rows.Add(data[i]);
                    }
                }
                

            }
            if (comboBox1.Text == "50")
            {

                if ((data.Count - dataOutputIndex) < 50)
                {
                    for (int i = dataOutputIndex; i < (data.Count - dataOutputIndex) + dataOutputIndex; i++)
                    {
                        dataGridView1.Rows.Add(data[i]);
                    }
                }
                else
                {
                    for (int i = dataOutputIndex; i <= dataOutputIndex + 49; i++)
                    {
                        dataGridView1.Rows.Add(data[i]);
                    }
                }

            }
            if (comboBox1.Text == "200")
            {
                if (data.Count < 200)
                {
                    for (int i = dataOutputIndex; i < data.Count; i++)
                    {
                        dataGridView1.Rows.Add(data[i]);
                    }
                }
                else
                {
                    if ((data.Count - dataOutputIndex) < 200)
                    {
                        for (int i = dataOutputIndex; i < (data.Count - dataOutputIndex) + dataOutputIndex; i++)
                        {
                            dataGridView1.Rows.Add(data[i]);
                        }
                    }
                    else
                    {
                        for (int i = dataOutputIndex; i <= dataOutputIndex + 199; i++)
                        {
                            dataGridView1.Rows.Add(data[i]);
                        }
                    }
                }


            }
            if (comboBox1.Text == "All")
            {
                for (int i = 0; i < data.Count; i++)
                {
                    dataGridView1.Rows.Add(data[i]);
                }

            }
            if (comboBox1.Text=="All")
            {
                label2.Text = $"Страница: 1/1";
            }
            else
            {
                label2.Text = $"Страница: {pageIndex + 1}/{pagesCount}";
            }
            if (comboBox1.Text != "All")
            {
                label3.Text = $"Выведено записей: {(((data.Count - dataOutputIndex) < int.Parse(comboBox1.Text)) ? data.Count - dataOutputIndex: int.Parse(comboBox1.Text))}";
            }
            else
            {
                label3.Text = $"Выведено записей: {data.Count}";
            }
            label4.Text = $"Всего записей: {data.Count}";
        }
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Image.RotateFlip(RotateFlipType.Rotate180FlipY);
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            comboBox3.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageIndex = 0;
            dataGridView1.Rows.Clear();
            data.Clear();
            if (comboBox2.Text=="")
            {
                data.Clear();
                Query("SELECT * FROM Client JOIN Visits ON Visits.idClient = Client.ID");
            }
            else
            {
                Select("Sort");
            }
            FillTable();
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            comboBox3.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            if (comboBox1.Text!="All")
            {
                button5.Enabled = false;
            }
            else
            {
                button5.Enabled = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                if (pageIndex != 0)
                {
                    pageIndex--;
                    FillTable();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox1.Text!= "All")
            {
                if (pageIndex != pagesCount && pageIndex != pagesCount-1)
                {
                    pageIndex++;
                    FillTable();
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "м" && comboBox1.Text != "")
            {

                pageIndex = 0;
                dataGridView1.Rows.Clear();
                data.Clear();
                if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
                {
                    Select("Sort");
                }
                else
                {
                    Select("Search");
                }
                FillTable();
            }
            else if (comboBox2.Text == "ж" && comboBox1.Text != "")
            {
                pageIndex = 0;
                dataGridView1.Rows.Clear();
                data.Clear();
                if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
                {
                    Select("Sort");
                }
                else
                {
                    Select("Search");
                }
                FillTable();
            }
            else if (comboBox2.Text == "All" && comboBox1.Text != "")
            {
                pageIndex = 0;
                dataGridView1.Rows.Clear();
                data.Clear();
                if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
                {
                    Select("Sort");
                }
                else
                {
                    Select("Search");
                }
                FillTable();
            }
            else
            {
                pageIndex = 0;
                dataGridView1.Rows.Clear();
                data.Clear();
                if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
                {
                    Select("Sort");
                }
                else
                {
                    Select("Search");
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pageIndex = 0;
            dataGridView1.Rows.Clear();
            data.Clear();
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
            {
                Select("Sort");
            }
            else
            {
                Search();
            }
            FillTable();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            pageIndex = 0;
            dataGridView1.Rows.Clear();
            data.Clear();
            if (textBox1.Text==""&& textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
            {
                Select("Sort");
            }
            else
            {
                Search();
            }
            FillTable();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            pageIndex = 0;
            dataGridView1.Rows.Clear();
            data.Clear();
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
            {
                Select("Sort");
            }
            else
            {
                Search();
            }
            FillTable();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            pageIndex = 0;
            dataGridView1.Rows.Clear();
            data.Clear();
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
            {
                Select("Sort");
            }
            else
            {
                Search();
            }
            FillTable();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            pageIndex = 0;
            dataGridView1.Rows.Clear();
            data.Clear();
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
            {
                Select("Sort");
            }
            else
            {
                Search();
            }
            FillTable();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageIndex = 0;
            dataGridView1.Rows.Clear();
            data.Clear();
            Select("Sort");
            FillTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            db.OpenConnection();
            pageIndex = 0;
            dataGridView1.Rows.Clear();
            data.Clear();
            Query("SELECT * FROM Client JOIN Visits ON Visits.idClient = Client.ID WHERE MONTH(Birthday) = MONTH(GETDATE()) ");
            FillTable();
            
            db.ClosedConnection();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result.ToString() == "Yes")
            {
                MessageBox.Show($"Пользователь удалён!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Delete(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                pageIndex = 0;
                dataGridView1.Rows.Clear();
                data.Clear();
                Query("SELECT * FROM Client JOIN Visits ON Visits.idClient = Client.ID");
                FillTable();
            }
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ID = new int[dataGridView1.Rows.Count];
            for (int i = 0; i < ID.Length-1; i++)
            {
                ID[i] = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            }
            Form3 form3 = new Form3();
            Hide();
            form3.ID = ID;
            form3.ShowDialog();
            pageIndex = 0;
            dataGridView1.Rows.Clear();
            data.Clear();
            Query("SELECT * FROM Client JOIN Visits ON Visits.idClient = Client.ID");
            FillTable();
            Show();
        }
    }
}
