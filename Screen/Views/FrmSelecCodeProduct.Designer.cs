namespace Screen.Views
{
    partial class FrmSelecCodeProduct
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
            buttonSearch = new Button();
            textCode = new TextBox();
            panel2 = new Panel();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(buttonSearch);
            panel1.Controls.Add(textCode);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(106, 87);
            panel1.Name = "panel1";
            panel1.Size = new Size(576, 240);
            panel1.TabIndex = 0;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(383, 159);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(78, 43);
            buttonSearch.TabIndex = 3;
            buttonSearch.Text = "BUSCAR";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += button1_Click;
            // 
            // textCode
            // 
            textCode.Location = new Point(202, 170);
            textCode.Name = "textCode";
            textCode.Size = new Size(92, 23);
            textCode.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(144, 29);
            panel2.Name = "panel2";
            panel2.Size = new Size(317, 103);
            panel2.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(120, 42);
            label3.Name = "label3";
            label3.Size = new Size(68, 15);
            label3.TabIndex = 4;
            label3.Text = "PRODUCTO";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(332, 208);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 0;
            label2.Text = "label2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(143, 173);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 0;
            label1.Text = "CODIGO";
            // 
            // FrmSelecCodeProduct
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Name = "FrmSelecCodeProduct";
            Text = "FrmSelecCodeProduct";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button buttonSearch;
        private TextBox textCode;
        private Panel panel2;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}