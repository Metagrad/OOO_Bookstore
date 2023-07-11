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
    public partial class ProductView : Form
    {
        public ProductView()
        {
            InitializeComponent();
            View(dataGridView3, offset);
            Count();
        }

        string connect = "host = localhost; uid = root; pwd=; database = Bookstore";
        string Sort = "ASC";
        string filtr;
        int offset = 0;
        int count;
        int cnt;

        private void View(DataGridView name, int offset)
        {
            if (textBox1.Text != "")
            {
                label14.Text = "1";

                Up.Enabled = false;
                Down.Enabled = false;

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

                        dt1.Columns[0].ColumnName = "Артикл";
                        dt1.Columns[1].ColumnName = "Наименование продукта";
                        dt1.Columns[2].ColumnName = "Категория";
                        dt1.Columns[3].ColumnName = "Цена в рублях";

                        name.DataSource = dt1;
                        name.Columns[0].Visible = false;
                    }
                //}

                //catch { MessageBox.Show("Ошибка загрузки товаров!"); }
            }

            else
            {
                Up.Enabled = true;
                Down.Enabled = true;
                label14.Text = "1";
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

                        dt1.Columns[0].ColumnName = "Артикл";
                        dt1.Columns[1].ColumnName = "Наименование продукта";
                        dt1.Columns[2].ColumnName = "Категория";
                        dt1.Columns[3].ColumnName = "Цена в рублях";

                        name.DataSource = dt1;
                        name.Columns[0].Visible = false;
                    }
                //}

                //catch { MessageBox.Show("Ошибка загрузки товаров!"); }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы уверены, что хотите вернуться в главное меню?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Up_Click(object sender, EventArgs e)
        {
            label13.Text = (Convert.ToInt32(label13.Text) + 1).ToString();

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

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void ProductView_Load(object sender, EventArgs e)
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

            comboBox4.DisplayMember = "genre_name";
            comboBox4.ValueMember = "id_genre";
            comboBox4.DataSource = ds.Tables[0];

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

            comboBox5.DisplayMember = "publisher_name";
            comboBox5.ValueMember = "id_publisher";
            comboBox5.DataSource = ds1.Tables[0];

            conn.Open();
            Sql = "SELECT * FROM languages";
            cmd = new MySqlCommand(Sql, conn);
            da = new MySqlDataAdapter(Sql, connect);
            DataSet ds3 = new DataSet();
            da.Fill(ds3);
            cmd.ExecuteNonQuery();
            conn.Close();

            dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            View(dataGridView3, offset);
            Count();
        }

        private void Count()
        {
            cnt = dataGridView3.RowCount;
            label24.Text = "Кол-во записей - " + cnt;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtr = " AND publisher_name = '" + comboBox5.Text + "'";
            View(dataGridView3, offset);
            Count();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtr = " AND genre_name = '" + comboBox4.Text + "'";
            View(dataGridView3, offset);
            Count();
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

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ManufacturerTextBox.Text = dataGridView3.CurrentRow.Cells[8].Value.ToString();
                TitleTextBox.Text = dataGridView3.CurrentRow.Cells[2].Value.ToString();
                CompoundTextBox.Text = dataGridView3.CurrentRow.Cells[5].Value.ToString();
                textBox6.Text = dataGridView3.CurrentRow.Cells[4].Value.ToString();
                ArticulTextBox.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
                CountTextBox.Text = dataGridView3.CurrentRow.Cells[10].Value.ToString();
                PriceTextBox.Text = dataGridView3.CurrentRow.Cells[7].Value.ToString();
                textBox2.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                textBox3.Text = dataGridView3.CurrentRow.Cells[6].Value.ToString();
                textBox4.Text = dataGridView3.CurrentRow.Cells[9].Value.ToString();
                textBox5.Text = dataGridView3.CurrentRow.Cells[11].Value.ToString();
                maskedTextBox1.Text = dataGridView3.CurrentRow.Cells[13].Value.ToString();
                pictureBox.ImageLocation = "Pics/" + Convert.ToString(dataGridView3.CurrentRow.Cells[12].Value.ToString());
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Выбрана пустая строка!");
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
            }
        }
    }
}
