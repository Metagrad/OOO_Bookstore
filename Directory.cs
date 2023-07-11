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
using System.Security.Cryptography; 

namespace OOO_Bookstore
{
    public partial class Directory : Form
    {
        public Directory()
        {
            InitializeComponent();
            this.KeyPreview = true;
            View();
            View2();
            View3();
        }
        System.Windows.Forms.Timer myTimer1 = new System.Windows.Forms.Timer();
        int counttime1 = 60;
        int tx2 = 0;
        string passwd;
        int tx3 = 0;
        int tx4 = 0;

        private void View()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connect))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("Select * From users;", connection);
                    cmd.ExecuteNonQuery();

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dt.Columns[1].ColumnName = "Фамилия пользователя";
                    dt.Columns[2].ColumnName = "Имя пользователя";
                    dt.Columns[3].ColumnName = "Отчество пользователя";
                    dt.Columns[4].ColumnName = "Логин пользователя";
                    dt.Columns[5].ColumnName = "Пароль пользователя";
                    dt.Columns[6].ColumnName = "Доступ";
                    dt.Columns[7].ColumnName = "Телефон";

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка подключения к БД");
            }
        }

        private void View2()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connect))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("Select * From Genres;", connection);
                    cmd.ExecuteNonQuery();

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dt.Columns[1].ColumnName = "Жанр";

                    dataGridView3.DataSource = dt;
                    dataGridView3.Columns[0].Visible = false;
                    foreach (DataGridViewColumn column in dataGridView3.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка подключения к БД");
            }
        }

        private void View3()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connect))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("Select * From publishers;", connection);
                    cmd.ExecuteNonQuery();

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dt.Columns[1].ColumnName = "Издательства";

                    dataGridView2.DataSource = dt;
                    dataGridView2.Columns[0].Visible = false;
                    foreach (DataGridViewColumn column in dataGridView2.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            } catch (Exception)
            {
                MessageBox.Show("Ошибка подключения к БД");
            }
        }

        string connect = @"server=localhost;userid=root;password=;database=Bookstore;charset=utf8";
        public string comStr = "";
        int rowindex1 = 0;
        int rowindex2 = 0;
        int rowindex3 = 0;

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
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

                        MySqlCommand cmd = new MySqlCommand(@"Delete From users Where id_user = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "';", connection);
                        cmd.ExecuteNonQuery();

                        connection.Close();
                    }

                    View();

                    MessageBox.Show("Запись успешно удалена!");
                    textBox1.Text = "";
                }
            }

            catch { MessageBox.Show("Ошибка удаления записи"); }
        }

        private void button2_Click(object sender, EventArgs e)
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

                        MySqlCommand cmd = new MySqlCommand(@"Delete From Genres Where id_genre = '" + dataGridView3.CurrentRow.Cells[0].Value.ToString() + "'", connection);
                        cmd.ExecuteNonQuery();

                        connection.Close();
                    }

                    View2();

                    MessageBox.Show("Запись успешно удалена!");
                    textBox7.Text = "";
                }
            }

            catch { MessageBox.Show("Ошибка удаления записи"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы уверены, что хотите вернуться в главное меню?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы уверены, что хотите вернуться в главное меню?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                //myTimer1.Stop();
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "" || maskedTextBox1.Text == "")
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

                DataTable dt = new DataTable();
                using (MySqlConnection con = new MySqlConnection(connect))
                {
                    con.Open();

                    MySqlCommand com = new MySqlCommand("SELECT COUNT(*) FROM users WHERE user_login = '" + textBox4.Text + "' AND user_password ='" + textBox5.Text + "';", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(com);
                    da.Fill(dt);

                    con.Close();
                }
                passwd = Crypto(textBox5.Text.ToString());

                if (Convert.ToInt32(dt.Rows[0].ItemArray.GetValue(0)) == 0)
                {
                    using (MySqlConnection connection = new MySqlConnection(connect))
                    {
                        connection.Open();

                        MySqlCommand cmd = new MySqlCommand(@"Insert users(user_surname, user_name, user_patronym, user_login, user_password, user_status, user_phone) Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + passwd + "','" + comboBox1.Text + "','" + maskedTextBox1.Text + "');", connection);

                        cmd.ExecuteNonQuery();


                        connection.Close();
                    }

                    View();

                    MessageBox.Show("Запись успешно добавлена!");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    comboBox1.Text = "";
                    maskedTextBox1.Text = "";
                } else
                {
                    MessageBox.Show("Такой пользователь уже существует.");
                }
            //}

            //catch { MessageBox.Show("Ошибка добавления записи"); }
        }

        private string Crypto(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox7.Text == "")
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

                    MySqlCommand cmd = new MySqlCommand(@"Insert Genres(genre_name) Values ('" + textBox7.Text + "');", connection);

                    cmd.ExecuteNonQuery();


                    connection.Close();
                }

                View2();

                MessageBox.Show("Запись успешно добавлена!");
                textBox7.Text = "";
            }

            catch { MessageBox.Show("Ошибка добавления записи"); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
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
                        if (textBox7.Text == "")
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

                        MySqlCommand cmd = new MySqlCommand(@"UPDATE Genres SET genre_name = '" + textBox7.Text + "' WHERE (id_genre = " + dataGridView3.CurrentRow.Cells[0].Value.ToString() + ")", connection);

                        cmd.ExecuteNonQuery();

                        connection.Close();
                    }

                    View2();

                    MessageBox.Show("Запись успешно изменена!");
                    textBox7.Text = "";
                }
            }

            catch { MessageBox.Show("Ошибка изменения записи"); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
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
                        if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "" || maskedTextBox1.Text == "")
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

                        DataTable dt = new DataTable();
                        using (MySqlConnection con = new MySqlConnection(connect))
                        {
                            con.Open();

                            MySqlCommand com = new MySqlCommand("SELECT COUNT(*) FROM users WHERE user_login = '" + textBox4.Text + "' AND user_password =' " + textBox5.Text + "';", con);
                            MySqlDataAdapter da = new MySqlDataAdapter(com);
                            da.Fill(dt);

                            con.Close();
                        }
                        passwd = Crypto(textBox5.Text.ToString());

                        if (Convert.ToInt32(dt.Rows[0].ItemArray.GetValue(0)) == 0)
                        {
                            connection.Open();
                            MySqlCommand cmd1 = new MySqlCommand();
                            cmd1.CommandText = @"UPDATE users SET user_surname = '" + textBox1.Text + "', user_name = '" + textBox2.Text + "', user_patronym = '" + textBox3.Text + "', user_login = '" + textBox4.Text + "', user_password = '" + passwd + "', user_status = '" + comboBox1.Text + "', user_phone = '" + maskedTextBox1.Text + "' WHERE (id_user = " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + ")";
                            cmd1.Connection = connection;

                            cmd1.ExecuteNonQuery();
                            View();

                            MessageBox.Show("Запись успешно изменена!");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            comboBox1.Text = "";
                            maskedTextBox1.Text = "";
                        } else
                        {
                            MessageBox.Show("Такой пользователь уже существует.");
                        }
                    }
                }
            }
            catch { MessageBox.Show("Ошибка изменения записи"); }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox8.Text == "")
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

                    MySqlCommand cmd = new MySqlCommand(@"Insert publishers(publisher_name) Values ('" + textBox8.Text + "');", connection);

                    cmd.ExecuteNonQuery();

                }

                View3();

                MessageBox.Show("Запись успешно добавлена!");
                textBox8.Text = "";
            }

            catch { MessageBox.Show("Ошибка добавления записи"); }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
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
                        if (textBox8.Text == "")
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

                        MySqlCommand cmd = new MySqlCommand(@"UPDATE publishers SET publisher_name = '" + textBox8.Text + "' WHERE (id_publisher = " + dataGridView2.CurrentRow.Cells[0].Value.ToString() + ")", connection);

                        cmd.ExecuteNonQuery();

                    }

                    View3();

                    MessageBox.Show("Запись успешно изменена!");
                    textBox8.Text = "";
                }
            }

            catch { MessageBox.Show("Ошибка изменения записи"); }
        }

        private void button9_Click(object sender, EventArgs e)
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

                        MySqlCommand cmd = new MySqlCommand(@"Delete From publishers Where id_publisher = '" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'", connection);
                        cmd.ExecuteNonQuery();

                    }

                    View3();

                    MessageBox.Show("Запись успешно удалена!");
                    textBox8.Text = "";
                }
            }

            catch (MySql.Data.MySqlClient.MySqlException)
            { 
                MessageBox.Show("Запись невозможно удалить, так как она используется"); 
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка удаления");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы уверены, что хотите вернуться в главное меню?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                //textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            } catch (Exception)
            {
                MessageBox.Show("Выбрана некорректная или пустая строка!");
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox7.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString(); 
            } catch (Exception)
            {
                MessageBox.Show("Выбрана некорректная или пустая строка!");
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox8.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            } catch (Exception)
            {
                MessageBox.Show("Выбрана некорректная или пустая строка!");
            }
        }

        private void Directory_Load(object sender, EventArgs e)
        {
            myTimer1.Tick += new EventHandler(Counter);
            myTimer1.Interval = 1000;
            myTimer1.Start();
        }

        private void Counter(object sender, EventArgs e)
        {
            counttime1--;

            if (counttime1 == 0)
            {
                myTimer1.Stop();
                Timer.fl_unactivity = true;
                this.Close();
            }
        }

        private void Directory_KeyDown(object sender, KeyEventArgs e)
        {
            counttime1 = 60;
        }

        private void Directory_MouseMove(object sender, MouseEventArgs e)
        {
            counttime1 = 60;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0 && e.KeyChar == '-')
                {
                    e.Handled = true;
                }
                if (e.KeyChar != 8)
                {
                    tx2 = textBox1.SelectionStart + 1;
                }
                else
                {
                    tx2 = textBox1.SelectionStart;
                }

                if ((e.KeyChar >= 'А' && e.KeyChar <= 'Я') || (e.KeyChar >= 'а' && e.KeyChar <= 'я') || e.KeyChar == 8 || e.KeyChar == '-' || e.KeyChar == 32)
                {

                }
                else
                {
                    e.Handled = true;
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (textBox2.Text.Length == 0 && e.KeyChar == '-')
                {
                    e.Handled = true;
                }
                if (e.KeyChar != 8)
                {
                    tx3 = textBox2.SelectionStart + 1;
                }
                else
                {
                    tx3 = textBox2.SelectionStart;
                }

                if ((e.KeyChar >= 'А' && e.KeyChar <= 'Я') || (e.KeyChar >= 'а' && e.KeyChar <= 'я') || e.KeyChar == 8 || e.KeyChar == '-' || e.KeyChar == 32)
                {

                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (textBox3.Text.Length == 0 && e.KeyChar == '-')
                {
                    e.Handled = true;
                }
                if (e.KeyChar != 8)
                {
                    tx4 = textBox3.SelectionStart + 1;
                }
                else
                {
                    tx4 = textBox3.SelectionStart;
                }

                if ((e.KeyChar >= 'А' && e.KeyChar <= 'Я') || (e.KeyChar >= 'а' && e.KeyChar <= 'я') || e.KeyChar == 8 || e.KeyChar == '-' || e.KeyChar == 32)
                {

                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox5.Text = Generate();
        }

        private string Generate()
        {
            string sym = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890><?;{}[]:-_=+";

            Random rnd = new Random();

            return sym[rnd.Next(sym.Length)].ToString() + sym[rnd.Next(sym.Length)].ToString() + sym[rnd.Next(sym.Length)].ToString() + sym[rnd.Next(sym.Length)].ToString() + sym[rnd.Next(sym.Length)].ToString() + sym[rnd.Next(sym.Length)].ToString() + sym[rnd.Next(sym.Length)].ToString();
        }
    }
}
