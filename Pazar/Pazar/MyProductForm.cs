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
    public partial class MyProductForm : Form
    {
        private User currentUser;
        private FlowLayoutPanel activeProductsPanel;
        private FlowLayoutPanel soldProductsPanel;
        private FlowLayoutPanel favoritesPanel;
        private FlowLayoutPanel favorites2Panel;
        private Image defaultImage;
        private TabControl tabControl;

        public MyProductForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            InitializeCustomComponents();
            CreateDefaultImage();
            LoadMyProducts();
        }

        private void InitializeCustomComponents()
        {
            // Tab kontrolü
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            // Mevcut ürünler tab'ı
            TabPage activeProductsTab = new TabPage("Mevcut Ürünler");
            activeProductsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10),
                BackColor = Color.White
            };
            activeProductsTab.Controls.Add(activeProductsPanel);

            // Satılan ürünler tab'ı
            TabPage soldProductsTab = new TabPage("Satılan Ürünler");
            soldProductsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10),
                BackColor = Color.White
            };
            soldProductsTab.Controls.Add(soldProductsPanel);

            // Favoriler tab'ı
            TabPage favoritesTab = new TabPage("Favorilerim");
            favoritesPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10),
                BackColor = Color.White
            };
            favoritesTab.Controls.Add(favoritesPanel);

            // Sepet tab'ı
            TabPage cartTab = new TabPage("Sepetim");
            favorites2Panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10),
                BackColor = Color.White
            };
            cartTab.Controls.Add(favorites2Panel);

            // Tab'ları ekle
            tabControl.TabPages.Add(activeProductsTab);
            tabControl.TabPages.Add(soldProductsTab);
            tabControl.TabPages.Add(favoritesTab);
            tabControl.TabPages.Add(cartTab);

            // Tab kontrolünü forma ekle
            this.Controls.Add(tabControl);
        }

        private void CreateDefaultImage()
        {
            // Varsayılan resim oluştur
            defaultImage = new Bitmap(280, 200);
            using (Graphics g = Graphics.FromImage(defaultImage))
            {
                g.Clear(Color.LightGray);
                g.DrawString("Resim Yok",
                    new Font("Arial", 20),
                    Brushes.DarkGray,
                    new PointF(70, 80));
            }
        }

        private void LoadMyProducts()
        {
            // Kullanıcının tüm ürünlerini al
            var products = ProductManager.GetProductsBySeller(currentUser.Id);

            // Panelleri temizle
            activeProductsPanel.Controls.Clear();
            soldProductsPanel.Controls.Clear();
            favoritesPanel.Controls.Clear();
            favorites2Panel.Controls.Clear();

            // Ürünleri ayır ve panellere ekle
            foreach (var product in products)
            {
                Panel productPanel = CreateProductPanel(product);
                if (product.IsSold)
                {
                    soldProductsPanel.Controls.Add(productPanel);
                }
                else
                {
                    activeProductsPanel.Controls.Add(productPanel);
                }
            }

            // Favori ürünleri yükle
            LoadFavoriteProducts();
            LoadCartProducts();
        }

        private void LoadFavoriteProducts()
        {
            var favoriteProductIds = UserManager.GetFavoriteProducts(currentUser.Id);
            foreach (var productId in favoriteProductIds)
            {
                var product = ProductManager.GetProductById(productId);
                if (product != null)
                {
                    Panel productPanel = CreateFavoriteProductPanel(product);
                    favoritesPanel.Controls.Add(productPanel);
                }
            }
        }

        private void LoadCartProducts()
        {
            var cartProductIds = UserManager.GetFavorite2Products(currentUser.Id);
            foreach (var productId in cartProductIds)
            {
                var product = ProductManager.GetProductById(productId);
                if (product != null && !product.IsSold)
                {
                    Panel productPanel = CreateCartProductPanel(product);
                    favorites2Panel.Controls.Add(productPanel);
                }
            }
        }

        private Panel CreateProductPanel(Product product)
        {
            // Ürün paneli
            Panel panel = new Panel
            {
                Width = 300,
                Height = 460,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Ürün resmi
            PictureBox pictureBox = new PictureBox
            {
                Width = 280,
                Height = 200,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray
            };

            // Resmi yükle
            LoadProductImage(pictureBox, product.Id);

            // Ürün adı
            Label nameLabel = new Label
            {
                Text = product.Name,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 220),
                Width = 280,
                Height = 30
            };

            // Ürün açıklaması
            Label descLabel = new Label
            {
                Text = product.Description,
                Font = new Font("Arial", 9),
                Location = new Point(10, 250),
                Width = 280,
                Height = 40
            };

            // Fiyat
            Label priceLabel = new Label
            {
                Text = $"{product.Price:C}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Green,
                Location = new Point(10, 290),
                Width = 280,
                Height = 30
            };

            // Kategori ve durum
            Label infoLabel = new Label
            {
                Text = $"{product.Category} - {(product.IsNew ? "Yeni" : "Kullanılmış")} - {(product.IsSold ? "Satıldı" : "Satılık")}",
                Font = new Font("Arial", 9),
                Location = new Point(10, 320),
                Width = 280,
                Height = 20
            };

            // Düzenle butonu
            Button editButton = new Button
            {
                Text = "Düzenle",
                Location = new Point(10, 350),
                Width = 130,
                Height = 30,
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            editButton.Click += (s, e) => EditProduct(product);

            // Sil butonu
            Button deleteButton = new Button
            {
                Text = "Sil",
                Location = new Point(150, 350),
                Width = 130,
                Height = 30,
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            deleteButton.Click += (s, e) => DeleteProduct(product);

            // Kontrolleri panele ekle
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(descLabel);
            panel.Controls.Add(priceLabel);
            panel.Controls.Add(infoLabel);
            panel.Controls.Add(editButton);
            panel.Controls.Add(deleteButton);

            return panel;
        }

        private Panel CreateFavoriteProductPanel(Product product)
        {
            // Ürün paneli
            Panel panel = new Panel
            {
                Width = 300,
                Height = 460,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Ürün resmi
            PictureBox pictureBox = new PictureBox
            {
                Width = 280,
                Height = 200,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray
            };

            // Resmi yükle
            LoadProductImage(pictureBox, product.Id);

            // Ürün adı
            Label nameLabel = new Label
            {
                Text = product.Name,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 220),
                Width = 280,
                Height = 30
            };

            // Ürün açıklaması
            Label descLabel = new Label
            {
                Text = product.Description,
                Font = new Font("Arial", 9),
                Location = new Point(10, 250),
                Width = 280,
                Height = 40
            };

            // Fiyat
            Label priceLabel = new Label
            {
                Text = $"{product.Price:C}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Green,
                Location = new Point(10, 290),
                Width = 280,
                Height = 30
            };

            // Kategori ve durum
            Label infoLabel = new Label
            {
                Text = $"{product.Category} - {(product.IsNew ? "Yeni" : "Kullanılmış")} - {(product.IsSold ? "Satıldı" : "Satılık")}",
                Font = new Font("Arial", 9),
                Location = new Point(10, 320),
                Width = 280,
                Height = 20
            };

            // Satın Al butonu
            Button buyButton = new Button
            {
                Text = "Satın Al",
                Location = new Point(10, 350),
                Width = 280,
                Height = 30,
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            buyButton.Click += (s, e) => BuyProduct(product);

            // Favorilerden Çıkar butonu
            Button removeFavoriteButton = new Button
            {
                Text = "Favorilerden Çıkar",
                Location = new Point(10, 390),
                Width = 280,
                Height = 30,
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            removeFavoriteButton.Click += (s, e) => RemoveFromFavorites(product);

            // Kontrolleri panele ekle
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(descLabel);
            panel.Controls.Add(priceLabel);
            panel.Controls.Add(infoLabel);
            panel.Controls.Add(buyButton);
            panel.Controls.Add(removeFavoriteButton);

            return panel;
        }

        private Panel CreateCartProductPanel(Product product)
        {
            // Ürün paneli
            Panel panel = new Panel
            {
                Width = 300,
                Height = 460,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Ürün resmi
            PictureBox pictureBox = new PictureBox
            {
                Width = 280,
                Height = 200,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray
            };

            // Resmi yükle
            LoadProductImage(pictureBox, product.Id);

            // Ürün adı
            Label nameLabel = new Label
            {
                Text = product.Name,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 220),
                Width = 280,
                Height = 30
            };

            // Ürün açıklaması
            Label descLabel = new Label
            {
                Text = product.Description,
                Font = new Font("Arial", 9),
                Location = new Point(10, 250),
                Width = 280,
                Height = 40
            };

            // Fiyat
            Label priceLabel = new Label
            {
                Text = $"{product.Price:C}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Green,
                Location = new Point(10, 290),
                Width = 280,
                Height = 30
            };

            // Kategori ve durum
            Label infoLabel = new Label
            {
                Text = $"{product.Category} - {(product.IsNew ? "Yeni" : "Kullanılmış")} - {(product.IsSold ? "Satıldı" : "Satılık")}",
                Font = new Font("Arial", 9),
                Location = new Point(10, 320),
                Width = 280,
                Height = 20
            };

            // Satın Al butonu
            Button buyButton = new Button
            {
                Text = "Satın Al",
                Location = new Point(10, 350),
                Width = 280,
                Height = 30,
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            buyButton.Click += (s, e) => BuyProduct(product);

            // Sepetten Çıkar butonu
            Button removeFromCartButton = new Button
            {
                Text = "Sepetten Çıkar",
                Location = new Point(10, 390),
                Width = 280,
                Height = 30,
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            removeFromCartButton.Click += (s, e) => RemoveFromCart(product);

            // Kontrolleri panele ekle
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(descLabel);
            panel.Controls.Add(priceLabel);
            panel.Controls.Add(infoLabel);
            panel.Controls.Add(buyButton);
            panel.Controls.Add(removeFromCartButton);

            return panel;
        }

        private void LoadProductImage(PictureBox pictureBox, int productId)
        {
            string imagePath = Path.Combine(Application.StartupPath, "ProductImages", $"product_{productId}.jpg");

            if (File.Exists(imagePath))
            {
                try
                {
                    using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox.Image = Image.FromStream(stream);
                    }
                }
                catch
                {
                    pictureBox.Image = defaultImage;
                }
            }
            else
            {
                pictureBox.Image = defaultImage;
            }
        }

        private void EditProduct(Product product)
        {
            EditProductForm editForm = new EditProductForm(product);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // Ürün güncellendiyse listeyi yenile
                LoadMyProducts();
            }
        }

        private void DeleteProduct(Product product)
        {
            var result = MessageBox.Show(
                $"{product.Name} ürününü silmek istediğinizden emin misiniz?",
                "Ürün Silme",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (ProductManager.DeleteProduct(product.Id))
                {
                    MessageBox.Show("Ürün başarıyla silindi.");
                    LoadMyProducts();
                }
                else
                {
                    MessageBox.Show("Ürün silinirken bir hata oluştu.");
                }
            }
        }

        private void BuyProduct(Product product)
        {
            if (product.SellerId == currentUser.Id)
            {
                MessageBox.Show("Kendi ürününüzü satın alamazsınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"{product.Name} ürününü {product.Price:C} fiyatına satın almak istediğinizden emin misiniz?",
                "Satın Alma Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (ProductManager.MarkAsSold(product.Id, currentUser.Id))
                {
                    MessageBox.Show("Satın alma işlemi başarıyla tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMyProducts();
                }
                else
                {
                    MessageBox.Show("Satın alma işlemi sırasında bir hata oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RemoveFromFavorites(Product product)
        {
            var result = MessageBox.Show(
                $"{product.Name} ürününü favorilerden çıkarmak istediğinizden emin misiniz?",
                "Favorilerden Çıkarma",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Hangi favori listesinden çıkarılacağını belirle
                if (tabControl.SelectedTab.Text == "Favorilerim")
                {
                    UserManager.RemoveFromFavorites(currentUser.Id, product.Id);
                }
                else if (tabControl.SelectedTab.Text == "Sepetim")
                {
                    UserManager.RemoveFromFavorites2(currentUser.Id, product.Id);
                }

                MessageBox.Show($"{product.Name} favorilerden çıkarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMyProducts();
            }
        }

        private void RemoveFromCart(Product product)
        {
            var result = MessageBox.Show(
                $"{product.Name} ürününü sepetten çıkarmak istediğinizden emin misiniz?",
                "Sepetten Çıkarma",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                UserManager.RemoveFromFavorites2(currentUser.Id, product.Id);
                MessageBox.Show($"{product.Name} sepetten çıkarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMyProducts();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Varsayılan resmi temizle
            if (defaultImage != null)
            {
                defaultImage.Dispose();
            }
        }
    }
}
