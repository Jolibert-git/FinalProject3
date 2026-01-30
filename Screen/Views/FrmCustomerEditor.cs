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
using Model.Entities;

namespace Screen.Views
{
    public partial class FrmCustomerEditor : Form
    {
        public Customer _customer;
        public readonly CustomerBLL _customerBLL;
     

        public FrmCustomerEditor(CustomerBLL _customerBLL)
        {
            InitializeComponent();
            this._customerBLL = _customerBLL;
        }

        public void AssessCustomer()
        {
            bool validateCustomer;

            validateCustomer = FrmSelectCodeCustomer._validateCustomer;

            if (validateCustomer)
            {
                LoadDataCustomer();
            }
        }

        public void LoadDataCustomer()
        {
            string? code;
            code = FrmSelectCodeCustomer._code;
            
            _customer = _customerBLL.GetCustomerByCode(code);

            textCodigo.Text = code;

            textNombre.Text = _customer.FullNameCustomer ;
            textCedula.Text = _customer.IdCustomer;
            textTelefono.Text = _customer.PhoneCustomer;
            textDireccion.Text = _customer.LocationCustomer;
            textCorreo.Text = _customer.EmailCustomer;
            textRNC.Text = _customer.RncCustomer;
            textCiudad.Text = _customer.CityCustomer;
            textArea.Text = _customer.AreaCustomer;
            textAutDescuento.Text = Convert.ToString(_customer.AutDescuentoCustomer);
            textDescontado.Text = Convert.ToString(_customer.TotalDescontadoCustomer);
            textFGastado.Text = Convert.ToString(_customer.TotalGastadoCustomer);
            textGananncias.Text = Convert.ToString(_customer.TotalGastadoCustomer);
        }



        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
