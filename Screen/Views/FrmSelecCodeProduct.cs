using Business.Negocio;
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
    public partial class FrmSelecCodeProduct : Form
    {
        public bool result;// VARIEBLE FOR SAVE VALUE THE EXIST O NOT EXIST ONE PRODUCT

        public readonly ProductBLL ProductLogic;//

        public readonly FrmProductEditor formEditor;

        //ProductBLL  ProducLogic = new ProductBLL();
        public FrmSelecCodeProduct(ProductBLL ProductLogic, FrmProductEditor formEditor)
        {
            InitializeComponent();
            this.ProductLogic = ProductLogic;
            this.formEditor = formEditor;
        }

        //METHOD USED IN FORM FrmProductEdit FOR KNOW IF PRODUCT EXIST
        public bool GetResult() => result;


        //METHOD USED IN FORM FrmProductEdit FOR AUNTOFILL CODE AND SEARCH PRODUCT IN CASE EXIST
        public string GetCode() => textCode.Text;


        //IT'S USED FOR VALIDATE IF PRODUCT EXIST AND OPEN FORM FrmProductEdit
        private void button1_Click(object sender, EventArgs e)
        {
            string code = textCode.Text;
            if (!string.IsNullOrEmpty(code))
            {
                result = ProductLogic.ValidateExistencesProduct(code);

                //FrmProductEditor formEditor = new FrmProductEditor();

                // 2. Establecer el 'Owner'. Esto asegura que el formulario aparezca encima del dueño (this).
                formEditor.Owner = this;

                // 3. Mostrar la ventana de forma NO MODAL.
                formEditor.Show();

                MessageBox.Show($"El producto {result}");
            }

        }
    }
}
