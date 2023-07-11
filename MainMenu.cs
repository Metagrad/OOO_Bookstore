using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOO_Bookstore
{
    public partial class MainMenu : Form
    {
        public string name;
        public string privilegue;
        public string Idd;
        public MainMenu(string fio, string privileg, string id)
        {
            InitializeComponent();
            name = fio;
            privilegue = privileg;
            Idd = id;
            this.KeyPreview = true;
        }

        System.Windows.Forms.Timer myTimer1 = new System.Windows.Forms.Timer();
        int counttime1 = 680;

        private void ProductsAButton_Click(object sender, EventArgs e)
        {
            myTimer1.Stop();
            counttime1 = 680;
            Products form = new Products();
            this.Hide();
            form.ShowDialog();
            this.Show();
            //myTimer1.Start();
            if (Timer.fl_unactivity == true)
            {
                myTimer1.Stop();
                this.Close();
            }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            if (privilegue == "Менеджер")
            {
                DirectoryAButton.Visible = false;
                ProductsAButton.Visible = false;
                ImpExpAButton.Visible = false;
            }
            else
            {
                ProductViewUButton.Visible = false;
            }

            label4.Text = name;
            label5.Text = privilegue;

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

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы уверены, что хотите выйти из программы?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                myTimer1.Stop();
                this.Close();
            }
        }

        private void DirectoryAButton_Click(object sender, EventArgs e)
        {
            myTimer1.Stop();
            counttime1 = 680;
            Directory form = new Directory();
            this.Hide();
            form.ShowDialog();
            this.Show();
            //myTimer1.Start();
            if (Timer.fl_unactivity == true)
            {
                myTimer1.Stop();
                this.Close();
            }
        }

        private void ProductViewUButton_Click(object sender, EventArgs e)
        {
            myTimer1.Stop();
            counttime1 = 680;
            ProductView form = new ProductView();
            this.Hide();
            form.ShowDialog();
            this.Show();
            myTimer1.Start();
            if (Timer.fl_unactivity == true)
            {
                myTimer1.Stop();
                this.Close();
            }
        }

        private void SellingsUButton_Click(object sender, EventArgs e)
        {
            myTimer1.Stop();
            counttime1 = 680;
            Orders form = new Orders(Idd, privilegue, name);
            this.Hide();
            form.ShowDialog();
            this.Show();
            myTimer1.Start();
            if (Timer.fl_unactivity == true)
            {
                myTimer1.Stop();
                this.Close();
            }
        }

        private void AccountinfUButton_Click(object sender, EventArgs e)
        {
            myTimer1.Stop();
            counttime1 = 680;
            Order form = new Order(Idd, privilegue, name);
            this.Hide();
            form.ShowDialog();
            this.Show();
            //myTimer1.Start();
            if (Timer.fl_unactivity == true)
            {
                myTimer1.Stop();
                this.Close();
            }
        }

        private void ImpExpAButton_Click(object sender, EventArgs e)
        {
            myTimer1.Stop();
            counttime1 = 680;
            Special form = new Special();
            this.Hide();
            form.ShowDialog();
            this.Show();
            //myTimer1.Start();
            if (Timer.fl_unactivity == true)
            {
                myTimer1.Stop();
                this.Close();
            }
        }

        private void MainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            counttime1 = 680;
        }

        private void MainMenu_MouseMove(object sender, MouseEventArgs e)
        {
            counttime1 = 680;
        }
    }
}
