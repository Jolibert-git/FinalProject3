using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Negocio;
using Microsoft.Identity.Client;
using Model.Entities;


namespace Screen.Views
{


    public partial class FrmProductEditor : Form
    {
        private readonly ProductBLL _productBLL;
        private Product _product = new Product();
        public FrmProductEditor(ProductBLL _productBLL)
        {
            InitializeComponent();
            this.Load += FrmProductEditor_Load;
            this._productBLL = _productBLL;
        }

        private void FrmProductEditor_Load(object sender, EventArgs e)
        {
            SelectEditOrNew();
        }

        //Product _product = new Product();
        //ProductBLL _productBLL = new ProductBLL();



        //------------------------------------------------------------------------------
        //METHOD FOR SELECT IF PRODUCT EXISTS OR NOT EXITS
        //If exists the form auntofill
        //if nat exists only auntofill the needs its
        public void SelectEditOrNew()
        {
            try
            {
                FrmSelecCodeProduct padre = this.Owner as FrmSelecCodeProduct;  // IT'S WAY THE INTANCES OTHER FORM

                bool result = padre.GetResult();
                string code = padre.GetCode();

                string[] estadoActivo = {"true","false"};
                comboActivo.Items.AddRange(estadoActivo);
                comboActivo.SelectedIndex = 0 ;

                string[] estadoITBIS = {"0.1","0.16","0.18" };
                comboITBIS.Items.AddRange(estadoITBIS);
                comboITBIS.SelectedIndex = 0;

                textCodigo.Text = code;

                textCodigo.ReadOnly = true;   //because user can't chage the code
                comboActivo.Enabled = false; //for the user can't change with move, he cans muve with the button ANULAR 

                if (result == true)
                {
                    Edit(code);   
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Posible error al convertir un dato, enviado desde FrmProductEditor metodo SelectEditOrNew{Ex}");
                
            }
        }


        //--------------------------------------------------------------------
        //COMPLEMEN OF THE METHOD SelectEditOrNew(), ESPECIFIC IN AUTOFILL
         
        public void Edit(string code)
        {
            _product = _productBLL.GetProductById(code);

            textCodigo.Text = _product.CodeProduct ;
            textDescripcion.Text = _product.NameProduct;
            textPrecio.Text = Convert.ToString(_product.PriceProduct);
            textCantidad.Text = Convert.ToString(_product.StockProduct);
            textUnidMedida.Text = Convert.ToString(_product.UnitOfMeasure);

            dateTimeFecha.Format = DateTimePickerFormat.Custom;
            dateTimeFecha.CustomFormat = "dd/MM/yyyy";
            dateTimeFecha.Value = _product.ExpiryDateProduct.Value;//?? DateTime.Today;


            //= null = _product.LocationProduct 
            textDistribuidor.Text = Convert.ToString(_product.CodeDistributor);
            textCosto.Text = Convert.ToString(_product.CostProduct);
            //_product.DiscountCostProduct = null = ;


            dateTimeFechaExpi.Format = DateTimePickerFormat.Custom;
            dateTimeFechaExpi.CustomFormat = "dd/MM/yyyy";
            dateTimeFechaExpi.Value = _product.DateInProduct.Value;



            textDescuento.Text = Convert.ToString(_product.DiscountSellProduct);
            textUltPrecio.Text = Convert.ToString(_product.LastPriceProduct); 
            textUtilidad.Text = Convert.ToString(_product.UtilityProduct);
            textMinimoExist.Text = Convert.ToString(_product.MinimunExistenProduct);
            comboITBIS.SelectedItem = Convert.ToString(_product.TaxProduct);
            comboActivo.SelectedItem = _product.IsActive ? "true" : "false" ;

        }


        //--------------------------------------------------------------------------------
        //METHOD TO SEND INFORMATION ABOUT PRODUCT.
        //here with the intances Product( _product ) put it information for to product  

        public void New()
        {
            //Product _product = new Product();

            try
            {                                           //Intenta de pasar a cada atributo a su tipo de dato original, el que es decimal a decimal y asi sucesivamente;
                _product.CodeProduct = textCodigo.Text;
                _product.NameProduct = textDescripcion.Text;
                _product.PriceProduct = decimal.Parse(textPrecio.Text);
                _product.StockProduct = decimal.Parse(textCantidad.Text);
                _product.UnitOfMeasure = textUnidMedida.Text;
                _product.ExpiryDateProduct = dateTimeFechaExpi.Value;
                _product.LocationProduct = null;
                _product.CodeDistributor = textDistribuidor.Text;
                _product.CostProduct = decimal.Parse(textCosto.Text);
                _product.DiscountCostProduct = null;
                _product.DateInProduct = dateTimeFecha.Value;
                _product.DiscountSellProduct = decimal.Parse(textDescuento.Text);
                _product.LastPriceProduct = decimal.Parse(textUltPrecio.Text);
                _product.UtilityProduct = textUtilidad.Text;
                _product.MinimunExistenProduct = decimal.Parse(textMinimoExist.Text);
                _product.TaxProduct = decimal.Parse(comboITBIS.Text);
                _product.IsActive = bool.Parse(comboActivo.Text);

            }
            catch (Exception Ex)
            {
                MessageBox.Show($"Error al Intentar de Guardar el Producto{Ex}");
                Console.WriteLine($"Posible error al convertir un dato, enviado desde FrmProductEditor{Ex}");
            }
        }

        //-----------------------------------------------------------------------------
        //BUTTON FOR CREATE NEW PRODUCT OR UPDATE PRODUCT

        private void buttonCrear_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSelecCodeProduct padre = this.Owner as FrmSelecCodeProduct;

                if (padre.GetResult() == false)
                {
                    New();
                    _productBLL.InsertProduct(_product);
                }
                else
                {
                    New();
                    _productBLL.UpdateProduct(_product);
                }
                
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Posible error al convertir un dato, enviado desde FrmProductEditor boton Crear{Ex}");
            }
        }
    }
}
