using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Customer
    {

        public string CodeCustomer { get; set; }
        public string FullNameCustomer { get; set; }
        public string IdCustomer { get; set; } 
        public string PhoneCustomer { get; set; }
        public string LocationCustomer { get; set; } 
        public string CityCustomer { get; set; }
        public string EmailCustomer { get; set; }
        public string RncCustomer { get; set; }
        public string AreaCustomer { get; set; }
        public decimal AutDescuentoCustomer { get; set; } 
        public decimal TotalDescontadoCustomer { get; set; } 
        public decimal TotalGastadoCustomer { get; set; } 
        public decimal TotalGananciasCustomer { get; set; }

        
    }
}
