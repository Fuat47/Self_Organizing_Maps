namespace DSS_SOM
{
    partial class DataDisplayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataDisplayForm));
            listBoxOutput = new ListBox();
            SuspendLayout();
            // 
            // listBoxOutput
            // 
            listBoxOutput.Dock = DockStyle.Fill;
            listBoxOutput.FormattingEnabled = true;
            listBoxOutput.ItemHeight = 15;
            listBoxOutput.Location = new Point(0, 0);
            listBoxOutput.Name = "listBoxOutput";
            listBoxOutput.Size = new Size(483, 292);
            listBoxOutput.TabIndex = 1;
            // 
            // DataDisplayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(483, 292);
            Controls.Add(listBoxOutput);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DataDisplayForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DataDisplayForm";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxOutput;
    }
}