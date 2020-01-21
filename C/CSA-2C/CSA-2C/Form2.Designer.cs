namespace CSA_2C
{
    partial class Form2
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
            this.single = new System.Windows.Forms.Button();
            this.multi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // single
            // 
            this.single.Font = new System.Drawing.Font("Book Antiqua", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.single.ForeColor = System.Drawing.Color.Red;
            this.single.Location = new System.Drawing.Point(101, 300);
            this.single.Name = "single";
            this.single.Size = new System.Drawing.Size(253, 32);
            this.single.TabIndex = 0;
            this.single.Text = "Одиночная игра";
            this.single.UseVisualStyleBackColor = true;
            this.single.Click += new System.EventHandler(this.single_Click);
            // 
            // multi
            // 
            this.multi.Font = new System.Drawing.Font("Book Antiqua", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.multi.ForeColor = System.Drawing.Color.Red;
            this.multi.Location = new System.Drawing.Point(101, 338);
            this.multi.Name = "multi";
            this.multi.Size = new System.Drawing.Size(253, 32);
            this.multi.TabIndex = 1;
            this.multi.Text = "Многопользовательская игра";
            this.multi.UseVisualStyleBackColor = true;
            this.multi.Click += new System.EventHandler(this.multi_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(102, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 60);
            this.label1.TabIndex = 2;
            this.label1.Text = "ШАШКИ";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(462, 429);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.multi);
            this.Controls.Add(this.single);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form2";
            this.Text = "Шашки";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button single;
        private System.Windows.Forms.Button multi;
        private System.Windows.Forms.Label label1;
    }
}