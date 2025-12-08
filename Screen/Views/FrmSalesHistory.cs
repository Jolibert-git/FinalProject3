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

namespace Screen.Views
{


    public partial class FrmSalesHistory : Form
    {



        private readonly InvoiceBLL _invoiceBLL = new InvoiceBLL();

        // Propiedad para almacenar el ID de la factura seleccionada (si aplica)
        public int SelectedInvoiceId { get; private set; } = -1;

        public FrmSalesHistory()
        {
            InitializeComponent();
            InitializeFormDefaults();
            InitializeDataGridView();
            LoadInvoices();
        }

        private void InitializeFormDefaults()
        {
            // Inicializar el DateTimePicker con la fecha de hoy.
            // Para cumplir con "la fecha de mañana hacia atrás", se establece la fecha
            // con un día extra para que al hacer la búsqueda, incluya las de hoy.
            dtpFecha.Value = DateTime.Today.AddDays(1);
        }

        private void InitializeDataGridView()
        {
            dgvSalesHistory.AutoGenerateColumns = false;
            dgvSalesHistory.Columns.Clear();

            // Columna oculta para el ID (Primary Key)
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "CodeInvoice",
                HeaderText = "No. Factura",
                DataPropertyName = "CodeInvoice",
                Visible = true,
                ReadOnly = true
            });

            // Columna Nombre del Cliente
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "CustomerFullName",
                HeaderText = "Nombre del Cliente",
                DataPropertyName = "CustomerFullName", // Asumiendo que InvoiceHeader tiene esta propiedad
                Visible = true,
                ReadOnly = true
            });

            // Columna Fecha
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DateHeader",
                HeaderText = "Fecha",
                DataPropertyName = "DateHeader",
                Visible = true,
                ReadOnly = true,
                DefaultCellStyle = { Format = "yyyy-MM-dd" }
            });

            // Permitir selección de fila completa y solo selección simple
            dgvSalesHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSalesHistory.MultiSelect = false;
        }

        /// Carga las facturas realizadas hasta (e incluyendo) la fecha seleccionada.
        /// </summary>
        private void LoadInvoices()
        {
            try
            {
                // La búsqueda es de la fecha seleccionada hacia atrás.
                DateTime searchDate = dtpFecha.Value;

                // Llamar a la lógica de negocio para obtener la lista de encabezados de factura
                // Se asume la existencia de un método GetInvoicesBeforeOrOnDate(DateTime date) en InvoiceBLL.
                // Se ordena por CodeInvoice (Primary Key identity) de forma descendente (mayor a menor).
                List<InvoiceHeader> invoices = _invoiceBLL.GetInvoicesBeforeDate(searchDate)
                                                           .OrderByDescending(i => i.CodeInvoiceHeader)
                                                           .ToList();

                dgvSalesHistory.DataSource = invoices;

                if (invoices.Any())
                {
                    // Seleccionar la primera fila por defecto para una mejor UX
                    dgvSalesHistory.ClearSelection();
                    dgvSalesHistory.Rows[0].Selected = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial de facturas: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            // Recargar las facturas cada vez que cambia la fecha
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
