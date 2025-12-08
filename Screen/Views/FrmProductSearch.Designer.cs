namespace Screen.Views
{
    partial class FrmProductSearch
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
            txtSearch = new TextBox();
            label1 = new Label();
            button1 = new Button();
            panel1 = new Panel();
            dgvProducts = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(86, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(282, 23);
            txtSearch.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 6);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 1;
            label1.Text = "SEARCH";
            // 
            // button1
            // 
            button1.Location = new Point(57, 2);
            button1.Name = "button1";
            button1.Size = new Size(23, 23);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(dgvProducts);
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(25, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 831);
            panel1.TabIndex = 3;
            // 
            // dgvProducts
            // 
            dgvProducts.BackgroundColor = SystemColors.ButtonHighlight;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(20, 31);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Size = new Size(758, 787);
            dgvProducts.TabIndex = 3;
            dgvProducts.CellContentClick += dgvProducts_CellDoubleClick;
            // 
            // FrmProductSearch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(859, 855);
            Controls.Add(panel1);
            Name = "FrmProductSearch";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmProductSearch";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtSearch;
        private Label label1;
        private Button button1;
        private Panel panel1;
        private DataGridView dgvProducts;
    }
}