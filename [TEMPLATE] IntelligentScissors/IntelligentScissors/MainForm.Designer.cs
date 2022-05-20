namespace IntelligentScissors
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnCrop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.originalX = new System.Windows.Forms.Label();
            this.originalY = new System.Windows.Forms.Label();
            this.textOriginalY = new System.Windows.Forms.TextBox();
            this.textOriginalX = new System.Windows.Forms.TextBox();
            this.pixelNum = new System.Windows.Forms.Label();
            this.textPixelNum = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAutoAnchor = new System.Windows.Forms.Button();
            this.frequencyValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyValue)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(427, 360);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpen.Location = new System.Drawing.Point(478, 644);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(122, 82);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open Image";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(102, 593);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Original Image";
            // 
            // txtHeight
            // 
            this.txtHeight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtHeight.Location = new System.Drawing.Point(376, 693);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.ReadOnly = true;
            this.txtHeight.Size = new System.Drawing.Size(75, 27);
            this.txtHeight.TabIndex = 8;
            // 
            // txtWidth
            // 
            this.txtWidth.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtWidth.Location = new System.Drawing.Point(375, 644);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.ReadOnly = true;
            this.txtWidth.Size = new System.Drawing.Size(75, 27);
            this.txtWidth.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(301, 650);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 21);
            this.label5.TabIndex = 12;
            this.label5.Text = "Width";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(301, 699);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 21);
            this.label6.TabIndex = 13;
            this.label6.Text = "Height";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(16, 19);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 569);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(828, 19);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(495, 569);
            this.panel2.TabIndex = 16;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(4, 5);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(412, 360);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // btnCrop
            // 
            this.btnCrop.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCrop.Location = new System.Drawing.Point(685, 540);
            this.btnCrop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCrop.Name = "btnCrop";
            this.btnCrop.Size = new System.Drawing.Size(122, 48);
            this.btnCrop.TabIndex = 5;
            this.btnCrop.Text = "Crop";
            this.btnCrop.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(991, 588);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Croped Image";
            // 
            // originalX
            // 
            this.originalX.AutoSize = true;
            this.originalX.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.originalX.Location = new System.Drawing.Point(697, 47);
            this.originalX.Name = "originalX";
            this.originalX.Size = new System.Drawing.Size(94, 21);
            this.originalX.TabIndex = 17;
            this.originalX.Text = "Original X";
            // 
            // originalY
            // 
            this.originalY.AutoSize = true;
            this.originalY.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.originalY.Location = new System.Drawing.Point(697, 121);
            this.originalY.Name = "originalY";
            this.originalY.Size = new System.Drawing.Size(93, 21);
            this.originalY.TabIndex = 18;
            this.originalY.Text = "Original Y";
            // 
            // textOriginalY
            // 
            this.textOriginalY.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textOriginalY.Location = new System.Drawing.Point(697, 147);
            this.textOriginalY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textOriginalY.Name = "textOriginalY";
            this.textOriginalY.ReadOnly = true;
            this.textOriginalY.Size = new System.Drawing.Size(93, 27);
            this.textOriginalY.TabIndex = 19;
            // 
            // textOriginalX
            // 
            this.textOriginalX.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textOriginalX.Location = new System.Drawing.Point(697, 73);
            this.textOriginalX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textOriginalX.Name = "textOriginalX";
            this.textOriginalX.ReadOnly = true;
            this.textOriginalX.Size = new System.Drawing.Size(93, 27);
            this.textOriginalX.TabIndex = 20;
            // 
            // pixelNum
            // 
            this.pixelNum.AutoSize = true;
            this.pixelNum.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.pixelNum.Location = new System.Drawing.Point(685, 235);
            this.pixelNum.Name = "pixelNum";
            this.pixelNum.Size = new System.Drawing.Size(122, 21);
            this.pixelNum.TabIndex = 21;
            this.pixelNum.Text = "Pixel number";
            // 
            // textPixelNum
            // 
            this.textPixelNum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textPixelNum.Location = new System.Drawing.Point(685, 261);
            this.textPixelNum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textPixelNum.Name = "textPixelNum";
            this.textPixelNum.ReadOnly = true;
            this.textPixelNum.Size = new System.Drawing.Size(122, 27);
            this.textPixelNum.TabIndex = 22;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClear.Location = new System.Drawing.Point(685, 460);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(122, 48);
            this.btnClear.TabIndex = 23;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAutoAnchor
            // 
            this.btnAutoAnchor.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAutoAnchor.Location = new System.Drawing.Point(767, 644);
            this.btnAutoAnchor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAutoAnchor.Name = "btnAutoAnchor";
            this.btnAutoAnchor.Size = new System.Drawing.Size(122, 82);
            this.btnAutoAnchor.TabIndex = 24;
            this.btnAutoAnchor.Text = "Auto Anchor";
            this.btnAutoAnchor.UseVisualStyleBackColor = true;
            this.btnAutoAnchor.Click += new System.EventHandler(this.btnAutoAnchor_Click);
            // 
            // frequencyValue
            // 
            this.frequencyValue.AllowDrop = true;
            this.frequencyValue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.frequencyValue.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.frequencyValue.Location = new System.Drawing.Point(923, 675);
            this.frequencyValue.Margin = new System.Windows.Forms.Padding(4);
            this.frequencyValue.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.frequencyValue.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.frequencyValue.Name = "frequencyValue";
            this.frequencyValue.Size = new System.Drawing.Size(94, 27);
            this.frequencyValue.TabIndex = 10;
            this.frequencyValue.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            this.frequencyValue.ValueChanged += new System.EventHandler(this.frequencyValue_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(918, 650);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 21);
            this.label3.TabIndex = 25;
            this.label3.Text = "Frequency";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1361, 769);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAutoAnchor);
            this.Controls.Add(this.frequencyValue);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.textPixelNum);
            this.Controls.Add(this.pixelNum);
            this.Controls.Add(this.textOriginalX);
            this.Controls.Add(this.textOriginalY);
            this.Controls.Add(this.originalY);
            this.Controls.Add(this.originalX);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.btnCrop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpen);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Intelligent Scissors...";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnCrop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label originalX;
        private System.Windows.Forms.Label originalY;
        private System.Windows.Forms.TextBox textOriginalY;
        private System.Windows.Forms.TextBox textOriginalX;
        private System.Windows.Forms.Label pixelNum;
        private System.Windows.Forms.TextBox textPixelNum;
        private System.Windows.Forms.NumericUpDown frequencyValue;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAutoAnchor;
        private System.Windows.Forms.Label label3;
    }
}