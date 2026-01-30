using Business.Negocio;
using Model.Entities;
using System;
using System.Collections;
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


    public partial class FrmSalesHistory : Form
    {
        private readonly InvoiceBLL _invoiceBLL ;

        // Propiedad para almacenar el ID de la factura seleccionada (si aplica)
        public int SelectedInvoiceId { get; private set; } = -1;

        public FrmSalesHistory(InvoiceBLL _invoiceBLL)
        {
            this._invoiceBLL = _invoiceBLL;
            InitializeComponent();
            InitializeFormDefaults();
            InitializeDataGridView();
            LoadInvoices();
        }

        private void InitializeFormDefaults()
        {

            // start  dtpFecha with  date of today
            // add one day for include the invoice today
            dtpFecha.Value = DateTime.Today.AddDays(1);
        }

        private void InitializeDataGridView()
        {
            dgvSalesHistory.AutoGenerateColumns = false;       // It is false becouse who generel is propiry of 
            dgvSalesHistory.Columns.Clear();

            // Columna oculta para el ID (Primary Key)
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Numero de Factura",
                HeaderText = "No. Factura",               // it is title about Colunm
                DataPropertyName = "CodeInvoiceHeader",   //it is propiry of the object invoices stay in the column 
                Visible = true,
                ReadOnly = true
            });

            // Columna Fecha
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DateHeader",
                HeaderText = "Fecha",               // it is title about Colunm
                DataPropertyName = "DateHeader",   //it is propiry of the object invoices stay in the column 
                Visible = true,
                ReadOnly = true,
                DefaultCellStyle = { Format = "yyyy-MM-dd" }
            });

            //merthod Payment
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MathodPayment",
                HeaderText = "Metodo",                    // it is title about Colunm
                DataPropertyName = "PaymentMethodCode",  //it is propiry of the object invoices stay in the column 
                Visible = true,
                ReadOnly = true,
            });


            //subAmount of invoice
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SubAmount",
                HeaderText = "Sub Total",                 // it is title about Colunm
                DataPropertyName = "SubtotalHeader",      //it is propiry of the object invoices stay in the column 
                Visible = true,
                ReadOnly = true
            });



            //Amount of Invoice 

            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TotalInvoice",
                HeaderText = "Total",                   // it is title about Colunm
                DataPropertyName = "TotalAmountHeader", //it is propiry of the object invoices stay in the column 
                Visible = true,
                ReadOnly = true,

            });


            // Columna Nombre del Cliente
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "CustomerFullName",
                HeaderText = "Codigo del Cliente",  // it is title about Colunm
                DataPropertyName = "CodeCustomer", //it is propiry of the object invoices stay in the column 
                Visible = true,
                ReadOnly = true
            });

            
            // Permitir selección de fila completa y solo selección simple
            dgvSalesHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSalesHistory.MultiSelect = false;
        }

        /// load invoice ready until date selected.
        /// </summary>
        private void LoadInvoices()
        {
            try
            {
                
                DateTime searchDate = dtpFecha.Value;

                
                List<InvoiceHeader> invoices = _invoiceBLL.GetInvoicesBeforeDate(searchDate)
                                                           .OrderByDescending(i => i.CodeInvoiceHeader)   //used LINQ for consult and agrout in descendent for CodeInvoice...
                                                           .ToList();                                     // it is more easy to use CodeInvoice... that DateInvoice... and it is the same result
                
                dgvSalesHistory.DataSource = invoices;

                if (invoices.Any())
                {
                    // Seleccionar la primera fila por defecto
                    dgvSalesHistory.ClearSelection();
                    dgvSalesHistory.Rows[0].Selected = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial de facturas: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //
        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            // Load dataGriViev each chage date 
            LoadInvoices();
        }

        private void dgvSalesHistory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                OpenInvoiceForEditing();
            }
        }

        private void btnSelectInvoice_Click(object sender, EventArgs e)
        {
            OpenInvoiceForEditing();
        }

        /// Obtiene el ID de la factura seleccionada y abre FrmInvoiceCreation para edición.
        /// </summary>
        private void OpenInvoiceForEditing()
        {
            if (dgvSalesHistory.SelectedRows.Count == 1)
            {
                try
                {
                    // Obtener la fila seleccionada
                    DataGridViewRow selectedRow = dgvSalesHistory.SelectedRows[0];

                    // Obtener el ID de la factura (CodeInvoice)
                    if (selectedRow.Cells["CodeInvoice"].Value is int invoiceId)
                    {
                        // Abrir el formulario FrmInvoiceCreation para cargar y modificar
                        // Creamos una nueva instancia de FrmInvoiceCreation pasándole el ID
                        using (FrmInvoiceCreation invoiceCreationForm = new FrmInvoiceCreation(invoiceId))
                        {
                            this.Hide(); // Ocultamos el formulario actual
                            invoiceCreationForm.ShowDialog();
                            this.Show(); // Volvemos a mostrar al cerrar FrmInvoiceCreation

                            // Re-cargar la lista por si la factura fue modificada o anulada
                            LoadInvoices();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar la factura para edición: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una factura para ver o modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Asumiendo que tienes un botón de búsqueda o que la búsqueda se hace solo al cambiar la fecha.
        // Si tienes un botón de búsqueda por texto (como en la imagen):
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Implementar lógica de filtrado adicional por cliente o número de factura
            // utilizando el texto en la caja de búsqueda 'SEARCH'
            // (Esta implementación se omite para enfocarse en la funcionalidad principal solicitada).
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dgvSalesHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
