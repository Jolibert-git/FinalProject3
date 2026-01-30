using Business.Negocio;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
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
    public partial class FrmSelectCodeCustomer : Form
    {
        //public readonly Customer CustomerLogic;
        public readonly CustomerBLL _customerBLL;

        public FrmSelectCodeCustomer(CustomerBLL _customerBLL)
        {
            InitializeComponent();
            this._customerBLL = _customerBLL;
        }

        static public string? _code;
        static public bool _validateCustomer;

        private void button1_Click(object sender, EventArgs e)
        {
            _code = textCode.Text;

            if (string.IsNullOrWhiteSpace(_code))
            {
                MessageBox.Show("El codigo es Nulo o no valido");
                return;
            }

            _validateCustomer = _customerBLL.ValidateCustomer(_code);

            var frmEditor  = Program.ServiceProvider.GetRequiredService<FrmCustomerEditor>();

            frmEditor.Owner = this;
            frmEditor.AssessCustomer();//execute the fist method of formulary FrmEditorCustomer the name AssessCustomer()
            frmEditor.Show();

        }

    }

}
