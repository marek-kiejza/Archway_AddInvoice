namespace Hydro_Csharp
{
    partial class Form1
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
            this.Button1 = new System.Windows.Forms.Button();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDes = new System.Windows.Forms.TextBox();
            this.textBoxQuan = new System.Windows.Forms.TextBox();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.Description = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(96, 160);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(169, 49);
            this.Button1.TabIndex = 12;
            this.Button1.Text = "Add Invoice";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(200, 52);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.Size = new System.Drawing.Size(100, 20);
            this.textBoxPass.TabIndex = 13;
            this.textBoxPass.TextChanged += new System.EventHandler(this.textBoxPass_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Enter password";
            // 
            // textBoxDes
            // 
            this.textBoxDes.Location = new System.Drawing.Point(611, 63);
            this.textBoxDes.Name = "textBoxDes";
            this.textBoxDes.Size = new System.Drawing.Size(162, 20);
            this.textBoxDes.TabIndex = 15;
            // 
            // textBoxQuan
            // 
            this.textBoxQuan.Location = new System.Drawing.Point(611, 135);
            this.textBoxQuan.Name = "textBoxQuan";
            this.textBoxQuan.Size = new System.Drawing.Size(100, 20);
            this.textBoxQuan.TabIndex = 16;
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Location = new System.Drawing.Point(611, 214);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(100, 20);
            this.textBoxPrice.TabIndex = 17;
            // 
            // Description
            // 
            this.Description.AutoSize = true;
            this.Description.Location = new System.Drawing.Point(507, 63);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(60, 13);
            this.Description.TabIndex = 18;
            this.Description.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(512, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Quantity";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(512, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "NetPrice";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 346);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.textBoxPrice);
            this.Controls.Add(this.textBoxQuan);
            this.Controls.Add(this.textBoxDes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.Button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Button Button1;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDes;
        private System.Windows.Forms.TextBox textBoxQuan;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Label Description;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

