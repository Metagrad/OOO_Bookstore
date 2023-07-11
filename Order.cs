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
using Microsoft.Office.Interop.Excel;

namespace OOO_Bookstore
{
    public partial class Order : Form
    {
        public string name;
        public string privilegue;
        public string Idd;
        public Order(string id, string fio, string privil)
        {
            InitializeComponent();
            this.KeyPreview = true;
            Idd = id;
            privilegue = privil;
            name = fio;
        }

        System.Windows.Forms.Timer myTimer1 = new System.Windows.Forms.Timer();
        int counttime1 = 60;

        string connectionString = @"server=localhost;userid=root;password=;database=Bookstore;charset=utf8";
        public string comStr = "";
        public string tempCmd = @"SELECT `order`.order_id, order_date, users.user_surname, users.user_name, users.user_patronym, status, discount, finalVal
        FROM Bookstore.`order` INNER JOIN users ON users.id_user = `order`.id_user";

        private void Order_Load(object sender, EventArgs e)
        {
            myTimer1.Tick += new EventHandler(Counter1);
            myTimer1.Interval = 1000;
            myTimer1.Start();

            if (privilegue == "Администратор")
            {

            }
            else if (privilegue == "Менеджер фирмы")
            {
                button1.Visible = false;
                FinTextBox1.Visible = false;
                FinTextBox1.Visible = false;
            }

            fillDataGrid1(tempCmd);
        }

        private void Counter1(object sender, EventArgs e)
        {
            counttime1--;

            if (counttime1 == 0)
            {
                myTimer1.Stop();
                Timer.fl_unactivity = true;
                this.Close();
            }
        }

        private void fillDataGrid1(string tempCmd)
        {
            comStr = tempCmd + " " + filter();
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            MySqlCommand com = new MySqlCommand(comStr, conn);
            com.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(com);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns["order_id"].HeaderText = "Номер заказа";
            dataGridView1.Columns["order_date"].HeaderText = "Дата заказа";
            dataGridView1.Columns["user_surname"].HeaderText = "Фамилия пользователя";
            dataGridView1.Columns["user_name"].HeaderText = "Имя пользователя";
            dataGridView1.Columns["user_patronym"].HeaderText = "Отчество пользователя";
            dataGridView1.Columns["status"].HeaderText = "Статус";
            dataGridView1.Columns["discount"].HeaderText = "Скидка, %";
            dataGridView1.Columns["finalval"].HeaderText = "Сумма заказа, Р";

            dataGridView1.Columns[0].Visible = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            conn.Close();
            double sum = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                sum += Convert.ToDouble(r.Cells[7].Value);
                r.Cells[6].Value = (1 - Convert.ToDouble(r.Cells[6].Value)) * 100;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1[5, i].Value.ToString() == "Выполнен")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
            }
            FinTextBox1.Text = "Сумма от выбранных, Р: " + sum.ToString();

