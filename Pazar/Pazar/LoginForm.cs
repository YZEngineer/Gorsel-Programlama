using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pazar
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Bu özellik yakında eklenecektir.");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox2.Text;
            string password = textBox1.Text;
            bool isAdmin = radioButton2.Checked;
            bool isUser = radioButton1.Checked;

            // Boş alan kontrolü
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

            // Kullanıcı girişi kontrolü
            var (success, user) = UserManager.Authenticate(email, password, isAdmin, isUser);

            if (success)
            {
                Program.CurrentUser = user;
                MessageBox.Show($"Hoş geldiniz, {user.Name}!");
                MainForm mainForm = new MainForm();
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Giriş başarısız! Lütfen bilgilerinizi kontrol edin.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register regForm = new Register();
            this.Hide();
            regForm.Show();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
        //Admin
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = false;
            }
        }

        //kullanıcı
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
            }
        }
        //Admin
        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

        }


    }

}
