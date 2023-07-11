using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace OOO_Bookstore
{
    public partial class Special : Form
    {
        public Special()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        System.Windows.Forms.Timer myTimer1 = new System.Windows.Forms.Timer();
        int counttime1 = 380;

        private static string SelectedPath;
        private static string[] fileName;
        string files = "";
        string files1 = "";
        public static string ConnectString = "host = localhost; uid = root; pwd=; database = Bookstore";

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы уверены, что хотите вернуться в главное меню?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                myTimer1.Stop();
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedPath != string.Empty)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();

                    openFileDialog.Filter = "(*.sql;*.SQL) |*.sql;*.SQL|All files(*.* | *.*)";
                    openFileDialog.ShowDialog();

                    fileName = openFileDialog.FileNames;

                    //string file = SelectedPath + "\\" + "backup.sql";
                    using (MySqlConnection conn = new MySqlConnection(ConnectString))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;
                                conn.Open();
                                mb.ImportFromFile(fileName[0]);
                                conn.Close();
                                MessageBox.Show("Импорт резервной копии прошел успешно");
                            }
                        }
                    }
                }
                else { MessageBox.Show("Папка для экспорта не выбрана!"); }
            }

            catch { MessageBox.Show("Ошибка импорта!"); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedPath != string.Empty)
                {
                    string constring = ConnectString;
                    string file = SelectedPath + "\\" + DateTime.Now.ToShortDateString() + "_backup.sql";

                    using (MySqlConnection conn = new MySqlConnection(constring))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;
                                conn.Open();
                                mb.ExportToFile(file);
                                conn.Close();
                                MessageBox.Show("Экспорт резервной копии прошел успешно");
                                SelectedPath = String.Empty;
                            }
                        }
                    }
                }

                else { MessageBox.Show("Папка для экспорта не выбрана!"); }
            }

            catch { MessageBox.Show("Ошибка экспорта!"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        SelectedPath = fbd.SelectedPath;
                    }
                }
            }

            catch { MessageBox.Show("Ошибка выбора папки!"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            DialogResult result = folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                files = folderBrowser.SelectedPath;
            }

            if (comboBox1.Text != "Все таблицы" && comboBox1.Text != "")
            {
                //try
                //{
                    ExportF(comboBox1.Text);
                    MessageBox.Show("Экспорт завершен файл сохранен в по пути " + files, "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //catch
                //{
                //    MessageBox.Show("Файл уже существует в папке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            if (comboBox1.Text == "Все таблицы")
            {
                //try
                //{
                    ExportF("users");
                    ExportF("Genres");
                    ExportF("languages");
                    ExportF("publishers");
                    ExportF("books");
                    MessageBox.Show("Экспорт завершен файл сохранен в по пути " + files, "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //catch
                //{
                //    MessageBox.Show("Файл уже существует в папке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

            }
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Выберите таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportF(string Name)
        {
            File.Delete(files + "\\" + Name + ".csv");

            using (MySqlConnection con = new MySqlConnection(ConnectString))
            {
                con.Open();
                string f = files.Replace("\\", "/");

                MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM `" + Name + "` INTO OUTFILE '" + f + @"/" + Name + ".csv' FIELDS TERMINATED BY ';' LINES TERMINATED BY '\n';", con);
                cmd.ExecuteNonQuery();

            }
        }

        private void Special_Load(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            files1 = openFileDialog1.FileName;

            if (comboBox1.Text != "")
            {
                try
                {
                    ImportF(comboBox1.Text);
                    MessageBox.Show("Импорт завершен ", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Ошибка импорта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (comboBox1.Text == "")
            {
                MessageBox.Show("Выберите таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportF(string p)
        {
            using (MySqlConnection con = new MySqlConnection(ConnectString))
            {
                con.Open();
                string f = files1.Replace("\\", "/");

                MySqlCommand cmd = new MySqlCommand(@"delete from `" + Name + "`;", con);
                cmd.ExecuteNonQuery();

            }
            using (MySqlConnection con = new MySqlConnection(ConnectString))
            {
                con.Open();
                string f = files1.Replace("\\", "/");

                MySqlCommand cmd = new MySqlCommand(@"LOAD DATA INFILE '" + f + "' INTO TABLE `" + Name + "` FIELDS TERMINATED BY ';';", con);
                cmd.ExecuteNonQuery();

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Special_MouseMove(object sender, MouseEventArgs e)
        {
            counttime1 = 380;
        }

        private void Special_KeyPress(object sender, KeyPressEventArgs e)
        {
            counttime1 = 380;
        }
    }
}