            conn = new MySqlConnection(connectionString);
            conn.Open();
            string Sql = "SELECT SUM(finalval) FROM `order` WHERE status = 'Выполнен';";
            MySqlCommand cmd = new MySqlCommand(Sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                FinTextBox2.Text = "Общая выручка, Р: " + reader[0].ToString();
            }
            reader.Close();
            string gcount = "";
            string scount = "";
            MySqlCommand count_cmd = new MySqlCommand("SELECT count(*) FROM books", conn);
            MySqlDataReader dr = count_cmd.ExecuteReader();
            while (dr.Read())
            {
                gcount = dr[0].ToString();
            }
            scount = Convert.ToString(dataGridView1.Rows.Count);
            label8.Text = "Записей: " + dataGridView1.Rows.Count.ToString();
            conn.Close();
        }

        private string filter()
        {
            string pcmd = "";
            if (radioButton1.Checked)
            {
                pcmd = "WHERE `order`.status = 'Выполнен' ";
            }
            else if (radioButton2.Checked)
            {
                pcmd = "WHERE `order`.status = 'Отменён' ";
            }
            else if (radioButton3.Checked)
            {
                pcmd = "WHERE `order`.status != '' ";
            }
            if (checkBox1.Checked)
            {
                pcmd = pcmd + @" AND `order`.order_date >='" + dateTimeOT.Value.ToString("yyyy-MM-dd")
                    + "' AND `order`.order_date <='" + dateTimePO.Value.ToString("yyyy-MM-dd") + "' ";
            }
            pcmd = pcmd + "AND `order`.order_id LIKE '" + textBox1.Text + "%' ";
            return pcmd;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы уверены, что хотите вернуться в главное меню?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                myTimer1.Stop();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            MySqlCommand cmd;
            con.Open();
            if (checkBox1.Checked)
            {
                cmd = new MySqlCommand(@"SELECT `order`.order_id, order_date, users.user_surname, status, discount, finalval
                            FROM Bookstore.`order` INNER JOIN users ON users.id_user = Bookstore.`order`.id_user
                            WHERE order_date >='" + dateTimeOT.Value.Date.ToString("yyyy-MM-dd") + "' AND order_date <='" + dateTimePO.Value.Date.ToString("yyyy-MM-dd") + "' AND Status = 'Выполнен'", con);
            }
            else
            {
                cmd = new MySqlCommand(@"SELECT `order`.order_id, order_date, users.user_surname, status, discount, finalval
                            FROM Bookstore.`order` INNER JOIN users ON users.id_user = Bookstore.`order`.id_user WHERE Status = 'Выполнен'", con);
            }
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            //Книга.
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            //Таблица.
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            ExcelApp.Cells[1, 1] = "Учёт заказов магазина";

            if (checkBox1.Checked)
            {
                ExcelApp.Cells[1, 5] = "Заказы по дате ";
                ExcelApp.Cells[1, 7] = "от: " + dateTimeOT.Value.ToString();
                ExcelApp.Cells[2, 7] = "до: " + dateTimePO.Value.ToString();
            }

            ExcelApp.Cells[2, 1] = "Номер заказа";
            ExcelApp.Cells[2, 2] = "Дата заказа";
            ExcelApp.Cells[2, 3] = "Сотрудник";
            ExcelApp.Cells[2, 4] = "Статус";
            ExcelApp.Cells[2, 5] = "Скидка";
            ExcelApp.Cells[2, 6] = "Сумма заказа, Р";

            for (Int32 i = 1; i < dt.Rows.Count + 1; i++)
            {
                for (Int32 j = 0; j < dt.Columns.Count; j++)
                {
                    if (i == 0)
                    {
                        if (j == 4)
                        {
                            ExcelApp.Cells[i + 2, j + 1] = ((1 - Convert.ToDouble(dt.Rows[0].ItemArray[j])) * 100).ToString() + "%";
                        }
                        else
                        {
                            ExcelApp.Cells[i + 2, j + 1] = dt.Rows[0].ItemArray[j].ToString();
                        }
                    }
                    else
                    {
                        if (j == 4)
                        {
                            ExcelApp.Cells[i + 2, j + 1] = ((1 - Convert.ToDouble(dt.Rows[i - 1].ItemArray[j])) * 100).ToString() + "%";
                        }
                        else
                        {
                            ExcelApp.Cells[i + 2, j + 1] = dt.Rows[i - 1].ItemArray[j].ToString();
                        }
                    }
                }
            }
            //Вызываем нашу созданную эксельку.
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            fillDataGrid1(tempCmd);
            if (checkBox1.Checked)
            {
                dateTimeOT.Enabled = true;
                dateTimePO.Enabled = true;
            }
            else
            {
                dateTimeOT.Enabled = false;
                dateTimePO.Enabled = false;
            }
        }

        private void dateTimeOT_ValueChanged(object sender, EventArgs e)
        {
            fillDataGrid1(tempCmd);
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            OrdInform.selectedOrder = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            this.Hide();
            OrderList form = new OrderList(Idd, name, privilegue);
            form.ShowDialog();
            this.Show();
            fillDataGrid1(tempCmd);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            fillDataGrid1(tempCmd);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            fillDataGrid1(tempCmd);
            if (checkBox1.Checked)
            {
                dateTimeOT.Enabled = true;
                dateTimePO.Enabled = true;
            }
            else
            {
                dateTimeOT.Enabled = false;
                dateTimePO.Enabled = false;
            }
        }

        private void Order_KeyDown(object sender, KeyEventArgs e)
        {
            counttime1 = 60;
        }

        private void Order_MouseMove(object sender, MouseEventArgs e)
        {
            counttime1 = 60;
        }
    }
}
