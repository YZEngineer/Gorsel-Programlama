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

namespace Pazar
{
    public partial class UserDetailsForm : Form
    {
        private User selectedUser;
        private User currentUser = Program.CurrentUser;

        public UserDetailsForm(User user)
        {
            InitializeComponent();
            selectedUser = user;
            LoadUserData();
        }

        private void LoadUserData()
        {
            // Kullanıcı bilgilerini form kontrollerine yükle
            textBox1.Text = selectedUser.Name;
            textBox2.Text = selectedUser.Password; // Şifreyi direkt göster
            textBox3.Text = selectedUser.Email;
            textBox4.Text = selectedUser.Address;
            textBox5.Text = selectedUser.Phone;
            checkBox1.Checked = selectedUser.IsAdmin;

            // Sadece admin kullanıcı rolünü değiştirebilir
            checkBox1.Enabled = currentUser.IsAdmin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validasyonlar
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Lütfen ad soyad giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Lütfen e-posta giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kullanıcı bilgilerini güncelle
                selectedUser.Name = textBox1.Text;
                selectedUser.Email = textBox3.Text;
                selectedUser.Address = textBox4.Text;
                selectedUser.Phone = textBox5.Text;

                // Şifre değiştirilecekse
                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    selectedUser.Password = textBox2.Text;
                }

                // Admin yetkisi değiştirilecekse
                if (currentUser.IsAdmin)
                {
                    selectedUser.IsAdmin = checkBox1.Checked;
                }

                // Kullanıcıyı güncelle
                if (UserManager.UpdateUser(selectedUser))
                {
                    MessageBox.Show("Kullanıcı bilgileri başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı bilgileri güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Admin yetkisi değiştiğinde yapılacak işlemler
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Ad değiştiğinde yapılacak işlemler
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // E-posta değiştiğinde yapılacak işlemler
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Şifre değiştiğinde yapılacak işlemler
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // Adres değiştiğinde yapılacak işlemler
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // Telefon numarası değiştiğinde yapılacak işlemler
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //MakeAdmin checkBox
        }
    }
}
