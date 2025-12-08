namespace Screen.Views
{
    partial class FrmInvoiceCreation
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
            dtpFechaVencimiento = new DateTimePicker();
            dtpFecha = new DateTimePicker();
            button5 = new Button();
            btnVerificarCliente = new Button();
            cmbITBIS = new ComboBox();
            cmbNCF = new ComboBox();
            btnVerHistorialVenta = new Button();
            cmbCaja = new ComboBox();
            cmbFormaPago = new ComboBox();
            cmbEstado = new ComboBox();
            panel3 = new Panel();
            label20 = new Label();
            lblITBIS = new Label();
            lblMonto = new Label();
            label17 = new Label();
            label8 = new Label();
            label16 = new Label();
            txtRNC = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label15 = new Label();
            label10 = new Label();
            label5 = new Label();
            label14 = new Label();
            txtCodCliente = new TextBox();
            label4 = new Label();
            label12 = new Label();
            label13 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtCliente = new TextBox();
            label11 = new Label();
            label1 = new Label();
            txtDireccion = new TextBox();
            txtTelefono = new TextBox();
            txtDescuento = new TextBox();
            txtNoFactura = new TextBox();
            txtNombreComercial = new TextBox();
            panel2 = new Panel();
            dgvProductos = new DataGridView();
            textBox16 = new TextBox();
            grpMontos = new GroupBox();
            label21 = new Label();
            label22 = new Label();
            label23 = new Label();
            label25 = new Label();
            label26 = new Label();
            btnCrearFacturar = new Button();
            btnMovimientoProducto = new Button();
            btnCrearCliente = new Button();
            btnCrearProducto = new Button();
            btnCrearDistribuido = new Button();
            btnBuscarProducto = new Button();
            lblCosto = new Label();
            lblGanancia = new Label();
            lblUltimoPrecio = new Label();
            lblPV = new Label();
            btnFacturar = new Button();
            btnLimpiar = new Button();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(dtpFechaVencimiento);
            panel1.Controls.Add(dtpFecha);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(btnVerificarCliente);
            panel1.Controls.Add(cmbITBIS);
            panel1.Controls.Add(cmbNCF);
            panel1.Controls.Add(btnVerHistorialVenta);
            panel1.Controls.Add(cmbCaja);
            panel1.Controls.Add(cmbFormaPago);
            panel1.Controls.Add(cmbEstado);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label16);
            panel1.Controls.Add(txtRNC);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(txtCodCliente);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtCliente);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtDireccion);
            panel1.Controls.Add(txtTelefono);
            panel1.Controls.Add(txtDescuento);
            panel1.Controls.Add(txtNoFactura);
            panel1.Controls.Add(txtNombreComercial);
            panel1.Location = new Point(38, 53);
            panel1.Name = "panel1";
            panel1.Size = new Size(971, 263);
            panel1.TabIndex = 0;
            // 
            // dtpFechaVencimiento
            // 
            dtpFechaVencimiento.Location = new Point(815, 125);
            dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            dtpFechaVencimiento.Size = new Size(75, 23);
            dtpFechaVencimiento.TabIndex = 37;
            dtpFechaVencimiento.ValueChanged += dtpFechaVencimiento_ValueChanged;
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(610, 125);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(76, 23);
            dtpFecha.TabIndex = 36;
            // 
            // button5
            // 
            button5.Location = new Point(327, 109);
            button5.Name = "button5";
            button5.Size = new Size(36, 23);
            button5.TabIndex = 35;
            button5.Text = "FACTURAR";
            button5.UseVisualStyleBackColor = true;
            // 
            // btnVerificarCliente
            // 
            btnVerificarCliente.Location = new Point(309, 138);
            btnVerificarCliente.Name = "btnVerificarCliente";
            btnVerificarCliente.Size = new Size(36, 23);
            btnVerificarCliente.TabIndex = 34;
            btnVerificarCliente.Text = "FACTURAR";
            btnVerificarCliente.UseVisualStyleBackColor = true;
            btnVerificarCliente.Click += btnVerificarCliente_Click;
            // 
            // cmbITBIS
            // 
            cmbITBIS.FormattingEnabled = true;
            cmbITBIS.Location = new Point(611, 197);
            cmbITBIS.Name = "cmbITBIS";
            cmbITBIS.Size = new Size(75, 23);
            cmbITBIS.TabIndex = 33;
            // 
            // cmbNCF
            // 
            cmbNCF.FormattingEnabled = true;
            cmbNCF.Location = new Point(192, 80);
            cmbNCF.Name = "cmbNCF";
            cmbNCF.Size = new Size(77, 23);
            cmbNCF.TabIndex = 11;
            // 
            // btnVerHistorialVenta
            // 
            btnVerHistorialVenta.Location = new Point(284, 51);
            btnVerHistorialVenta.Name = "btnVerHistorialVenta";
            btnVerHistorialVenta.Size = new Size(36, 23);
            btnVerHistorialVenta.TabIndex = 2;
            btnVerHistorialVenta.Text = "FACTURAR";
            btnVerHistorialVenta.UseVisualStyleBackColor = true;
            btnVerHistorialVenta.Click += btnVerHistorialVenta_Click;
            // 
            // cmbCaja
            // 
            cmbCaja.FormattingEnabled = true;
            cmbCaja.Location = new Point(611, 232);
            cmbCaja.Name = "cmbCaja";
            cmbCaja.Size = new Size(75, 23);
            cmbCaja.TabIndex = 10;
            // 
            // cmbFormaPago
            // 
            cmbFormaPago.FormattingEnabled = true;
            cmbFormaPago.Location = new Point(815, 162);
            cmbFormaPago.Name = "cmbFormaPago";
            cmbFormaPago.Size = new Size(75, 23);
            cmbFormaPago.TabIndex = 10;
            // 
            // cmbEstado
            // 
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(611, 162);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(75, 23);
            cmbEstado.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.GradientActiveCaption;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(label20);
            panel3.Controls.Add(lblITBIS);
            panel3.Controls.Add(lblMonto);
            panel3.Controls.Add(label17);
            panel3.Location = new Point(554, 16);
            panel3.Name = "panel3";
            panel3.Size = new Size(312, 94);
            panel3.TabIndex = 16;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label20.Location = new Point(34, 55);
            label20.Name = "label20";
            label20.Size = new Size(55, 25);
            label20.TabIndex = 3;
            label20.Text = "ITBIS";
            // 
            // lblITBIS
            // 
            lblITBIS.AutoSize = true;
            lblITBIS.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblITBIS.Location = new Point(126, 55);
            lblITBIS.Name = "lblITBIS";
            lblITBIS.Size = new Size(23, 25);
            lblITBIS.TabIndex = 2;
            lblITBIS.Text = "0";
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMonto.Location = new Point(149, 18);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(23, 25);
            lblMonto.TabIndex = 1;
            lblMonto.Text = "0";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.Location = new Point(34, 18);
            label17.Name = "label17";
            label17.Size = new Size(87, 25);
            label17.TabIndex = 0;
            label17.Text = "MONTO:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(691, 200);
            label8.Name = "label8";
            label8.Size = new Size(72, 15);
            label8.TabIndex = 24;
            label8.Text = "DESCUENTO";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(691, 165);
            label16.Name = "label16";
            label16.Size = new Size(99, 15);
            label16.TabIndex = 32;
            label16.Text = "FORMA DE PAGO";
            // 
            // txtRNC
            // 
            txtRNC.Location = new Point(192, 109);
            txtRNC.Name = "txtRNC";
            txtRNC.Size = new Size(129, 23);
            txtRNC.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(691, 130);
            label7.Name = "label7";
            label7.Size = new Size(123, 15);
            label7.TabIndex = 23;
            label7.Text = "FECHA VENCIMINETO";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(554, 235);
            label6.Name = "label6";
            label6.Size = new Size(36, 15);
            label6.TabIndex = 22;
            label6.Text = "CAJA";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(554, 200);
            label15.Name = "label15";
            label15.Size = new Size(32, 15);
            label15.TabIndex = 31;
            label15.Text = "ITBIS";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(61, 54);
            label10.Name = "label10";
            label10.Size = new Size(76, 15);
            label10.TabIndex = 26;
            label10.Text = "No.FACTURA";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(554, 165);
            label5.Name = "label5";
            label5.Size = new Size(49, 15);
            label5.TabIndex = 21;
            label5.Text = "ESTADO";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(553, 130);
            label14.Name = "label14";
            label14.Size = new Size(44, 15);
            label14.TabIndex = 30;
            label14.Text = "FECHA";
            // 
            // txtCodCliente
            // 
            txtCodCliente.Location = new Point(192, 138);
            txtCodCliente.Name = "txtCodCliente";
            txtCodCliente.Size = new Size(111, 23);
            txtCodCliente.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(61, 228);
            label4.Name = "label4";
            label4.Size = new Size(64, 15);
            label4.TabIndex = 20;
            label4.Text = "TELEFONO";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(61, 199);
            label12.Name = "label12";
            label12.Size = new Size(68, 15);
            label12.TabIndex = 28;
            label12.Text = "DIRECCION";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(61, 170);
            label13.Name = "label13";
            label13.Size = new Size(51, 15);
            label13.TabIndex = 29;
            label13.Text = "CLIENTE";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(61, 83);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 18;
            label2.Text = "NCF";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(61, 112);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 19;
            label3.Text = "RNC";
            // 
            // txtCliente
            // 
            txtCliente.Location = new Point(192, 167);
            txtCliente.Name = "txtCliente";
            txtCliente.Size = new Size(197, 23);
            txtCliente.TabIndex = 12;
            txtCliente.Text = "NOMBRE CLIENTE";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(61, 141);
            label11.Name = "label11";
            label11.Size = new Size(32, 15);
            label11.TabIndex = 27;
            label11.Text = "COD";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(61, 25);
            label1.Name = "label1";
            label1.Size = new Size(125, 15);
            label1.TabIndex = 17;
            label1.Text = "NOMBRE COMERCIAL";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(192, 196);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(207, 23);
            txtDireccion.TabIndex = 11;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(192, 225);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(111, 23);
            txtTelefono.TabIndex = 10;
            // 
            // txtDescuento
            // 
            txtDescuento.Location = new Point(815, 197);
            txtDescuento.Name = "txtDescuento";
            txtDescuento.Size = new Size(75, 23);
            txtDescuento.TabIndex = 4;
            // 
            // txtNoFactura
            // 
            txtNoFactura.Location = new Point(192, 51);
            txtNoFactura.Name = "txtNoFactura";
            txtNoFactura.Size = new Size(86, 23);
            txtNoFactura.TabIndex = 1;
            // 
            // txtNombreComercial
            // 
            txtNombreComercial.Location = new Point(192, 22);
            txtNombreComercial.Name = "txtNombreComercial";
            txtNombreComercial.Size = new Size(186, 23);
            txtNombreComercial.TabIndex = 0;
            txtNombreComercial.Text = "Plaza Lama";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.GradientInactiveCaption;
            panel2.Controls.Add(dgvProductos);
            panel2.Controls.Add(textBox16);
            panel2.Location = new Point(38, 322);
            panel2.Name = "panel2";
            panel2.Size = new Size(971, 475);
            panel2.TabIndex = 1;
            // 
            // dgvProductos
            // 
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProductos.BackgroundColor = SystemColors.ButtonHighlight;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(12, 12);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(938, 448);
            dgvProductos.TabIndex = 0;
            dgvProductos.CellEndEdit += dgvProductos_CellEndEdit;
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            // 
            // textBox16
            // 
            textBox16.Location = new Point(200, 118);
            textBox16.Name = "textBox16";
            textBox16.Size = new Size(100, 23);
            textBox16.TabIndex = 15;
            // 
            // grpMontos
            // 
            grpMontos.Location = new Point(1036, 757);
            grpMontos.Name = "grpMontos";
            grpMontos.Size = new Size(230, 100);
            grpMontos.TabIndex = 33;
            grpMontos.TabStop = false;
            grpMontos.Text = "groupBox1";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(312, 810);
            label21.Name = "label21";
            label21.Size = new Size(50, 15);
            label21.TabIndex = 2;
            label21.Text = "COSTO :";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(362, 810);
            label22.Name = "label22";
            label22.Size = new Size(0, 15);
            label22.TabIndex = 3;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(312, 834);
            label23.Name = "label23";
            label23.Size = new Size(27, 15);
            label23.TabIndex = 4;
            label23.Text = "PV :";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(631, 810);
            label25.Name = "label25";
            label25.Size = new Size(75, 15);
            label25.TabIndex = 6;
            label25.Text = "ULT PRECIO :";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(631, 834);
            label26.Name = "label26";
            label26.Size = new Size(65, 15);
            label26.TabIndex = 7;
            label26.Text = "GANACIA :";
            // 
            // btnCrearFacturar
            // 
            btnCrearFacturar.BackColor = SystemColors.GradientInactiveCaption;
            btnCrearFacturar.Location = new Point(171, 3);
            btnCrearFacturar.Name = "btnCrearFacturar";
            btnCrearFacturar.Size = new Size(702, 44);
            btnCrearFacturar.TabIndex = 1;
            btnCrearFacturar.Text = "CREAR FACTURAR";
            btnCrearFacturar.UseVisualStyleBackColor = false;
            // 
            // btnMovimientoProducto
            // 
            btnMovimientoProducto.BackColor = SystemColors.GradientInactiveCaption;
            btnMovimientoProducto.Location = new Point(606, 3);
            btnMovimientoProducto.Name = "btnMovimientoProducto";
            btnMovimientoProducto.Size = new Size(120, 44);
            btnMovimientoProducto.TabIndex = 22;
            btnMovimientoProducto.Text = "MOVIMIENTO PRODUCTO";
            btnMovimientoProducto.UseVisualStyleBackColor = false;
            btnMovimientoProducto.Click += btnMovimientoProducto_Click;
            // 
            // btnCrearCliente
            // 
            btnCrearCliente.BackColor = SystemColors.GradientInactiveCaption;
            btnCrearCliente.Location = new Point(462, 3);
            btnCrearCliente.Name = "btnCrearCliente";
            btnCrearCliente.Size = new Size(120, 44);
            btnCrearCliente.TabIndex = 23;
            btnCrearCliente.Text = "CREAR CLIENTE";
            btnCrearCliente.UseVisualStyleBackColor = false;
            btnCrearCliente.Click += btnCrearCliente_Click;
            // 
            // btnCrearProducto
            // 
            btnCrearProducto.BackColor = SystemColors.GradientInactiveCaption;
            btnCrearProducto.Location = new Point(315, 3);
            btnCrearProducto.Name = "btnCrearProducto";
            btnCrearProducto.Size = new Size(120, 44);
            btnCrearProducto.TabIndex = 24;
            btnCrearProducto.Text = "CREAR PRODUCTO";
            btnCrearProducto.UseVisualStyleBackColor = false;
            btnCrearProducto.Click += btnCrearProducto_Click;
            // 
            // btnCrearDistribuido
            // 
            btnCrearDistribuido.BackColor = SystemColors.GradientInactiveCaption;
            btnCrearDistribuido.Location = new Point(753, 3);
            btnCrearDistribuido.Name = "btnCrearDistribuido";
            btnCrearDistribuido.Size = new Size(120, 44);
            btnCrearDistribuido.TabIndex = 25;
            btnCrearDistribuido.Text = "CREAR DISTRIBUIDOR";
            btnCrearDistribuido.UseVisualStyleBackColor = false;
            btnCrearDistribuido.Click += btnCrearDistribuido_Click;
            // 
            // btnBuscarProducto
            // 
            btnBuscarProducto.BackColor = SystemColors.GradientInactiveCaption;
            btnBuscarProducto.Location = new Point(769, 813);
            btnBuscarProducto.Name = "btnBuscarProducto";
            btnBuscarProducto.Size = new Size(90, 44);
            btnBuscarProducto.TabIndex = 26;
            btnBuscarProducto.Text = "BUSCAR";
            btnBuscarProducto.UseVisualStyleBackColor = false;
            btnBuscarProducto.Click += btnBuscarProducto_Click;
            // 
            // lblCosto
            // 
            lblCosto.AutoSize = true;
            lblCosto.Location = new Point(362, 810);
            lblCosto.Name = "lblCosto";
            lblCosto.Size = new Size(38, 15);
            lblCosto.TabIndex = 27;
            lblCosto.Text = "label9";
            // 
            // lblGanancia
            // 
            lblGanancia.AutoSize = true;
            lblGanancia.Location = new Point(693, 834);
            lblGanancia.Name = "lblGanancia";
            lblGanancia.Size = new Size(44, 15);
            lblGanancia.TabIndex = 28;
            lblGanancia.Text = "label24";
            // 
            // lblUltimoPrecio
            // 
            lblUltimoPrecio.AutoSize = true;
            lblUltimoPrecio.Location = new Point(704, 810);
            lblUltimoPrecio.Name = "lblUltimoPrecio";
            lblUltimoPrecio.Size = new Size(44, 15);
            lblUltimoPrecio.TabIndex = 29;
            lblUltimoPrecio.Text = "label27";
            // 
            // lblPV
            // 
            lblPV.AutoSize = true;
            lblPV.Location = new Point(362, 834);
            lblPV.Name = "lblPV";
            lblPV.Size = new Size(44, 15);
            lblPV.TabIndex = 30;
            lblPV.Text = "label28";
            // 
            // btnFacturar
            // 
            btnFacturar.BackColor = SystemColors.GradientInactiveCaption;
            btnFacturar.Location = new Point(467, 813);
            btnFacturar.Name = "btnFacturar";
            btnFacturar.Size = new Size(90, 44);
            btnFacturar.TabIndex = 31;
            btnFacturar.Text = "FACTURAR";
            btnFacturar.UseVisualStyleBackColor = false;
            btnFacturar.Click += btnFacturar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = SystemColors.GradientInactiveCaption;
            btnLimpiar.Location = new Point(161, 813);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(90, 44);
            btnLimpiar.TabIndex = 32;
            btnLimpiar.Text = "LIMPIAR";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // FrmInvoiceCreation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(1047, 894);
            Controls.Add(btnCrearFacturar);
            Controls.Add(grpMontos);
            Controls.Add(btnLimpiar);
            Controls.Add(btnFacturar);
            Controls.Add(lblPV);
            Controls.Add(lblUltimoPrecio);
            Controls.Add(lblGanancia);
            Controls.Add(lblCosto);
            Controls.Add(btnBuscarProducto);
            Controls.Add(btnCrearDistribuido);
            Controls.Add(btnCrearProducto);
            Controls.Add(btnCrearCliente);
            Controls.Add(btnMovimientoProducto);
            Controls.Add(label26);
            Controls.Add(label25);
            Controls.Add(label23);
            Controls.Add(label22);
            Controls.Add(label21);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmInvoiceCreation";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmInvoiceCreation";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private TextBox textBox16;
        private TextBox txtRNC;
        private TextBox txtCodCliente;
        private TextBox txtCliente;
        private TextBox txtDireccion;
        private TextBox txtTelefono;
        private TextBox txtDescuento;
        private TextBox txtNoFactura;
        private TextBox txtNombreComercial;
        private Panel panel2;
        private Panel panel3;
        private Label label8;
        private Label label16;
        private Label label7;
        private Label label6;
        private Label label15;
        private Label label10;
        private Label label5;
        private Label label14;
        private Label label4;
        private Label label12;
        private Label label13;
        private Label label2;
        private Label label3;
        private Label label11;
        private Label label1;
        private ComboBox cmbCaja;
        private ComboBox cmbFormaPago;
        private ComboBox cmbEstado;
        private Label label20;
        private Label lblITBIS;
        private Label lblMonto;
        private Label label17;
        private DataGridView dgvProductos;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label25;
        private Label label26;
        private Button btnCrearFacturar;
        private Button btnMovimientoProducto;
        private Button btnCrearCliente;
        private Button btnCrearProducto;
        private Button btnVerHistorialVenta;
        private ComboBox cmbNCF;
        private ComboBox cmbITBIS;
        private Button btnCrearDistribuido;
        private Button button5;
        private Button btnVerificarCliente;
        private DateTimePicker dtpFecha;
        private DateTimePicker dtpFechaVencimiento;
        private Button btnBuscarProducto;
        private Label lblCosto;
        private Label lblGanancia;
        private Label lblUltimoPrecio;
        private Label lblPV;
        private Button btnFacturar;
        private Button btnLimpiar;
        private GroupBox grpMontos;
    }
}