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
    public partial class UsersForm : Form
    {
        private FlowLayoutPanel flowLayoutPanel;
        private Image defaultUserImage;

        public UsersForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            CreateDefaultImage();
            LoadUsers();
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

        private void CreateDefaultImage()
        {
            // Varsayılan kullanıcı resmi oluştur
            defaultUserImage = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(defaultUserImage))
            {
                g.Clear(Color.LightGray);
                g.DrawString("Resim Yok",
                    new Font("Arial", 10),
                    Brushes.DarkGray,
                    new PointF(10, 40));
            }
        }

        private void LoadUsers()
        {
            // Tüm kullanıcıları al
            var users = UserManager.GetAllUsers();

            // Her kullanıcı için bir panel oluştur
            foreach (var user in users)
            {
                Panel userPanel = CreateUserPanel(user);
                flowLayoutPanel.Controls.Add(userPanel);
            }
        }

        private Panel CreateUserPanel(User user)
        {
            // Kullanıcı paneli
            Panel panel = new Panel
            {
                Width = 300,
                Height = 200,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Kullanıcı resmi
            PictureBox pictureBox = new PictureBox
            {
                Width = 100,
                Height = 100,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray
            };

            // Resmi yükle
            LoadUserImage(pictureBox, user.Id);

            // Kullanıcı adı
            Label nameLabel = new Label
            {
                Text = user.Name,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(120, 10),
                Width = 170,
                Height = 30
            };

            // E-posta
            Label emailLabel = new Label
            {
                Text = user.Email,
                Font = new Font("Arial", 9),
                Location = new Point(120, 40),
                Width = 170,
                Height = 20
            };

            // Rol
            Label roleLabel = new Label
            {
                Text = user.IsAdmin ? "Admin" : "Kullanıcı",
                Font = new Font("Arial", 9),
                Location = new Point(120, 60),
                Width = 170,
                Height = 20
            };

            // Detay butonu
            Button detailsButton = new Button
            {
                Text = "Detaylar",
                Location = new Point(120, 90),
                Width = 80,
                Height = 30,
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            detailsButton.Click += (s, e) => ShowUserDetails(user);

            // Sil butonu
            Button deleteButton = new Button
            {
                Text = "Sil",
                Location = new Point(210, 90),
                Width = 80,
                Height = 30,
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            deleteButton.Click += (s, e) => DeleteUser(user);

            // Kontrolleri panele ekle
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(emailLabel);
            panel.Controls.Add(roleLabel);
            panel.Controls.Add(detailsButton);
            panel.Controls.Add(deleteButton);

            return panel;
        }

        private void LoadUserImage(PictureBox pictureBox, int userId)
        {
            string imagePath = Path.Combine(Application.StartupPath, "UserImages", $"user_{userId}.jpg");

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
                    pictureBox.Image = defaultUserImage;
                }
            }
            else
            {
                pictureBox.Image = defaultUserImage;
            }
        }

        private void ShowUserDetails(User user)
        {
            UserDetailsForm detailsForm = new UserDetailsForm(user);
            detailsForm.ShowDialog();
        }

        private void DeleteUser(User user)
        {
            // Admin kendini silemesin
            if (user.Id == Program.CurrentUser.Id)
            {
                MessageBox.Show("Kendinizi silemezsiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show(
                $"{user.Name} kullanıcısını silmek istediğinizden emin misiniz?\nBu işlem geri alınamaz!",
                "Kullanıcı Silme",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (UserManager.DeleteUser(user.Id))
                {
                    MessageBox.Show("Kullanıcı başarıyla silindi.");
                    flowLayoutPanel.Controls.Clear();
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Kullanıcı silinirken bir hata oluştu.");
                }
            }
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // UsersForm
            // 
            ClientSize = new Size(773, 392);
            Name = "UsersForm";
            Load += UsersForm_Load;
            ResumeLayout(false);

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Varsayılan resmi temizle
            if (defaultUserImage != null)
            {
                defaultUserImage.Dispose();
            }
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {

        }
    }
} 