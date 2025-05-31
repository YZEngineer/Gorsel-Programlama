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
    public partial class AddProductForm : Form
    {
        private User currentUser;
        private string selectedImagePath;

        public AddProductForm(User user)
        {
            InitializeComponent();
            currentUser = user;
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            // Kategori listesini doldur
            string[] categories = ProductManager.GetAllCategories();
            textBox4.AutoCompleteCustomSource.AddRange(categories);
        }


        //ürün adı
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Ürün adı değiştiğinde yapılacak işlemler
        }

        //fiyat
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Fiyat değiştiğinde yapılacak işlemler
        }

        //category
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // Kategori değiştiğinde yapılacak işlemler
        }

        //açıklama
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Açıklama değiştiğinde yapılacak işlemler
        }

        //yeni ürün mü
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Yeni ürün durumu değiştiğinde yapılacak işlemler
        }

        //ürün ekle
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validasyonlar
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Lütfen ürün adını giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox2.Text) || !decimal.TryParse(textBox2.Text, out decimal price))
                {
                    MessageBox.Show("Lütfen geçerli bir fiyat giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Lütfen kategori seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    MessageBox.Show("Lütfen ürün açıklaması giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Yeni ürün oluştur
                Product newProduct = new Product
                {
                    Name = textBox1.Text,
                    Price = price,
                    Category = textBox4.Text,
                    Description = richTextBox1.Text,
                    IsNew = checkBox1.Checked,
                    SellerId = currentUser.Id
                };

                // Ürünü ekle
                if (ProductManager.AddProduct(newProduct))
                {
                    // Eğer resim seçildiyse kaydet
                    if (!string.IsNullOrEmpty(selectedImagePath))
                    {
                        ProductManager.SaveProductImage(newProduct.Id, selectedImagePath);
                    }

                    MessageBox.Show("Ürün başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ürün eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
