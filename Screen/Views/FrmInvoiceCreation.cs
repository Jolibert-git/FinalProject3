using Business.Negocio;
using DataAccess.Data;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screen.Views
{

    public partial class FrmInvoiceCreation : Form
    {

        private readonly CustomerBLL _customerBLL ;
        private readonly ProductBLL _productBLL ;
        private readonly InvoiceBLL _invoiceBLL;
        private readonly PaymentBLL _paymentBLL ;
        private readonly FrmSelecCodeProduct _selecCodeProduct;
        private readonly FrmProductSearch _productSearch;
        private readonly FrmSalesHistory _salesHistory;
        /*
        private readonly CustomerBLL _customerBLL = new CustomerBLL();
        private readonly ProductBLL _productBLL = new ProductBLL();
        private readonly InvoiceBLL _invoiceBLL = new InvoiceBLL();
        private readonly PaymentBLL _paymentBLL = new PaymentBLL();
        */


        private readonly int? _invoiceIdToLoad;

        private BindingList<InvoiceDetail> currentInvoiceDetails = new BindingList<InvoiceDetail>();

        // Propiedad para almacenar el producto seleccionado desde FrmProductSearch
        public Product SelectedProduct { get; set; }

        public FrmInvoiceCreation(CustomerBLL _customerBLL, ProductBLL _productBLL, InvoiceBLL _invoiceBLL, PaymentBLL _paymentBLL, FrmSelecCodeProduct _selecCodeProduct, FrmProductSearch _productSearch, FrmSalesHistory _salesHistory)
        {
            InitializeComponent();
            InitializeNewInvoiceNumber();
            InitializeFormDefaults();
            InitializeDataGridView();
            LoadComboBoxes();
            this.dgvProductos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellEndEdit);
            _invoiceIdToLoad = null;
            this.dgvProductos.SelectionChanged += new System.EventHandler(this.dgvProductos_SelectionChanged);
            this.dtpFechaVencimiento.ValueChanged += new System.EventHandler(this.dtpFechaVencimiento_ValueChanged);

            this._customerBLL = _customerBLL;
            this._productBLL = _productBLL;
            this._invoiceBLL = _invoiceBLL;
            this._paymentBLL = _paymentBLL;
            this._selecCodeProduct = _selecCodeProduct;
            this._productSearch = _productSearch;
            this._salesHistory = _salesHistory;
        }
        
        public FrmInvoiceCreation(int invoiceId) //: this() 
        {
            _invoiceIdToLoad = invoiceId; 
                                         
            LoadInvoiceData(invoiceId); 
        }



        private void LoadInvoiceData(int invoiceId)
        {
            
            try
            {
                Invoice invoice = _invoiceBLL.GetInvoiceById(invoiceId);

                if (invoice == null)
                {
                    MessageBox.Show($"La factura con ID {invoiceId} no fue encontrada.", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close(); 
                    return;
                }

               
                LoadHeaderControls(invoice.Header);

                
                LoadDetailsGrid(invoice.Details);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos de la factura: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void InitializeNewInvoiceNumber()
        {
            // Solo para que el campo no esté vacío. El ID real se obtiene al facturar.
            txtNoFactura.Text = "NUEVA";
            txtNoFactura.ReadOnly = true;

            // Si quieres usar la lógica del ID siguiente:
            // int nextId = _invoiceBLL.GetNextInvoiceId(); // Asumiendo que implementaste este método en BLL
            // txtNoFactura.Text = nextId.ToString(); 
        }




        private void LoadHeaderControls(InvoiceHeader header)
        {
            txtNoFactura.Text = header.CodeInvoiceHeader.ToString();
            txtCodCliente.Text = header.CodeCustomer;
            dtpFecha.Value = header.DateHeader;
            dtpFechaVencimiento.Value = header.DueDateHeader ?? DateTime.Now.AddDays(30); 
            cmbEstado.SelectedItem = header.StatusCode;
            cmbNCF.SelectedItem = header.NCF; 
            cmbFormaPago.SelectedValue = header.PaymentMethodCode; 
            lblITBIS.Text = header.TotalTaxHeader.ToString("N2");
            lblMonto.Text = header.TotalAmountHeader.ToString("N2");
        }

        private void LoadDetailsGrid(List<InvoiceDetail> details)
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = details.ToList();
            dgvProductos.Refresh();
        }

     

        private void InitializeFormDefaults()
        {
            
            dtpFecha.Value = DateTime.Today;
            dtpFechaVencimiento.Value = DateTime.Today;      
            cmbEstado.SelectedItem = "PAGADO"; 
            cmbCaja.SelectedItem = "G"; 
            cmbITBIS.SelectedItem = "G";

            
        }
      

        private void InitializeDataGridView()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();
            /*
            var indexColumn = new DataGridViewTextBoxColumn
            {
                Name = "LineNumber",
                HeaderText = "No.",
                ReadOnly = true,
                Width = 35, // Ajusta el ancho para el número
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            dgvProductos.Columns.Add(indexColumn);
            */

            dgvProductos.Columns.Add("CodeProduct", "Código");
            dgvProductos.Columns["CodeProduct"].DataPropertyName = "CodeProduct";
            dgvProductos.Columns["CodeProduct"].ReadOnly = false;
            dgvProductos.Columns["CodeProduct"].Width = 80;

            dgvProductos.Columns.Add("NameProduct", "Producto");
            dgvProductos.Columns["NameProduct"].DataPropertyName = "NameProduct";
            dgvProductos.Columns["NameProduct"].ReadOnly = true;
            dgvProductos.Columns["NameProduct"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvProductos.Columns.Add("UnitOfMeasure", "Unidad");
            dgvProductos.Columns["UnitOfMeasure"].DataPropertyName = "UnitOfMeasure";
            dgvProductos.Columns["UnitOfMeasure"].ReadOnly = true;
            dgvProductos.Columns["UnitOfMeasure"].Width = 60;

            dgvProductos.Columns.Add("Quantity", "Cantidad");
            dgvProductos.Columns["Quantity"].DataPropertyName = "Quantity";
            dgvProductos.Columns["Quantity"].ValueType = typeof(decimal); 
            dgvProductos.Columns["Quantity"].DefaultCellStyle.Format = "N0";
            dgvProductos.Columns["Quantity"].ReadOnly = false; // Editable
            dgvProductos.Columns["Quantity"].Width = 70;

            dgvProductos.Columns.Add("Price", "Precio Unit.");
            dgvProductos.Columns["Price"].DataPropertyName = "Price";
            dgvProductos.Columns["Price"].ValueType = typeof(decimal);
            dgvProductos.Columns["Price"].DefaultCellStyle.Format = "C2";
            dgvProductos.Columns["Price"].ReadOnly = true;
            dgvProductos.Columns["Quantity"].Width = 70;

            /*
            dgvProductos.Columns.Add("TotalLine", "Valor Total");
            dgvProductos.Columns["TotalLine"].DataPropertyName = "TotalLine";
            dgvProductos.Columns["TotalLine"].ValueType = typeof(decimal);
            dgvProductos.Columns["TotalLine"].DefaultCellStyle.Format = "C2";
            dgvProductos.Columns["TotalLine"].ReadOnly = true;
            */
            DataGridViewTextBoxColumn totalColumn = new DataGridViewTextBoxColumn();
            totalColumn.Name = "TotalLine";
            totalColumn.HeaderText = "Valor Total";
            totalColumn.DataPropertyName = "TotalLine";
            totalColumn.ValueType = typeof(decimal);
            totalColumn.DefaultCellStyle.Format = "C2";
            totalColumn.ReadOnly = true;
            totalColumn.Width = 100;

            dgvProductos.Columns.Add(totalColumn);

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CostProduct", DataPropertyName = "CostProduct", Visible = false });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TaxRate", DataPropertyName = "TaxRate", Visible = false });

            dgvProductos.DataSource = currentInvoiceDetails;
        }





        private void LoadComboBoxes()
        {
            cmbNCF.Items.AddRange(new string[] { "B01", "B02", "B03", "B04", "B11", "B12", "B13", "B14", "B15", "B16", "B17" });
            cmbNCF.SelectedIndex = 0; 
            cmbEstado.Items.AddRange(new string[] { "PAGADO","PENDIENTE","PARCIAL", "ANULADO" });
            cmbEstado.SelectedIndex = 0; // Por defecto
            cmbITBIS.Items.AddRange(new string[] { "18%", "16%", "0%", "G" });
            cmbITBIS.SelectedItem = "G";

            // Cargar Caja
            cmbCaja.Items.AddRange(new string[] { "1", "2", "3", "4", "5", "G" });
            cmbCaja.SelectedItem = "G";

            
            try
            {
                var paymentMethods = _paymentBLL.GetPaymentMethods(); 
                cmbFormaPago.DataSource = paymentMethods;
                cmbFormaPago.DisplayMember = "NameMethod"; 
                cmbFormaPago.ValueMember = "CodeMethod";  
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar métodos de pago: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



       


        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            
            
            using (FrmProductSearch productSearchForm = _productSearch)
            {
                if (productSearchForm.ShowDialog() == DialogResult.OK)
                {
                    Product selectedProduct = productSearchForm.SelectedProduct; 
                    if (selectedProduct != null)
                    {
                        AddProductToInvoiceDetails(selectedProduct); 
                    }
                    else
                    {
                        MessageBox.Show("Error: lanzado desde FrmInvoiceCreation linea 336.", "FrmInvoiceCreation linea 336", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            
        }











        private void AddProductToInvoiceDetails(Product product)
        {
             InvoiceDetail existingDetail = currentInvoiceDetails.FirstOrDefault(d => d.CodeProduct == product.CodeProduct);

             if (existingDetail != null)
             {
                 existingDetail.Quantity++; 
             }
             else
             {
                 // Crear un nuevo detalle de factura
                 InvoiceDetail newDetail = new InvoiceDetail
                 {
                     CodeProduct = product.CodeProduct,
                     NameProduct = product.NameProduct,
                     UnitOfMeasure = product.UnitOfMeasure, 
                     Quantity = 1, 
                     Price = product.PriceProduct,
                     TaxRate = product.TaxProduct, 
                     CostProduct = product.CostProduct, 
                     LastPrice = product.LastPriceProduct ?? 0.00m,
                 };
                 currentInvoiceDetails.Add(newDetail);
             }

             RefreshInvoiceDetails();
            
        }

        private void RefreshInvoiceDetails()
        {
            foreach (var detail in currentInvoiceDetails)
            {
                detail.TotalLine = detail.Quantity * detail.Price;
            }
            
            dgvProductos.DataSource = null; 
            dgvProductos.DataSource = currentInvoiceDetails;

            CalculateOverallTotals();
        }

        private void CalculateOverallTotals()
        {
            decimal subtotal = currentInvoiceDetails.Sum(d => d.TotalLine);
            decimal totalITBIS = currentInvoiceDetails.Sum(d => (d.TotalLine * d.TaxRate)); // ITBIS de todos los productos

            decimal discountPercentage = 0;
            if (decimal.TryParse(txtDescuento.Text, out discountPercentage))
            {
                if (discountPercentage < 0 || discountPercentage > 100)
                {
                    MessageBox.Show("El porcentaje de descuento debe estar entre 0 y 100.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDescuento.Text = "0";
                    discountPercentage = 0;
                }
                subtotal -= (subtotal * (discountPercentage / 100));
                totalITBIS -= (totalITBIS * (discountPercentage / 100)); // El descuento también afecta el ITBIS
            }


            decimal totalAmount = subtotal;
            subtotal = totalAmount - totalITBIS;

            lblMonto.Text = totalAmount.ToString("C2"); // Formato de moneda
            lblITBIS.Text = totalITBIS.ToString("C2");

            UpdateOverallITBISCmb(currentInvoiceDetails);
        }

        private void UpdateOverallITBISCmb(BindingList<InvoiceDetail> details)
        {
            if (details == null || !details.Any())
            {
                cmbITBIS.SelectedItem = "G";
                return;
            }

            // Obtener todas las tasas de impuesto únicas de los productos
            var distinctTaxRates = details.Select(d => d.TaxRate).Distinct().ToList();

            if (distinctTaxRates.Count == 1)
            {
                // Si todos los productos tienen la misma tasa de impuesto
                decimal singleRate = distinctTaxRates.First();
                if (singleRate == 0.18m) cmbITBIS.SelectedItem = "18%";
                else if (singleRate == 0.16m) cmbITBIS.SelectedItem = "16%";
                else if (singleRate == 0.0m) cmbITBIS.SelectedItem = "0%";
                else cmbITBIS.SelectedItem = "G"; // En caso de una tasa no estándar
            }
            else
            {
                // Si hay diferentes tasas de impuesto
                cmbITBIS.SelectedItem = "G";
            }
        }

        private void dgvProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProductos.Columns["Quantity"].Index)
            {
                // Cuando el usuario termina de editar la cantidad
                if (e.RowIndex >= 0 && e.RowIndex < currentInvoiceDetails.Count)
                {
                    InvoiceDetail detail = currentInvoiceDetails[e.RowIndex];
                    if (dgvProductos.Rows[e.RowIndex].Cells["Quantity"].Value != null &&
                        int.TryParse(dgvProductos.Rows[e.RowIndex].Cells["Quantity"].Value.ToString(), out int newQuantity))
                    {
                        if (newQuantity <= 0)
                        {
                            // Si la cantidad es cero o negativa, eliminar el producto
                            currentInvoiceDetails.RemoveAt(e.RowIndex);
                            MessageBox.Show("Cantidad inválida. El producto ha sido eliminado de la factura.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            detail.Quantity = newQuantity;
                        }
                        RefreshInvoiceDetails();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese una cantidad válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Revertir el valor si es inválido o eliminar el producto
                        dgvProductos.Rows[e.RowIndex].Cells["Quantity"].Value = detail.Quantity;
                    }
                }
            }
        }



        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {


            if (dgvProductos.CurrentRow == null)
            {
                // Limpiar los labels de detalle si no hay fila seleccionada.
                lblCosto.Text = "0.00";
                lblPV.Text = "0.00";
                lblGanancia.Text = "0.00";
                lblUltimoPrecio.Text = "0.00";
                return;
            }


            int rowIndex = dgvProductos.CurrentRow.Index;

            // Aseguramos que el índice sea válido y no la fila de "nueva entrada"
            if (rowIndex >= 0 && rowIndex < currentInvoiceDetails.Count)
            {
                InvoiceDetail detail = currentInvoiceDetails[rowIndex];

                decimal costo = detail.CostProduct ;

                // Asumiendo que detail.Price ya es decimal (no nulo) 
                decimal precioVenta = detail.Price ;
                // Usamos LastPrice (que ya no es nulo gracias a AddProductToInvoiceDetails)
                decimal ultimoPrecio = (detail.LastPrice <= 0) ? precioVenta : detail.LastPrice;

                // 2. Calcular la Ganancia (la operación de resta ahora es segura)
                decimal ganancia = precioVenta - costo;

                // 3. Actualizar los Labels con los valores seguros
                lblCosto.Text = $"{costo:C2}";
                lblPV.Text = $"{precioVenta:C2}";
                lblGanancia.Text = $"{ganancia:C2}";
                lblUltimoPrecio.Text = $"{ultimoPrecio:C2}";
            }
            else
            {
                lblCosto.Text = "0.00";
                lblPV.Text = "0.00";
                lblGanancia.Text = "0.00";
                lblUltimoPrecio.Text = "0.00";
            }
        }






        private void btnVerificarCliente_Click(object sender, EventArgs e)
        {
            string customerCode = txtCodCliente.Text.Trim();
            if (!string.IsNullOrEmpty(customerCode))
            {
                try
                {
                    Customer customer = _customerBLL.GetCustomerByCode(customerCode);
                    if (customer != null)
                    {
                        txtCliente.Text = customer.FullNameCustomer;
                        txtDireccion.Text = customer.LocationCustomer;
                        txtTelefono.Text = customer.PhoneCustomer;
                        txtRNC.Text = customer.RncCustomer; // Rellenar RNC si el cliente tiene uno
                    }
                    else
                    {
                        MessageBox.Show("Cliente no encontrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearCustomerFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un código de cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearCustomerFields()
        {
            txtCliente.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtRNC.Clear();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            int NoFacturar = Convert.ToInt32(txtNoFactura.Text);
            txtNoFactura.Text=Convert.ToString(NoFacturar+1);
            cmbNCF.SelectedIndex = 0;
            txtRNC.Clear();
            txtCodCliente.Clear();
            ClearCustomerFields();
            dtpFecha.Value = DateTime.Today;
            cmbEstado.SelectedItem = "PAGADO";
            dtpFechaVencimiento.Value = DateTime.Today;
            cmbFormaPago.SelectedIndex = 0; // Deseleccionar
            txtDescuento.Text = "0";
            lblMonto.Text = "0.00";
            lblITBIS.Text = "0.00";
            cmbITBIS.SelectedItem = "G";
            cmbCaja.SelectedItem = "G";

            currentInvoiceDetails.Clear();
            dgvProductos.DataSource = null; 
            dgvProductos.DataSource = currentInvoiceDetails;

            lblCosto.Text = "0.00";
            lblPV.Text = "0.00";
            lblGanancia.Text = "0.00";
            lblUltimoPrecio.Text = "0.00";
        }


        
        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbFormaPago.SelectedValue == null)
                {
                    throw new ArgumentException("Debe seleccionar una forma de pago.");
                }

                int paymentMethodCode = (int)cmbFormaPago.SelectedValue;

                // 1. Crear el objeto InvoiceHeader
                InvoiceHeader header = new InvoiceHeader
                {
                    DateHeader = dtpFecha.Value,
                    CodeCustomer = txtCodCliente.Text.Trim(),
                    StatusCode = cmbEstado.SelectedItem?.ToString(),
                    PaymentMethodCode = paymentMethodCode, 
                    DiscountRate = decimal.TryParse(txtDescuento.Text, out decimal discount) ? discount / 100 : 0,
                    
                    NCF = cmbNCF.SelectedItem?.ToString(),
                    RNC = txtRNC.Text.Trim(),
                    CashRegisterNumber = cmbCaja.SelectedItem?.ToString(),
                    // Si hay fecha de vencimiento, asegurarse de que el estado sea PENDIENTE
                    DueDateHeader = dtpFechaVencimiento.Value == dtpFecha.Value.AddMonths(1) ? (DateTime?)null : dtpFechaVencimiento.Value
                };

                

                

                if (header.DueDateHeader.HasValue && header.DueDateHeader.Value.Date > DateTime.Today.Date)
                {
                    header.StatusCode = "PENDIENTE";
                }


                // 2. Crear el objeto Invoice (que contiene Header y Details)
                Invoice newInvoice = new Invoice
                {
                    Header = header,
                    Details = currentInvoiceDetails.ToList()
                };

   



                // 3. Llamar a la lógica de negocio para crear la factura
                int invoiceId = _invoiceBLL.CreateInvoice(newInvoice);
                txtNoFactura.Text = invoiceId.ToString();

                MessageBox.Show($"Factura No. {invoiceId} creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Opcional: Abrir FrmInvoiceView para ver la factura recién creada
                using (FrmInvoiceView invoiceViewForm = new FrmInvoiceView(invoiceId))
                {
                    invoiceViewForm.ShowDialog();
                }

                ClearForm(); // Limpiar el formulario después de facturar
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Error de validación: {ex.Message}", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error de operación: {ex.Message}", "Error de Negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"Error de datos: {ex.Message}", "Error de Negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al facturar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        









        private void btnVerHistorialVenta_Click(object sender, EventArgs e)
        {
            using (FrmSalesHistory salesHistoryForm = _salesHistory)
            {
                salesHistoryForm.ShowDialog();
            }
        }

        
        private void btnCrearProducto_Click(object sender, EventArgs e)
        {
            using (FrmSelecCodeProduct selectCodeProduct = _selecCodeProduct)
            {
                selectCodeProduct.ShowDialog();
            }
        }
        

        private void btnCrearCliente_Click(object sender, EventArgs e)
        {
            using (FrmSelectCodeCustomer selectCodeCustomer = new FrmSelectCodeCustomer())
            {
                selectCodeCustomer.ShowDialog();
            }
        }

        private void btnCrearDistribuido_Click(object sender, EventArgs e)
        {
            using (FrmDistributorEditor distributorEditorForm = new FrmDistributorEditor())
            {
                distributorEditorForm.ShowDialog();
            }
        }

        private void btnMovimientoProducto_Click(object sender, EventArgs e)
        {
            using (FrmStockMovement stockMovementForm = new FrmStockMovement())
            {
                stockMovementForm.ShowDialog();
            }
        }

        private void txtDescuento_Leave(object sender, EventArgs e)
        {
            CalculateOverallTotals(); // Recalcular totales al salir del campo de descuento
        }

        private void dtpFechaVencimiento_ValueChanged(object sender, EventArgs e)
        {
            // Si se selecciona una fecha de vencimiento futura, el estado debe ser PENDIENTE
            if (dtpFechaVencimiento.Value.Date > DateTime.Today.Date)
            {
                cmbEstado.SelectedItem = "PENDIENTE";
            }
        }

        private void cmbNCF_SelectedIndexChanged(object sender, EventArgs e)
        {
            //esta inplementada, pero debo mejorarla poniendola aqui 
        }


    }
}

