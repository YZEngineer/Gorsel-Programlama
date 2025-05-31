using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Pazar
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {
        }

        //kayitOl
        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            string email = textBox3.Text;
            string password = textBox1.Text;
            string passwordr = textBox4.Text;

            // Şifre kontrolü
            if (password != passwordr)
            {
                MessageBox.Show("Şifreler eşleşmiyor!");
                return;
            }

            // Yeni kullanıcı oluştur
            User newUser = new User
            {
                Name = name,
                Email = email,
                Password = password,
                IsAdmin = false,
                RegisterDate = DateTime.Now
            };

            // Kullanıcıyı ekle
            bool success = UserManager.RegisterUser(newUser);
            if (success)
            {
                MessageBox.Show("Kayıt başarılı! Giriş yapabilirsiniz.");
                LoginForm loginForm = new LoginForm();
                this.Hide();
                loginForm.Show();
            }
            else
            {
                MessageBox.Show("Bu e-posta adresi zaten kullanımda!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Show();
        }

        //pass
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        //pass
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        //epost
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        //kullaniciAdi
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
