namespace Screen.Views
{
    partial class FrmPrincipalScreen
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
            CrearFacturar = new Button();
            btnEditarProducto = new Button();
            btnEditarCliente = new Button();
            btnEditarDistribuidor = new Button();
            btnHistorialVenta = new Button();
            btnMovimientoInventario = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(CrearFacturar);
            panel1.Controls.Add(btnEditarProducto);
            panel1.Controls.Add(btnEditarCliente);
            panel1.Controls.Add(btnEditarDistribuidor);
            panel1.Controls.Add(btnHistorialVenta);
            panel1.Controls.Add(btnMovimientoInventario);
            panel1.Location = new Point(34, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1478, 807);
            panel1.TabIndex = 0;
            // 
            // CrearFacturar
            // 
            CrearFacturar.BackColor = SystemColors.GradientActiveCaption;
            CrearFacturar.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CrearFacturar.Location = new Point(33, 47);
            CrearFacturar.Name = "CrearFacturar";
            CrearFacturar.Size = new Size(292, 101);
            CrearFacturar.TabIndex = 0;
            CrearFacturar.Text = "CREAR FACTURAR";
            CrearFacturar.UseVisualStyleBackColor = false;
            CrearFacturar.Click += CrearFacturar_Click;
            // 
            // btnEditarProducto
            // 
            btnEditarProducto.Location = new Point(353, 60);
            btnEditarProducto.Name = "btnEditarProducto";
            btnEditarProducto.Size = new Size(106, 88);
            btnEditarProducto.TabIndex = 12;
            btnEditarProducto.Text = "EDITAR PRODUCTO";
            btnEditarProducto.UseVisualStyleBackColor = true;
            btnEditarProducto.Click += btnEditarProducto_Click;
            // 
            // btnEditarCliente
            // 
            btnEditarCliente.Location = new Point(495, 60);
            btnEditarCliente.Name = "btnEditarCliente";
            btnEditarCliente.Size = new Size(106, 88);
            btnEditarCliente.TabIndex = 11;
            btnEditarCliente.Text = "EDITAR    CLIENTE";
            btnEditarCliente.UseVisualStyleBackColor = true;
            btnEditarCliente.Click += btnEditarCliente_Click;
            // 
            // btnEditarDistribuidor
            // 
            btnEditarDistribuidor.Location = new Point(638, 60);
            btnEditarDistribuidor.Name = "btnEditarDistribuidor";
            btnEditarDistribuidor.Size = new Size(106, 88);
            btnEditarDistribuidor.TabIndex = 10;
            btnEditarDistribuidor.Text = "EDITAR DISTRIBUIDOR";
            btnEditarDistribuidor.UseVisualStyleBackColor = true;
            btnEditarDistribuidor.Click += btnEditarDistribuidor_Click;
            // 
            // btnHistorialVenta
            // 
            btnHistorialVenta.Location = new Point(780, 60);
            btnHistorialVenta.Name = "btnHistorialVenta";
            btnHistorialVenta.Size = new Size(106, 88);
            btnHistorialVenta.TabIndex = 9;
            btnHistorialVenta.Text = "HISTORIAL DE VENTA";
            btnHistorialVenta.UseVisualStyleBackColor = true;
            btnHistorialVenta.Click += btnHistorialVenta_Click;
            // 
            // btnMovimientoInventario
            // 
            btnMovimientoInventario.Location = new Point(919, 60);
            btnMovimientoInventario.Name = "btnMovimientoInventario";
            btnMovimientoInventario.Size = new Size(106, 88);
            btnMovimientoInventario.TabIndex = 8;
            btnMovimientoInventario.Text = "HISTRIAL MOVIMIENTO DE INVENTARIO";
            btnMovimientoInventario.UseVisualStyleBackColor = true;
            btnMovimientoInventario.Click += btnMovimientoInventario_Click;
            // 
            // FrmPrincipalScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(1479, 561);
            Controls.Add(panel1);
            Name = "FrmPrincipalScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmPrincipalScreen";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnEditarProducto;
        private Button btnEditarCliente;
        private Button btnEditarDistribuidor;
        private Button btnHistorialVenta;
        private Button btnMovimientoInventario;
        private Button CrearFacturar;
    }
}