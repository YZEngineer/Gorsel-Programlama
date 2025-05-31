namespace Pazar
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button3 = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button6 = new Button();
            button9 = new Button();
            button12 = new Button();
            button14 = new Button();
            label3 = new Label();
            button13 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button3
            // 
            button3.Location = new Point(12, 5);
            button3.Name = "button3";
            button3.Size = new Size(57, 29);
            button3.TabIndex = 25;
            button3.Text = "Geri";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(27, 64);
            button2.Name = "button2";
            button2.Size = new Size(119, 29);
            button2.TabIndex = 22;
            button2.Text = "urunler";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(75, 9);
            label1.Name = "label1";
            label1.Size = new Size(99, 20);
            label1.TabIndex = 26;
            label1.Text = "kullanıcı adı : ";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(180, 9);
            label2.Name = "label2";
            label2.Size = new Size(27, 20);
            label2.TabIndex = 27;
            label2.Text = "rol";
            label2.Click += label2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(27, 141);
            button1.Name = "button1";
            button1.Size = new Size(119, 29);
            button1.TabIndex = 28;
            button1.Text = "Ürün Ekle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button6
            // 
            button6.Location = new Point(27, 106);
            button6.Name = "button6";
            button6.Size = new Size(119, 29);
            button6.TabIndex = 31;
            button6.Text = "urunlerim";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button9
            // 
            button9.Location = new Point(213, 64);
            button9.Name = "button9";
            button9.Size = new Size(119, 29);
            button9.TabIndex = 34;
            button9.Text = "Profilim";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button12
            // 
            button12.Location = new Point(213, 115);
            button12.Name = "button12";
            button12.Size = new Size(119, 29);
            button12.TabIndex = 37;
            button12.Text = "Şifre Değiştir";
            button12.UseVisualStyleBackColor = true;
            button12.Click += button12_Click;
            // 
            // button14
            // 
            button14.Location = new Point(608, 93);
            button14.Name = "button14";
            button14.Size = new Size(119, 55);
            button14.TabIndex = 38;
            button14.Text = "Kullanıcı Yönetimi";
            button14.UseVisualStyleBackColor = true;
            button14.Click += button14_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(618, 68);
            label3.Name = "label3";
            label3.Size = new Size(98, 20);
            label3.TabIndex = 41;
            label3.Text = "Admin'e Özel";
            // 
            // button13
            // 
            button13.Location = new Point(695, 409);
            button13.Name = "button13";
            button13.Size = new Size(93, 29);
            button13.TabIndex = 42;
            button13.Text = "Çıkış Yap:";
            button13.UseVisualStyleBackColor = true;
            button13.Click += button13_Click;
            // 
            // button4
            // 
            button4.Location = new Point(608, 160);
            button4.Name = "button4";
            button4.Size = new Size(119, 55);
            button4.TabIndex = 43;
            button4.Text = "istatistik";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click_1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button4);
            Controls.Add(button13);
            Controls.Add(label3);
            Controls.Add(button14);
            Controls.Add(button12);
            Controls.Add(button9);
            Controls.Add(button6);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(button2);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button3;
        private Button button2;
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button6;
        private Button button9;
        private Button button12;
        private Button button14;
        private Label label3;
        private Button button13;
        private Button button4;
    }
}