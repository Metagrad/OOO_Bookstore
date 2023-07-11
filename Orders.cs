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
    public partial class Orders : Form
    {
        public string name;
        public string privilegue;
        public string Idd;
        public bool context_flag = true;
        public Orders(string id, string privileg, string fio)
        {
            InitializeComponent();
            name = fio;
            privilegue = privileg;
            Idd = id;
            ToolStripMenuItem AddBask = new ToolStripMenuItem("Добавить в корзину");
            contextMenuStrip1.Items.AddRange(new[] {AddBask});
            AddBask.Click += contextMenuStrip1_Click;
        }
        string connectionString = @"server=localhost;userid=root;password=;database=Bookstore;charset=utf8";
        public string Str = "";
        int rowindex1 = -1;
        bool bask_fl = false;
        int rowindex2 = -1; 

        private void button3_Click(object sender, EventArgs e)
        {
            OrdInform.Val = 0;
            OrdInform.OrderDate = "";
            OrdInform.discount = 0;
            OrdInform.FinVal = 0;
            button4.Visible = true;
            button3.Enabled = false;
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string temp = DateTime.Now.Date.ToString();
            temp = temp.Substring(0, 10);
            temp = temp.Substring(6, 4) + "-" + temp.Substring(3, 2) + "-" + temp.Substring(0, 2);
            OrdInform.OrderDate = temp;
            string Sql = "INSERT INTO `order` (order_date, id_user, status) VALUES ('" + OrdInform.OrderDate + "', " + Idd + ", 'Обработка');";
            MySqlCommand cmd = new MySqlCommand(Sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand userCommand2 = new MySqlCommand("SELECT order_id FROM `order` WHERE order_date = '" + OrdInform.OrderDate + "' AND id_user = " + Idd + " AND (value IS NULL OR value = 0 );", connection);
            MySqlDataReader reader2 = userCommand2.ExecuteReader();
            while (reader2.Read())
            {
                OrdInform.selectedOrder = Convert.ToInt32(reader2[0]);
            }
            reader2.Close();

            fillDataGrid2(@"SELECT basket.order_id, books.book_name as 'Название', 
                            basket.count as 'Кол-во', basket.book_id, books.cost as 'цена'
                            FROM basket INNER JOIN books ON books.book_id = basket.book_id  WHERE basket.count > 0 AND order_id = " + OrdInform.selectedOrder + ";");
        }

        private void fillDataGrid2(string comStr)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            MySqlCommand com = new MySqlCommand(comStr, conn);
            com.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            dataGridView2.Columns["order_id"].Visible = false;
            dataGridView2.Columns["book_id"].Visible = false;
            conn.Close();
            dataGridView2.Columns["Название"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            CostCheck();
            IndexCheck();
        }

        private void CostCheck()
        {
            OrdInform.Val = 0;
            OrdInform.discount = 1;
            OrdInform.FinVal = 0;
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                OrdInform.Val = OrdInform.Val + (Convert.ToInt32(r.Cells[4].Value) * Convert.ToInt32(r.Cells[2].Value));
            }
            if (numericUpDown1.Value == 2)
            {
                OrdInform.discount = 0.26;
            }
            else if (OrdInform.Val > 2500)
            {
                OrdInform.discount = 0.35;
            }
            else if (OrdInform.Val > 1000 && OrdInform.Val < 2500)
            {
                OrdInform.discount = 0.45;
            }
            OrdInform.FinVal = OrdInform.Val * OrdInform.discount;
            ValueToOrder();
        }

        private void ValueToOrder()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string Sql = "UPDATE `order` SET value = '" + OrdInform.Val.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "', discount = '" + OrdInform.discount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "', finalval = '" + OrdInform.FinVal.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "' WHERE order_id=" + OrdInform.selectedOrder.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ";";
            MySqlCommand cmd = new MySqlCommand(Sql, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(Sql, connectionString);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            OrdInform.selectedOrder = -1;
            OrdInform.Val = 0;
            OrdInform.FinVal = 0;
            OrdInform.OrderDate = "";
            OrdInform.discount = 1;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            fillDataGrid1(@"Select book_id, article_num, book_name, book_author, Genres.genre_name, book_description, book_listnum, cost, publishers.publisher_name, book_publish_year, book_num, languages.language_name, photo, size From books
              Inner Join Bookstore.Genres On books.id_genre = Genres.id_genre
              Inner Join Bookstore.publishers On books.book_publisher = publishers.id_publisher
              Inner Join Bookstore.languages On books.language_id = languages.language_id WHERE books.book_num > 0");
        }

        public void IndexCheck()
        {
            if (rowindex1 < 0)
            {
                
            }
            else
            {
                if (OrdInform.selectedOrder > 0)
                {
                    dataGridView1.ContextMenuStrip = contextMenuStrip1;
                }
                else
                {
                }
            }
            if (rowindex2 < 0)
            {
            }
            else
            {

            }
            if (dataGridView2.Rows.Count < 1)
            {
                button4.Enabled = false;
            }
            else
            {
                button4.Enabled = true;
            }
            //if ()
            //{

            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            CompoundTextBox.Clear();
            TitleTextBox.Clear();
            PriceTextBox.Clear();
            CountTextBox.Clear();
            ManufacturerTextBox.Clear();
            ArticulTextBox.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox2.SelectedIndex = -1;
            CategoryTextBox.SelectedIndex = -1;
            pictureBox.ImageLocation = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void fillDataGrid1(string Str)
        {
            Str = Str + " AND books.article_num LIKE '" + textBox1.Text + "%' ;";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            MySqlCommand com = new MySqlCommand(Str, conn);
            com.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns[1].ColumnName = "Артикул";
            dt.Columns[2].ColumnName = "Название книги";
            dt.Columns[3].ColumnName = "Автор";
            dt.Columns[4].ColumnName = "Жанр";
            dt.Columns[5].ColumnName = "Описание";
            dt.Columns[6].ColumnName = "Кол-во листов";
            dt.Columns[7].ColumnName = "Стоимость";
            dt.Columns[8].ColumnName = "Издательство";
            dt.Columns[9].ColumnName = "Год издания";
            dt.Columns[10].ColumnName = "Кол-во на складе";
            dt.Columns[11].ColumnName = "Язык";
            dt.Columns[13].ColumnName = "Размер";
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            conn.Close();
            IndexCheck();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            IndexCheck();
            this.Hide();
            OrderList form = new OrderList(Idd, name, privilegue);
            form.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
            button3.Enabled = true;
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string Sql = "DELETE FROM `order` WHERE value IS NULL OR value = 0 OR status = 'Обработка';";
            MySqlCommand cmd = new MySqlCommand(Sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            fillDataGrid2(@"SELECT basket.order_id, books.book_name as 'Название', 
                            basket.count as 'Кол-во', basket.book_id, books.cost as 'цена'
                            FROM basket INNER JOIN books ON books.book_id = basket.book_id  WHERE basket.count > 0 AND order_id = " + OrdInform.selectedOrder + ";");
            conn = new MySqlConnection(connectionString);
            if (!OrdInform.flag)
            {
                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    Sql = "UPDATE books SET book_num = book_num + " + (Convert.ToInt32(r.Cells[2].Value)) + " WHERE book_id='" + (Convert.ToInt32(r.Cells[3].Value)) + "';";
                    cmd = new MySqlCommand(Sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    Sql = "DELETE FROM basket WHERE book_id = " + (Convert.ToInt32(r.Cells[3].Value)) + " AND order_id = " + OrdInform.selectedOrder + " AND count = " + (Convert.ToInt32(r.Cells[2].Value)) + ";";
                    cmd = new MySqlCommand(Sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    rowindex2 = -1;
                    fillDataGrid1(@"Select book_id, article_num, book_name, book_author, Genres.genre_name, book_description, book_listnum, cost, publishers.publisher_name, book_publish_year, book_num, languages.language_name, photo, size From books
              Inner Join Bookstore.Genres On books.id_genre = Genres.id_genre
              Inner Join Bookstore.publishers On books.book_publisher = publishers.id_publisher
              Inner Join Bookstore.languages On books.language_id = languages.language_id");
                }
                fillDataGrid2(@"SELECT basket.order_id, books.book_name as 'Название', 
                            basket.count as 'Кол-во', basket.book_id, books.cost as 'цена'
                            FROM basket INNER JOIN books ON books.book_id = basket.book_id  WHERE basket.count > 0 AND order_id = " + OrdInform.selectedOrder + ";");
            }
            OrdInform.selectedOrder = -1;
            this.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowindex2 = e.RowIndex;
            IndexCheck();
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            if (context_flag == false)
            {
                return;
            }
            else
            {
                bool flag = false;
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                string Sql = "UPDATE books SET book_num = " + (Convert.ToInt32(CountTextBox.Text) - numericUpDown1.Value).ToString() + " WHERE book_id='" + dataGridView1.Rows[rowindex1].Cells[0].Value.ToString() + "';";
                MySqlCommand cmd = new MySqlCommand(Sql, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(Sql, connectionString);
                cmd.ExecuteNonQuery();
                conn.Close();
                int AddedIndex = 0;
                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    string temp = dataGridView1.Rows[rowindex1].Cells[0].Value.ToString();
                    if (dataGridView1.Rows[rowindex1].Cells[0].Value.ToString() == r.Cells[3].Value.ToString())
                    {
                        flag = true;
                        AddedIndex = r.Index;
                        break;
                    }
                }
                if (flag)
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    Sql = "UPDATE basket SET count  = " + (Convert.ToInt32(dataGridView2.Rows[AddedIndex].Cells[2].Value) + numericUpDown1.Value) + " WHERE book_id='" + dataGridView1.Rows[rowindex1].Cells[0].Value.ToString() + "'; ";
                    cmd = new MySqlCommand(Sql, conn);
                    da = new MySqlDataAdapter(Sql, connectionString);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    Sql = "INSERT INTO basket (order_id, book_id, count) VALUE (" + OrdInform.selectedOrder + ", " + dataGridView1.Rows[rowindex1].Cells[0].Value.ToString() + ", " + numericUpDown1.Value + ");";
                    cmd = new MySqlCommand(Sql, conn);
                    da = new MySqlDataAdapter(Sql, connectionString);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                rowindex1 = -1;
                context_flag = false;
                fillDataGrid1(@"Select book_id, article_num, book_name, book_author, Genres.genre_name, book_description, book_listnum, cost, publishers.publisher_name, book_publish_year, book_num, languages.language_name, photo, size From books
              Inner Join Bookstore.Genres On books.id_genre = Genres.id_genre
              Inner Join Bookstore.publishers On books.book_publisher = publishers.id_publisher
              Inner Join Bookstore.languages On books.language_id = languages.language_id WHERE books.book_num > 0");
                fillDataGrid2(@"SELECT basket.order_id, books.book_name as 'Название', 
                            basket.count as 'Кол-во', basket.book_id, books.cost as 'цена'
                            FROM basket INNER JOIN books ON books.book_id = basket.book_id  WHERE basket.count > 0 AND order_id = " + OrdInform.selectedOrder + ";");
                CostCheck();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox2.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                TitleTextBox.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                CompoundTextBox.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                CategoryTextBox.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                CategoryTextBox.Items.Add(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                CategoryTextBox.SelectedIndex = 0;
                CategoryTextBox.Enabled = false;
                ManufacturerTextBox.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                ArticulTextBox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                CountTextBox.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                PriceTextBox.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                comboBox2.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                comboBox2.Items.Add(dataGridView1.CurrentRow.Cells[11].Value.ToString());
                maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
                comboBox2.SelectedIndex = 0;
                comboBox2.Enabled = false;
                pictureBox.ImageLocation = "Pics/" + Convert.ToString(dataGridView1.CurrentRow.Cells[12].Value.ToString());
                //Filename = pictureBox.ImageLocation;
                numericUpDown1.Value = 1;
                numericUpDown1.Maximum = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
                rowindex1 = e.RowIndex;
                IndexCheck();
                context_flag = true;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("");
            }
        }
    }
}
