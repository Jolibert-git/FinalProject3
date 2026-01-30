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
using Business.Negocio;

namespace Screen.Views
{
    public partial class FrmDistributorEditor : Form
    {
        public readonly DistributorBLL _distributorBLL;
        public Distributor _distributor;
        public FrmDistributorEditor(DistributorBLL _distributorBLL)
        {
            InitializeComponent();
            this._distributorBLL = _distributorBLL;
        }

        public void AssessDistributor()
        {
            bool validateDistributor = FrmSelectCodeDistributor.validateDistributor;

            if (validateDistributor)
            {
                LoadDataDistributor();
            }
        }

        public void LoadDataDistributor()
        {
            string code = FrmSelectCodeDistributor.code;
            _distributor = _distributorBLL.GetDistributorByCode(code);

            textCodigo.Text = code;
            textNombre.Text = _distributor.nameDistributor;
            textRNC.Text = _distributor.rncDistributor ;
            textTelefono.Text = _distributor.phoneDistributor;
            textDireccion.Text = _distributor.locationDistributor;
            textCorreo.Text = _distributor.emailDistributor;
            textBanco.Text = _distributor.bancoDistributor;
            textCuenta.Text = _distributor.cuentaDistributor;
            textNoCuenta.Text = _distributor.numCuentaDistrubutor;
        }


        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
