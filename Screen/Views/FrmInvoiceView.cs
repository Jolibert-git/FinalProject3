using Business.Negocio;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Screen.Views
{

    

    public partial class FrmInvoiceView : Form
    {
        // Declara una variable privada para guardar el ID de la factura a mostrar
        private  int _invoiceId;
        private readonly InvoiceBLL _invoiceBLL ;
        //private readonly InvoiceBLL _invoiceBLL = new InvoiceBLL();

        /*
        public FrmInvoiceView(InvoiceBLL _invoiceBLL)
        {
            InitializeComponent();
            this._invoiceBLL = _invoiceBLL;
        }
        */

        public FrmInvoiceView(int invoiceId, InvoiceBLL _invoiceBLL)
        {
            InitializeComponent();
            this._invoiceBLL = _invoiceBLL;
            this._invoiceId = invoiceId;// Llamar a la lógica de carga
            InitializeDetailsDataGridView();
            LoadInvoiceViewData(invoiceId);
        }
      

        private void LoadInvoiceViewData(int invoiceId)
        {
            try
            {
                // Llama al BLL para obtener la factura completa (Header + Details)
                var invoice = _invoiceBLL.GetInvoiceById(invoiceId);

                if (invoice != null)
                {
                    // ... (Toda tu lógica de carga de Labels)
                    label14.Text = invoice.Header.CodeInvoiceHeader.ToString();
                    label15.Text = invoice.Header.DateHeader.ToShortDateString();
                    label17.Text = invoice.Header.NCF;
                    label19.Text = "Plaza Lama"; // NOMBRE COMERCIAL (Fijo)
                    label18.Text = invoice.Header.CodeCustomer;
                    label16.Text = invoice.Header.RNC;
                    //label12.Text = invoice.Header.TotalTaxHeader.ToString("N2"); // ITBIS
                    //label13.Text = (invoice.Header.TotalAmountHeader - invoice.Header.TotalTaxHeader).ToString("N2"); // SUBTOTAL (Mejor calculado para evitar error)
                    //label6.Text = invoice.Header.TotalAmountHeader.ToString("N2"); // TOTAL



                    decimal totalTax = invoice.Header.TotalTaxHeader;
                    decimal totalAmount = invoice.Header.TotalAmountHeader;
                    decimal porcentual=0;

                    if (totalAmount == 0m && invoice.Details != null && invoice.Details.Any())
                    {
                        // Recalcula el Total General (Suma de los TotalLine de cada detalle)
                        totalAmount = invoice.Details.Sum(d => d.TotalLine);
                        porcentual = invoice.Details.Sum(d => d.TaxRate);
                        totalTax = (totalAmount * porcentual);

                        // ADVERTENCIA: Si totalTax sigue siendo 0 y totalAmount se recalculó, 
                        // significa que el ITBIS no se mapeó. Lo dejamos en 0.00 para la vista.
                    }


                    // Calcula el Subtotal (Total General - Impuestos)
                    decimal subtotalCalculated = totalAmount - totalTax;
                    label12.Text = totalTax.ToString("N2");       // ITBIS
                    label13.Text = subtotalCalculated.ToString("N2"); // SUBTOTAL (Calculado)
                    label6.Text = totalAmount.ToString("N2");




                    // 🟢 Cargar detalles en el DataGridView 🟢
                    // Asegúrate de que este control tenga el nombre dgvDetalles o el que uses en tu diseñador
                    if (invoice.Details != null && invoice.Details.Any())
                    {
                        dgvDetalles.DataSource = invoice.Details;
                    }
                    else
                    {
                        // Limpiar la grilla si no hay detalles
                        dgvDetalles.DataSource = null;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la vista de la factura: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        private void InitializeDetailsDataGridView()
        {
            // Asegúrate de que el DataGridView en tu formulario se llame 'dgvDetalles' o 'dgvProductos'
            // Asumiremos que se llama 'dgvDetalles' para este ejemplo.

            dgvDetalles.AutoGenerateColumns = false;
            dgvDetalles.Columns.Clear();

            // 1. Columna Cantidad
            dgvDetalles.Columns.Add("Quantity", "Cantidad");
            dgvDetalles.Columns["Quantity"].DataPropertyName = "Quantity";
            dgvDetalles.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalles.Columns["Quantity"].ReadOnly = true;
            dgvDetalles.Columns["Quantity"].Width = 50; // Ajuste de ancho

            // 2. Columna Nombre del Producto
            dgvDetalles.Columns.Add("NameProduct", "Producto");
            dgvDetalles.Columns["NameProduct"].DataPropertyName = "NameProduct";
            dgvDetalles.Columns["NameProduct"].ReadOnly = true;
            //dgvDetalles.Columns["NameProduct"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Que ocupe el espacio restante
            dgvDetalles.Columns["NameProduct"].Width = 130;

            // 3. Columna Precio Unitario
            dgvDetalles.Columns.Add("Price", "Precio Unitario");
            dgvDetalles.Columns["Price"].DataPropertyName = "Price";
            dgvDetalles.Columns["Price"].DefaultCellStyle.Format = "C2"; // Formato de moneda
            dgvDetalles.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalles.Columns["Price"].ReadOnly = true;
            dgvDetalles.Columns["Price"].Width = 60;

            // Opcional: Agregar el total de línea si lo deseas
            dgvDetalles.Columns.Add("TotalLine", "Valor Total");
            dgvDetalles.Columns["TotalLine"].DataPropertyName = "TotalLine";
            dgvDetalles.Columns["TotalLine"].DefaultCellStyle.Format = "C2";
            dgvDetalles.Columns["TotalLine"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalles.Columns["TotalLine"].ReadOnly = true;
            dgvDetalles.Columns["TotalLine"].Width = 70;
        }


        private void label19_Click(object sender, EventArgs e)
        {

        }


    }
}
