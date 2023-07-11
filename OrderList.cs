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
    public partial class OrderList : Form
    {
        string connectionString = @"server=localhost;userid=root;password=;database=Bookstore;charset=utf8";
        public string comStr = "";
        public string name;
        public string privilegue;
        public string Idd;
        int rowindex1 = -1;
        int rowindex2 = -1; 
        public string tempCmd = @"SELECT basket.order_id, books.book_name as 'Название',
                            basket.count as 'Кол-во', basket.book_id, books.cost as 'Цена, Р'
                            FROM basket INNER JOIN books ON books.book_id = basket.book_id";
        public OrderList(string id, string fio, string privil)
        {
            InitializeComponent();
            name = fio;
            Idd = id;
            privilegue = privil;
        }

        public void fillDataGrid1(string comStr)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                MySqlCommand com = new MySqlCommand(comStr, conn);
                com.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["order_id"].Visible = false;
                dataGridView1.Columns["book_id"].Visible = false;
                conn.Close();
                dataGridView1.Columns["Название"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            } catch (Exception)
            {
                MessageBox.Show("Ошибка подключения к БД");
            }
        }

        private void OrderList_Load(object sender, EventArgs e)
        {
            try
            {
                OrdInform.flag = false;

                fillDataGrid1(tempCmd + " WHERE order_id=" + OrdInform.selectedOrder + "");
                label3.Text = OrdInform.selectedOrder.ToString();
                label5.Text = OrdInform.OrderDate;
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                string Sql = "SELECT value, discount, finalval FROM `order` WHERE order_id = " + OrdInform.selectedOrder + "";
                MySqlCommand cmd = new MySqlCommand(Sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    OrdInform.Val = Convert.ToDouble(reader[0]);
                    OrdInform.discount = Convert.ToDouble(reader[1]);
                    OrdInform.FinVal = Convert.ToDouble(reader[2]);
                }
                reader.Close();
                conn.Close();

                ValueTextBox.Text = "Сумма, Р: " + OrdInform.Val.ToString();
                discountTextBox.Text = "Скидка: " + ((1 - OrdInform.discount) * 100).ToString() + "%";
                FinValTextBox.Text = "Итого, Р: " + OrdInform.FinVal.ToString();
                label2.Text = privilegue + ": " + name;
                if (privilegue == "Администратор")
                {
                    comboBox1.Items.Add("Выполнен");
                    comboBox1.Items.Add("Отменен");
                }
                else if (privilegue == "Пользователь")
                {
                    label6.Visible = false;
                    comboBox1.Visible = false;
                    button2.Visible = false;
                    comboBox1.Items.Add("Выполнен");
                }
                if (OrdInform.flag)
                {
                    button2.Visible = true;
                }

                conn = new MySqlConnection(connectionString);
                conn.Open();
                Sql = "SELECT status FROM `order` WHERE order_id = " + OrdInform.selectedOrder + "";
                cmd = new MySqlCommand(Sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[0].ToString() == "Выполнен")
                    {
                        comboBox1.SelectedIndex = 0;
                        OrdInform.flag = true;
                    }
                    else if (reader[0].ToString() == "Отменён")
                    {
                        comboBox1.SelectedIndex = 1;
                        OrdInform.flag = true;
                    }
                }
                if (OrdInform.flag)
                {
                    button1.Enabled = false;
                }
                reader.Close();
                conn.Close();
            } catch (Exception)
            {
                MessageBox.Show("Ошибка подключения к БД");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                String Sql = "UPDATE `order` SET status = '" + comboBox1.SelectedItem.ToString() + "' WHERE order_id='" + OrdInform.selectedOrder.ToString() + "';";
                MySqlCommand cmd = new MySqlCommand(Sql, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(Sql, connectionString);
                cmd.ExecuteNonQuery();
                conn.Close();
                OrdInform.flag = true;
                button1.Enabled = false;
                if (OrdInform.flag)
                {
                    button2.Visible = true;
                }
            } catch (Exception)
            {
                MessageBox.Show("Ошибка занесения заказа в БД");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (OrdInform.flag)
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                string Sql = "DELETE FROM `order` WHERE value IS NULL OR value = 0 OR status = 'Обработка';";
                MySqlCommand cmd = new MySqlCommand(Sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                this.Close();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Заказ не был утверждён. Вы действительно хотите выйти?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    MySqlConnection conn = new MySqlConnection(connectionString);
                    conn.Open();
                    string Sql = "DELETE FROM `order` WHERE value IS NULL OR value = 0;";
                    MySqlCommand cmd = new MySqlCommand(Sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    this.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //try
            //{
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                String Sql = "UPDATE books SET book_num = book_num + " + Convert.ToInt32(dataGridView1.Rows[rowindex2].Cells[2].Value) + " WHERE book_id='" + dataGridView1.Rows[rowindex2].Cells[3].Value.ToString() + "';";
                MySqlCommand cmd = new MySqlCommand(Sql, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(Sql, connectionString);
                cmd.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                if (Convert.ToInt32(dataGridView1.Rows[rowindex2].Cells[2].Value) == 1)
                {
                    Sql = "DELETE FROM basket WHERE book_id = " + dataGridView1.Rows[rowindex2].Cells[3].Value.ToString() + " AND order_id = " + OrdInform.selectedOrder + " AND count = " + Convert.ToInt32(dataGridView1.Rows[rowindex2].Cells[2].Value) + ";";
                    cmd = new MySqlCommand(Sql, conn);
                    da = new MySqlDataAdapter(Sql, connectionString);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    rowindex2 = -1;
                    fillDataGrid1(@"SELECT basket.order_id, books.book_name as 'Название', 
                            basket.count as 'Кол-во', basket.book_id, books.cost as 'цена'
                            FROM basket INNER JOIN books ON books.book_id = basket.book_id  WHERE basket.count > 0 AND order_id = " + OrdInform.selectedOrder + ";");
                    CostCheck();
                }
                else
                {
                    Sql = "UPDATE basket SET count = " + Convert.ToInt32(dataGridView1.Rows[rowindex2].Cells[2].Value) + "-1 WHERE order_id = " + OrdInform.selectedOrder + " AND book_id = " + dataGridView1.Rows[rowindex2].Cells[3].Value.ToString() + ";";
                    cmd = new MySqlCommand(Sql, conn);
                    da = new MySqlDataAdapter(Sql, connectionString);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    rowindex2 = -1;
                    fillDataGrid1(@"SELECT basket.order_id, books.book_name as 'Название', 
                            basket.count as 'Кол-во', basket.book_id, books.cost as 'цена'
                            FROM basket INNER JOIN books ON books.book_id = basket.book_id  WHERE basket.count > 0 AND order_id = " + OrdInform.selectedOrder + ";");
                    CostCheck();
                }
                conn.Close();
            //} catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void CostCheck()
        {
            try
            {
                OrdInform.Val = 0;
                OrdInform.discount = 1;
                OrdInform.FinVal = 0;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    OrdInform.Val = OrdInform.Val + (Convert.ToInt32(r.Cells[4].Value) * Convert.ToInt32(r.Cells[2].Value));
                    if ((Convert.ToInt32(r.Cells[2].Value) == 2))
                    {
                        OrdInform.discount = 0.26;
                    }
                    else if (OrdInform.Val > 2500)
                    {
                        OrdInform.discount = 0.35;
                    }
                    else if (OrdInform.Val > 1000 && OrdInform.Val < 2500)
                    {
                        OrdInform.discount = 0.20;
                    }
                }
                OrdInform.FinVal = OrdInform.Val * OrdInform.discount;
                ValueToOrder();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ValueToOrder()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                string Sql = "UPDATE `order` SET value = '" + OrdInform.Val.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "', discount = '" + OrdInform.discount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "', finalval = '" + OrdInform.FinVal.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "' WHERE order_id=" + OrdInform.selectedOrder.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ";";
                MySqlCommand cmd = new MySqlCommand(Sql, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(Sql, connectionString);
                cmd.ExecuteNonQuery();
                conn.Close();
            } catch (Exception)
            {
                MessageBox.Show("Ошибка вычисления суммы заказа");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string t = "";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string Sql = @"SELECT users.user_surname, users.user_name, users.user_patronym
                           FROM Bookstore.`order` INNER JOIN users ON users.id_user = Bookstore.`order`.id_user";
            MySqlCommand cmd = new MySqlCommand(Sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                t = reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString();
            }
            reader.Close();
            conn.Close();

            if (OrdInform.flag)
            {
                CreateWord cw = new CreateWord
                {
                    TabRows = dataGridView1.Rows.Count,
                    order_num = OrdInform.selectedOrder,
                    order_usr = t,
                    Disc = ((1 - OrdInform.discount) * 100).ToString() + "%",
                    Cost = OrdInform.Val.ToString(),
                    FullPrice = OrdInform.FinVal.ToString()
                };
                cw.Chek(dataGridView1);
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowindex2 = e.RowIndex;
            }
            catch (Exception)
            {
                MessageBox.Show("Выбрана пустая или некорректная строка.");
            }
        }
    }
}
