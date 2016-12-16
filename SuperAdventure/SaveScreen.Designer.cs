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
            this.delete = new System.Windows.Forms.Button();
            this.saveName1 = new System.Windows.Forms.Label();
            this.saveName2 = new System.Windows.Forms.Label();
            this.saveName3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // save1
            // 
            this.save1.Location = new System.Drawing.Point(30, 88);
            this.save1.Name = "save1";
            this.save1.Size = new System.Drawing.Size(330, 65);
            this.save1.TabIndex = 0;
            this.save1.Text = "File 1";
            this.save1.UseVisualStyleBackColor = true;
            this.save1.Click += new System.EventHandler(this.save1_Click);
            // 
            // save2
            // 
            this.save2.Location = new System.Drawing.Point(30, 159);
            this.save2.Name = "save2";
            this.save2.Size = new System.Drawing.Size(330, 65);
            this.save2.TabIndex = 1;
            this.save2.Text = "File 2";
            this.save2.UseVisualStyleBackColor = true;
            this.save2.Click += new System.EventHandler(this.save2_Click);
            // 
            // save3
            // 
            this.save3.Location = new System.Drawing.Point(30, 230);
            this.save3.Name = "save3";
            this.save3.Size = new System.Drawing.Size(330, 65);
            this.save3.TabIndex = 2;
            this.save3.Text = "File 3";
            this.save3.UseVisualStyleBackColor = true;
            this.save3.Click += new System.EventHandler(this.save3_Click);
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(30, 413);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(330, 65);
            this.delete.TabIndex = 3;
            this.delete.Text = "Delete";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // saveName1
            // 
            this.saveName1.AutoSize = true;
            this.saveName1.Location = new System.Drawing.Point(392, 114);
            this.saveName1.Name = "saveName1";
            this.saveName1.Size = new System.Drawing.Size(35, 13);
            this.saveName1.TabIndex = 4;
            this.saveName1.Text = "label1";
            this.saveName1.Click += new System.EventHandler(this.saveName1_Click);
            // 
            // saveName2
            // 
            this.saveName2.AutoSize = true;
            this.saveName2.Location = new System.Drawing.Point(392, 185);
            this.saveName2.Name = "saveName2";
            this.saveName2.Size = new System.Drawing.Size(35, 13);
            this.saveName2.TabIndex = 5;
            this.saveName2.Text = "label2";
            this.saveName2.Click += new System.EventHandler(this.saveName2_Click);
            // 
            // saveName3
            // 
            this.saveName3.AutoSize = true;
            this.saveName3.Location = new System.Drawing.Point(392, 256);
            this.saveName3.Name = "saveName3";
            this.saveName3.Size = new System.Drawing.Size(35, 13);
            this.saveName3.TabIndex = 6;
            this.saveName3.Text = "label3";
            this.saveName3.Click += new System.EventHandler(this.saveName3_Click);
            // 
            // SaveScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 651);
            this.Controls.Add(this.saveName3);
            this.Controls.Add(this.saveName2);
            this.Controls.Add(this.saveName1);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.save3);
            this.Controls.Add(this.save2);
            this.Controls.Add(this.save1);
            this.Name = "SaveScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SaveScreen";
            this.Load += new System.EventHandler(this.SaveScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button save1;
        private System.Windows.Forms.Button save2;
        private System.Windows.Forms.Button save3;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Label saveName1;
        private System.Windows.Forms.Label saveName2;
        private System.Windows.Forms.Label saveName3;
    }
}