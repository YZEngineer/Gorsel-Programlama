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
    public partial class PasswordChange : Form
    {
        private int currentUserId;

        public PasswordChange()
        {
            InitializeComponent();
            currentUserId = Program.CurrentUser.Id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == textBox3.Text)
            {
                bool result = UserManager.UpdatePassword(currentUserId, textBox1.Text);
                if (result)
                {
                    MessageBox.Show("Şifre başarıyla değiştirildi");
                }
                else
                {
                    MessageBox.Show("Şifre güncellenemedi");
                }
            }
            else
            {
                MessageBox.Show("Şifreler eşleşmiyor");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close();
        }
        //mail
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        //password
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //passwordr
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
