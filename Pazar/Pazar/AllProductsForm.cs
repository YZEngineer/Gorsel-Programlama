using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace Pazar
{
    public partial class AllProductsForm : Form
    {
        private FlowLayoutPanel flowLayoutPanel;
        private List<Product> products;
        private Image defaultImage;
        private User currentUser;

        // Filtreleme kontrolleri
        private TextBox txtMinPrice;
        private TextBox txtMaxPrice;
        private CheckBox chkNewOnly;
        private CheckBox chkFavoritesOnly;
        private Button btnApplyFilter;

        public AllProductsForm(User user = null)
        {
            InitializeComponent();
            currentUser = user;
            InitializeCustomComponents();
            CreateDefaultImage();
            InitializeFilterControls();
            LoadProducts();
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

        private void InitializeCustomComponents()
        {
            // Ana panel
            flowLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10),
                BackColor = Color.White
            };
            this.Controls.Add(flowLayoutPanel);
        }

        private void InitializeFilterControls()
        {
            // Filtre paneli
            Panel filterPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.WhiteSmoke
            };

            // Minimum fiyat
            Label lblMinPrice = new Label
            {
                Text = "Min Fiyat:",
                Location = new Point(10, 15),
                AutoSize = true
            };

            txtMinPrice = new TextBox
            {
                Location = new Point(80, 12),
                Width = 100
            };

            // Maximum fiyat
            Label lblMaxPrice = new Label
            {
                Text = "Max Fiyat:",
                Location = new Point(190, 15),
                AutoSize = true
            };

            txtMaxPrice = new TextBox
            {
                Location = new Point(260, 12),
                Width = 100
            };

            // Yeni ürün filtresi
            chkNewOnly = new CheckBox
            {
                Text = "Sadece Yeni Ürünler",
                Location = new Point(10, 50),
                AutoSize = true
            };

            // Favori ürün filtresi
            chkFavoritesOnly = new CheckBox
            {
                Text = "Sadece Favorilerim",
                Location = new Point(200, 50),
                AutoSize = true
            };

            // Filtre uygula butonu
            btnApplyFilter = new Button
            {
                Text = "Filtrele",
                Location = new Point(400, 40),
                Width = 100,
                Height = 30
            };
            btnApplyFilter.Click += BtnApplyFilter_Click;

            // Kontrolleri panele ekle
            filterPanel.Controls.Add(lblMinPrice);
            filterPanel.Controls.Add(txtMinPrice);
            filterPanel.Controls.Add(lblMaxPrice);
            filterPanel.Controls.Add(txtMaxPrice);
            filterPanel.Controls.Add(chkNewOnly);
            filterPanel.Controls.Add(chkFavoritesOnly);
            filterPanel.Controls.Add(btnApplyFilter);

            // Filtre panelini forma ekle
            this.Controls.Add(filterPanel);
        }

        private void BtnApplyFilter_Click(object sender, EventArgs e)
        {
            try
            {
                // Fiyat filtrelerini al
                decimal minPrice = string.IsNullOrEmpty(txtMinPrice.Text) ? 0 : decimal.Parse(txtMinPrice.Text);
                decimal maxPrice = string.IsNullOrEmpty(txtMaxPrice.Text) ? 1000000 : decimal.Parse(txtMaxPrice.Text);

                // Filtreleri uygula
                var filteredProducts = products.Where(p =>
                {
                    // Fiyat aralığı filtresi
                    bool priceMatch = p.Price >= minPrice && p.Price <= maxPrice;

                    // Yeni ürün filtresi
                    bool newProductMatch = !chkNewOnly.Checked || p.IsNew;

                    // Favori ürün filtresi
                    bool favoriteMatch = !chkFavoritesOnly.Checked || 
                        (currentUser != null && currentUser.IsProductFavorite(p.Id));

                    return priceMatch && newProductMatch && favoriteMatch;
                }).ToList();

                // Filtrelenmiş ürünleri göster
                flowLayoutPanel.Controls.Clear();
                foreach (var product in filteredProducts)
                {
                    Panel productPanel = CreateProductPanel(product);
                    flowLayoutPanel.Controls.Add(productPanel);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen geçerli fiyat değerleri girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            // Satılmamış tüm ürünleri al
            products = ProductManager.GetAllAvailableProducts();

            // Her ürün için bir panel oluştur
            foreach (var product in products)
            {
                Panel productPanel = CreateProductPanel(product);
                flowLayoutPanel.Controls.Add(productPanel);
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
                Text = $"{product.Category} - {(product.IsNew ? "Yeni" : "Kullanılmış")}",
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

            // Favori butonu
            Button favoriteButton = new Button
            {
                Text = "★ Favorilere Ekle",
                Location = new Point(10, 390),
                Width = 130,
                Height = 30,
                BackColor = Color.White,
                ForeColor = Color.Gold,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            // Favori durumunu kontrol et ve butonu güncelle
            UpdateFavoriteButtonState(favoriteButton, product);

            favoriteButton.Click += (s, e) => ToggleFavorite(product, favoriteButton);

            // Sepet butonu
            Button cartButton = new Button
            {
                Text = "🛒 Sepete Ekle",
                Location = new Point(150, 390),
                Width = 130,
                Height = 30,
                BackColor = Color.White,
                ForeColor = Color.DodgerBlue,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            // Sepet durumunu kontrol et ve butonu güncelle
            UpdateCartButtonState(cartButton, product);

            cartButton.Click += (s, e) => ToggleCart(product, cartButton);

            // Satın Al butonu
            Button buyButton = new Button
            {
                Text = "Satın Al",
                Location = new Point(10, 430),
                Width = 280,
                Height = 30,
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            buyButton.Click += (s, e) => BuyProduct(product);

            // Kontrolleri panele ekle
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(descLabel);
            panel.Controls.Add(priceLabel);
            panel.Controls.Add(infoLabel);
            panel.Controls.Add(editButton);
            panel.Controls.Add(deleteButton);
            panel.Controls.Add(favoriteButton);
            panel.Controls.Add(cartButton);
            panel.Controls.Add(buyButton);

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
                    // Resim yüklenemezse varsayılan resmi göster
                    pictureBox.Image = defaultImage;
                }
            }
            else
            {
                // Resim dosyası yoksa varsayılan resmi göster
                pictureBox.Image = defaultImage;
            }
        }

        private void EditProduct(Product product)
        {
            EditProductForm editForm = new EditProductForm(product);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // Ürün güncellendiyse listeyi yenile
                flowLayoutPanel.Controls.Clear();
                LoadProducts();
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
                    flowLayoutPanel.Controls.Clear();
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("Ürün silinirken bir hata oluştu.");
                }
            }
        }

        private void UpdateFavoriteButtonState(Button favoriteButton, Product product)
        {
            if (currentUser != null && currentUser.IsProductFavorite(product.Id))
            {
                favoriteButton.Text = "★ Favorilerden Çıkar";
                favoriteButton.BackColor = Color.Gold;
                favoriteButton.ForeColor = Color.White;
            }
            else
            {
                favoriteButton.Text = "☆ Favorilere Ekle";
                favoriteButton.BackColor = Color.White;
                favoriteButton.ForeColor = Color.Gold;
            }
        }

        private void UpdateCartButtonState(Button cartButton, Product product)
        {
            if (currentUser != null && currentUser.IsProductFavorite2(product.Id))
            {
                cartButton.Text = "🛒 Sepetten Çıkar";
                cartButton.BackColor = Color.DodgerBlue;
                cartButton.ForeColor = Color.White;
            }
            else
            {
                cartButton.Text = "🛒 Sepete Ekle";
                cartButton.BackColor = Color.White;
                cartButton.ForeColor = Color.DodgerBlue;
            }
        }

        private void ToggleFavorite(Product product, Button favoriteButton)
        {
            if (currentUser == null)
            {
                MessageBox.Show("Favori eklemek için giriş yapmalısınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentUser.IsProductFavorite(product.Id))
            {
                // Favorilerden çıkar
                currentUser.RemoveFromFavorites(product.Id);
                MessageBox.Show($"{product.Name} favorilerden çıkarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Favorilere ekle
                currentUser.AddToFavorites(product.Id);
                MessageBox.Show($"{product.Name} favorilere eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Buton durumunu güncelle
            UpdateFavoriteButtonState(favoriteButton, product);
        }

        private void ToggleCart(Product product, Button cartButton)
        {
            if (currentUser == null)
            {
                MessageBox.Show("Sepete eklemek için giriş yapmalısınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (product.SellerId == currentUser.Id)
            {
                MessageBox.Show("Kendi ürününüzü sepete ekleyemezsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (product.IsSold)
            {
                MessageBox.Show("Bu ürün satılmış!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentUser.IsProductFavorite2(product.Id))
            {
                // Sepetten çıkar
                currentUser.RemoveFromFavorites2(product.Id);
                MessageBox.Show($"{product.Name} sepetten çıkarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Sepete ekle
                currentUser.AddToFavorites2(product.Id);
                MessageBox.Show($"{product.Name} sepete eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Buton durumunu güncelle
            UpdateCartButtonState(cartButton, product);
        }

        private void BuyProduct(Product product)
        {
            if (currentUser == null)
            {
                MessageBox.Show("Satın almak için giriş yapmalısınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kendi ürününü satın alamazsın
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
                // Ürünü satıldı olarak işaretle ve alıcıyı kaydet
                if (ProductManager.MarkAsSold(product.Id, currentUser.Id))
                {
                    MessageBox.Show("Satın alma işlemi başarıyla tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Ürün listesini güncelle
                    flowLayoutPanel.Controls.Clear();
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("Satın alma işlemi sırasında bir hata oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void RefreshProducts()
        {
            flowLayoutPanel.Controls.Clear();
            LoadProducts();
        }

        private void AllProductsForm_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler
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