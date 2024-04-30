namespace DSS_SOM
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btnBrowse = new Button();
            label1 = new Label();
            label2 = new Label();
            txtDataset = new TextBox();
            txtDimension = new TextBox();
            btnSOM = new Button();
            SuspendLayout();
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(231, 42);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 0;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 46);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 1;
            label1.Text = "Dataset File:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(49, 77);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 2;
            label2.Text = "Dimension:";
            // 
            // txtDataset
            // 
            txtDataset.Location = new Point(125, 43);
            txtDataset.Name = "txtDataset";
            txtDataset.ReadOnly = true;
            txtDataset.Size = new Size(100, 23);
            txtDataset.TabIndex = 3;
            // 
            // txtDimension
            // 
            txtDimension.Location = new Point(125, 74);
            txtDimension.Name = "txtDimension";
            txtDimension.Size = new Size(100, 23);
            txtDimension.TabIndex = 4;
            txtDimension.KeyDown += txtDimension_KeyDown;
            // 
            // btnSOM
            // 
            btnSOM.BackColor = SystemColors.MenuHighlight;
            btnSOM.Location = new Point(49, 124);
            btnSOM.Name = "btnSOM";
            btnSOM.Size = new Size(257, 38);
            btnSOM.TabIndex = 5;
            btnSOM.Text = "SOM";
            btnSOM.UseVisualStyleBackColor = false;
            btnSOM.Click += btnSOM_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(351, 214);
            Controls.Add(btnSOM);
            Controls.Add(txtDimension);
            Controls.Add(txtDataset);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnBrowse);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DEUCENG - SOM";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBrowse;
        private Label label1;
        private Label label2;
        private TextBox txtDataset;
        private TextBox txtDimension;
        private Button btnSOM;
    }
}
