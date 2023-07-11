using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OOO_Bookstore
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            View(dataGridView3, offset);
            Count();
        }

        public string Filename = "";
        string Sort = "ASC";
        string filtr;
        bool filtr_fl = false;
        int cnt;
        private void View(DataGridView name, int offset)
        {
            if (textBox1.Text != "")
            {
                label14.Text = "1";

                Up.Enabled = false;
                Down.Enabled = false;
                filtr_fl = true;

                //try
                //{
                    using (MySqlConnection connection = new MySqlConnection(connect))
                    {
                        connection.Open();

                        MySqlCommand cmd1 = new MySqlCommand(@"Select book_id, article_num, book_name, book_author, Genres.genre_name, book_description, book_listnum, cost, publishers.publisher_name, book_publish_year, book_num, languages.language_name, photo, size From books
                                                        Inner Join Bookstore.Genres On books.id_genre = Genres.id_genre
                                                        Inner Join Bookstore.publishers On books.book_publisher = publishers.id_publisher
                                                        Inner Join Bookstore.languages On books.language_id = languages.language_id
                                                        Where book_name Like '%%" + textBox1.Text + "%%'" + filtr + " Order by book_name " + Sort + " Limit 10 Offset " + offset + " ", connection);
                        cmd1.ExecuteNonQuery();

                        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();

                        da1.Fill(dt1);

                        dt1.Columns[1].ColumnName = "Артикул";
                        dt1.Columns[2].ColumnName = "Название книги";
                        dt1.Columns[3].ColumnName = "Автор";
                        dt1.Columns[4].ColumnName = "Жанр";
                        dt1.Columns[5].ColumnName = "Описание";
                        dt1.Columns[6].ColumnName = "Кол-во листов";
                        dt1.Columns[7].ColumnName = "Стоимость";
                        dt1.Columns[8].ColumnName = "Издательство";
                        dt1.Columns[9].ColumnName = "Год издания";
                        dt1.Columns[10].ColumnName = "Кол-во на складе";
                        dt1.Columns[11].ColumnName = "Язык";
                        dt1.Columns[13].ColumnName = "Размер";

                        foreach (DataGridViewColumn column in name.Columns)
                        {
                            column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }

                        name.DataSource = dt1;
                        name.Columns[0].Visible = false;
                        name.Columns[12].Visible = false;
                    }
                //}

                //catch { MessageBox.Show("Ошибка загрузки товаров!"); }
            }

            else
            {
                Up.Enabled = true;
                Down.Enabled = true;

                //try
                //{
                    using (MySqlConnection connection = new MySqlConnection(connect))
                    {
                        connection.Open();

                        MySqlCommand cmd1 = new MySqlCommand(@"Select book_id, article_num, book_name, book_author, Genres.genre_name, book_description, book_listnum, cost, publishers.publisher_name, book_publish_year, book_num, languages.language_name, photo, size From books
                                                        Inner Join Bookstore.Genres On books.id_genre = Genres.id_genre
                                                        Inner Join Bookstore.publishers On books.book_publisher = publishers.id_publisher
                                                        Inner Join Bookstore.languages On books.language_id = languages.language_id
                                                        Where book_name Like '%%" + textBox1.Text + "%%'" + filtr + " Order by book_name " + Sort + " Limit 10 Offset " + offset + " ", connection);
                        cmd1.ExecuteNonQuery();

                        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();

                        MySqlCommand cmd2 = new MySqlCommand(@"Select * From books", connection);
                        cmd2.ExecuteNonQuery();

                        MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                        DataTable dt2 = new DataTable();

                        da1.Fill(dt1);
                        da2.Fill(dt2);

                        count = dt2.Rows.Count;

                        if ((dt2.Rows.Count / 10) % 10 != 0)
                        {
                            decimal round = Math.Round((decimal)(dt2.Rows.Count / 10), 1);

                            label14.Text = (round + 1).ToString();
                        }

                        else { label14.Text = (dt2.Rows.Count / 10).ToString(); }

                        dt1.Columns[1].ColumnName = "Артикул";
                        dt1.Columns[2].ColumnName = "Название книги";
                        dt1.Columns[3].ColumnName = "Автор";
                        dt1.Columns[4].ColumnName = "Жанр";
                        dt1.Columns[5].ColumnName = "Описание";
                        dt1.Columns[6].ColumnName = "Кол-во листов";
                        dt1.Columns[7].ColumnName = "Стоимость";
                        dt1.Columns[8].ColumnName = "Издательство";
                        dt1.Columns[9].ColumnName = "Год издания";
                        dt1.Columns[10].ColumnName = "Кол-во на складе";
                        dt1.Columns[11].ColumnName = "Язык";
                        dt1.Columns[13].ColumnName = "Размер";

                        foreach (DataGridViewColumn column in name.Columns)
                        {
                            column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }

                        name.DataSource = dt1;
                        name.Columns[0].Visible = false;
                        name.Columns[12].Visible = false;
                    }
                //}

                //catch { MessageBox.Show("Ошибка загрузки товаров!"); }
            }
        }

        string connect = "host = localhost; uid = root; pwd=; database = Bookstore";

        int offset = 0;
        int count;

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                "Вы уверены что хотите удалить эту запись?",
                "Сообщение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection(connect))
                    {
                        connection.Open();

                        MySqlCommand cmd = new MySqlCommand(@"Delete From books Where book_id = '" + dataGridView3.CurrentRow.Cells[0].Value.ToString() + "';", connection);
                        cmd.ExecuteNonQuery();

                        connection.Close();
                    }

                    View(dataGridView3, offset);

                    MessageBox.Show("Запись успешно удалена!");
                    textBox1.Text = "";
                }
            }

            catch { MessageBox.Show("Ошибка удаления записи"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (comboBox1.Text == "" || TitleTextBox.Text == "" || CompoundTextBox.Text == "" || CategoryTextBox.Text == "" || ArticulTextBox.Text == "" || CountTextBox.Text == "" || PriceTextBox.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox2.Text == "")
                {
                    DialogResult result = MessageBox.Show(
                    "Не все поля заполнены!",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.OK)
                    {
                        return;
                    }

                    else { return; }
                }

                using (MySqlConnection connection = new MySqlConnection(connect))
                {
                    connection.Open();
                    int categid = (int)CategoryTextBox.SelectedValue;
                    int publishid = (int)comboBox1.SelectedValue;
                    int langid = (int)comboBox2.SelectedValue;
                    if (Filename == "")
                    {
                        Filename = "picture.png";
                    }
                    MySqlCommand cmd = new MySqlCommand(@"Insert books(article_num, book_name, book_author, id_genre, book_description, book_listnum, cost, book_publisher, book_publish_year, book_num, language_id, photo) Values ('" + ArticulTextBox.Text + "','" + TitleTextBox.Text + "','" + textBox2.Text + "','" + categid.ToString() + "','" + CompoundTextBox.Text + "','" + textBox3.Text + "','" + PriceTextBox.Text + "','" + publishid.ToString() + "','" + textBox4.Text + "','" + CountTextBox.Text + "','" + langid.ToString() + "','" + Filename.ToString() + "');", connection);

                    cmd.ExecuteNonQuery();

                    connection.Close();
                }

                View(dataGridView3, offset);
                Count();
                MessageBox.Show("Запись успешно добавлена!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                maskedTextBox1.Text = "";
            //}

            //catch { MessageBox.Show("Ошибка добавления записи"); }
        }

        private void Products_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connect);
            conn.Open();
            string Sql = "SELECT * FROM Genres";
            MySqlCommand cmd = new MySqlCommand(Sql, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(Sql, connect);
            DataSet ds = new DataSet();
            DataSet ds_0 = new DataSet();
            da.Fill(ds);
            da.Fill(ds_0);
            cmd.ExecuteNonQuery();
            conn.Close();

            CategoryTextBox.DisplayMember = "genre_name";
            CategoryTextBox.ValueMember = "id_genre";
            CategoryTextBox.DataSource = ds.Tables[0];
            comboBox4.DisplayMember = "genre_name";
            comboBox4.ValueMember = "id_genre";
            comboBox4.DataSource = ds_0.Tables[0];

            conn.Open();
            Sql = "SELECT * FROM publishers";
            cmd = new MySqlCommand(Sql, conn);
            da = new MySqlDataAdapter(Sql, connect);
            DataSet ds1 = new DataSet();
            DataSet ds_1 = new DataSet();
            da.Fill(ds1);
            da.Fill(ds_1);
            cmd.ExecuteNonQuery();
            conn.Close();

            comboBox1.DisplayMember = "publisher_name";
            comboBox1.ValueMember = "id_publisher";
            comboBox1.DataSource = ds1.Tables[0];
            comboBox5.DisplayMember = "publisher_name";
            comboBox5.ValueMember = "id_publisher";
            comboBox5.DataSource = ds_1.Tables[0];

            conn.Open();
            Sql = "SELECT * FROM languages";
            cmd = new MySqlCommand(Sql, conn);
            da = new MySqlDataAdapter(Sql, connect);
            DataSet ds3 = new DataSet();
            da.Fill(ds3);
            cmd.ExecuteNonQuery();
            conn.Close();

            comboBox2.DisplayMember = "language_name";
            comboBox2.ValueMember = "language_id";
            comboBox2.DataSource = ds3.Tables[0];

            dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            CategoryTextBox.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog upload = new OpenFileDialog())
            {
                upload.Filter = "jpg Files|*.jpg|png Files|*.png";
                upload.Title = "Select File";
                if (upload.ShowDialog() != DialogResult.OK)
                    return;
                string filePath = upload.FileName;
                Filename = System.IO.Path.GetFileName(filePath);
                //label1.Text = Filename;
                pictureBox.ImageLocation = "Pics/" + Filename;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //try
            //{
                DialogResult result = MessageBox.Show(
            "Вы уверены что хотите изменить эту запись?",
            "Сообщение",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
            );

                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection(connect))
                    {
                        if (comboBox1.Text == "" || TitleTextBox.Text == "" || CompoundTextBox.Text == "" || CategoryTextBox.Text == "" || ArticulTextBox.Text == "" || CountTextBox.Text == "" || PriceTextBox.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox2.Text == "")
                        {
                            DialogResult result1 = MessageBox.Show(
                            "Не все поля заполнены!",
                            "Сообщение",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );

                            if (result1 == DialogResult.OK)
                            {
                                return;
                            }

                            else { return; }
                        }

                        connection.Open();
                        int categid = (int)CategoryTextBox.SelectedValue;
                        int publishid = (int)comboBox1.SelectedValue;
                        int langid = (int)comboBox2.SelectedValue;
                        if (Filename == "")
                        {
                            Filename = "picture.png";
                        }
                        MySqlCommand cmd = new MySqlCommand(@"UPDATE books SET article_num = '" + ArticulTextBox.Text + "', book_name = '" + TitleTextBox.Text + "', book_author = '" + textBox2.Text + "', id_genre = '" + categid.ToString() + "', book_description = '" + CompoundTextBox.Text + "', book_listnum = '" + textBox3.Text + "', cost = '" + PriceTextBox.Text + "', book_publisher ='" + publishid.ToString() + "', book_publish_year = '" + textBox4.Text + "', book_num = '" + CountTextBox.Text + "', language_id = '" + langid.ToString() + "', photo = '" + Filename + "', size = '" + maskedTextBox1.Text + "' WHERE (book_id = " + dataGridView3.CurrentRow.Cells[0].Value.ToString() + ")", connection);

                        cmd.ExecuteNonQuery();

                        connection.Close();

                        View(dataGridView3, offset);
                        Count();
                        MessageBox.Show("Запись успешно изменена!");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                        maskedTextBox1.Text = "";
                    }
                }
            //}
            //catch { MessageBox.Show("Ошибка изменения записи"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArticulTextBox.Text = Articul();
        }

        private string Articul()
        {
            string sym = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";

            Random rnd = new Random();

            return sym[rnd.Next(sym.Length)].ToString() + sym[rnd.Next(sym.Length)].ToString() + sym[rnd.Next(sym.Length)].ToString() + sym[rnd.Next(sym.Length)].ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "по возрастанию(от А до Я)")
            {
                Sort = "ASC";
                View(dataGridView3, offset);
                Count();
            }
            else
            {
                Sort = "DESC";
                View(dataGridView3, offset);
                Count();
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtr = " AND genre_name = '" + comboBox4.Text + "'";
            View(dataGridView3, offset);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtr = " AND publisher_name = '" + comboBox5.Text + "'";
            View(dataGridView3, offset);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы уверены, что хотите вернуться в главное меню?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                comboBox1.Text = dataGridView3.CurrentRow.Cells[8].Value.ToString();
                TitleTextBox.Text = dataGridView3.CurrentRow.Cells[2].Value.ToString();
                CompoundTextBox.Text = dataGridView3.CurrentRow.Cells[5].Value.ToString();
                CategoryTextBox.Text = dataGridView3.CurrentRow.Cells[4].Value.ToString();
                ArticulTextBox.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
                CountTextBox.Text = dataGridView3.CurrentRow.Cells[10].Value.ToString();
                PriceTextBox.Text = dataGridView3.CurrentRow.Cells[7].Value.ToString();
                textBox2.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                textBox3.Text = dataGridView3.CurrentRow.Cells[6].Value.ToString();
                textBox4.Text = dataGridView3.CurrentRow.Cells[9].Value.ToString();
                comboBox2.Text = dataGridView3.CurrentRow.Cells[11].Value.ToString();
                maskedTextBox1.Text = dataGridView3.CurrentRow.Cells[13].Value.ToString();
                pictureBox.ImageLocation = "Pics/" + Convert.ToString(dataGridView3.CurrentRow.Cells[12].Value.ToString());
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Выбрана пустая строка!");
            }
        }

        private void comboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                filtr = " AND genre_name = '" + comboBox4.Text + "'";
                View(dataGridView3, offset);
                Count();
            }

        }

        private void comboBox5_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                filtr = " AND publisher_name = '" + comboBox5.Text + "'";
                View(dataGridView3, offset);
                Count();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;
            }
            if (checkBox1.Checked == false)
            {
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox4.SelectedIndex = -1;
                comboBox5.SelectedIndex = -1;
                filtr = "";
                View(dataGridView3, offset);
                Count();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            View(dataGridView3, offset);
            Count();
        }

        private void Count()
        {
            MySqlConnection conn = new MySqlConnection(connect);
            conn.Open();
            string Sql = "SELECT COUNT(*) FROM books;";
            MySqlCommand cmd = new MySqlCommand(Sql, conn);
            label24.Text ="Кол-во записей - " + cmd.ExecuteScalar();
            label23.Text = "на данной странице - " + dataGridView3.Rows.Count;
            conn.Close();
        }

        private void Up_Click(object sender, EventArgs e)
        {
            label13.Text = (Convert.ToInt32(label13.Text) + 1).ToString();
            Count();
            offset += 10;
            View(dataGridView3, offset);

            if (label13.Text == label14.Text)
            {
                Up.Enabled = false;
            }

            else { Up.Enabled = true; }

            if (label13.Text == "1")
            {
                Down.Enabled = false;
            }

            else { Down.Enabled = true; }
        }

        private void Down_Click(object sender, EventArgs e)
        {
            label13.Text = (Convert.ToInt32(label13.Text) - 1).ToString();
            Count();
            offset -= 10;
            View(dataGridView3, offset);

            if (label13.Text == "1")
            {
                Down.Enabled = false;
            }

            else { Down.Enabled = true; }

            if (label13.Text == label14.Text)
            {
                Up.Enabled = false;
            }

            else { Up.Enabled = true; }
        }

        private void dataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
