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
    public partial class AnalizForm : Form
    {
        private UserManager userManager;
        private ProductManager productManager;

        public AnalizForm()
        {
            InitializeComponent();
            userManager = new UserManager();
            productManager = new ProductManager();
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            // Kullanıcı sayısını al
            int userCount = userManager.GetUsersCount();
            label6.Text = userCount.ToString();

            // Toplam ürün sayısını al
            int productCount = productManager.GetProductsCount();
            label5.Text = productCount.ToString();

            // Toplam satış sayısını al (örnek olarak)
            int totalSales = productManager.GetSoldProductsCount();
            label4.Text = totalSales.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Bu event handler'ı kaldırabiliriz
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Bu event handler'ı kaldırabiliriz
        }
    }
}
