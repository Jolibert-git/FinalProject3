using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Screen;


namespace Screen.Views
{
    public partial class FrmPrincipalScreen : Form
    {
        public FrmPrincipalScreen()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.IsMdiContainer = true;
        }

        private void ShowForm<T>() where T : Form, new()
        {
            T form = new T();

            if (this.IsMdiContainer)
            {
                // Intentar encontrar si la ventana ya está abierta
                foreach (Form child in this.MdiChildren)
                {
                    if (child.GetType() == typeof(T))
                    {
                        child.BringToFront();
                        return;
                    }
                }
                // Abrir como ventana hija MDI
                form.MdiParent = this;
                form.Show();
            }
            else
            {
                // Abrir como ventana normal (no MDI)
                form.Show();
            }
        }


        private void CrearFacturar_Click(object sender, EventArgs e)
        {
            // 1. Crear la instancia (sin 'using' si usas Show())
            FrmInvoiceCreation formFactura = new FrmInvoiceCreation();

            // 2. Establecer el 'Owner'. Esto asegura que el formulario aparezca encima del dueño (this).
            formFactura.Owner = this;

            // 3. Mostrar la ventana de forma NO MODAL.
            formFactura.Show();
        }

        /*
        private void CrearFacturar_Click(object sender, EventArgs e)
        {
            ShowDialog(< FrmInvoiceCreation>());
        }
        */


        private void btnEditarProducto_Click(object sender, EventArgs e)
        {
            ShowForm<FrmSelecCodeProduct>();
        }

        private void btnEditarCliente_Click(object sender, EventArgs e)
        {
            ShowForm<FrmSelectCodeCustomer>();
        }

        private void btnEditarDistribuidor_Click(object sender, EventArgs e)
        {
            ShowForm<FrmSelectCodeDistributor>();
        }

        private void btnHistorialVenta_Click(object sender, EventArgs e)
        {
            ShowForm<FrmDistributorEditor>();
        }

        private void btnMovimientoInventario_Click(object sender, EventArgs e)
        {
            ShowForm<FrmStockMovement>();
        }
    }
}
