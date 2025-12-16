using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model.Entities;


namespace Screen.Views
{
    FrmSelecCodeProduct selecCodeProduct = new FrmSelecCodeProduct();
    Product _product = new Product();


    public bool SelectEditOrNew()
    {
        return true;
    }

    public void Edit()
    {


    }

    public void New()
    {
        Product _product = new Product();

        try
        {                                           //Intenta de pasar a cada atributo a su tipo de dato original, el que es decimal a decimal y asi sucesivamente;
            _product.CodeProduct = textCodigo.Text;
            _product.NameProduct = textDescripcion.Text;
            _product.PriceProduct = textPrecio.Text;
            _product.StockProduct = textCantidad.Text;
            _product.UnitOfMeasure = textUnidMedida.Text;
            _product.ExpiryDateProduct = dateTimeFecha.Text;
            _product.LocationProduct = null;
            _product.CodeDistributor = textDistributor.Text;
            _product.CostProduct = textCosto.Text;
            _product.DiscountCostProduct = null;
            _product.DateInProduct =
            _product.DiscountSellProduct =
            _product.LastPriceProduct =
            _product.UtilityProduct =
            _product.MinimunExistenProduct =
            _product.TaxProduct =
            _product.IsActive =
            }
        catch ()
        {

        }
    }

    public partial class FrmProductEditor : Form
    {
        public FrmProductEditor()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
