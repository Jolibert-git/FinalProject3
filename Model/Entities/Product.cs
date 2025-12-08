using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Product
    {
        public string CodeProduct { get; set; }           
        public string NameProduct { get; set; }           
        public decimal PriceProduct { get; set; }         
        public decimal StockProduct { get; set; }         
        public string UnitOfMeasure { get; set; }         
        public DateTime? ExpiryDateProduct { get; set; }  
        public string LocationProduct { get; set; }      
        public string CodeDistributor { get; set; }      
        public decimal CostProduct { get; set; }          
        public decimal? DiscountCostProduct { get; set; } 
        public DateTime? DateInProduct { get; set; }      
        public decimal? DiscountSellProduct { get; set; } 
        public decimal? LastPriceProduct { get; set; }     
        public string UtilityProduct { get; set; }        
        public decimal? MinimunExistenProduct { get; set; }
        public decimal TaxProduct { get; set; }           
        public bool IsActive { get; set; }
        public Product()
        {
        }
    }
}
