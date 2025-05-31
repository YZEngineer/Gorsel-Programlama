using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pazar
{
    public partial class MainForm : Form
    {
        User currentUser = Program.CurrentUser;
        public MainForm()
        {
            InitializeComponent();
            UpdateUserInfo();
        }

        private void UpdateUserInfo()
        {
            if (Program.CurrentUser != null)
            {
                // Kullanıcı adını güncelle
                label1.Text = Program.CurrentUser.Name;

                // Rolü güncelle
                label2.Text = Program.CurrentUser.IsAdmin ? "Admin" : "Kullanıcı";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Text = kullanıcı adı
        }
        //satışlarım
        private void button4_Click(object sender, EventArgs e)
        {

        }
        //urunler
        private void button2_Click(object sender, EventArgs e)
        {
            AllProductsForm allProductsForm = new AllProductsForm(Program.CurrentUser);
            allProductsForm.ShowDialog();
        }
        //Profilim
        private void button9_Click(object sender, EventArgs e)
        {
            UserDetailsForm profileForm = new UserDetailsForm(currentUser);
            profileForm.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // Çıkış yap
            Program.CurrentUser = null; // Kullanıcı bilgisini temizle
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Show();
        }
        //admin tüm ürünler
        private void button10_Click(object sender, EventArgs e)
        {

        }
        //admin kullanıcı yönetimi
        private void button14_Click(object sender, EventArgs e)
        {
            if (Program.CurrentUser == null || !Program.CurrentUser.IsAdmin)
            {
                MessageBox.Show("Bu sayfaya erişim yetkiniz yok!", "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UsersForm usersForm = new UsersForm();
            usersForm.ShowDialog();
        }
        //Adres Bilgisi
        private void button11_Click(object sender, EventArgs e)
        {

        }
        //şifre Değiştir
        private void button12_Click(object sender, EventArgs e)
        {
            //go to PasswordChange
            PasswordChange passwordChange = new PasswordChange();
            passwordChange.Show();
        }
        //urunlerim
        private void button6_Click(object sender, EventArgs e)
        {
            MyProductForm myProductForm = new MyProductForm(Program.CurrentUser);
            myProductForm.ShowDialog();
        }
        //urun ekle
        private void button1_Click(object sender, EventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm(Program.CurrentUser);
            if (addProductForm.ShowDialog() == DialogResult.OK)
            {
                // mesaj başarıyla eklendi
            }
        }
        //favorim
        private void button5_Click(object sender, EventArgs e)
        {
            //List All Product with filtered 
        }
        //sepet()
        private void button7_Click(object sender, EventArgs e)
        {
           
        }
        //Mesajlarım
        private void button8_Click(object sender, EventArgs e)
        {
           
        }
        //Geri
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Rol label'ı tıklanabilir olabilir
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            AnalizForm analizForm = new AnalizForm();
            analizForm.ShowDialog();
        }
    }
}
