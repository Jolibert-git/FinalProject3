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
    public partial class FrmProductSearch : Form
    {
        private readonly ProductBLL _productBLL;
        //private readonly ProductBLL _productBLL = new ProductBLL();
        private List<Product> _allProducts; // Lista cache de todos los productos

        // Propiedad que almacenará el producto seleccionado para devolverlo a FrmInvoiceCreation
        public Product SelectedProduct { get; private set; }


        public FrmProductSearch(ProductBLL _productBLL)
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadAllProducts();
            this.Text = "Búsqueda de Productos";
            this.dgvProducts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellDoubleClick);
            this._productBLL = _productBLL;
        }

        private void InitializeDataGridView()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;

            // Limpiar columnas previas si existen
            dgvProducts.Columns.Clear();

            // Definición de las columnas solicitadas
            dgvProducts.Columns.Add("CodeProduct", "Código");
            dgvProducts.Columns["CodeProduct"].DataPropertyName = "CodeProduct";
            dgvProducts.Columns["CodeProduct"].ReadOnly = true;
            dgvProducts.Columns["CodeProduct"].Width = 80;

            dgvProducts.Columns.Add("NameProduct", "Nombre del Producto");
            dgvProducts.Columns["NameProduct"].DataPropertyName = "NameProduct";
            dgvProducts.Columns["NameProduct"].ReadOnly = true;
            dgvProducts.Columns["NameProduct"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvProducts.Columns.Add("StockProduct", "Existencia");
            dgvProducts.Columns["StockProduct"].DataPropertyName = "StockProduct"; // Asumimos esta propiedad en la clase Product
            dgvProducts.Columns["StockProduct"].ReadOnly = true;
            dgvProducts.Columns["StockProduct"].Width = 80; // Ancho fijo

            dgvProducts.Columns.Add("PriceProduct", "Precio");
            dgvProducts.Columns["PriceProduct"].DataPropertyName = "PriceProduct";
            dgvProducts.Columns["PriceProduct"].DefaultCellStyle.Format = "C2";
            dgvProducts.Columns["PriceProduct"].ReadOnly = true;
            dgvProducts.Columns["PriceProduct"].Width = 100;

            // Ocultar otras propiedades de Product que no se deben mostrar
            // Nota: Se debe asegurar que las propiedades del modelo Product existan.
            // Ejemplo: dgvProducts.Columns.Add(new DataGridViewTextBoxColumn() { Name = "UnitOfMeasure", Visible = false }); 
        }

        private void LoadAllProducts()
        {
            try
            {
                // Obtener todos los productos y almacenarlos en la lista local.
                // Ordenar alfabéticamente por nombre.
                _allProducts = _productBLL.GetAllProducts().OrderBy(p => p.NameProduct).ToList();

                // Mostrar inicialmente todos los productos
                FilterProducts(string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _allProducts = new List<Product>(); // Inicializar vacía en caso de error
            }
        }

        private void FilterProducts(string searchText)
        {
            if (_allProducts == null) return;

            // Convertir a minúsculas y buscar coincidencias que *empiecen* con el texto
            var filteredList = _allProducts
                .Where(p => p.NameProduct.ToLower().StartsWith(searchText.ToLower()))
                .ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = filteredList;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Filtrar la lista de productos cada vez que el texto cambie
            FilterProducts(txtSearch.Text);
        }

        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Seleccionar el producto y cerrar el formulario
            if (e.RowIndex >= 0)
            {
                SelectAndClose();
            }
            else
            {
                MessageBox.Show("Error: FrmProductSearch linea 111", "FrmProductSearch linea 111", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvProducts_KeyDown(object sender, KeyEventArgs e)
        {
            // Permitir la selección con Enter
            if (e.KeyCode == Keys.Enter)
            {
                SelectAndClose();
                e.Handled = true; // Prevenir el sonido de "ding"
            }
        }


        
        private void SelectAndClose()
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                // 1. Obtener la fila seleccionada
                DataGridViewRow selectedRow = dgvProducts.SelectedRows[0];

                // 2. Intentar obtener el objeto Product enlazado a la fila
                SelectedProduct = selectedRow.DataBoundItem as Product;

                if (SelectedProduct != null)
                {
                    // 3. Establecer el resultado y cerrar
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Opcional: Mostrar un mensaje si el enlace falla (ayuda a depurar)
                    MessageBox.Show("Error: No se pudo enlazar el objeto Product desde la fila seleccionada.", "Error de Enlace", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
