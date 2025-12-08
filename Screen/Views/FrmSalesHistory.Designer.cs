namespace Screen.Views
{
    partial class FrmSalesHistory
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
            panel1 = new Panel();
            dtpFecha = new DateTimePicker();
            button1 = new Button();
            dgvSalesHistory = new DataGridView();
            label1 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSalesHistory).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(dtpFecha);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(dgvSalesHistory);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(45, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(714, 796);
            panel1.TabIndex = 0;
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(81, 3);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(193, 23);
            dtpFecha.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(280, 3);
            button1.Name = "button1";
            button1.Size = new Size(24, 23);
            button1.TabIndex = 3;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dgvSalesHistory
            // 
            dgvSalesHistory.BackgroundColor = SystemColors.ButtonHighlight;
            dgvSalesHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSalesHistory.Location = new Point(31, 32);
            dgvSalesHistory.Name = "dgvSalesHistory";
            dgvSalesHistory.Size = new Size(655, 731);
            dgvSalesHistory.TabIndex = 2;
            dgvSalesHistory.CellContentClick += dgvSalesHistory_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 6);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 0;
            label1.Text = "FECHA";
            // 
            // FrmSalesHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(800, 820);
            Controls.Add(panel1);
            Name = "FrmSalesHistory";
            Text = "FrmSalesHistory";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSalesHistory).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private DataGridView dgvSalesHistory;
        private Label label1;
        private DateTimePicker dtpFecha;
    }
}