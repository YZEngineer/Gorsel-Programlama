namespace Pazar
{
    partial class Register
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
            label4 = new Label();
            textBox4 = new TextBox();
            label3 = new Label();
            textBox3 = new TextBox();
            button3 = new Button();
            label2 = new Label();
            label1 = new Label();
            button2 = new Button();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(76, 271);
            label4.Name = "label4";
            label4.Size = new Size(83, 20);
            label4.TabIndex = 29;
            label4.Text = "Şifre Tekrar";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(76, 294);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(157, 27);
            textBox4.TabIndex = 28;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(76, 116);
            label3.Name = "label3";
            label3.Size = new Size(64, 20);
            label3.TabIndex = 27;
            label3.Text = " E-posta";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(76, 139);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(157, 27);
            textBox3.TabIndex = 26;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // button3
            // 
            button3.Location = new Point(555, 14);
            button3.Name = "button3";
            button3.Size = new Size(125, 29);
            button3.TabIndex = 25;
            button3.Text = "Geri";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(76, 188);
            label2.Name = "label2";
            label2.Size = new Size(39, 20);
            label2.TabIndex = 24;
            label2.Text = "Şifre";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(76, 42);
            label1.Name = "label1";
            label1.Size = new Size(92, 20);
            label1.TabIndex = 23;
            label1.Text = "Kullanıcı Adı";
            // 
            // button2
            // 
            button2.Location = new Point(76, 344);
            button2.Name = "button2";
            button2.Size = new Size(157, 29);
            button2.TabIndex = 22;
            button2.Text = "Kayit Ol";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(76, 65);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(157, 27);
            textBox2.TabIndex = 21;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(76, 211);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(157, 27);
            textBox1.TabIndex = 20;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(textBox4);
            Controls.Add(label3);
            Controls.Add(textBox3);
            Controls.Add(button3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "Register";
            Text = "Register";
            Load += Register_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label4;
        private TextBox textBox4;
        private Label label3;
        private TextBox textBox3;
        private Button button3;
        private Label label2;
        private Label label1;
        private Button button2;
        private TextBox textBox2;
        private TextBox textBox1;
    }
}