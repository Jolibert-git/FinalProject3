using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class StockMovement
    {
        public int CodeMovement { get; set; } 
        public string NameMovement { get; set; }
        public string Operation { get; set; }
        
        public DateTime MovementDate { get; set; }      
        public string CodeProduct { get; set; }        
        public decimal MovementQuantity { get; set; }  
        public string MovementType { get; set; }        
        public string MovementReason { get; set; }


    }
}
