﻿namespace Pazar
{
    partial class AnalizForm
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 23);
            label1.Name = "label1";
            label1.Size = new Size(106, 20);
            label1.TabIndex = 0;
            label1.Text = "Kullanici Sayısı";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 89);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 1;
            label2.Text = "Ilan Sayısı";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 139);
            label3.Name = "label3";
            label3.Size = new Size(131, 20);
            label3.TabIndex = 2;
            label3.Text = "Toplam satış sayısı";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(218, 139);
            label4.Name = "label4";
            label4.Size = new Size(17, 20);
            label4.TabIndex = 5;
            label4.Text = "0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(218, 89);
            label5.Name = "label5";
            label5.Size = new Size(17, 20);
            label5.TabIndex = 4;
            label5.Text = "0";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(218, 23);
            label6.Name = "label6";
            label6.Size = new Size(17, 20);
            label6.TabIndex = 3;
            label6.Text = "0";
            // 
            // AnalizForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "AnalizForm";
            Text = "AnalizForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}