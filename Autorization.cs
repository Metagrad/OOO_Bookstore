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
using System.Threading;
using System.IO;
using System.Security.Cryptography;

namespace OOO_Bookstore
{
    public partial class Autorization : Form
    {
        public Autorization()
        {
            InitializeComponent();
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            pictureBox2.Visible = false;
            textBox3.Visible = false;
            this.KeyPreview = true;
        }

        int count = 2;
        System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        int counttime = 10;
        //int count = 0;
        bool ban = false;
        string cap = "";

        string connect = "host = localhost; uid = root; pwd=; database = Bookstore;";

        public string CapchaCode()
        {
            string sym = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";

            Random rnd = new Random();

            return sym[rnd.Next(sym.Length)].ToString() + sym[rnd.Next(sym.Length)].ToString() + sym[rnd.Next(sym.Length)].ToString() + sym[rnd.Next(sym.Length)].ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                myTimer.Tick += new EventHandler(Counter);
                myTimer.Interval = 1000;
                Timer.fl_unactivity = false;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Counter(object sender, EventArgs e)
        {
            label9.Visible = true;
            counttime--;
            label9.Text = "Программа заблокирована на " + counttime + " сек.";
            if (counttime == 0)
            {
                label9.Text = "Программа заблокирована на 10 сек.";
                label9.Visible = false;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button1.Enabled = true;
                myTimer.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
                string password = textBox2.Text;
                password = Hash(password);
                if (ban == true && textBox3.Text == cap) { }
                else if (ban == true && textBox3.Text != cap)
                {
                    MessageBox.Show("Неверно введена капча или не верно введён логин или пароль!");

                    MessageBox.Show("Программа заблокирована на 10 секунд");
                    block();

                    cap = CapchaCode();

                    label5.Text = cap[0].ToString();
                    label6.Text = cap[1].ToString();
                    label7.Text = cap[2].ToString();
                    label8.Text = cap[3].ToString();

                    return;
                }

                using (MySqlConnection connection = new MySqlConnection(connect))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("Select * From users Where user_login = '" + textBox1.Text + "' And user_password = '" + password + "'", connection);
                    cmd.ExecuteNonQuery();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string id = reader.GetValue(0).ToString();
                                string privilege = reader.GetValue(6).ToString();
                                string FIO = reader.GetValue(1).ToString() + " " + reader.GetValue(2).ToString() + " " + reader.GetValue(3).ToString();

                                if (privilege == "Администратор")
                                {
                                    MessageBox.Show("Вы вошли как Администратор");

                                    textBox1.Text = "";
                                    textBox2.Text = "";
                                    textBox3.Text = "";

                                    ban = false;

                                    this.Hide();
                                    MainMenu menu = new MainMenu(FIO, privilege, id);
                                    clear();
                                    menu.ShowDialog();
                                    this.Show();
                                    return;
                                }

                                else if (privilege == "Менеджер")
                                {
                                    MessageBox.Show("Вы вошли как Менеджер");

                                    textBox1.Text = "";
                                    textBox2.Text = "";
                                    textBox3.Text = "";

                                    ban = false;

                                    this.Hide();
                                    clear();
                                    MainMenu menu = new MainMenu(FIO, privilege, id);
                                    menu.ShowDialog();
                                    this.Show();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль!");

                            count++;
                            if (count >= 1)
                            {

                                ban = true;

                                VisibleText();

                                textBox1.Visible = true;
                                label3.Visible = true;
                                label4.Visible = true;
                                label5.Visible = true;
                                label6.Visible = true;

                                cap = CapchaCode();

                                label5.Text = cap[0].ToString();
                                label6.Text = cap[1].ToString();
                                label7.Text = cap[2].ToString();
                                label8.Text = cap[3].ToString();
                            }
                        }
                    }
                }
            //} catch (Exception )
            //{
            //    MessageBox.Show("Ошибка аутентификации.");
            //}
        }

        private string Hash(string input)
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

        private void clear()
        {
            pictureBox2.Visible = false;
            label4.Visible = false;
            textBox3.Visible = false;
        }

        private void block()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            checkBox1.Enabled = false;
            counttime = 10;
            myTimer.Start();
            counttime = 10;
        }

        private void VisibleText()
        {
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            pictureBox2.Visible = true;
            textBox3.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Вы уверены, что хотите выйти из программы?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    string file = System.IO.Directory.GetCurrentDirectory() + @"\backup.sql";

                    using (MySqlConnection conn = new MySqlConnection(connect))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;
                                conn.Open();
                                mb.ExportToFile(file);
                                conn.Close();
                            }
                        }
                    }
                    this.Close();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Autorization_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Autorization_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox1.Text) && !String.IsNullOrWhiteSpace(textBox2.Text))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox1.Text) && !String.IsNullOrWhiteSpace(textBox2.Text))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
    }
}
