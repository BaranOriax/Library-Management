namespace Kütüphane
{
    partial class KitapHakkında
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
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(552, 100);
            label1.Name = "label1";
            label1.Size = new Size(33, 20);
            label1.TabIndex = 1;
            label1.Text = "Boş";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(552, 183);
            label2.Name = "label2";
            label2.Size = new Size(33, 20);
            label2.TabIndex = 2;
            label2.Text = "Boş";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(552, 248);
            label3.Name = "label3";
            label3.Size = new Size(33, 20);
            label3.TabIndex = 3;
            label3.Text = "Boş";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(552, 314);
            label4.Name = "label4";
            label4.Size = new Size(33, 20);
            label4.TabIndex = 4;
            label4.Text = "Boş";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(552, 374);
            label5.Name = "label5";
            label5.Size = new Size(33, 20);
            label5.TabIndex = 5;
            label5.Text = "Boş";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(314, 100);
            label6.Name = "label6";
            label6.Size = new Size(74, 20);
            label6.TabIndex = 6;
            label6.Text = "Kitap Adı:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(314, 183);
            label7.Name = "label7";
            label7.Size = new Size(74, 20);
            label7.TabIndex = 7;
            label7.Text = "Yazar Adı:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(314, 248);
            label8.Name = "label8";
            label8.Size = new Size(58, 20);
            label8.TabIndex = 8;
            label8.Text = "Tür adı:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(314, 314);
            label9.Name = "label9";
            label9.Size = new Size(126, 20);
            label9.TabIndex = 9;
            label9.Text = "Yayınlanma Tarihi:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(314, 374);
            label10.Name = "label10";
            label10.Size = new Size(89, 20);
            label10.TabIndex = 10;
            label10.Text = "Sayfa Sayısı:";
            // 
            // button1
            // 
            button1.Location = new Point(768, 124);
            button1.Name = "button1";
            button1.Size = new Size(125, 107);
            button1.TabIndex = 11;
            button1.Text = "Kirala";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(768, 281);
            button2.Name = "button2";
            button2.Size = new Size(125, 113);
            button2.TabIndex = 12;
            button2.Text = "Geri";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // KitapHakkında
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.kapak;
            ClientSize = new Size(1059, 560);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "KitapHakkında";
            Text = "KitapHakkında";
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
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Button button1;
        private Button button2;
    }
}