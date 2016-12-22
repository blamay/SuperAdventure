namespace SuperAdventure
{
    partial class SaveScreen
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
            this.save1 = new System.Windows.Forms.Button();
            this.save2 = new System.Windows.Forms.Button();
            this.save3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDelete1 = new System.Windows.Forms.Button();
            this.buttonDelete2 = new System.Windows.Forms.Button();
            this.buttonDelete3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // save1
            // 
            this.save1.Font = new System.Drawing.Font("Segoe Print", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save1.Location = new System.Drawing.Point(12, 66);
            this.save1.Name = "save1";
            this.save1.Size = new System.Drawing.Size(330, 65);
            this.save1.TabIndex = 0;
            this.save1.Text = "File 1";
            this.save1.UseVisualStyleBackColor = true;
            this.save1.Click += new System.EventHandler(this.save1_Click);
            // 
            // save2
            // 
            this.save2.Font = new System.Drawing.Font("Segoe Print", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save2.Location = new System.Drawing.Point(12, 137);
            this.save2.Name = "save2";
            this.save2.Size = new System.Drawing.Size(330, 65);
            this.save2.TabIndex = 1;
            this.save2.Text = "File 2";
            this.save2.UseVisualStyleBackColor = true;
            this.save2.Click += new System.EventHandler(this.save2_Click);
            // 
            // save3
            // 
            this.save3.Font = new System.Drawing.Font("Segoe Print", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save3.Location = new System.Drawing.Point(12, 208);
            this.save3.Name = "save3";
            this.save3.Size = new System.Drawing.Size(330, 65);
            this.save3.TabIndex = 2;
            this.save3.Text = "File 3";
            this.save3.UseVisualStyleBackColor = true;
            this.save3.Click += new System.EventHandler(this.save3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(136, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 33);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select Game File";
            // 
            // buttonDelete1
            // 
            this.buttonDelete1.Location = new System.Drawing.Point(348, 66);
            this.buttonDelete1.Name = "buttonDelete1";
            this.buttonDelete1.Size = new System.Drawing.Size(65, 65);
            this.buttonDelete1.TabIndex = 5;
            this.buttonDelete1.Text = "Delete";
            this.buttonDelete1.UseVisualStyleBackColor = true;
            this.buttonDelete1.Click += new System.EventHandler(this.buttonDelete1_Click);
            // 
            // buttonDelete2
            // 
            this.buttonDelete2.Location = new System.Drawing.Point(348, 137);
            this.buttonDelete2.Name = "buttonDelete2";
            this.buttonDelete2.Size = new System.Drawing.Size(65, 65);
            this.buttonDelete2.TabIndex = 6;
            this.buttonDelete2.Text = "Delete";
            this.buttonDelete2.UseVisualStyleBackColor = true;
            this.buttonDelete2.Click += new System.EventHandler(this.buttonDelete2_Click);
            // 
            // buttonDelete3
            // 
            this.buttonDelete3.Location = new System.Drawing.Point(348, 208);
            this.buttonDelete3.Name = "buttonDelete3";
            this.buttonDelete3.Size = new System.Drawing.Size(65, 65);
            this.buttonDelete3.TabIndex = 7;
            this.buttonDelete3.Text = "Delete";
            this.buttonDelete3.UseVisualStyleBackColor = true;
            this.buttonDelete3.Click += new System.EventHandler(this.buttonDelete3_Click);
            // 
            // SaveScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 281);
            this.Controls.Add(this.buttonDelete3);
            this.Controls.Add(this.buttonDelete2);
            this.Controls.Add(this.buttonDelete1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.save3);
            this.Controls.Add(this.save2);
            this.Controls.Add(this.save1);
            this.Name = "SaveScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SaveScreen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveScreen_FormClosing);
            this.Load += new System.EventHandler(this.SaveScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button save1;
        private System.Windows.Forms.Button save2;
        private System.Windows.Forms.Button save3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDelete1;
        private System.Windows.Forms.Button buttonDelete2;
        private System.Windows.Forms.Button buttonDelete3;
    }
}