using DataAccess.Data;
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
using Microsoft.Extensions.DependencyInjection;

namespace Screen.Views
{
    public partial class FrmSelectCodeDistributor : Form
    {
        static public string code { get; set; } //It is declerec static because i need it in the formulary FrmDistributorEditor
        static public bool validateDistributor { get; set; }

        public readonly DistributorBLL _distributorBLL;

        public FrmSelectCodeDistributor(DistributorBLL _distributorBLL)
        {
            InitializeComponent();
            this._distributorBLL = _distributorBLL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            code = textCodigo.Text;

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("El codigo esta vacio o es Invalido");
                return;
            }

            validateDistributor = _distributorBLL.ValidateDistributor(code);

            FrmDistributorEditor frmEditor = Program.ServiceProvider.GetRequiredService< FrmDistributorEditor >(); // I say to class program that send formulary FrmDistributorEditor becouse i get it new formulary without error  
            frmEditor.Owner = this;
            frmEditor.AssessDistributor();       //It is the principal method of Formulary FrmDistributorEditor Load values in the formulary
            frmEditor.Show();
        }
    }
}
