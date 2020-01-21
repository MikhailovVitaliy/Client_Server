namespace CSA_2C
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.start = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.turn = new System.Windows.Forms.Button();
            this.victoryLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(647, 22);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(665, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Enabled = false;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(520, 522);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(614, 73);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(126, 74);
            this.start.TabIndex = 3;
            this.start.Text = "Начать игру";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(614, 282);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(126, 33);
            this.close.TabIndex = 4;
            this.close.Text = "Завершить";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // turn
            // 
            this.turn.Enabled = false;
            this.turn.Location = new System.Drawing.Point(614, 174);
            this.turn.Name = "turn";
            this.turn.Size = new System.Drawing.Size(126, 31);
            this.turn.TabIndex = 5;
            this.turn.Text = "Ход";
            this.turn.UseVisualStyleBackColor = true;
            this.turn.Click += new System.EventHandler(this.turn_Click);
            // 
            // victoryLabel
            // 
            this.victoryLabel.AutoSize = true;
            this.victoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.16F);
            this.victoryLabel.Location = new System.Drawing.Point(558, 420);
            this.victoryLabel.Name = "victoryLabel";
            this.victoryLabel.Size = new System.Drawing.Size(0, 29);
            this.victoryLabel.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.turn;
            this.ClientSize = new System.Drawing.Size(786, 584);
            this.Controls.Add(this.victoryLabel);
            this.Controls.Add(this.turn);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Одиночная игра";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button turn;
        private System.Windows.Forms.Label victoryLabel;
    }
}

